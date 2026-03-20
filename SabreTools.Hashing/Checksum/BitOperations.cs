namespace SabreTools.Hashing.Checksum
{
    internal static class BitOperations
    {
        /// <summary>
        /// Clamp a value to a certain bit width and convert to a byte array
        /// </summary>
#if NET7_0_OR_GREATER
        public static byte[] ClampValueToBytes(System.UInt128 value, int bitWidth)
#else
        public static byte[] ClampValueToBytes(ulong value, int bitWidth)
#endif
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
