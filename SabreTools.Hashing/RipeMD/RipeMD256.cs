using System;
using static SabreTools.Hashing.HashOperations;
using static SabreTools.Hashing.RipeMD.Constants;

namespace SabreTools.Hashing.RipeMD
{
    /// <see href="https://cdn.standards.iteh.ai/samples/39876/10f9f9f4bb614eaaaeba7e157e183ca3/ISO-IEC-10118-3-2004.pdf"/>
    /// <see href="https://homes.esat.kuleuven.be/~bosselae/ripemd160/pdf/AB-9601/AB-9601.pdf"/>
    public class RipeMD256
    {
        /// <summary>
        /// Set of 4 32-bit numbers representing the hash state
        /// </summary>
        private readonly uint[] _state = new uint[8];

        /// <summary>
        /// Total number of bytes processed
        /// </summary>
        private long _totalBytes;

        /// <summary>
        /// Internal byte buffer to accumulate before <see cref="_block"/> 
        /// </summary>
        private readonly byte[] _buffer = new byte[64];

        /// <summary>
        /// Internal UInt32 buffer for processing
        /// </summary>
        private readonly uint[] _block = new uint[16];

        public RipeMD256()
        {
            Reset();
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
        {
            // Reset the seed values
            _state[0] = RMD128Y0;
            _state[1] = RMD128Y1;
            _state[2] = RMD128Y2;
            _state[3] = RMD128Y3;
            _state[4] = RMD256Y4;
            _state[5] = RMD256Y5;
            _state[6] = RMD256Y6;
            _state[7] = RMD256Y7;

            // Reset the byte count
            _totalBytes = 0;

            // Reset the buffers
            Array.Clear(_buffer, 0, _buffer.Length);
            Array.Clear(_block, 0, _block.Length);
        }

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
        {
            // Figure out how much buffer is needed
            int bufferLen = (int)(_totalBytes & 0x3f);

            // Increment the processed byte count
            _totalBytes += length;

            // If there is buffer to fill and it will meet the limit
            if (bufferLen > 0 && bufferLen + length >= 64)
            {
                // Fill the buffer from the input
                Array.Copy(data, offset, _buffer, bufferLen, 64 - bufferLen);

                // Set the new values
                offset += 64 - bufferLen;
                length -= 64 - bufferLen;

                // Split the buffer for the round
                for (int i = 0; i < 16; i++)
                {
                    _block[i] = ReadLE32(_buffer, i * 4);
                }

                // Run the round
                Round();
                bufferLen = 0;
            }

            /// Process any standalone blocks
            while (length >= 64)
            {
                // Fill the buffer from the input
                Array.Copy(data, offset, _buffer, 0, 64);

                // Set the new values
                offset += 64;
                length -= 64;

                // Split the buffer for the round
                for (int i = 0; i < 16; i++)
                {
                    _block[i] = ReadLE32(_buffer, i * 4);
                }

                // Run the round
                Round();
            }

            // Save the remainder in the buffer
            if (length > 0)
                Array.Copy(data, offset, _buffer, bufferLen, length);
        }

        /// <summary>
        /// End the hashing process
        /// </summary>
        public void Terminate()
        {
            // Determine the pad length
            int padLength = 64 - (int)(_totalBytes & 0x3f);
            if (padLength <= 8)
                padLength += 64;

            // Get the total byte count in bits
            long totalBitCount = _totalBytes * 8;

            // Prebuild the padding
            var padding = new byte[padLength];
            padding[0] = 0x80;
            padding[padLength - 1] = (byte)((totalBitCount >> 56) & 0xff);
            padding[padLength - 2] = (byte)((totalBitCount >> 48) & 0xff);
            padding[padLength - 3] = (byte)((totalBitCount >> 40) & 0xff);
            padding[padLength - 4] = (byte)((totalBitCount >> 32) & 0xff);
            padding[padLength - 5] = (byte)((totalBitCount >> 24) & 0xff);
            padding[padLength - 6] = (byte)((totalBitCount >> 16) & 0xff);
            padding[padLength - 7] = (byte)((totalBitCount >> 8) & 0xff);
            padding[padLength - 8] = (byte)((totalBitCount >> 0) & 0xff);

            // Pad the block
            TransformBlock(padding, 0, padding.Length);
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
            var hash = new byte[32];
            int hashOffset = 0;

            // Assemble the hash array
            for (int i = 0; i < _state.Length; i++)
            {
                byte[] segment = BitConverter.GetBytes(_state[i]);
                Array.Copy(segment, 0, hash, hashOffset, 4);
                hashOffset += 4;
            }

            // Reset the state and return
            Reset();
            return hash;
        }

        /// <summary>
        /// Perform one round of updates on the cached values
        /// </summary>
        /// <remarks>
        /// The official specification for RIPEMD-128 includes tables
        /// and instructions that represent a loop. Most standard implementations
        /// use the unrolled version of that loop to make it more efficient.
        /// 
        /// The below code started with the looped version but has been converted
        /// to the more standard implementation instead.
        /// </remarks>
        private void Round()
        {
            // Setup values
            uint x0 = _state[0], xp0 = _state[4];
            uint x1 = _state[1], xp1 = _state[5];
            uint x2 = _state[2], xp2 = _state[6];
            uint x3 = _state[3], xp3 = _state[7];
            uint t;

            #region Rounds 0-15

            // Round 0
            x0 = RotateLeft32(x0 + G00_15(x1, x2, x3) + _block[0] + RMD128Round00To15, 11);
            xp0 = RotateLeft32(xp0 + G48_63(xp1, xp2, xp3) + _block[5] + RMD128RoundPrime00To15, 8);

            // Round 1
            x3 = RotateLeft32(x3 + G00_15(x0, x1, x2) + _block[1] + RMD128Round00To15, 14);
            xp3 = RotateLeft32(xp3 + G48_63(xp0, xp1, xp2) + _block[14] + RMD128RoundPrime00To15, 9);

            // Round 2
            x2 = RotateLeft32(x2 + G00_15(x3, x0, x1) + _block[2] + RMD128Round00To15, 15);
            xp2 = RotateLeft32(xp2 + G48_63(xp3, xp0, xp1) + _block[7] + RMD128RoundPrime00To15, 9);

            // Round 3
            x1 = RotateLeft32(x1 + G00_15(x2, x3, x0) + _block[3] + RMD128Round00To15, 12);
            xp1 = RotateLeft32(xp1 + G48_63(xp2, xp3, xp0) + _block[0] + RMD128RoundPrime00To15, 11);

            // Round 4
            x0 = RotateLeft32(x0 + G00_15(x1, x2, x3) + _block[4] + RMD128Round00To15, 5);
            xp0 = RotateLeft32(xp0 + G48_63(xp1, xp2, xp3) + _block[9] + RMD128RoundPrime00To15, 13);

            // Round 5
            x3 = RotateLeft32(x3 + G00_15(x0, x1, x2) + _block[5] + RMD128Round00To15, 8);
            xp3 = RotateLeft32(xp3 + G48_63(xp0, xp1, xp2) + _block[2] + RMD128RoundPrime00To15, 15);

            // Round 6
            x2 = RotateLeft32(x2 + G00_15(x3, x0, x1) + _block[6] + RMD128Round00To15, 7);
            xp2 = RotateLeft32(xp2 + G48_63(xp3, xp0, xp1) + _block[11] + RMD128RoundPrime00To15, 15);

            // Round 7
            x1 = RotateLeft32(x1 + G00_15(x2, x3, x0) + _block[7] + RMD128Round00To15, 9);
            xp1 = RotateLeft32(xp1 + G48_63(xp2, xp3, xp0) + _block[4] + RMD128RoundPrime00To15, 5);

            // Round 8
            x0 = RotateLeft32(x0 + G00_15(x1, x2, x3) + _block[8] + RMD128Round00To15, 11);
            xp0 = RotateLeft32(xp0 + G48_63(xp1, xp2, xp3) + _block[13] + RMD128RoundPrime00To15, 7);

            // Round 9
            x3 = RotateLeft32(x3 + G00_15(x0, x1, x2) + _block[9] + RMD128Round00To15, 13);
            xp3 = RotateLeft32(xp3 + G48_63(xp0, xp1, xp2) + _block[6] + RMD128RoundPrime00To15, 7);

            // Round 10
            x2 = RotateLeft32(x2 + G00_15(x3, x0, x1) + _block[10] + RMD128Round00To15, 14);
            xp2 = RotateLeft32(xp2 + G48_63(xp3, xp0, xp1) + _block[15] + RMD128RoundPrime00To15, 8);

            // Round 11
            x1 = RotateLeft32(x1 + G00_15(x2, x3, x0) + _block[11] + RMD128Round00To15, 15);
            xp1 = RotateLeft32(xp1 + G48_63(xp2, xp3, xp0) + _block[8] + RMD128RoundPrime00To15, 11);

            // Round 12
            x0 = RotateLeft32(x0 + G00_15(x1, x2, x3) + _block[12] + RMD128Round00To15, 6);
            xp0 = RotateLeft32(xp0 + G48_63(xp1, xp2, xp3) + _block[1] + RMD128RoundPrime00To15, 14);

            // Round 13
            x3 = RotateLeft32(x3 + G00_15(x0, x1, x2) + _block[13] + RMD128Round00To15, 7);
            xp3 = RotateLeft32(xp3 + G48_63(xp0, xp1, xp2) + _block[10] + RMD128RoundPrime00To15, 14);

            // Round 14
            x2 = RotateLeft32(x2 + G00_15(x3, x0, x1) + _block[14] + RMD128Round00To15, 9);
            xp2 = RotateLeft32(xp2 + G48_63(xp3, xp0, xp1) + _block[3] + RMD128RoundPrime00To15, 12);

            // Round 15
            x1 = RotateLeft32(x1 + G00_15(x2, x3, x0) + _block[15] + RMD128Round00To15, 8);
            xp1 = RotateLeft32(xp1 + G48_63(xp2, xp3, xp0) + _block[12] + RMD128RoundPrime00To15, 6);

            // Swap set 1
            t = x0; x0 = xp0; xp0 = t;

            #endregion

            #region Rounds 16-31

            // Round 16
            x0 = RotateLeft32(x0 + G16_31(x1, x2, x3) + _block[7] + RMD128Round16To31, 7);
            xp0 = RotateLeft32(xp0 + G32_47(xp1, xp2, xp3) + _block[6] + RMD128RoundPrime16To31, 9);

            // Round 17
            x3 = RotateLeft32(x3 + G16_31(x0, x1, x2) + _block[4] + RMD128Round16To31, 6);
            xp3 = RotateLeft32(xp3 + G32_47(xp0, xp1, xp2) + _block[11] + RMD128RoundPrime16To31, 13);

            // Round 18
            x2 = RotateLeft32(x2 + G16_31(x3, x0, x1) + _block[13] + RMD128Round16To31, 8);
            xp2 = RotateLeft32(xp2 + G32_47(xp3, xp0, xp1) + _block[3] + RMD128RoundPrime16To31, 15);

            // Round 19
            x1 = RotateLeft32(x1 + G16_31(x2, x3, x0) + _block[1] + RMD128Round16To31, 13);
            xp1 = RotateLeft32(xp1 + G32_47(xp2, xp3, xp0) + _block[7] + RMD128RoundPrime16To31, 7);

            // Round 20
            x0 = RotateLeft32(x0 + G16_31(x1, x2, x3) + _block[10] + RMD128Round16To31, 11);
            xp0 = RotateLeft32(xp0 + G32_47(xp1, xp2, xp3) + _block[0] + RMD128RoundPrime16To31, 12);

            // Round 21
            x3 = RotateLeft32(x3 + G16_31(x0, x1, x2) + _block[6] + RMD128Round16To31, 9);
            xp3 = RotateLeft32(xp3 + G32_47(xp0, xp1, xp2) + _block[13] + RMD128RoundPrime16To31, 8);

            // Round 22
            x2 = RotateLeft32(x2 + G16_31(x3, x0, x1) + _block[15] + RMD128Round16To31, 7);
            xp2 = RotateLeft32(xp2 + G32_47(xp3, xp0, xp1) + _block[5] + RMD128RoundPrime16To31, 9);

            // Round 23
            x1 = RotateLeft32(x1 + G16_31(x2, x3, x0) + _block[3] + RMD128Round16To31, 15);
            xp1 = RotateLeft32(xp1 + G32_47(xp2, xp3, xp0) + _block[10] + RMD128RoundPrime16To31, 11);

            // Round 24
            x0 = RotateLeft32(x0 + G16_31(x1, x2, x3) + _block[12] + RMD128Round16To31, 7);
            xp0 = RotateLeft32(xp0 + G32_47(xp1, xp2, xp3) + _block[14] + RMD128RoundPrime16To31, 7);

            // Round 25
            x3 = RotateLeft32(x3 + G16_31(x0, x1, x2) + _block[0] + RMD128Round16To31, 12);
            xp3 = RotateLeft32(xp3 + G32_47(xp0, xp1, xp2) + _block[15] + RMD128RoundPrime16To31, 7);

            // Round 26
            x2 = RotateLeft32(x2 + G16_31(x3, x0, x1) + _block[9] + RMD128Round16To31, 15);
            xp2 = RotateLeft32(xp2 + G32_47(xp3, xp0, xp1) + _block[8] + RMD128RoundPrime16To31, 12);

            // Round 27
            x1 = RotateLeft32(x1 + G16_31(x2, x3, x0) + _block[5] + RMD128Round16To31, 9);
            xp1 = RotateLeft32(xp1 + G32_47(xp2, xp3, xp0) + _block[12] + RMD128RoundPrime16To31, 7);

            // Round 28
            x0 = RotateLeft32(x0 + G16_31(x1, x2, x3) + _block[2] + RMD128Round16To31, 11);
            xp0 = RotateLeft32(xp0 + G32_47(xp1, xp2, xp3) + _block[4] + RMD128RoundPrime16To31, 6);

            // Round 29
            x3 = RotateLeft32(x3 + G16_31(x0, x1, x2) + _block[14] + RMD128Round16To31, 7);
            xp3 = RotateLeft32(xp3 + G32_47(xp0, xp1, xp2) + _block[9] + RMD128RoundPrime16To31, 15);

            // Round 30
            x2 = RotateLeft32(x2 + G16_31(x3, x0, x1) + _block[11] + RMD128Round16To31, 13);
            xp2 = RotateLeft32(xp2 + G32_47(xp3, xp0, xp1) + _block[1] + RMD128RoundPrime16To31, 13);

            // Round 31
            x1 = RotateLeft32(x1 + G16_31(x2, x3, x0) + _block[8] + RMD128Round16To31, 12);
            xp1 = RotateLeft32(xp1 + G32_47(xp2, xp3, xp0) + _block[2] + RMD128RoundPrime16To31, 11);

            // Swap set 2
            t = x1; x1 = xp1; xp1 = t;

            #endregion

            #region Rounds 32-47

            // Round 32
            x0 = RotateLeft32(x0 + G32_47(x1, x2, x3) + _block[3] + RMD128Round32To47, 11);
            xp0 = RotateLeft32(xp0 + G16_31(xp1, xp2, xp3) + _block[15] + RMD128RoundPrime32To47, 9);

            // Round 33
            x3 = RotateLeft32(x3 + G32_47(x0, x1, x2) + _block[10] + RMD128Round32To47, 13);
            xp3 = RotateLeft32(xp3 + G16_31(xp0, xp1, xp2) + _block[5] + RMD128RoundPrime32To47, 7);

            // Round 34
            x2 = RotateLeft32(x2 + G32_47(x3, x0, x1) + _block[14] + RMD128Round32To47, 6);
            xp2 = RotateLeft32(xp2 + G16_31(xp3, xp0, xp1) + _block[1] + RMD128RoundPrime32To47, 15);

            // Round 35
            x1 = RotateLeft32(x1 + G32_47(x2, x3, x0) + _block[4] + RMD128Round32To47, 7);
            xp1 = RotateLeft32(xp1 + G16_31(xp2, xp3, xp0) + _block[3] + RMD128RoundPrime32To47, 11);

            // Round 36
            x0 = RotateLeft32(x0 + G32_47(x1, x2, x3) + _block[9] + RMD128Round32To47, 14);
            xp0 = RotateLeft32(xp0 + G16_31(xp1, xp2, xp3) + _block[7] + RMD128RoundPrime32To47, 8);

            // Round 37
            x3 = RotateLeft32(x3 + G32_47(x0, x1, x2) + _block[15] + RMD128Round32To47, 9);
            xp3 = RotateLeft32(xp3 + G16_31(xp0, xp1, xp2) + _block[14] + RMD128RoundPrime32To47, 6);

            // Round 38
            x2 = RotateLeft32(x2 + G32_47(x3, x0, x1) + _block[8] + RMD128Round32To47, 13);
            xp2 = RotateLeft32(xp2 + G16_31(xp3, xp0, xp1) + _block[6] + RMD128RoundPrime32To47, 6);

            // Round 39
            x1 = RotateLeft32(x1 + G32_47(x2, x3, x0) + _block[1] + RMD128Round32To47, 15);
            xp1 = RotateLeft32(xp1 + G16_31(xp2, xp3, xp0) + _block[9] + RMD128RoundPrime32To47, 14);

            // Round 40
            x0 = RotateLeft32(x0 + G32_47(x1, x2, x3) + _block[2] + RMD128Round32To47, 14);
            xp0 = RotateLeft32(xp0 + G16_31(xp1, xp2, xp3) + _block[11] + RMD128RoundPrime32To47, 12);

            // Round 41
            x3 = RotateLeft32(x3 + G32_47(x0, x1, x2) + _block[7] + RMD128Round32To47, 8);
            xp3 = RotateLeft32(xp3 + G16_31(xp0, xp1, xp2) + _block[8] + RMD128RoundPrime32To47, 13);

            // Round 42
            x2 = RotateLeft32(x2 + G32_47(x3, x0, x1) + _block[0] + RMD128Round32To47, 13);
            xp2 = RotateLeft32(xp2 + G16_31(xp3, xp0, xp1) + _block[12] + RMD128RoundPrime32To47, 5);

            // Round 43
            x1 = RotateLeft32(x1 + G32_47(x2, x3, x0) + _block[6] + RMD128Round32To47, 6);
            xp1 = RotateLeft32(xp1 + G16_31(xp2, xp3, xp0) + _block[2] + RMD128RoundPrime32To47, 14);

            // Round 44
            x0 = RotateLeft32(x0 + G32_47(x1, x2, x3) + _block[13] + RMD128Round32To47, 5);
            xp0 = RotateLeft32(xp0 + G16_31(xp1, xp2, xp3) + _block[10] + RMD128RoundPrime32To47, 13);

            // Round 45
            x3 = RotateLeft32(x3 + G32_47(x0, x1, x2) + _block[11] + RMD128Round32To47, 12);
            xp3 = RotateLeft32(xp3 + G16_31(xp0, xp1, xp2) + _block[0] + RMD128RoundPrime32To47, 13);

            // Round 46
            x2 = RotateLeft32(x2 + G32_47(x3, x0, x1) + _block[5] + RMD128Round32To47, 7);
            xp2 = RotateLeft32(xp2 + G16_31(xp3, xp0, xp1) + _block[4] + RMD128RoundPrime32To47, 7);

            // Round 47
            x1 = RotateLeft32(x1 + G32_47(x2, x3, x0) + _block[12] + RMD128Round32To47, 5);
            xp1 = RotateLeft32(xp1 + G16_31(xp2, xp3, xp0) + _block[13] + RMD128RoundPrime32To47, 5);

            // Swap set 3
            t = x2; x2 = xp2; xp2 = t;

            #endregion

            #region Rounds 48-63

            // Round 48
            x0 = RotateLeft32(x0 + G48_63(x1, x2, x3) + _block[1] + RMD128Round48To63, 11);
            xp0 = RotateLeft32(xp0 + G00_15(xp1, xp2, xp3) + _block[8] + RMD128RoundPrime48To63, 15);

            // Round 49
            x3 = RotateLeft32(x3 + G48_63(x0, x1, x2) + _block[9] + RMD128Round48To63, 12);
            xp3 = RotateLeft32(xp3 + G00_15(xp0, xp1, xp2) + _block[6] + RMD128RoundPrime48To63, 5);

            // Round 50
            x2 = RotateLeft32(x2 + G48_63(x3, x0, x1) + _block[11] + RMD128Round48To63, 14);
            xp2 = RotateLeft32(xp2 + G00_15(xp3, xp0, xp1) + _block[4] + RMD128RoundPrime48To63, 8);

            // Round 51
            x1 = RotateLeft32(x1 + G48_63(x2, x3, x0) + _block[10] + RMD128Round48To63, 15);
            xp1 = RotateLeft32(xp1 + G00_15(xp2, xp3, xp0) + _block[1] + RMD128RoundPrime48To63, 11);

            // Round 52
            x0 = RotateLeft32(x0 + G48_63(x1, x2, x3) + _block[0] + RMD128Round48To63, 14);
            xp0 = RotateLeft32(xp0 + G00_15(xp1, xp2, xp3) + _block[3] + RMD128RoundPrime48To63, 14);

            // Round 53
            x3 = RotateLeft32(x3 + G48_63(x0, x1, x2) + _block[8] + RMD128Round48To63, 15);
            xp3 = RotateLeft32(xp3 + G00_15(xp0, xp1, xp2) + _block[11] + RMD128RoundPrime48To63, 14);

            // Round 54
            x2 = RotateLeft32(x2 + G48_63(x3, x0, x1) + _block[12] + RMD128Round48To63, 9);
            xp2 = RotateLeft32(xp2 + G00_15(xp3, xp0, xp1) + _block[15] + RMD128RoundPrime48To63, 6);

            // Round 55
            x1 = RotateLeft32(x1 + G48_63(x2, x3, x0) + _block[4] + RMD128Round48To63, 8);
            xp1 = RotateLeft32(xp1 + G00_15(xp2, xp3, xp0) + _block[0] + RMD128RoundPrime48To63, 14);

            // Round 56
            x0 = RotateLeft32(x0 + G48_63(x1, x2, x3) + _block[13] + RMD128Round48To63, 9);
            xp0 = RotateLeft32(xp0 + G00_15(xp1, xp2, xp3) + _block[5] + RMD128RoundPrime48To63, 6);

            // Round 57
            x3 = RotateLeft32(x3 + G48_63(x0, x1, x2) + _block[3] + RMD128Round48To63, 14);
            xp3 = RotateLeft32(xp3 + G00_15(xp0, xp1, xp2) + _block[12] + RMD128RoundPrime48To63, 9);

            // Round 58
            x2 = RotateLeft32(x2 + G48_63(x3, x0, x1) + _block[7] + RMD128Round48To63, 5);
            xp2 = RotateLeft32(xp2 + G00_15(xp3, xp0, xp1) + _block[2] + RMD128RoundPrime48To63, 12);

            // Round 59
            x1 = RotateLeft32(x1 + G48_63(x2, x3, x0) + _block[15] + RMD128Round48To63, 6);
            xp1 = RotateLeft32(xp1 + G00_15(xp2, xp3, xp0) + _block[13] + RMD128RoundPrime48To63, 9);

            // Round 60
            x0 = RotateLeft32(x0 + G48_63(x1, x2, x3) + _block[14] + RMD128Round48To63, 8);
            xp0 = RotateLeft32(xp0 + G00_15(xp1, xp2, xp3) + _block[9] + RMD128RoundPrime48To63, 12);

            // Round 61
            x3 = RotateLeft32(x3 + G48_63(x0, x1, x2) + _block[5] + RMD128Round48To63, 6);
            xp3 = RotateLeft32(xp3 + G00_15(xp0, xp1, xp2) + _block[7] + RMD128RoundPrime48To63, 5);

            // Round 62
            x2 = RotateLeft32(x2 + G48_63(x3, x0, x1) + _block[6] + RMD128Round48To63, 5);
            xp2 = RotateLeft32(xp2 + G00_15(xp3, xp0, xp1) + _block[10] + RMD128RoundPrime48To63, 15);

            // Round 63
            x1 = RotateLeft32(x1 + G48_63(x2, x3, x0) + _block[2] + RMD128Round48To63, 12);
            xp1 = RotateLeft32(xp1 + G00_15(xp2, xp3, xp0) + _block[14] + RMD128RoundPrime48To63, 8);

            // Swap set 4
            t = x3; x3 = xp3; xp3 = t;

            #endregion

            // Avalanche values
            _state[0] += x0;
            _state[1] += x1;
            _state[2] += x2;
            _state[3] += x3;
            _state[4] += xp0;
            _state[5] += xp1;
            _state[6] += xp2;
            _state[7] += xp3;
        }

        /// <summary>
        /// Round operation [0, 15]
        /// </summary>
        private static uint G00_15(uint x, uint y, uint z) => x ^ y ^ z;

        /// <summary>
        /// Round operation [16, 31]
        /// </summary>
        private static uint G16_31(uint x, uint y, uint z) => (x & y) | (~x & z);

        /// <summary>
        /// Round operation [32, 47]
        /// </summary>
        private static uint G32_47(uint x, uint y, uint z) => (x | ~y) ^ z;

        /// <summary>
        /// Round operation [48, 63]
        /// </summary>
        private static uint G48_63(uint x, uint y, uint z) => (x & z) | (y & ~z);
    }
}