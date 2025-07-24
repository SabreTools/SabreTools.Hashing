using System;
using static SabreTools.Hashing.CryptographicHash.Constants;
using static SabreTools.Hashing.HashOperations;

namespace SabreTools.Hashing.CryptographicHash
{
    /// <see href="https://cdn.standards.iteh.ai/samples/39876/10f9f9f4bb614eaaaeba7e157e183ca3/ISO-IEC-10118-3-2004.pdf"/>
    /// <see href="https://homes.esat.kuleuven.be/~bosselae/ripemd160/pdf/AB-9601/AB-9601.pdf"/>
    public class RipeMD160 : MessageDigestBase<uint>
    {
        /// <inheritdoc/>
        public override int HashSize => 160;

        /// <summary>
        /// Set of 5 32-bit numbers representing the hash state
        /// </summary>
        private readonly uint[] _state = new uint[5];

        public RipeMD160() : base()
        {
        }

        /// <inheritdoc/>
        protected override void ResetImpl()
        {
            _state[0] = RMD160Y0;
            _state[1] = RMD160Y1;
            _state[2] = RMD160Y2;
            _state[3] = RMD160Y3;
            _state[4] = RMD160Y4;
        }

        /// <inheritdoc/>
        protected override void HashCore(byte[] data, int offset, int length)
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

        /// <inheritdoc/>
        protected override byte[] HashFinal()
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
            HashCore(padding, 0, padding.Length);

            // Get the hash
            var hash = new byte[20];
            int hashOffset = 0;

            // Assemble the hash array
            for (int i = 0; i < _state.Length; i++)
            {
                byte[] segment = BitConverter.GetBytes(_state[i]);
                Array.Copy(segment, 0, hash, hashOffset, 4);
                hashOffset += 4;
            }

            return hash;
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
            uint x0 = _state[0], xp0 = _state[0];
            uint x1 = _state[1], xp1 = _state[1];
            uint x2 = _state[2], xp2 = _state[2];
            uint x3 = _state[3], xp3 = _state[3];
            uint x4 = _state[4], xp4 = _state[4];

            #region Rounds 0-15

            // Round 0
            x0 = RotateLeft32(x0 + G00_15(x1, x2, x3) + _block[0] + RMD160Round00To15, 11) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G64_79(xp1, xp2, xp3) + _block[5] + RMD160RoundPrime00To15, 8) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 1
            x4 = RotateLeft32(x4 + G00_15(x0, x1, x2) + _block[1] + RMD160Round00To15, 14) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G64_79(xp0, xp1, xp2) + _block[14] + RMD160RoundPrime00To15, 9) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 2
            x3 = RotateLeft32(x3 + G00_15(x4, x0, x1) + _block[2] + RMD160Round00To15, 15) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G64_79(xp4, xp0, xp1) + _block[7] + RMD160RoundPrime00To15, 9) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 3
            x2 = RotateLeft32(x2 + G00_15(x3, x4, x0) + _block[3] + RMD160Round00To15, 12) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G64_79(xp3, xp4, xp0) + _block[0] + RMD160RoundPrime00To15, 11) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 4
            x1 = RotateLeft32(x1 + G00_15(x2, x3, x4) + _block[4] + RMD160Round00To15, 5) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G64_79(xp2, xp3, xp4) + _block[9] + RMD160RoundPrime00To15, 13) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 5
            x0 = RotateLeft32(x0 + G00_15(x1, x2, x3) + _block[5] + RMD160Round00To15, 8) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G64_79(xp1, xp2, xp3) + _block[2] + RMD160RoundPrime00To15, 15) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 6
            x4 = RotateLeft32(x4 + G00_15(x0, x1, x2) + _block[6] + RMD160Round00To15, 7) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G64_79(xp0, xp1, xp2) + _block[11] + RMD160RoundPrime00To15, 15) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 7
            x3 = RotateLeft32(x3 + G00_15(x4, x0, x1) + _block[7] + RMD160Round00To15, 9) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G64_79(xp4, xp0, xp1) + _block[4] + RMD160RoundPrime00To15, 5) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 8
            x2 = RotateLeft32(x2 + G00_15(x3, x4, x0) + _block[8] + RMD160Round00To15, 11) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G64_79(xp3, xp4, xp0) + _block[13] + RMD160RoundPrime00To15, 7) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 9
            x1 = RotateLeft32(x1 + G00_15(x2, x3, x4) + _block[9] + RMD160Round00To15, 13) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G64_79(xp2, xp3, xp4) + _block[6] + RMD160RoundPrime00To15, 7) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 10
            x0 = RotateLeft32(x0 + G00_15(x1, x2, x3) + _block[10] + RMD160Round00To15, 14) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G64_79(xp1, xp2, xp3) + _block[15] + RMD160RoundPrime00To15, 8) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 11
            x4 = RotateLeft32(x4 + G00_15(x0, x1, x2) + _block[11] + RMD160Round00To15, 15) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G64_79(xp0, xp1, xp2) + _block[8] + RMD160RoundPrime00To15, 11) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 12
            x3 = RotateLeft32(x3 + G00_15(x4, x0, x1) + _block[12] + RMD160Round00To15, 6) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G64_79(xp4, xp0, xp1) + _block[1] + RMD160RoundPrime00To15, 14) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 13
            x2 = RotateLeft32(x2 + G00_15(x3, x4, x0) + _block[13] + RMD160Round00To15, 7) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G64_79(xp3, xp4, xp0) + _block[10] + RMD160RoundPrime00To15, 14) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 14
            x1 = RotateLeft32(x1 + G00_15(x2, x3, x4) + _block[14] + RMD160Round00To15, 9) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G64_79(xp2, xp3, xp4) + _block[3] + RMD160RoundPrime00To15, 12) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 15
            x0 = RotateLeft32(x0 + G00_15(x1, x2, x3) + _block[15] + RMD160Round00To15, 8) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G64_79(xp1, xp2, xp3) + _block[12] + RMD160RoundPrime00To15, 6) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            #endregion

            #region Rounds 16-31

            // Round 16
            x4 = RotateLeft32(x4 + G16_31(x0, x1, x2) + _block[7] + RMD160Round16To31, 7) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G48_63(xp0, xp1, xp2) + _block[6] + RMD160RoundPrime16To31, 9) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 17
            x3 = RotateLeft32(x3 + G16_31(x4, x0, x1) + _block[4] + RMD160Round16To31, 6) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G48_63(xp4, xp0, xp1) + _block[11] + RMD160RoundPrime16To31, 13) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 18
            x2 = RotateLeft32(x2 + G16_31(x3, x4, x0) + _block[13] + RMD160Round16To31, 8) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G48_63(xp3, xp4, xp0) + _block[3] + RMD160RoundPrime16To31, 15) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 19
            x1 = RotateLeft32(x1 + G16_31(x2, x3, x4) + _block[1] + RMD160Round16To31, 13) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G48_63(xp2, xp3, xp4) + _block[7] + RMD160RoundPrime16To31, 7) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 20
            x0 = RotateLeft32(x0 + G16_31(x1, x2, x3) + _block[10] + RMD160Round16To31, 11) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G48_63(xp1, xp2, xp3) + _block[0] + RMD160RoundPrime16To31, 12) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 21
            x4 = RotateLeft32(x4 + G16_31(x0, x1, x2) + _block[6] + RMD160Round16To31, 9) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G48_63(xp0, xp1, xp2) + _block[13] + RMD160RoundPrime16To31, 8) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 22
            x3 = RotateLeft32(x3 + G16_31(x4, x0, x1) + _block[15] + RMD160Round16To31, 7) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G48_63(xp4, xp0, xp1) + _block[5] + RMD160RoundPrime16To31, 9) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 23
            x2 = RotateLeft32(x2 + G16_31(x3, x4, x0) + _block[3] + RMD160Round16To31, 15) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G48_63(xp3, xp4, xp0) + _block[10] + RMD160RoundPrime16To31, 11) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 24
            x1 = RotateLeft32(x1 + G16_31(x2, x3, x4) + _block[12] + RMD160Round16To31, 7) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G48_63(xp2, xp3, xp4) + _block[14] + RMD160RoundPrime16To31, 7) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 25
            x0 = RotateLeft32(x0 + G16_31(x1, x2, x3) + _block[0] + RMD160Round16To31, 12) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G48_63(xp1, xp2, xp3) + _block[15] + RMD160RoundPrime16To31, 7) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 26
            x4 = RotateLeft32(x4 + G16_31(x0, x1, x2) + _block[9] + RMD160Round16To31, 15) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G48_63(xp0, xp1, xp2) + _block[8] + RMD160RoundPrime16To31, 12) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 27
            x3 = RotateLeft32(x3 + G16_31(x4, x0, x1) + _block[5] + RMD160Round16To31, 9) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G48_63(xp4, xp0, xp1) + _block[12] + RMD160RoundPrime16To31, 7) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 28
            x2 = RotateLeft32(x2 + G16_31(x3, x4, x0) + _block[2] + RMD160Round16To31, 11) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G48_63(xp3, xp4, xp0) + _block[4] + RMD160RoundPrime16To31, 6) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 29
            x1 = RotateLeft32(x1 + G16_31(x2, x3, x4) + _block[14] + RMD160Round16To31, 7) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G48_63(xp2, xp3, xp4) + _block[9] + RMD160RoundPrime16To31, 15) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 30
            x0 = RotateLeft32(x0 + G16_31(x1, x2, x3) + _block[11] + RMD160Round16To31, 13) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G48_63(xp1, xp2, xp3) + _block[1] + RMD160RoundPrime16To31, 13) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 31
            x4 = RotateLeft32(x4 + G16_31(x0, x1, x2) + _block[8] + RMD160Round16To31, 12) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G48_63(xp0, xp1, xp2) + _block[2] + RMD160RoundPrime16To31, 11) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            #endregion

            #region Rounds 32-47

            // Round 32
            x3 = RotateLeft32(x3 + G32_47(x4, x0, x1) + _block[3] + RMD160Round32To47, 11) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G32_47(xp4, xp0, xp1) + _block[15] + RMD160RoundPrime32To47, 9) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 33
            x2 = RotateLeft32(x2 + G32_47(x3, x4, x0) + _block[10] + RMD160Round32To47, 13) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G32_47(xp3, xp4, xp0) + _block[5] + RMD160RoundPrime32To47, 7) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 34
            x1 = RotateLeft32(x1 + G32_47(x2, x3, x4) + _block[14] + RMD160Round32To47, 6) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G32_47(xp2, xp3, xp4) + _block[1] + RMD160RoundPrime32To47, 15) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 35
            x0 = RotateLeft32(x0 + G32_47(x1, x2, x3) + _block[4] + RMD160Round32To47, 7) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G32_47(xp1, xp2, xp3) + _block[3] + RMD160RoundPrime32To47, 11) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 36
            x4 = RotateLeft32(x4 + G32_47(x0, x1, x2) + _block[9] + RMD160Round32To47, 14) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G32_47(xp0, xp1, xp2) + _block[7] + RMD160RoundPrime32To47, 8) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 37
            x3 = RotateLeft32(x3 + G32_47(x4, x0, x1) + _block[15] + RMD160Round32To47, 9) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G32_47(xp4, xp0, xp1) + _block[14] + RMD160RoundPrime32To47, 6) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 38
            x2 = RotateLeft32(x2 + G32_47(x3, x4, x0) + _block[8] + RMD160Round32To47, 13) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G32_47(xp3, xp4, xp0) + _block[6] + RMD160RoundPrime32To47, 6) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 39
            x1 = RotateLeft32(x1 + G32_47(x2, x3, x4) + _block[1] + RMD160Round32To47, 15) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G32_47(xp2, xp3, xp4) + _block[9] + RMD160RoundPrime32To47, 14) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 40
            x0 = RotateLeft32(x0 + G32_47(x1, x2, x3) + _block[2] + RMD160Round32To47, 14) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G32_47(xp1, xp2, xp3) + _block[11] + RMD160RoundPrime32To47, 12) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 41
            x4 = RotateLeft32(x4 + G32_47(x0, x1, x2) + _block[7] + RMD160Round32To47, 8) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G32_47(xp0, xp1, xp2) + _block[8] + RMD160RoundPrime32To47, 13) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 42
            x3 = RotateLeft32(x3 + G32_47(x4, x0, x1) + _block[0] + RMD160Round32To47, 13) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G32_47(xp4, xp0, xp1) + _block[12] + RMD160RoundPrime32To47, 5) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 43
            x2 = RotateLeft32(x2 + G32_47(x3, x4, x0) + _block[6] + RMD160Round32To47, 6) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G32_47(xp3, xp4, xp0) + _block[2] + RMD160RoundPrime32To47, 14) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 44
            x1 = RotateLeft32(x1 + G32_47(x2, x3, x4) + _block[13] + RMD160Round32To47, 5) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G32_47(xp2, xp3, xp4) + _block[10] + RMD160RoundPrime32To47, 13) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 45
            x0 = RotateLeft32(x0 + G32_47(x1, x2, x3) + _block[11] + RMD160Round32To47, 12) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G32_47(xp1, xp2, xp3) + _block[0] + RMD160RoundPrime32To47, 13) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 46
            x4 = RotateLeft32(x4 + G32_47(x0, x1, x2) + _block[5] + RMD160Round32To47, 7) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G32_47(xp0, xp1, xp2) + _block[4] + RMD160RoundPrime32To47, 7) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 47
            x3 = RotateLeft32(x3 + G32_47(x4, x0, x1) + _block[12] + RMD160Round32To47, 5) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G32_47(xp4, xp0, xp1) + _block[13] + RMD160RoundPrime32To47, 5) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            #endregion

            #region Rounds 48-63

            // Round 48
            x2 = RotateLeft32(x2 + G48_63(x3, x4, x0) + _block[1] + RMD160Round48To63, 11) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G16_31(xp3, xp4, xp0) + _block[8] + RMD160RoundPrime48To63, 15) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 49
            x1 = RotateLeft32(x1 + G48_63(x2, x3, x4) + _block[9] + RMD160Round48To63, 12) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G16_31(xp2, xp3, xp4) + _block[6] + RMD160RoundPrime48To63, 5) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 50
            x0 = RotateLeft32(x0 + G48_63(x1, x2, x3) + _block[11] + RMD160Round48To63, 14) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G16_31(xp1, xp2, xp3) + _block[4] + RMD160RoundPrime48To63, 8) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 51
            x4 = RotateLeft32(x4 + G48_63(x0, x1, x2) + _block[10] + RMD160Round48To63, 15) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G16_31(xp0, xp1, xp2) + _block[1] + RMD160RoundPrime48To63, 11) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 52
            x3 = RotateLeft32(x3 + G48_63(x4, x0, x1) + _block[0] + RMD160Round48To63, 14) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G16_31(xp4, xp0, xp1) + _block[3] + RMD160RoundPrime48To63, 14) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 53
            x2 = RotateLeft32(x2 + G48_63(x3, x4, x0) + _block[8] + RMD160Round48To63, 15) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G16_31(xp3, xp4, xp0) + _block[11] + RMD160RoundPrime48To63, 14) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 54
            x1 = RotateLeft32(x1 + G48_63(x2, x3, x4) + _block[12] + RMD160Round48To63, 9) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G16_31(xp2, xp3, xp4) + _block[15] + RMD160RoundPrime48To63, 6) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 55
            x0 = RotateLeft32(x0 + G48_63(x1, x2, x3) + _block[4] + RMD160Round48To63, 8) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G16_31(xp1, xp2, xp3) + _block[0] + RMD160RoundPrime48To63, 14) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 56
            x4 = RotateLeft32(x4 + G48_63(x0, x1, x2) + _block[13] + RMD160Round48To63, 9) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G16_31(xp0, xp1, xp2) + _block[5] + RMD160RoundPrime48To63, 6) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 57
            x3 = RotateLeft32(x3 + G48_63(x4, x0, x1) + _block[3] + RMD160Round48To63, 14) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G16_31(xp4, xp0, xp1) + _block[12] + RMD160RoundPrime48To63, 9) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 58
            x2 = RotateLeft32(x2 + G48_63(x3, x4, x0) + _block[7] + RMD160Round48To63, 5) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G16_31(xp3, xp4, xp0) + _block[2] + RMD160RoundPrime48To63, 12) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 59
            x1 = RotateLeft32(x1 + G48_63(x2, x3, x4) + _block[15] + RMD160Round48To63, 6) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G16_31(xp2, xp3, xp4) + _block[13] + RMD160RoundPrime48To63, 9) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 60
            x0 = RotateLeft32(x0 + G48_63(x1, x2, x3) + _block[14] + RMD160Round48To63, 8) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G16_31(xp1, xp2, xp3) + _block[9] + RMD160RoundPrime48To63, 12) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 61
            x4 = RotateLeft32(x4 + G48_63(x0, x1, x2) + _block[5] + RMD160Round48To63, 6) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G16_31(xp0, xp1, xp2) + _block[7] + RMD160RoundPrime48To63, 5) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 62
            x3 = RotateLeft32(x3 + G48_63(x4, x0, x1) + _block[6] + RMD160Round48To63, 5) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G16_31(xp4, xp0, xp1) + _block[10] + RMD160RoundPrime48To63, 15) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 63
            x2 = RotateLeft32(x2 + G48_63(x3, x4, x0) + _block[2] + RMD160Round48To63, 12) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G16_31(xp3, xp4, xp0) + _block[14] + RMD160RoundPrime48To63, 8) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            #endregion

            #region Rounds 64-79

            // Round 64
            x1 = RotateLeft32(x1 + G64_79(x2, x3, x4) + _block[4] + RMD160Round64To79, 9) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G00_15(xp2, xp3, xp4) + _block[12] + RMD160RoundPrime64To79, 8) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 65
            x0 = RotateLeft32(x0 + G64_79(x1, x2, x3) + _block[0] + RMD160Round64To79, 15) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G00_15(xp1, xp2, xp3) + _block[15] + RMD160RoundPrime64To79, 5) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 66
            x4 = RotateLeft32(x4 + G64_79(x0, x1, x2) + _block[5] + RMD160Round64To79, 5) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G00_15(xp0, xp1, xp2) + _block[10] + RMD160RoundPrime64To79, 12) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 67
            x3 = RotateLeft32(x3 + G64_79(x4, x0, x1) + _block[9] + RMD160Round64To79, 11) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G00_15(xp4, xp0, xp1) + _block[4] + RMD160RoundPrime64To79, 9) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 68
            x2 = RotateLeft32(x2 + G64_79(x3, x4, x0) + _block[7] + RMD160Round64To79, 6) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G00_15(xp3, xp4, xp0) + _block[1] + RMD160RoundPrime64To79, 12) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 69
            x1 = RotateLeft32(x1 + G64_79(x2, x3, x4) + _block[12] + RMD160Round64To79, 8) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G00_15(xp2, xp3, xp4) + _block[5] + RMD160RoundPrime64To79, 5) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 70
            x0 = RotateLeft32(x0 + G64_79(x1, x2, x3) + _block[2] + RMD160Round64To79, 13) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G00_15(xp1, xp2, xp3) + _block[8] + RMD160RoundPrime64To79, 14) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 71
            x4 = RotateLeft32(x4 + G64_79(x0, x1, x2) + _block[10] + RMD160Round64To79, 12) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G00_15(xp0, xp1, xp2) + _block[7] + RMD160RoundPrime64To79, 6) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 72
            x3 = RotateLeft32(x3 + G64_79(x4, x0, x1) + _block[14] + RMD160Round64To79, 5) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G00_15(xp4, xp0, xp1) + _block[6] + RMD160RoundPrime64To79, 8) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 73
            x2 = RotateLeft32(x2 + G64_79(x3, x4, x0) + _block[1] + RMD160Round64To79, 12) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G00_15(xp3, xp4, xp0) + _block[2] + RMD160RoundPrime64To79, 13) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 74
            x1 = RotateLeft32(x1 + G64_79(x2, x3, x4) + _block[3] + RMD160Round64To79, 13) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G00_15(xp2, xp3, xp4) + _block[13] + RMD160RoundPrime64To79, 6) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            // Round 75
            x0 = RotateLeft32(x0 + G64_79(x1, x2, x3) + _block[8] + RMD160Round64To79, 14) + x4;
            x2 = RotateLeft32(x2, 10);
            xp0 = RotateLeft32(xp0 + G00_15(xp1, xp2, xp3) + _block[14] + RMD160RoundPrime64To79, 5) + xp4;
            xp2 = RotateLeft32(xp2, 10);

            // Round 76
            x4 = RotateLeft32(x4 + G64_79(x0, x1, x2) + _block[11] + RMD160Round64To79, 11) + x3;
            x1 = RotateLeft32(x1, 10);
            xp4 = RotateLeft32(xp4 + G00_15(xp0, xp1, xp2) + _block[0] + RMD160RoundPrime64To79, 15) + xp3;
            xp1 = RotateLeft32(xp1, 10);

            // Round 77
            x3 = RotateLeft32(x3 + G64_79(x4, x0, x1) + _block[6] + RMD160Round64To79, 8) + x2;
            x0 = RotateLeft32(x0, 10);
            xp3 = RotateLeft32(xp3 + G00_15(xp4, xp0, xp1) + _block[3] + RMD160RoundPrime64To79, 13) + xp2;
            xp0 = RotateLeft32(xp0, 10);

            // Round 78
            x2 = RotateLeft32(x2 + G64_79(x3, x4, x0) + _block[15] + RMD160Round64To79, 5) + x1;
            x4 = RotateLeft32(x4, 10);
            xp2 = RotateLeft32(xp2 + G00_15(xp3, xp4, xp0) + _block[9] + RMD160RoundPrime64To79, 11) + xp1;
            xp4 = RotateLeft32(xp4, 10);

            // Round 79
            x1 = RotateLeft32(x1 + G64_79(x2, x3, x4) + _block[13] + RMD160Round64To79, 6) + x0;
            x3 = RotateLeft32(x3, 10);
            xp1 = RotateLeft32(xp1 + G00_15(xp2, xp3, xp4) + _block[11] + RMD160RoundPrime64To79, 11) + xp0;
            xp3 = RotateLeft32(xp3, 10);

            #endregion

            // Avalanche values
            xp3 += x2 + _state[1];
            _state[1] = _state[2] + x3 + xp4;
            _state[2] = _state[3] + x4 + xp0;
            _state[3] = _state[4] + x0 + xp1;
            _state[4] = _state[0] + x1 + xp2;
            _state[0] = xp3;
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

        /// <summary>
        /// Round operation [64, 79]
        /// </summary>
        private static uint G64_79(uint x, uint y, uint z) => x ^ (y | ~z);
    }
}
