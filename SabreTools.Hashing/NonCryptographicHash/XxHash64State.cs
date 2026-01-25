using System;
using static SabreTools.Hashing.HashOperations;
using static SabreTools.Hashing.NonCryptographicHash.Constants;

namespace SabreTools.Hashing.NonCryptographicHash
{
    /// <summary>
    /// Structure for xxHash-64 streaming API.
    /// </summary>
    /// <see href="https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h"/>
    internal class XxHash64State
    {
        /// <summary>
        /// Total length hashed. This is always 64-bit.
        /// </summary>
        private ulong _totalLen;

        /// <summary>
        /// Accumulator lanes
        /// </summary>
        private readonly ulong[] _acc = new ulong[4];

        /// <summary>
        /// Internal buffer for partial reads. Treated as unsigned char[32].
        /// </summary>
        private readonly byte[] _mem64 = new byte[32];

        /// <summary>
        /// Amount of data in <see cref="_mem64">
        /// </summary>
        private int _memsize;

        /// <summary>
        /// Resets to begin a new hash
        /// </summary>
        /// <param name="seed">The 64-bit seed to alter the hash result predictably.</param>
        public void Reset(ulong seed)
        {
            _totalLen = 0;

            unchecked
            {
                _acc[0] = seed + XXH_PRIME64_1 + XXH_PRIME64_2;
                _acc[1] = seed + XXH_PRIME64_2;
                _acc[2] = seed + 0;
                _acc[3] = seed - XXH_PRIME64_1;
            }

            Array.Clear(_mem64, 0, _mem64.Length);
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

            _totalLen += (ulong)length;

            // Fill in tmp buffer
            if (_memsize + length < 32)
            {
                Array.Copy(data, offset, _mem64, _memsize, length);
                _memsize += length;
                return;
            }

            // Some data left from previous update
            if (_memsize > 0)
            {
                Array.Copy(data, offset, _mem64, _memsize, 32 - _memsize);

                int p64 = 0;
                _acc[0] = Round(_acc[0], ReadLE64(_mem64, p64)); p64 += 8;
                _acc[1] = Round(_acc[1], ReadLE64(_mem64, p64)); p64 += 8;
                _acc[2] = Round(_acc[2], ReadLE64(_mem64, p64)); p64 += 8;
                _acc[3] = Round(_acc[3], ReadLE64(_mem64, p64));

                offset += 32 - _memsize;
                _memsize = 0;
            }

            if (offset <= bEnd - 32)
            {
                int limit = bEnd - 32;
                do
                {
                    _acc[0] = Round(_acc[0], ReadLE64(data, offset)); offset += 8;
                    _acc[1] = Round(_acc[1], ReadLE64(data, offset)); offset += 8;
                    _acc[2] = Round(_acc[2], ReadLE64(data, offset)); offset += 8;
                    _acc[3] = Round(_acc[3], ReadLE64(data, offset)); offset += 8;
                } while (offset <= limit);
            }

            if (offset < bEnd)
            {
                Array.Copy(data, offset, _mem64, 0, bEnd - offset);
                _memsize = bEnd - offset;
            }
        }

        /// <summary>
        /// Returns the calculated hash value
        /// </summary>
        /// <returns>The calculated 64-bit xxHash64 value from that state.</returns>
        public ulong Digest()
        {
            ulong h64;

            if (_totalLen >= 32)
            {
                h64 = RotateLeft64(_acc[0], 1)
                    + RotateLeft64(_acc[1], 7)
                    + RotateLeft64(_acc[2], 12)
                    + RotateLeft64(_acc[3], 18);
                h64 = MergeRound(h64, _acc[0]);
                h64 = MergeRound(h64, _acc[1]);
                h64 = MergeRound(h64, _acc[2]);
                h64 = MergeRound(h64, _acc[3]);
            }
            else
            {
                h64 = _acc[2] /*seed*/ + XXH_PRIME64_5;
            }

            h64 += _totalLen;
            return Finalize(h64, _mem64, 0, (int)_totalLen);
        }

        /// <summary>
        /// Normal stripe processing routine.
        ///
        /// This shuffles the bits so that any bit from @p input impacts
        /// several bits in @p acc.
        /// </summary>
        /// <param name="acc">The accumulator lane.</param>
        /// <param name="input">The stripe of input to mix.</param>
        /// <returns>The mixed accumulator lane.</returns>
        private static ulong Round(ulong acc, ulong input)
        {
            acc += unchecked(input * XXH_PRIME64_2);
            acc = RotateLeft64(acc, 31);
            acc *= XXH_PRIME64_1;
            return acc;
        }

        private static ulong MergeRound(ulong acc, ulong val)
        {
            val = Round(0, val);
            acc ^= val;
            acc = (acc * XXH_PRIME64_1) + XXH_PRIME64_4;
            return acc;
        }

        /// <summary>
        /// Processes the last 0-31 bytes of @p ptr.
        ///
        /// There may be up to 31 bytes remaining to consume from the input.
        /// This final stage will digest them to ensure that all input bytes are present
        /// in the final mix.
        /// </summary>
        /// <param name="hash">The hash to finalize.</param>
        /// <param name="data">The pointer to the remaining input.</param>
        /// <param name="offset">The pointer to the remaining input.</param>
        /// <param name="length">The remaining length, modulo 32.</param>
        /// <param name="align">Whether @p ptr is aligned.</param>
        /// <returns>The finalized hash</returns>
        private static ulong Finalize(ulong hash, byte[] data, int offset, int length)
        {
            length &= 31;
            while (length >= 8)
            {
                ulong k1 = Round(0, ReadLE64(data, offset));
                offset += 8;
                hash ^= k1;
                hash = (RotateLeft64(hash, 27) * XXH_PRIME64_1) + XXH_PRIME64_4;
                length -= 8;
            }

            if (length >= 4)
            {
                hash ^= ReadLE32(data, offset) * XXH_PRIME64_1;
                offset += 4;
                hash = (RotateLeft64(hash, 23) * XXH_PRIME64_2) + XXH_PRIME64_3;
                length -= 4;
            }

            while (length > 0)
            {
                hash ^= data[offset++] * XXH_PRIME64_5;
                hash = RotateLeft64(hash, 11) * XXH_PRIME64_1;
                --length;
            }

            return Avalanche(hash);
        }

        /// <summary>
        /// Mixes all bits to finalize the hash.
        ///
        /// The final mix ensures that all input bits have a chance to impact any bit in
        /// the output digest, resulting in an unbiased distribution.
        /// </summary>
        private static ulong Avalanche(ulong hash)
        {
            hash ^= hash >> 33;
            hash *= XXH_PRIME64_2;
            hash ^= hash >> 29;
            hash *= XXH_PRIME64_3;
            hash ^= hash >> 32;
            return hash;
        }
    }
}
