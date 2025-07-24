using System;
using static SabreTools.Hashing.HashOperations;
using static SabreTools.Hashing.NonCryptographicHash.Constants;

namespace SabreTools.Hashing.NonCryptographicHash
{
    /// <summary>
    /// Structure for xxHash-32 streaming API.
    /// </summary>
    /// <see href="https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h"/> 
    internal class XxHash32State
    {
        /// <summary>
        /// Total length hashed, modulo 2^32
        /// </summary>
        private uint _totalLen32;

        /// <summary>
        /// Whether the hash is >= 16 (handles <see cref="_totalLen32"/> overflow)
        /// </summary>
        private bool _largeLen;

        /// <summary>
        /// Accumulator lanes
        /// </summary>
        private readonly uint[] _acc = new uint[4];

        /// <summary>
        /// Internal buffer for partial reads. Treated as unsigned char[16].
        /// </summary>
        private readonly byte[] _mem32 = new byte[16];

        /// <summary>
        /// Amount of data in <see cref="_mem32">
        /// </summary>
        private int _memsize;

        /// <summary>
        /// Resets to begin a new hash
        /// </summary>
        /// <param name="seed">The 32-bit seed to alter the hash result predictably.</param>
        public void Reset(uint seed)
        {
            _totalLen32 = 0;
            _largeLen = false;

            unchecked
            {
                _acc[0] = seed + XXH_PRIME32_1 + XXH_PRIME32_2;
                _acc[1] = seed + XXH_PRIME32_2;
                _acc[2] = seed + 0;
                _acc[3] = seed - XXH_PRIME32_1;
            }

            Array.Clear(_mem32, 0, _mem32.Length);
            _memsize = 0;
        }

        /// <summary>
        /// Consumes a block of input
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void Update(byte[] data, int offset, int length)
        {
            int bEnd = offset + length;

            _totalLen32 += (uint)length;
            _largeLen |= (length >= 16) | (_totalLen32 >= 16);

            // Fill in tmp buffer
            if (_memsize + length < 16)
            {
                Array.Copy(data, offset, _mem32, _memsize, length);
                _memsize += length;
                return;
            }

            // Some data left from previous update
            if (_memsize > 0)
            {
                Array.Copy(data, offset, _mem32, _memsize, 16 - _memsize);

                int p32 = 0;
                _acc[0] = Round(_acc[0], ReadLE32(_mem32, p32)); p32 += 4;
                _acc[1] = Round(_acc[1], ReadLE32(_mem32, p32)); p32 += 4;
                _acc[2] = Round(_acc[2], ReadLE32(_mem32, p32)); p32 += 4;
                _acc[3] = Round(_acc[3], ReadLE32(_mem32, p32));

                offset += 16 - _memsize;
                _memsize = 0;
            }

            if (offset <= bEnd - 16)
            {
                int limit = bEnd - 16;
                do
                {
                    _acc[0] = Round(_acc[0], ReadLE32(data, offset)); offset += 4;
                    _acc[1] = Round(_acc[1], ReadLE32(data, offset)); offset += 4;
                    _acc[2] = Round(_acc[2], ReadLE32(data, offset)); offset += 4;
                    _acc[3] = Round(_acc[3], ReadLE32(data, offset)); offset += 4;
                } while (offset <= limit);
            }

            if (offset < bEnd)
            {
                Array.Copy(data, offset, _mem32, 0, bEnd - offset);
                _memsize = bEnd - offset;
            }
        }

        /// <summary>
        /// Returns the calculated hash value
        /// </summary>
        /// <returns>The calculated 32-bit xxHash32 value from that state.</returns>
        public uint Digest()
        {
            uint hash;

            if (_largeLen)
            {
                hash = RotateLeft32(_acc[0], 1)
                     + RotateLeft32(_acc[1], 7)
                     + RotateLeft32(_acc[2], 12)
                     + RotateLeft32(_acc[3], 18);
            }
            else
            {
                hash = _acc[2] /* == seed */ + XXH_PRIME32_5;
            }

            hash += _totalLen32;
            return Finalize(hash, _mem32, 0, _memsize);
        }

        /// <summary>
        /// Normal stripe processing routine.
        /// 
        /// This shuffles the bits so that any bit from <paramref name="input"/> impacts
        /// several bits in <paramref name="acc"/>.
        /// </summary>
        /// <param name="acc">The accumulator lane.</param>
        /// <param name="input">The stripe of input to mix.</param>
        /// <returns>The mixed accumulator lane.</returns>
        private static uint Round(uint acc, uint input)
        {
            acc += input * XXH_PRIME32_2;
            acc = RotateLeft32(acc, 13);
            acc *= XXH_PRIME32_1;
            return acc;
        }

        /// <summary>
        /// Mixes all bits to finalize the hash.
        /// 
        /// The final mix ensures that all input bits have a chance to impact any bit in
        /// the output digest, resulting in an unbiased distribution.
        /// </summary>
        private static uint Avalanche(uint hash)
        {
            hash ^= hash >> 15;
            hash *= XXH_PRIME32_2;
            hash ^= hash >> 13;
            hash *= XXH_PRIME32_3;
            hash ^= hash >> 16;
            return hash;
        }

        /// <summary>
        /// Processes the last 0-15 bytes of @p ptr.
        /// 
        /// There may be up to 15 bytes remaining to consume from the input.
        /// This final stage will digest them to ensure that all input bytes are present
        /// in the final mix.
        /// </summary>
        /// <param name="hash">The hash to finalize.</param>
        /// <param name="data">The remaining input.</param>
        /// <param name="offset">The pointer to the remaining input.</param>
        /// <param name="length">The remaining length, modulo 16.</param>
        /// <returns>The finalized hash.</returns>
        private static uint Finalize(uint hash, byte[] data, int offset, int length)
        {
            length &= 15;
            while (length >= 4)
            {
                hash += ReadLE32(data, offset) * XXH_PRIME32_3;
                offset += 4;
                hash = RotateLeft32(hash, 17) * XXH_PRIME32_4;
                length -= 4;
            }

            while (length > 0)
            {
                hash += data[offset++] * XXH_PRIME32_5;
                hash = RotateLeft32(hash, 11) * XXH_PRIME32_1;
                --length;
            }

            return Avalanche(hash);
        }
    }
}
