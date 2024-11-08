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
            ulong hi_lo = MultiplyTo64(lhs >> 32,        rhs & 0xFFFFFFFF);
            ulong lo_hi = MultiplyTo64(lhs & 0xFFFFFFFF, rhs >> 32);
            ulong hi_hi = MultiplyTo64(lhs >> 32,        rhs >> 32);

            // Now add the products together. These will never overflow.
            ulong cross = (lo_lo >> 32) + (hi_lo & 0xFFFFFFFF) + lo_hi;
            ulong upper = (hi_lo >> 32) + (cross >> 32)        + hi_hi;
            ulong lower = (cross << 32) | (lo_lo & 0xFFFFFFFF);

            return new XXH3_128Hash
            {
                Low = lower,
                High = upper,
            };
        }

        #endregion

        #region Read Big-Endian

        /// <summary>
        /// 32-bit big-endian read
        /// </summary>
        public static uint ReadBE32(byte[] data, int offset)
        {
            return (uint)(data[offset + 3]
                        | data[offset + 2] << 8
                        | data[offset + 1] << 16
                        | data[offset + 0] << 24);
        }

        /// <summary>
        /// 64-bit big-endian read
        /// </summary>
        public static ulong ReadBE64(byte[] data, int offset)
        {
            return data[offset + 7]
          | (ulong)data[offset + 6] << 8
          | (ulong)data[offset + 5] << 16
          | (ulong)data[offset + 4] << 24
          | (ulong)data[offset + 3] << 32
          | (ulong)data[offset + 2] << 40
          | (ulong)data[offset + 1] << 48
          | (ulong)data[offset + 0] << 56;
        }

        #endregion

        #region Read Litte-Endian

        /// <summary>
        /// 32-bit little-endian read
        /// </summary>
        public static uint ReadLE32(byte[] data, int offset)
        {
            return (uint)(data[offset + 0]
                        | data[offset + 1] << 8
                        | data[offset + 2] << 16
                        | data[offset + 3] << 24);
        }

        /// <summary>
        /// 32-bit little-endian read with alignment
        /// </summary>
        public static uint ReadLE32Align(byte[] data, int offset, Alignment align)
        {
            if (align == Alignment.XXH_aligned)
                return ReadLE32(data, offset);

            uint value = 0;
            if (offset + 0 < data.Length)
                value |= data[offset + 0];
            if (offset + 1 < data.Length)
                value |= data[offset + 1];
            if (offset + 2 < data.Length)
                value |= data[offset + 2];
            if (offset + 3 < data.Length)
                value |= data[offset + 3];

            return value;
        }

        /// <summary>
        /// 64-bit little-endian read
        /// </summary>
        public static ulong ReadLE64(byte[] data, int offset)
        {
            return data[offset + 0]
          | (ulong)data[offset + 1] << 8
          | (ulong)data[offset + 2] << 16
          | (ulong)data[offset + 3] << 24
          | (ulong)data[offset + 4] << 32
          | (ulong)data[offset + 5] << 40
          | (ulong)data[offset + 6] << 48
          | (ulong)data[offset + 7] << 56;
        }

        /// <summary>
        /// 64-bit little-endian read with alignment
        /// </summary>
        public static ulong ReadLE64Align(byte[] data, int offset, Alignment align)
        {
            if (align == Alignment.XXH_aligned)
                return ReadLE64(data, offset);

            ulong value = 0;
            if (offset + 0 < data.Length)
                value |= data[offset + 0];
            if (offset + 1 < data.Length)
                value |= data[offset + 1];
            if (offset + 2 < data.Length)
                value |= data[offset + 2];
            if (offset + 3 < data.Length)
                value |= data[offset + 3];
            if (offset + 4 < data.Length)
                value |= data[offset + 4];
            if (offset + 5 < data.Length)
                value |= data[offset + 5];
            if (offset + 6 < data.Length)
                value |= data[offset + 6];
            if (offset + 7 < data.Length)
                value |= data[offset + 7];

            return value;
        }

        #endregion

        #region Rotate

        /// <summary>
        /// 32-bit rotate left.
        /// </summary>
        public static uint RotateLeft32(uint x, int r)
            => (x << r) | (x >> (32 - r));

        /// <summary>
        /// 64-bit rotate left.
        /// </summary>
        public static ulong RotateLeft64(ulong x, int r)
            => (x << r) | (x >> (64 - r));

        #endregion

        #region Shift

        public static ulong XorShift64(ulong v64, int shift)
        {
            return v64 ^ (v64 >> shift);
        }

        #endregion

        #region Swap

        /// <summary>
        /// A 32-bit byteswap.
        /// </summary>
        public static uint Swap32(uint x)
        {
            return ((x << 24) & 0xff000000)
                 | ((x << 8) & 0x00ff0000)
                 | ((x >> 8) & 0x0000ff00)
                 | ((x >> 24) & 0x000000ff);
        }

        /// <summary>
        /// A 64-bit byteswap.
        /// </summary>
        public static ulong Swap64(ulong x)
        {
            return ((x << 56) & 0xff00000000000000)
                 | ((x << 40) & 0x00ff000000000000)
                 | ((x << 24) & 0x0000ff0000000000)
                 | ((x << 8) & 0x000000ff00000000)
                 | ((x >> 8) & 0x00000000ff000000)
                 | ((x >> 24) & 0x0000000000ff0000)
                 | ((x >> 40) & 0x000000000000ff00)
                 | ((x >> 56) & 0x00000000000000ff);
        }

        #endregion
    }
}