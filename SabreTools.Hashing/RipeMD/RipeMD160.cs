using System;
using static SabreTools.Hashing.RipeMD.Constants;

namespace SabreTools.Hashing.RipeMD
{
    // TODO: Determine if unrolled version of Round is more efficient
    internal class RipeMD160
    {
        /// <summary>
        /// Leftmost 32 bits
        /// </summary>
        private uint _y0;

        /// <summary>
        /// Second from left 32 bits
        /// </summary>
        private uint _y1;

        /// <summary>
        /// Third from left 32 bits
        /// </summary>
        private uint _y2;

        /// <summary>
        /// Fourth from left 32 bits
        /// </summary>
        private uint _y3;

        /// <summary>
        /// Fifth from left 32 bits
        /// </summary>
        private uint _y4;

        /// <summary>
        /// Total number of bytes processed
        /// </summary>
        private ulong _totalBytes;

        /// <summary>
        /// Internal byte buffer to accumulate before <see cref="_z"/> 
        /// </summary>
        private readonly byte[] _preZ = new byte[4];

        /// <summary>
        /// Current pointer to <see cref="_preZ"/> 
        /// </summary>
        private int _preZPtr;

        /// <summary>
        /// Internal UInt32 buffer for processing
        /// </summary>
        private readonly uint[] _z = new uint[16];

        /// <summary>
        /// Current pointer to <see cref="_z"/> 
        /// </summary>
        private int _zPtr;

        public RipeMD160()
        {
            Reset();
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
        {
            // Reset the seed values
            _y0 = RMD160Y0;
            _y1 = RMD160Y1;
            _y2 = RMD160Y2;
            _y3 = RMD160Y3;
            _y4 = RMD160Y4;

            // Reset the byte count
            _totalBytes = 0;

            // Reset the pre-Z buffer
            Array.Clear(_preZ, 0, _preZ.Length);
            _preZPtr = 0;

            // Reset the accumulator
            Array.Clear(_z, 0, _z.Length);
            _zPtr = 0;
        }

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
        {
            // Increment the processed byte count
            _totalBytes += (ulong)length;

            // Fill the pre-Z buffer first if partially full
            if (_preZPtr > 0)
            {
                // Read as many bytes as possible to either fill or exhaust
                int preZRead = Math.Min(4 - _preZPtr, length);
                Array.Copy(data, offset, _preZ, _preZPtr, preZRead);

                // Set the new values
                _preZPtr += preZRead;
                offset += preZRead;
                length -= preZRead;

                // Process the pre-Z buffer if necessary
                if (_preZPtr == 4)
                {
                    AppendUInt32(_preZ);
                    _preZPtr = 0;
                }

                // Exit if there are no more bytes
                if (length == 0)
                    return;
            }

            // Process 4-byte blocks
            while (length >= 4)
            {
                // Get the next 4 bytes
                var temp = new byte[4];
                Array.Copy(data, offset, temp, 0, 4);

                // Process the array
                AppendUInt32(temp);

                // Set bytes as processed
                offset += 4;
                length -= 4;
            }

            // Fill the pre-Z buffer with the remainder
            if (length > 0)
            {
                Array.Copy(data, offset, _preZ, 0, length);
                _preZPtr = length;
            }
        }

        /// <summary>
        /// End the hashing process
        /// </summary>
        public void Terminate()
        {
            // Pad the block
            TransformBlock([128], 0, 1);

            // Add zero bytes until pre-Z is cleared
            while (_preZPtr != 0)
            {
                TransformBlock([0], 0, 1);
            }

            // Handle if the accumulator is nearly full
            if (_zPtr > 14)
                Round();

            // Split the block count
            ulong width = _totalBytes << 3;
            _z[14] = (uint)(width & 0xffffffff);
            _z[15] = (uint)(width >> 32);

            // Run a final round
            Round();
        }

        /// <summary>
        /// Get the current value of the hash
        /// </summary>
        /// <remarks>
        /// If <see cref="Terminate"/> has not been run, this value
        /// will not be accurate for the processed bytes so far.
        /// </remarks>
        public byte[] GetHash()
        {
            var hash = new byte[20];
            int hashOffset = 0;

            // Y0
            byte[] segment = BitConverter.GetBytes(_y0);
            Array.Reverse(segment);
            Array.Copy(segment, 0, hash, hashOffset, 4);
            hashOffset += 4;

            // Y1
            segment = BitConverter.GetBytes(_y1);
            Array.Reverse(segment);
            Array.Copy(segment, 0, hash, hashOffset, 4);
            hashOffset += 4;

            // Y2
            segment = BitConverter.GetBytes(_y2);
            Array.Reverse(segment);
            Array.Copy(segment, 0, hash, hashOffset, 4);
            hashOffset += 4;

            // Y3
            segment = BitConverter.GetBytes(_y3);
            Array.Reverse(segment);
            Array.Copy(segment, 0, hash, hashOffset, 4);
            hashOffset += 4;

            // Y4
            segment = BitConverter.GetBytes(_y4);
            Array.Reverse(segment);
            Array.Copy(segment, 0, hash, hashOffset, 4);

            // Reset the state and return
            Reset();
            return hash;
        }

        /// <summary>
        /// Append a block of 4 bytes, processing the set if necessary
        /// </summary>
        private void AppendUInt32(byte[] data)
        {
            // Read in the next 4 bytes as a little-endian UInt32
            _z[_zPtr++] = (uint)data[0] + data[1] << 8
                              + data[2] << 16 + data[3] << 24;

            // If the accumulator is full, perform an update round
            if (_zPtr == 16)
                Round();
        }

        /// <summary>
        /// Perform one round of updates on the cached values
        /// </summary>
        /// <remarks>
        /// The official specification for RIPEMD-160 includes tables
        /// and instructions that represent a loop. Most standard implementations
        /// use the unrolled version of that loop to make it more efficient.
        /// 
        /// The below code started with the looped version but has been converted
        /// to the more standard implementation instead.
        /// </remarks>
        private void Round()
        {
            // Setup values
            uint x0 = _y0, xp0 = _y0;
            uint x1 = _y1, xp1 = _y1;
            uint x2 = _y2, xp2 = _y2;
            uint x3 = _y3, xp3 = _y3;
            uint x4 = _y4, xp4 = _y4;

            #region Rounds 0-15

            // Round 0
            x0 = RotateLeft(x0 + G00_15(x1, x2, x3) + _z[0] + RMD160Round00To15, 11) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G64_79(xp1, xp2, xp3) + _z[5] + RMD160RoundPrime00To15, 8) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 1
            x4 = RotateLeft(x4 + G00_15(x0, x1, x2) + _z[1] + RMD160Round00To15, 14) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G64_79(xp0, xp1, xp2) + _z[14] + RMD160RoundPrime00To15, 9) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 2
            x3 = RotateLeft(x3 + G00_15(x4, x0, x1) + _z[2] + RMD160Round00To15, 15) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G64_79(xp4, xp0, xp1) + _z[7] + RMD160RoundPrime00To15, 9) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 3
            x2 = RotateLeft(x2 + G00_15(x3, x4, x0) + _z[3] + RMD160Round00To15, 12) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G64_79(xp3, xp4, xp0) + _z[0] + RMD160RoundPrime00To15, 11) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 4
            x1 = RotateLeft(x1 + G00_15(x2, x3, x4) + _z[4] + RMD160Round00To15, 5) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G64_79(xp2, xp3, xp4) + _z[9] + RMD160RoundPrime00To15, 13) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 5
            x0 = RotateLeft(x0 + G00_15(x1, x2, x3) + _z[5] + RMD160Round00To15, 8) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G64_79(xp1, xp2, xp3) + _z[2] + RMD160RoundPrime00To15, 15) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 6
            x4 = RotateLeft(x4 + G00_15(x0, x1, x2) + _z[6] + RMD160Round00To15, 7) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G64_79(xp0, xp1, xp2) + _z[11] + RMD160RoundPrime00To15, 15) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 7
            x3 = RotateLeft(x3 + G00_15(x4, x0, x1) + _z[7] + RMD160Round00To15, 9) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G64_79(xp4, xp0, xp1) + _z[4] + RMD160RoundPrime00To15, 5) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 8
            x2 = RotateLeft(x2 + G00_15(x3, x4, x0) + _z[8] + RMD160Round00To15, 11) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G64_79(xp3, xp4, xp0) + _z[13] + RMD160RoundPrime00To15, 7) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 9
            x1 = RotateLeft(x1 + G00_15(x2, x3, x4) + _z[9] + RMD160Round00To15, 13) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G64_79(xp2, xp3, xp4) + _z[6] + RMD160RoundPrime00To15, 7) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 10
            x0 = RotateLeft(x0 + G00_15(x1, x2, x3) + _z[10] + RMD160Round00To15, 14) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G64_79(xp1, xp2, xp3) + _z[15] + RMD160RoundPrime00To15, 8) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 11
            x4 = RotateLeft(x4 + G00_15(x0, x1, x2) + _z[11] + RMD160Round00To15, 15) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G64_79(xp0, xp1, xp2) + _z[8] + RMD160RoundPrime00To15, 11) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 12
            x3 = RotateLeft(x3 + G00_15(x4, x0, x1) + _z[12] + RMD160Round00To15, 6) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G64_79(xp4, xp0, xp1) + _z[1] + RMD160RoundPrime00To15, 14) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 13
            x2 = RotateLeft(x2 + G00_15(x3, x4, x0) + _z[13] + RMD160Round00To15, 7) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G64_79(xp3, xp4, xp0) + _z[10] + RMD160RoundPrime00To15, 14) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 14
            x1 = RotateLeft(x1 + G00_15(x2, x3, x4) + _z[14] + RMD160Round00To15, 9) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G64_79(xp2, xp3, xp4) + _z[3] + RMD160RoundPrime00To15, 12) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 15
            x0 = RotateLeft(x0 + G00_15(x1, x2, x3) + _z[15] + RMD160Round00To15, 8) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G64_79(xp1, xp2, xp3) + _z[12] + RMD160RoundPrime00To15, 6) + xp4;
            xp2 = RotateLeft(xp2, 10);

            #endregion

            #region Rounds 16-31

            // Round 16
            x4 = RotateLeft(x4 + G16_31(x0, x1, x2) + _z[7] + RMD160Round16To31, 7) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G48_63(xp0, xp1, xp2) + _z[6] + RMD160RoundPrime16To31, 9) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 17
            x3 = RotateLeft(x3 + G16_31(x4, x0, x1) + _z[4] + RMD160Round16To31, 6) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G48_63(xp4, xp0, xp1) + _z[11] + RMD160RoundPrime16To31, 13) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 18
            x2 = RotateLeft(x2 + G16_31(x3, x4, x0) + _z[13] + RMD160Round16To31, 8) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G48_63(xp3, xp4, xp0) + _z[3] + RMD160RoundPrime16To31, 15) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 19
            x1 = RotateLeft(x1 + G16_31(x2, x3, x4) + _z[1] + RMD160Round16To31, 13) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G48_63(xp2, xp3, xp4) + _z[7] + RMD160RoundPrime16To31, 7) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 20
            x0 = RotateLeft(x0 + G16_31(x1, x2, x3) + _z[10] + RMD160Round16To31, 11) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G48_63(xp1, xp2, xp3) + _z[0] + RMD160RoundPrime16To31, 12) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 21
            x4 = RotateLeft(x4 + G16_31(x0, x1, x2) + _z[6] + RMD160Round16To31, 9) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G48_63(xp0, xp1, xp2) + _z[13] + RMD160RoundPrime16To31, 8) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 22
            x3 = RotateLeft(x3 + G16_31(x4, x0, x1) + _z[15] + RMD160Round16To31, 7) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G48_63(xp4, xp0, xp1) + _z[5] + RMD160RoundPrime16To31, 9) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 23
            x2 = RotateLeft(x2 + G16_31(x3, x4, x0) + _z[3] + RMD160Round16To31, 15) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G48_63(xp3, xp4, xp0) + _z[10] + RMD160RoundPrime16To31, 11) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 24
            x1 = RotateLeft(x1 + G16_31(x2, x3, x4) + _z[12] + RMD160Round16To31, 7) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G48_63(xp2, xp3, xp4) + _z[14] + RMD160RoundPrime16To31, 7) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 25
            x0 = RotateLeft(x0 + G16_31(x1, x2, x3) + _z[0] + RMD160Round16To31, 12) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G48_63(xp1, xp2, xp3) + _z[15] + RMD160RoundPrime16To31, 7) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 26
            x4 = RotateLeft(x4 + G16_31(x0, x1, x2) + _z[9] + RMD160Round16To31, 15) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G48_63(xp0, xp1, xp2) + _z[8] + RMD160RoundPrime16To31, 12) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 27
            x3 = RotateLeft(x3 + G16_31(x4, x0, x1) + _z[5] + RMD160Round16To31, 9) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G48_63(xp4, xp0, xp1) + _z[12] + RMD160RoundPrime16To31, 7) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 28
            x2 = RotateLeft(x2 + G16_31(x3, x4, x0) + _z[2] + RMD160Round16To31, 11) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G48_63(xp3, xp4, xp0) + _z[4] + RMD160RoundPrime16To31, 6) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 29
            x1 = RotateLeft(x1 + G16_31(x2, x3, x4) + _z[14] + RMD160Round16To31, 7) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G48_63(xp2, xp3, xp4) + _z[9] + RMD160RoundPrime16To31, 15) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 30
            x0 = RotateLeft(x0 + G16_31(x1, x2, x3) + _z[11] + RMD160Round16To31, 13) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G48_63(xp1, xp2, xp3) + _z[1] + RMD160RoundPrime16To31, 13) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 31
            x4 = RotateLeft(x4 + G16_31(x0, x1, x2) + _z[8] + RMD160Round16To31, 12) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G48_63(xp0, xp1, xp2) + _z[2] + RMD160RoundPrime16To31, 11) + xp3;
            xp1 = RotateLeft(xp1, 10);

            #endregion

            #region Rounds 32-47

            // Round 32
            x3 = RotateLeft(x3 + G32_47(x4, x0, x1) + _z[3] + RMD160Round32To47, 11) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G32_47(xp4, xp0, xp1) + _z[15] + RMD160RoundPrime32To47, 9) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 33
            x2 = RotateLeft(x2 + G32_47(x3, x4, x0) + _z[10] + RMD160Round32To47, 13) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G32_47(xp3, xp4, xp0) + _z[5] + RMD160RoundPrime32To47, 7) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 34
            x1 = RotateLeft(x1 + G32_47(x2, x3, x4) + _z[14] + RMD160Round32To47, 6) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G32_47(xp2, xp3, xp4) + _z[1] + RMD160RoundPrime32To47, 15) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 35
            x0 = RotateLeft(x0 + G32_47(x1, x2, x3) + _z[4] + RMD160Round32To47, 7) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G32_47(xp1, xp2, xp3) + _z[3] + RMD160RoundPrime32To47, 11) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 36
            x4 = RotateLeft(x4 + G32_47(x0, x1, x2) + _z[9] + RMD160Round32To47, 14) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G32_47(xp0, xp1, xp2) + _z[7] + RMD160RoundPrime32To47, 8) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 37
            x3 = RotateLeft(x3 + G32_47(x4, x0, x1) + _z[15] + RMD160Round32To47, 9) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G32_47(xp4, xp0, xp1) + _z[14] + RMD160RoundPrime32To47, 6) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 38
            x2 = RotateLeft(x2 + G32_47(x3, x4, x0) + _z[8] + RMD160Round32To47, 13) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G32_47(xp3, xp4, xp0) + _z[6] + RMD160RoundPrime32To47, 6) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 39
            x1 = RotateLeft(x1 + G32_47(x2, x3, x4) + _z[1] + RMD160Round32To47, 15) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G32_47(xp2, xp3, xp4) + _z[9] + RMD160RoundPrime32To47, 14) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 40
            x0 = RotateLeft(x0 + G32_47(x1, x2, x3) + _z[2] + RMD160Round32To47, 14) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G32_47(xp1, xp2, xp3) + _z[11] + RMD160RoundPrime32To47, 12) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 41
            x4 = RotateLeft(x4 + G32_47(x0, x1, x2) + _z[7] + RMD160Round32To47, 8) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G32_47(xp0, xp1, xp2) + _z[8] + RMD160RoundPrime32To47, 13) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 42
            x3 = RotateLeft(x3 + G32_47(x4, x0, x1) + _z[0] + RMD160Round32To47, 13) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G32_47(xp4, xp0, xp1) + _z[12] + RMD160RoundPrime32To47, 5) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 43
            x2 = RotateLeft(x2 + G32_47(x3, x4, x0) + _z[6] + RMD160Round32To47, 6) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G32_47(xp3, xp4, xp0) + _z[2] + RMD160RoundPrime32To47, 14) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 44
            x1 = RotateLeft(x1 + G32_47(x2, x3, x4) + _z[13] + RMD160Round32To47, 5) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G32_47(xp2, xp3, xp4) + _z[10] + RMD160RoundPrime32To47, 13) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 45
            x0 = RotateLeft(x0 + G32_47(x1, x2, x3) + _z[11] + RMD160Round32To47, 12) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G32_47(xp1, xp2, xp3) + _z[0] + RMD160RoundPrime32To47, 13) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 46
            x4 = RotateLeft(x4 + G32_47(x0, x1, x2) + _z[5] + RMD160Round32To47, 7) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G32_47(xp0, xp1, xp2) + _z[4] + RMD160RoundPrime32To47, 7) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 47
            x3 = RotateLeft(x3 + G32_47(x4, x0, x1) + _z[12] + RMD160Round32To47, 5) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G32_47(xp4, xp0, xp1) + _z[13] + RMD160RoundPrime32To47, 5) + xp2;
            xp0 = RotateLeft(xp0, 10);

            #endregion

            #region Rounds 48-63

            // Round 48
            x2 = RotateLeft(x2 + G48_63(x3, x4, x0) + _z[1] + RMD160Round48To63, 11) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G16_31(xp3, xp4, xp0) + _z[8] + RMD160RoundPrime48To63, 15) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 49
            x1 = RotateLeft(x1 + G48_63(x2, x3, x4) + _z[9] + RMD160Round48To63, 12) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G16_31(xp2, xp3, xp4) + _z[6] + RMD160RoundPrime48To63, 5) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 50
            x0 = RotateLeft(x0 + G48_63(x1, x2, x3) + _z[11] + RMD160Round48To63, 14) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G16_31(xp1, xp2, xp3) + _z[4] + RMD160RoundPrime48To63, 8) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 51
            x4 = RotateLeft(x4 + G48_63(x0, x1, x2) + _z[10] + RMD160Round48To63, 15) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G16_31(xp0, xp1, xp2) + _z[1] + RMD160RoundPrime48To63, 11) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 52
            x3 = RotateLeft(x3 + G48_63(x4, x0, x1) + _z[0] + RMD160Round48To63, 14) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G16_31(xp4, xp0, xp1) + _z[3] + RMD160RoundPrime48To63, 14) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 53
            x2 = RotateLeft(x2 + G48_63(x3, x4, x0) + _z[8] + RMD160Round48To63, 15) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G16_31(xp3, xp4, xp0) + _z[11] + RMD160RoundPrime48To63, 14) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 54
            x1 = RotateLeft(x1 + G48_63(x2, x3, x4) + _z[12] + RMD160Round48To63, 9) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G16_31(xp2, xp3, xp4) + _z[15] + RMD160RoundPrime48To63, 6) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 55
            x0 = RotateLeft(x0 + G48_63(x1, x2, x3) + _z[4] + RMD160Round48To63, 8) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G16_31(xp1, xp2, xp3) + _z[0] + RMD160RoundPrime48To63, 14) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 56
            x4 = RotateLeft(x4 + G48_63(x0, x1, x2) + _z[13] + RMD160Round48To63, 9) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G16_31(xp0, xp1, xp2) + _z[5] + RMD160RoundPrime48To63, 6) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 57
            x3 = RotateLeft(x3 + G48_63(x4, x0, x1) + _z[3] + RMD160Round48To63, 14) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G16_31(xp4, xp0, xp1) + _z[12] + RMD160RoundPrime48To63, 9) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 58
            x2 = RotateLeft(x2 + G48_63(x3, x4, x0) + _z[7] + RMD160Round48To63, 5) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G16_31(xp3, xp4, xp0) + _z[2] + RMD160RoundPrime48To63, 12) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 59
            x1 = RotateLeft(x1 + G48_63(x2, x3, x4) + _z[15] + RMD160Round48To63, 6) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G16_31(xp2, xp3, xp4) + _z[13] + RMD160RoundPrime48To63, 9) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 60
            x0 = RotateLeft(x0 + G48_63(x1, x2, x3) + _z[14] + RMD160Round48To63, 8) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G16_31(xp1, xp2, xp3) + _z[9] + RMD160RoundPrime48To63, 12) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 61
            x4 = RotateLeft(x4 + G48_63(x0, x1, x2) + _z[5] + RMD160Round48To63, 6) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G16_31(xp0, xp1, xp2) + _z[7] + RMD160RoundPrime48To63, 5) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 62
            x3 = RotateLeft(x3 + G48_63(x4, x0, x1) + _z[6] + RMD160Round48To63, 5) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G16_31(xp4, xp0, xp1) + _z[10] + RMD160RoundPrime48To63, 15) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 63
            x2 = RotateLeft(x2 + G48_63(x3, x4, x0) + _z[2] + RMD160Round48To63, 12) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G16_31(xp3, xp4, xp0) + _z[14] + RMD160RoundPrime48To63, 8) + xp1;
            xp4 = RotateLeft(xp4, 10);

            #endregion

            #region Rounds 64-79

            // Round 64
            x1 = RotateLeft(x1 + G64_79(x2, x3, x4) + _z[4] + RMD160Round64To79, 9) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G00_15(xp2, xp3, xp4) + _z[12] + RMD160RoundPrime64To79, 8) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 65
            x0 = RotateLeft(x0 + G64_79(x1, x2, x3) + _z[0] + RMD160Round64To79, 15) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G00_15(xp1, xp2, xp3) + _z[15] + RMD160RoundPrime64To79, 5) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 66
            x4 = RotateLeft(x4 + G64_79(x0, x1, x2) + _z[5] + RMD160Round64To79, 5) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G00_15(xp0, xp1, xp2) + _z[10] + RMD160RoundPrime64To79, 12) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 67
            x3 = RotateLeft(x3 + G64_79(x4, x0, x1) + _z[9] + RMD160Round64To79, 11) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G00_15(xp4, xp0, xp1) + _z[4] + RMD160RoundPrime64To79, 9) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 68
            x2 = RotateLeft(x2 + G64_79(x3, x4, x0) + _z[7] + RMD160Round64To79, 6) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G00_15(xp3, xp4, xp0) + _z[1] + RMD160RoundPrime64To79, 12) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 69
            x1 = RotateLeft(x1 + G64_79(x2, x3, x4) + _z[12] + RMD160Round64To79, 8) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G00_15(xp2, xp3, xp4) + _z[5] + RMD160RoundPrime64To79, 5) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 70
            x0 = RotateLeft(x0 + G64_79(x1, x2, x3) + _z[2] + RMD160Round64To79, 13) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G00_15(xp1, xp2, xp3) + _z[8] + RMD160RoundPrime64To79, 14) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 71
            x4 = RotateLeft(x4 + G64_79(x0, x1, x2) + _z[10] + RMD160Round64To79, 12) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G00_15(xp0, xp1, xp2) + _z[7] + RMD160RoundPrime64To79, 6) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 72
            x3 = RotateLeft(x3 + G64_79(x4, x0, x1) + _z[14] + RMD160Round64To79, 5) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G00_15(xp4, xp0, xp1) + _z[6] + RMD160RoundPrime64To79, 8) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 73
            x2 = RotateLeft(x2 + G64_79(x3, x4, x0) + _z[1] + RMD160Round64To79, 12) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G00_15(xp3, xp4, xp0) + _z[2] + RMD160RoundPrime64To79, 13) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 74
            x1 = RotateLeft(x1 + G64_79(x2, x3, x4) + _z[3] + RMD160Round64To79, 13) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G00_15(xp2, xp3, xp4) + _z[13] + RMD160RoundPrime64To79, 6) + xp0;
            xp3 = RotateLeft(xp3, 10);

            // Round 75
            x0 = RotateLeft(x0 + G64_79(x1, x2, x3) + _z[8] + RMD160Round64To79, 14) + x4;
            x2 = RotateLeft(x2, 10);
            xp0 = RotateLeft(xp0 + G00_15(xp1, xp2, xp3) + _z[14] + RMD160RoundPrime64To79, 5) + xp4;
            xp2 = RotateLeft(xp2, 10);

            // Round 76
            x4 = RotateLeft(x4 + G64_79(x0, x1, x2) + _z[11] + RMD160Round64To79, 11) + x3;
            x1 = RotateLeft(x1, 10);
            xp4 = RotateLeft(xp4 + G00_15(xp0, xp1, xp2) + _z[0] + RMD160RoundPrime64To79, 15) + xp3;
            xp1 = RotateLeft(xp1, 10);

            // Round 77
            x3 = RotateLeft(x3 + G64_79(x4, x0, x1) + _z[6] + RMD160Round64To79, 8) + x2;
            x0 = RotateLeft(x0, 10);
            xp3 = RotateLeft(xp3 + G00_15(xp4, xp0, xp1) + _z[3] + RMD160RoundPrime64To79, 13) + xp2;
            xp0 = RotateLeft(xp0, 10);

            // Round 78
            x2 = RotateLeft(x2 + G64_79(x3, x4, x0) + _z[15] + RMD160Round64To79, 5) + x1;
            x4 = RotateLeft(x4, 10);
            xp2 = RotateLeft(xp2 + G00_15(xp3, xp4, xp0) + _z[9] + RMD160RoundPrime64To79, 11) + xp1;
            xp4 = RotateLeft(xp4, 10);

            // Round 79
            x1 = RotateLeft(x1 + G64_79(x2, x3, x4) + _z[13] + RMD160Round64To79, 6) + x0;
            x3 = RotateLeft(x3, 10);
            xp1 = RotateLeft(xp1 + G00_15(xp2, xp3, xp4) + _z[11] + RMD160RoundPrime64To79, 11) + xp0;
            xp3 = RotateLeft(xp3, 10);

            #endregion

            // Avalanche values
            xp3 += x2 + _y1;
            _y1 = _y2 + x3 + xp4;
            _y2 = _y3 + x4 + xp0;
            _y3 = _y4 + x0 + xp1;
            _y4 = _y0 + x1 + xp2;
            _y0 = xp3;

            // Reset the buffer
            Array.Clear(_z, 0, _z.Length);
            _zPtr = 0;
        }

        /// To facilitate software implementation, the round-function Φ is described in terms of operations on 32-bit words.
        /// A sequence of functions g0, g1, …, g79 is used in this round-function, where each function g i, 0 ≤ i ≤ 79, takes
        /// three words X0, X1 and X2 as input and produces a single word as output.

        /// <summary>
        /// Round operation [0, 15]
        /// </summary>
        private static uint G00_15(uint x0, uint x1, uint x2) => x0 ^ x1 ^ x2;

        /// <summary>
        /// Round operation [16, 31]
        /// </summary>
        private static uint G16_31(uint x0, uint x1, uint x2) => (x0 & x1) | (~x0 & x2);

        /// <summary>
        /// Round operation [32, 47]
        /// </summary>
        private static uint G32_47(uint x0, uint x1, uint x2) => (x0 | ~x1) ^ x2;

        /// <summary>
        /// Round operation [48, 63]
        /// </summary>
        private static uint G48_63(uint x0, uint x1, uint x2) => (x0 & x2) | (x1 & ~x2);

        /// <summary>
        /// Round operation [64, 79]
        /// </summary>
        private static uint G64_79(uint x0, uint x1, uint x2) => x0 ^ (x1 | ~x2);

        /// <summary>
        /// 32-bit rotate left
        /// </summary>
        private static uint RotateLeft(uint value, int shift)
            => (value << shift) | (value >> (32 - shift));
    }
}