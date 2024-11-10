namespace SabreTools.Hashing
{
    internal static class HashOperations
    {
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

        #endregion

        #region Reverse

        /// <summary>
        /// Reverse the endianness of a value
        /// </summary>
        public static ulong ReverseBits(ulong value, int bitWidth)
        {
            ulong reverse = 0;
            for (int i = 0; i < bitWidth; i++)
            {
                reverse <<= 1;
                reverse |= value & 1;
                value >>= 1;
            }

            return reverse;
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