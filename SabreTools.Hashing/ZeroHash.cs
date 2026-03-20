namespace SabreTools.Hashing
{
    /// <summary>
    /// Zero-byte / empty hash
    /// </summary>
    public static class ZeroHash
    {
        #region Shortcuts for Common Hash Arrays

        /// <summary>
        /// Zero-byte CRC-32 checksum
        /// </summary>
        public static byte[] CRC32Arr => HashType.CRC32.ZeroBytes;

        /// <summary>
        /// Zero-byte MD5 hash
        /// </summary>
        public static byte[] MD5Arr => HashType.MD5.ZeroBytes;

        /// <summary>
        /// Zero-byte SHA-1 hash
        /// </summary>
        public static byte[] SHA1Arr => HashType.SHA1.ZeroBytes;

        /// <summary>
        /// Zero-byte SHA-256 hash
        /// </summary>
        public static byte[] SHA256Arr => HashType.SHA256.ZeroBytes;

        /// <summary>
        /// Zero-byte SHA-384 hash
        /// </summary>
        public static byte[] SHA384Arr => HashType.SHA384.ZeroBytes;

        /// <summary>
        /// Zero-byte SHA-512 hash
        /// </summary>
        public static byte[] SHA512Arr => HashType.SHA512.ZeroBytes;

        /// <summary>
        /// Zero-byte SpamSum fuzzy hash
        /// </summary>
        public static byte[] SpamSumArr => HashType.SpamSum.ZeroBytes;

        #endregion

        #region Shortcuts for Common Hash Strings

        /// <summary>
        /// Zero-byte CRC-32 checksum
        /// </summary>
        public static string CRC32Str => HashType.CRC32.ZeroString;

        /// <summary>
        /// Zero-byte MD5 hash
        /// </summary>
        public static string MD5Str => HashType.MD5.ZeroString;

        /// <summary>
        /// Zero-byte SHA-1 hash
        /// </summary>
        public static string SHA1Str => HashType.SHA1.ZeroString;

        /// <summary>
        /// Zero-byte SHA-256 hash
        /// </summary>
        public static string SHA256Str => HashType.SHA256.ZeroString;

        /// <summary>
        /// Zero-byte SHA-384 hash
        /// </summary>
        public static string SHA384Str => HashType.SHA384.ZeroString;

        /// <summary>
        /// Zero-byte SHA-512 hash
        /// </summary>
        public static string SHA512Str => HashType.SHA512.ZeroString;

        /// <summary>
        /// Zero-byte SpamSum fuzzy hash
        /// </summary>
        public static string SpamSumStr => HashType.SpamSum.ZeroString;

        #endregion
    }
}
