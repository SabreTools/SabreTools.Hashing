using System;
using static SabreTools.Hashing.HashOperations;
using static SabreTools.Hashing.MessageDigest.Constants;

namespace SabreTools.Hashing.MessageDigest
{
    /// <see href="https://datatracker.ietf.org/doc/html/rfc1320"/>
    public class MD4 : MessageDigestBase<uint>
    {
        /// <summary>
        /// Set of 4 32-bit numbers representing the hash state
        /// </summary>
        private readonly uint[] _state = new uint[4];

        public MD4() : base()
        {
        }

        /// <inheritdoc/>
        protected override void ResetImpl()
        {
            _state[0] = MD4SeedA;
            _state[1] = MD4SeedB;
            _state[2] = MD4SeedC;
            _state[3] = MD4SeedD;
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
            var hash = new byte[16];
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
        private void Round()
        {
            // Setup values
            uint a = _state[0];
            uint b = _state[1];
            uint c = _state[2];
            uint d = _state[3];

            // Round 1
            a = RotateLeft32(a + F(b, c, d) + _block[0] + MD4Round1, 3);
            d = RotateLeft32(d + F(a, b, c) + _block[1] + MD4Round1, 7);
            c = RotateLeft32(c + F(d, a, b) + _block[2] + MD4Round1, 11);
            b = RotateLeft32(b + F(c, d, a) + _block[3] + MD4Round1, 19);
            a = RotateLeft32(a + F(b, c, d) + _block[4] + MD4Round1, 3);
            d = RotateLeft32(d + F(a, b, c) + _block[5] + MD4Round1, 7);
            c = RotateLeft32(c + F(d, a, b) + _block[6] + MD4Round1, 11);
            b = RotateLeft32(b + F(c, d, a) + _block[7] + MD4Round1, 19);
            a = RotateLeft32(a + F(b, c, d) + _block[8] + MD4Round1, 3);
            d = RotateLeft32(d + F(a, b, c) + _block[9] + MD4Round1, 7);
            c = RotateLeft32(c + F(d, a, b) + _block[10] + MD4Round1, 11);
            b = RotateLeft32(b + F(c, d, a) + _block[11] + MD4Round1, 19);
            a = RotateLeft32(a + F(b, c, d) + _block[12] + MD4Round1, 3);
            d = RotateLeft32(d + F(a, b, c) + _block[13] + MD4Round1, 7);
            c = RotateLeft32(c + F(d, a, b) + _block[14] + MD4Round1, 11);
            b = RotateLeft32(b + F(c, d, a) + _block[15] + MD4Round1, 19);

            // Round 2
            a = RotateLeft32(a + G(b, c, d) + _block[0] + MD4Round2, 3);
            d = RotateLeft32(d + G(a, b, c) + _block[4] + MD4Round2, 5);
            c = RotateLeft32(c + G(d, a, b) + _block[8] + MD4Round2, 9);
            b = RotateLeft32(b + G(c, d, a) + _block[12] + MD4Round2, 13);
            a = RotateLeft32(a + G(b, c, d) + _block[1] + MD4Round2, 3);
            d = RotateLeft32(d + G(a, b, c) + _block[5] + MD4Round2, 5);
            c = RotateLeft32(c + G(d, a, b) + _block[9] + MD4Round2, 9);
            b = RotateLeft32(b + G(c, d, a) + _block[13] + MD4Round2, 13);
            a = RotateLeft32(a + G(b, c, d) + _block[2] + MD4Round2, 3);
            d = RotateLeft32(d + G(a, b, c) + _block[6] + MD4Round2, 5);
            c = RotateLeft32(c + G(d, a, b) + _block[10] + MD4Round2, 9);
            b = RotateLeft32(b + G(c, d, a) + _block[14] + MD4Round2, 13);
            a = RotateLeft32(a + G(b, c, d) + _block[3] + MD4Round2, 3);
            d = RotateLeft32(d + G(a, b, c) + _block[7] + MD4Round2, 5);
            c = RotateLeft32(c + G(d, a, b) + _block[11] + MD4Round2, 9);
            b = RotateLeft32(b + G(c, d, a) + _block[15] + MD4Round2, 13);

            // Round 3
            a = RotateLeft32(a + H(b, c, d) + _block[0] + MD4Round3, 3);
            d = RotateLeft32(d + H(a, b, c) + _block[8] + MD4Round3, 9);
            c = RotateLeft32(c + H(d, a, b) + _block[4] + MD4Round3, 11);
            b = RotateLeft32(b + H(c, d, a) + _block[12] + MD4Round3, 15);
            a = RotateLeft32(a + H(b, c, d) + _block[2] + MD4Round3, 3);
            d = RotateLeft32(d + H(a, b, c) + _block[10] + MD4Round3, 9);
            c = RotateLeft32(c + H(d, a, b) + _block[6] + MD4Round3, 11);
            b = RotateLeft32(b + H(c, d, a) + _block[14] + MD4Round3, 15);
            a = RotateLeft32(a + H(b, c, d) + _block[1] + MD4Round3, 3);
            d = RotateLeft32(d + H(a, b, c) + _block[9] + MD4Round3, 9);
            c = RotateLeft32(c + H(d, a, b) + _block[5] + MD4Round3, 11);
            b = RotateLeft32(b + H(c, d, a) + _block[13] + MD4Round3, 15);
            a = RotateLeft32(a + H(b, c, d) + _block[3] + MD4Round3, 3);
            d = RotateLeft32(d + H(a, b, c) + _block[11] + MD4Round3, 9);
            c = RotateLeft32(c + H(d, a, b) + _block[7] + MD4Round3, 11);
            b = RotateLeft32(b + H(c, d, a) + _block[15] + MD4Round3, 15);

            // Update stored values
            _state[0] += a;
            _state[1] += b;
            _state[2] += c;
            _state[3] += d;
        }

        /// <summary>
        /// Auxiliary function F
        /// </summary>
        private static uint F(uint x, uint y, uint z) => (x & y) | (~x & z);

        /// <summary>
        /// Auxiliary function G
        /// </summary>
        private static uint G(uint x, uint y, uint z) => (x & y) | (x & z) | (y & z);

        /// <summary>
        /// Auxiliary function H
        /// </summary>
        private static uint H(uint x, uint y, uint z) => x ^ y ^ z;
    }
}