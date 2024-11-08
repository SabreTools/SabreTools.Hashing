using System;
using static SabreTools.Hashing.XxHash.Constants;
using static SabreTools.Hashing.XxHash.Utility;

namespace SabreTools.Hashing.XxHash
{
    /// <summary>
    /// Structure for XXH64 streaming API.
    /// </summary>
    /// <see href="https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h"/> 
    internal class XXH64State
    {
        /// <summary>
        /// Total length hashed. This is always 64-bit.
        /// </summary>
        public ulong TotalLength { get; set; }

        /// <summary>
        /// Accumulator lanes
        /// </summary>
        public ulong[] V { get; } = new ulong[4];

        /// <summary>
        /// Internal buffer for partial reads. Treated as unsigned char[16].
        /// </summary>
        public byte[] Memory { get; } = new byte[16];

        /// <summary>
        /// Amount of data in <see cref="Memory">
        /// </summary>
        public int Memsize { get; set; }

        /// <summary>
        /// Reserved field, needed for padding anyways
        /// </summary>
        public uint Reserved32 { get; set; }

        /// <summary>
        /// Reserved field. Do not read nor write to it.
        /// </summary>
        public ulong Reserved64 { get; set; }

        /// <summary>
        /// Resets to begin a new hash
        /// </summary>
        /// <param name="seed">The 64-bit seed to alter the hash result predictably.</param>
        public void Reset(ulong seed)
        {
            TotalLength = default;

            V[0] = seed + XXH_PRIME64_1 + XXH_PRIME64_2;
            V[1] = seed + XXH_PRIME64_2;
            V[2] = seed + 0;
            V[3] = seed - XXH_PRIME64_1;

            for (int i = 0; i < Memory.Length; i++)
            {
                Memory[i] = default;
            }

            Memsize = default;
            Reserved32 = default;
            Reserved64 = default;
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

            TotalLength += (ulong)length;

            if (Memsize + length < 32)
            {
                // Fill in tmp buffer
                Array.Copy(data, offset, Memory, Memsize, length);
                Memsize += length;
                return;
            }

            if (Memsize > 0)
            {
                // tmp buffer is full
                Array.Copy(data, offset, Memory, Memsize, 32 - Memsize);
                V[0] = Round(V[0], ReadLE64(Memory, 0));
                V[1] = Round(V[1], ReadLE64(Memory, 1));
                V[2] = Round(V[2], ReadLE64(Memory, 2));
                V[3] = Round(V[3], ReadLE64(Memory, 3));
                offset += 32 - Memsize;
                Memsize = 0;
            }

            if (offset + 32 <= bEnd)
            {
                int limit = bEnd - 32;

                do
                {
                    V[0] = Round(V[0], ReadLE64(data, offset)); offset += 8;
                    V[1] = Round(V[1], ReadLE64(data, offset)); offset += 8;
                    V[2] = Round(V[2], ReadLE64(data, offset)); offset += 8;
                    V[3] = Round(V[3], ReadLE64(data, offset)); offset += 8;
                } while (offset <= limit);
            }

            if (offset < bEnd)
            {
                Array.Copy(data, offset, Memory, 0, bEnd - offset);
                Memsize = bEnd - offset;
            }
        }

        /// <summary>
        /// Returns the calculated hash value
        /// </summary>
        /// <returns>The calculated 64-bit xxHash64 value from that state.</returns>
        public ulong Digest()
        {
            ulong h64;

            if (TotalLength >= 32)
            {
                h64 = XXH_rotl64(V[0], 1) + XXH_rotl64(V[1], 7) + XXH_rotl64(V[2], 12) + XXH_rotl64(V[3], 18);
                h64 = MergeRound(h64, V[0]);
                h64 = MergeRound(h64, V[1]);
                h64 = MergeRound(h64, V[2]);
                h64 = MergeRound(h64, V[3]);
            }
            else
            {
                h64 = V[2] /*seed*/ + XXH_PRIME64_5;
            }

            h64 += TotalLength;

            return Finalize(h64, Memory, 0, (int)TotalLength, Alignment.XXH_aligned);
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
            acc += input * XXH_PRIME64_2;
            acc = XXH_rotl64(acc, 31);
            acc *= XXH_PRIME64_1;
            return acc;
        }

        private static ulong MergeRound(ulong acc, ulong val)
        {
            val = Round(0, val);
            acc ^= val;
            acc = acc * XXH_PRIME64_1 + XXH_PRIME64_4;
            return acc;
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
        private static ulong Finalize(ulong hash, byte[] data, int offset, int length, Alignment align)
        {
            length &= 31;
            while (length >= 8)
            {
                ulong k1 = Round(0, XXH_get64bits(data, offset, align));
                offset += 8;
                hash ^= k1;
                hash = XXH_rotl64(hash, 27) * XXH_PRIME64_1 + XXH_PRIME64_4;
                length -= 8;
            }
            if (length >= 4)
            {
                hash ^= (ulong)(XXH_get32bits(data, offset, align)) * XXH_PRIME64_1;
                offset += 4;
                hash = XXH_rotl64(hash, 23) * XXH_PRIME64_2 + XXH_PRIME64_3;
                length -= 4;
            }
            while (length > 0)
            {
                hash ^= data[offset++] * XXH_PRIME64_5;
                hash = XXH_rotl64(hash, 11) * XXH_PRIME64_1;
                --length;
            }
            return Avalanche(hash);
        }

        /// <summary>
        /// The implementation for XXH64
        /// </summary>
        /// <returns>The calculated hash.</returns>
        private static ulong EndianAlign(byte[] data, int offset, int length, ulong seed, Alignment align)
        {
            ulong h64;
            if (length >= 32)
            {
                int bEnd = offset + length;
                int limit = bEnd - 31;
                ulong v1 = seed + XXH_PRIME64_1 + XXH_PRIME64_2;
                ulong v2 = seed + XXH_PRIME64_2;
                ulong v3 = seed + 0;
                ulong v4 = seed - XXH_PRIME64_1;

                do
                {
                    v1 = Round(v1, XXH_get64bits(data, offset, align)); offset += 8;
                    v2 = Round(v2, XXH_get64bits(data, offset, align)); offset += 8;
                    v3 = Round(v3, XXH_get64bits(data, offset, align)); offset += 8;
                    v4 = Round(v4, XXH_get64bits(data, offset, align)); offset += 8;
                } while (offset < limit);

                h64 = XXH_rotl64(v1, 1) + XXH_rotl64(v2, 7) + XXH_rotl64(v3, 12) + XXH_rotl64(v4, 18);
                h64 = MergeRound(h64, v1);
                h64 = MergeRound(h64, v2);
                h64 = MergeRound(h64, v3);
                h64 = MergeRound(h64, v4);

            }
            else
            {
                h64 = seed + XXH_PRIME64_5;
            }

            h64 += (ulong)length;

            return Finalize(h64, data, offset, length, align);
        }
    }
}