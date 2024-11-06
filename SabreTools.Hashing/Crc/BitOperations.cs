namespace SabreTools.Hashing.Crc
{
    internal static class BitOperations
    {
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

        /// <summary>
        /// Clamp a value to a certain bit width and convert to a byte array
        /// </summary>
        public static byte[] ClampValueToBytes(ulong value, int bitWidth)
        {
            value &= ulong.MaxValue >> (64 - bitWidth);
            byte[] bytes = new byte[(bitWidth + 7) / 8];

            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)value;
                value >>= 8;
            }

            return bytes;
        }
    }
}