namespace SabreTools.Hashing.XxHash
{
    internal static class Utility
    {
        /// <summary>
        /// 32-bit rotate left.
        /// </summary>
        public static uint XXH_rotl32(uint x, int r)
            => (x << r) | (x >> (32 - r));

        /// <summary>
        /// 64-bit rotate left.
        /// </summary>
        public static ulong XXH_rotl64(ulong x, int r)
            => (x << r) | (x >> (64 - r));

        /// <summary>
        /// A 32-bit byteswap.
        /// </summary>
        public static uint Swap32(uint x)
        {
            return ((x << 24) & 0xff000000)
                 | ((x << 8 ) & 0x00ff0000)
                 | ((x >> 8 ) & 0x0000ff00)
                 | ((x >> 24) & 0x000000ff);
        }

        public static uint ReadLE32(byte[] data, int offset)
        {
            return (uint)data[offset + 0]
                       | data[offset + 1]
                       | data[offset + 2]
                       | data[offset + 3];
        }

        public static uint ReadBE32(byte[] data, int offset)
        {
            return (uint)data[offset + 3]
                       | data[offset + 2]
                       | data[offset + 1]
                       | data[offset + 0];
        }

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
    
        public static uint XXH_get32bits(byte[] data, int offset, Alignment align)
            => ReadLE32Align(data, offset, align);
    }
}