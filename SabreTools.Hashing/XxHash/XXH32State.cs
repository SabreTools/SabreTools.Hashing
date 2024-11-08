using System;
using static SabreTools.Hashing.XxHash.Constants;
using static SabreTools.Hashing.XxHash.Utility;

namespace SabreTools.Hashing.XxHash
{
    /// <summary>
    /// Structure for XXH32 streaming API.
    /// </summary>
    /// <see href="https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h"/> 
    internal class XXH32State
    {
        /// <summary>
        /// Total length hashed, modulo 2^32
        /// </summary>
        public uint TotalLength { get; set; }

        /// <summary>
        /// Whether the hash is >= 16 (handles <see cref="TotalLength"/> overflow)
        /// </summary>
        public uint LargeLength { get; set; }

        /// <summary>
        /// Accumulator lanes
        /// </summary>
        public uint[] V { get; } = new uint[4];

        /// <summary>
        /// Internal buffer for partial reads. Treated as unsigned char[16].
        /// </summary>
        public byte[] Memory { get; } = new byte[16];

        /// <summary>
        /// Amount of data in <see cref="Memory">
        /// </summary>
        public int Memsize { get; set; }

        /// <summary>
        /// Reserved field. Do not read nor write to it.
        /// </summary>
        public uint Reserved { get; set; }

        /// <summary>
        /// Resets to begin a new hash
        /// </summary>
        /// <param name="seed">The 32-bit seed to alter the hash result predictably.</param>
        public void Reset(uint seed)
        {
            TotalLength = default;
            LargeLength = default;

            V[0] = seed + XXH_PRIME32_1 + XXH_PRIME32_2;
            V[1] = seed + XXH_PRIME32_2;
            V[2] = seed + 0;
            V[3] = seed - XXH_PRIME32_1;

            for (int i = 0; i < Memory.Length; i++)
            {
                Memory[i] = default;
            }

            Memsize = default;
            Reserved = default;
        }

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
        {
            int bEnd = offset + length;

            TotalLength += (uint)length;
            LargeLength |= (uint)((length >= 16) | (TotalLength >= 16) ? 1 : 0);

            if (Memsize + length < 16)
            {
                // Fill in tmp buffer
                Array.Copy(data, offset, Memory, Memsize, length);
                Memsize += length;
                return;
            }

            if (Memsize > 0)
            {
                // Some data left from previous update
                Array.Copy(data, offset, Memory, Memsize, 16 - Memsize);

                int p32 = 0;
                V[0] = Round(V[0], ReadLE32(Memory, p32)); p32++;
                V[1] = Round(V[1], ReadLE32(Memory, p32)); p32++;
                V[2] = Round(V[2], ReadLE32(Memory, p32)); p32++;
                V[3] = Round(V[3], ReadLE32(Memory, p32));

                offset += 16 - Memsize;
                Memsize = 0;
            }

            if (offset <= bEnd - 16)
            {
                int limit = bEnd - 16;

                do
                {
                    V[0] = Round(V[0], ReadLE32(data, offset)); offset += 4;
                    V[1] = Round(V[1], ReadLE32(data, offset)); offset += 4;
                    V[2] = Round(V[2], ReadLE32(data, offset)); offset += 4;
                    V[3] = Round(V[3], ReadLE32(data, offset)); offset += 4;
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
        /// <returns>The calculated 32-bit xxHash32 value from that state.</returns>
        public uint Digest()
        {
            uint h32;

            if (LargeLength > 0)
            {
                h32 = XXH_rotl32(V[0], 1)
                    + XXH_rotl32(V[1], 7)
                    + XXH_rotl32(V[2], 12)
                    + XXH_rotl32(V[3], 18);
            }
            else
            {
                h32 = V[2] /* == seed */ + XXH_PRIME32_5;
            }

            h32 += TotalLength;

            return Finalize(h32, Memory, 0, Memsize, Alignment.XXH_aligned);
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
        private static uint Round(uint acc, uint input)
        {
            acc += input * XXH_PRIME32_2;
            acc = XXH_rotl32(acc, 13);
            acc *= XXH_PRIME32_1;
            return acc;
        }

        /// <summary>
        /// Mixes all bits to finalize the hash.
        /// 
        /// The final mix ensures that all input bits have a chance to impact any bit in
        /// the output digest, resulting in an unbiased distribution.
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
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
        /// <param name="data">The pointer to the remaining input.</param>
        /// <param name="offset">The pointer to the remaining input.</param>
        /// <param name="length">The remaining length, modulo 16.</param>
        /// <param name="align">Whether @p ptr is aligned.</param>
        /// <returns>The finalized hash.</returns>
        private static uint Finalize(uint hash, byte[] data, int offset, int length, Alignment align)
        {
            length &= 15;
            while (length >= 4)
            {
                XXH_PROCESS4(ref hash, data, ref offset, align);
                length -= 4;
            }

            while (length > 0)
            {
                XXH_PROCESS1(ref hash, data, ref offset);
                --length;
            }

            return Avalanche(hash);
        }

        private static void XXH_PROCESS1(ref uint hash, byte[] data, ref int offset)
        {
            hash += data[offset++] * XXH_PRIME32_5;
            hash = XXH_rotl32(hash, 11) * XXH_PRIME32_1;
        }

        private static void XXH_PROCESS4(ref uint hash, byte[] data, ref int offset, Alignment align)
        {
            hash += XXH_get32bits(data, offset, align) * XXH_PRIME32_3;
            offset += 4;
            hash = XXH_rotl32(hash, 17) * XXH_PRIME32_4;
        }

        /// <summary>
        /// The implementation for XXH32
        /// </summary>
        /// <returns>The calculated hash.</returns>
        private static uint EndianAlign(byte[] data, int offset, int length, uint seed, Alignment align)
        {
            uint h32;

            if (length >= 16)
            {
                int bEnd = offset + length;
                int limit = bEnd - 15;
                uint v1 = seed + XXH_PRIME32_1 + XXH_PRIME32_2;
                uint v2 = seed + XXH_PRIME32_2;
                uint v3 = seed + 0;
                uint v4 = seed - XXH_PRIME32_1;

                do
                {
                    v1 = Round(v1, XXH_get32bits(data, offset, align)); offset += 4;
                    v2 = Round(v2, XXH_get32bits(data, offset, align)); offset += 4;
                    v3 = Round(v3, XXH_get32bits(data, offset, align)); offset += 4;
                    v4 = Round(v4, XXH_get32bits(data, offset, align)); offset += 4;
                } while (offset < limit);

                h32 = XXH_rotl32(v1, 1) + XXH_rotl32(v2, 7)
                    + XXH_rotl32(v3, 12) + XXH_rotl32(v4, 18);
            }
            else
            {
                h32 = seed + XXH_PRIME32_5;
            }

            h32 += (uint)length;

            return Finalize(h32, data, offset, length & 15, align);
        }
    }
}