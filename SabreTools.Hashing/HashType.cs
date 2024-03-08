namespace SabreTools.Hashing
{
    /// <summary>
    /// Available hashing and checksumming types
    /// </summary>
    public enum HashType
    {
        /// <summary>
        /// CRC 32-bit checksum
        /// </summary>
        /// <remarks>Same as CRC32_Optimized in .NET Framework 4.5.2 and lower</remarks>
        CRC32,

        /// <summary>
        /// CRC 32-bit checksum (NaiveCRC)
        /// </summary>
        CRC32_Naive,

        /// <summary>
        /// CRC 32-bit checksum (OptimizedCRC)
        /// </summary>
        CRC32_Optimized,

        /// <summary>
        /// CRC 32-bit checksum (ParallelCRC)
        /// </summary>
        CRC32_Parallel,

#if NET462_OR_GREATER || NETCOREAPP
        /// <summary>
        /// CRC 64-bit checksum
        /// </summary>
        CRC64,
#endif

        /// <summary>
        /// MD5 hash
        /// </summary>
        MD5,

#if NETFRAMEWORK
        /// <summary>
        /// RIPEMD160 hash
        /// </summary>
        RIPEMD160,
#endif

        /// <summary>
        /// SHA-1 hash
        /// </summary>
        SHA1,

        /// <summary>
        /// SHA-256 hash
        /// </summary>
        SHA256,

        /// <summary>
        /// SHA-384 hash
        /// </summary>
        SHA384,

        /// <summary>
        /// SHA-512 hash
        /// </summary>
        SHA512,

        /// <summary>
        /// spamsum fuzzy hash
        /// </summary>
        SpamSum,

#if NET462_OR_GREATER || NETCOREAPP
        /// <summary>
        /// xxHash32 hash
        /// </summary>
        XxHash32,

        /// <summary>
        /// xxHash64 hash
        /// </summary>
        XxHash64,

        /// <summary>
        /// XXH3 64-bit hash
        /// </summary>
        XxHash3,

        /// <summary>
        /// XXH128 128-bit hash
        /// </summary>
        XxHash128,
#endif
    }
}