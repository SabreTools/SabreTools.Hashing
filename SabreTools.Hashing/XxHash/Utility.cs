using static SabreTools.Hashing.HashOperations;
using static SabreTools.Hashing.XxHash.Constants;

namespace SabreTools.Hashing.XxHash
{
    internal static class Utility
    {
        #region Multiply and Fold

        /// <summary>
        /// Calculates a 64-bit to 128-bit multiply, then XOR folds it.
        /// </summary>
        public static ulong MultiplyTo128Fold64(ulong lhs, ulong rhs)
        {
            var product = MultiplyTo128(lhs, rhs);
            return product.Low ^ product.High;
        }

        /// <summary>
        /// Calculates a 32-bit to 64-bit long multiply.
        /// </summary>
        public static ulong MultiplyTo64(ulong x, ulong y)
        {
            return (x & 0xFFFFFFFF) * (y & 0xFFFFFFFF);
        }

        /// <summary>
        /// Calculates a 64->128-bit long multiply.
        /// </summary>
        public static XXH3_128Hash MultiplyTo128(ulong lhs, ulong rhs)
        {
            // First calculate all of the cross products.
            ulong lo_lo = MultiplyTo64(lhs & 0xFFFFFFFF, rhs & 0xFFFFFFFF);
            ulong hi_lo = MultiplyTo64(lhs >> 32, rhs & 0xFFFFFFFF);
            ulong lo_hi = MultiplyTo64(lhs & 0xFFFFFFFF, rhs >> 32);
            ulong hi_hi = MultiplyTo64(lhs >> 32, rhs >> 32);

            // Now add the products together. These will never overflow.
            ulong cross = (lo_lo >> 32) + (hi_lo & 0xFFFFFFFF) + lo_hi;
            ulong upper = (hi_lo >> 32) + (cross >> 32) + hi_hi;
            ulong lower = (cross << 32) | (lo_lo & 0xFFFFFFFF);

            return new XXH3_128Hash
            {
                Low = lower,
                High = upper,
            };
        }

        #endregion

        #region Shift

        public static ulong XorShift64(ulong v64, int shift)
        {
            return v64 ^ (v64 >> shift);
        }

        #endregion

        #region XXH64 Common

        /// <summary>
        /// Mixes all bits to finalize the hash.
        /// 
        /// The final mix ensures that all input bits have a chance to impact any bit in
        /// the output digest, resulting in an unbiased distribution.
        /// </summary>
        public static ulong XXH64Avalanche(ulong hash)
        {
            hash ^= hash >> 33;
            hash *= XXH_PRIME64_2;
            hash ^= hash >> 29;
            hash *= XXH_PRIME64_3;
            hash ^= hash >> 32;
            return hash;
        }

        #endregion

        #region XXH3 Common

        /// <summary>
        /// This is a fast avalanche stage,
        /// suitable when input bits are already partially mixed
        /// </summary>
        public static ulong XXH3Avalanche(ulong hash)
        {
            hash = XorShift64(hash, 37);
            hash *= PRIME_MX1;
            hash = XorShift64(hash, 32);
            return hash;
        }

        /// <summary>
        /// This is a stronger avalanche,
        /// inspired by Pelle Evensen's rrmxmx
        /// preferable when input has not been previously mixed
        /// </summary>
        public static ulong XXH3Rrmxmx(ulong hash, ulong length)
        {
            // This mix is inspired by Pelle Evensen's rrmxmx
            hash ^= RotateLeft64(hash, 49) ^ RotateLeft64(hash, 24);
            hash *= PRIME_MX2;
            hash ^= (hash >> 35) + length;
            hash *= PRIME_MX2;
            return XorShift64(hash, 28);
        }

        /// <summary>
        /// Handle length 1 to 3 values
        /// </summary>
        public static ulong Len1To3Out64(byte[] data, int offset, int length, byte[] secret, ulong seed)
        {
            byte c1 = data[offset + 0];
            byte c2 = data[offset + (length >> 1)];
            byte c3 = data[offset + (length - 1)];

            uint combined = ((uint)c1 << 16)
                          | ((uint)c2 << 24)
                          | ((uint)c3 << 0)
                          | ((uint)length << 8);
            ulong bitflip = (ReadLE32(secret, 0) ^ ReadLE32(secret, 4)) + seed;
            ulong keyed = combined ^ bitflip;

            return XXH64Avalanche(keyed);
        }

        /// <summary>
        /// Handle length 4 to 8 values
        /// </summary>
        public static ulong Len4To8Out64(byte[] data, int offset, int length, byte[] secret, ulong seed)
        {
            seed ^= (ulong)Swap32((uint)seed) << 32;

            uint input1 = ReadLE32(data, offset);
            uint input2 = ReadLE32(data, offset + length - 4);
            ulong bitflip = (ReadLE64(secret, 8) ^ ReadLE64(secret, 16)) - seed;
            ulong input64 = input2 + (((ulong)input1) << 32);
            ulong keyed = input64 ^ bitflip;

            return XXH3Rrmxmx(keyed, (ulong)length);
        }

        /// <summary>
        /// Handle length 9 to 16 values
        /// </summary>
        public static ulong Len9To16Out64(byte[] data, int offset, int length, byte[] secret, ulong seed)
        {
            ulong bitflip1 = (ReadLE64(secret, 24) ^ ReadLE64(secret, 32)) + seed;
            ulong bitflip2 = (ReadLE64(secret, 40) ^ ReadLE64(secret, 48)) - seed;
            ulong input_lo = ReadLE64(data, offset) ^ bitflip1;
            ulong input_hi = ReadLE64(data, offset + length - 8) ^ bitflip2;

            ulong acc = (ulong)length
                      + Swap64(input_lo) + input_hi
                      + MultiplyTo128Fold64(input_lo, input_hi);

            return XXH3Avalanche(acc);
        }

        /// <summary>
        /// Handle length 0 to 16 values
        /// </summary>
        public static ulong Len0To16Out64(byte[] data, int offset, int length, byte[] secret, ulong seed)
        {
            if (length > 8)
                return Len9To16Out64(data, offset, length, secret, seed);
            if (length >= 4)
                return Len4To8Out64(data, offset, length, secret, seed);
            if (length > 0)
                return Len1To3Out64(data, offset, length, secret, seed);

            return XXH64Avalanche(seed ^ (ReadLE64(secret, 56) ^ ReadLE64(secret, 64)));
        }

        public static ulong Mix16B(byte[] data, int offset, byte[] secret, int secretOffset, ulong seed)
        {
            ulong input_lo = ReadLE64(data, offset + 0);
            ulong input_hi = ReadLE64(data, offset + 8);

            return MultiplyTo128Fold64(
                input_lo ^ (ReadLE64(secret, secretOffset + 0) + seed),
                input_hi ^ (ReadLE64(secret, secretOffset + 8) - seed)
            );
        }

        /// <summary>
        /// Handle length 7 to 128 values
        /// </summary>
        public static ulong Len17To128Out64(byte[] data, int offset, int length, byte[] secret, ulong seed)
        {
            ulong acc = (ulong)length * XXH_PRIME64_1;

            if (length > 32)
            {
                if (length > 64)
                {
                    if (length > 96)
                    {
                        acc += Mix16B(data, offset + 48, secret, 96, seed);
                        acc += Mix16B(data, offset + length - 64, secret, 112, seed);
                    }

                    acc += Mix16B(data, offset + 32, secret, 64, seed);
                    acc += Mix16B(data, offset + length - 48, secret, 80, seed);
                }

                acc += Mix16B(data, offset + 16, secret, 32, seed);
                acc += Mix16B(data, offset + length - 32, secret, 48, seed);
            }

            acc += Mix16B(data, offset + 0, secret, 0, seed);
            acc += Mix16B(data, offset + length - 16, secret, 16, seed);

            return XXH3Avalanche(acc);
        }

        /// <summary>
        /// Handle length 129 to 240 values
        /// </summary>
        public static ulong Len129To240Out64(byte[] data, int offset, int length, byte[] secret, ulong seed)
        {
            const int XXH3_MIDSIZE_STARTOFFSET = 3;
            const int XXH3_MIDSIZE_LASTOFFSET = 17;

            ulong acc = (ulong)length * XXH_PRIME64_1;
            ulong acc_end;
            uint nbRounds = (uint)length / 16;

            uint i;
            for (i = 0; i < 8; i++)
            {
                acc += Mix16B(data, offset + (int)(16 * i), secret, (int)(16 * i), seed);
            }

            // Last bytes
            acc_end = Mix16B(data, offset + length - 16, secret, XXH3_SECRET_SIZE_MIN - XXH3_MIDSIZE_LASTOFFSET, seed);
            acc = XXH3Avalanche(acc);

            for (i = 8; i < nbRounds; i++)
            {
                acc_end += Mix16B(data, offset + (int)(16 * i), secret, (int)(16 * (i - 8)) + XXH3_MIDSIZE_STARTOFFSET, seed);
            }

            return XXH3Avalanche(acc + acc_end);
        }

        #endregion
    }
}