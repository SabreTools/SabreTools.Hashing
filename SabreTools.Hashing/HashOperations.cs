using System;

namespace SabreTools.Hashing
{
    internal static class HashOperations
    {
        #region Conversions

        /// <summary>
        /// Convert a byte array to a hex string
        /// </summary>
        /// <param name="bytes">Byte array to convert</param>
        /// <returns>Hex string representing the byte array</returns>
        /// <link>http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa</link>
        public static string? ByteArrayToString(byte[]? bytes)
        {
            // If we get null in, we send null out
            if (bytes is null)
                return null;

            try
            {
                string hex = BitConverter.ToString(bytes);
                return hex.Replace("-", string.Empty).ToLowerInvariant();
            }
            catch
            {
                return null;
            }
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Convert a byte array to a UInt64
        /// </summary>
        /// <param name="bytes">Byte array to convert</param>
        /// <returns>UInt64 representing the byte array</returns>
        /// <link>https://stackoverflow.com/questions/66750224/how-to-convert-a-byte-array-of-any-size-to-ulong-in-c</link>
        public static UInt128 BytesToUInt128(byte[]? bytes)
        {
            // If we get null in, we send 0 out
            if (bytes is null)
                return default;

            UInt128 result = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                result |= (UInt128)bytes[i] << (i * 8);
            }

            return result;
        }
#else
        /// <summary>
        /// Convert a byte array to a UInt64
        /// </summary>
        /// <param name="bytes">Byte array to convert</param>
        /// <returns>UInt64 representing the byte array</returns>
        /// <link>https://stackoverflow.com/questions/66750224/how-to-convert-a-byte-array-of-any-size-to-ulong-in-c</link>
        public static ulong BytesToUInt64(byte[]? bytes)
        {
            // If we get null in, we send 0 out
            if (bytes is null)
                return default;

            ulong result = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                result |= (ulong)bytes[i] << (i * 8);
            }

            return result;
        }
#endif

        #endregion

        #region Read Litte-Endian

        /// <summary>
        /// Convert a byte array at an offset to a UInt32
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static uint ToUInt32LittleEndian(byte[] value, int offset)
        {
            return (uint)(value[offset + 0]
                       | (value[offset + 1] << 8)
                       | (value[offset + 2] << 16)
                       | (value[offset + 3] << 24));
        }

        /// <summary>
        /// Convert a byte array at an offset to a UInt64
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        public static ulong ToUInt64LittleEndian(byte[] value, int offset)
        {
            return value[offset + 0]
                | ((ulong)value[offset + 1] << 8)
                | ((ulong)value[offset + 2] << 16)
                | ((ulong)value[offset + 3] << 24)
                | ((ulong)value[offset + 4] << 32)
                | ((ulong)value[offset + 5] << 40)
                | ((ulong)value[offset + 6] << 48)
                | ((ulong)value[offset + 7] << 56);
        }

        #endregion

        #region Reverse

        /// <summary>
        /// Reverse the endianness of a value
        /// </summary>
#if NET7_0_OR_GREATER
        public static UInt128 ReverseBits(UInt128 value, int bitWidth)
#else
        public static ulong ReverseBits(ulong value, int bitWidth)
#endif
        {
#if NET7_0_OR_GREATER
            UInt128 reverse = 0;
#else
            ulong reverse = 0;
#endif
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
