namespace SabreTools.Hashing
{
    /// <summary>
    /// Available hashing and checksumming types
    /// </summary>
    public enum HashType
    {
        /// <summary>
        /// Mark Adler's 32-bit checksum
        /// </summary>
        Adler32,

#if NET7_0_OR_GREATER
        /// <summary>
        /// BLAKE3 512-bit digest
        /// </summary>
        BLAKE3,
#endif

        // <summary>
        /// CRC 16-bit checksum using the CCITT polynomial
        /// </summary>
        CRC16_CCITT,

        // <summary>
        /// CRC 16-bit checksum using the IBM polynomial
        /// </summary>
        CRC16_IBM,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/ISO-HDLC, reversed output)
        /// </summary>
        CRC32,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/ISO-HDLC)
        /// </summary>
        CRC32_ISOHDLC,

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/ECMA-182, Microsoft implementation)
        /// </summary>
        CRC64_ECMA182,

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/GO-ISO)
        /// </summary>
        CRC64_GOISO,

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/XZ)
        /// </summary>
        CRC64_XZ,

        /// <summary>
        /// John G. Fletcher's 16-bit checksum
        /// </summary>
        Fletcher16,

        /// <summary>
        /// John G. Fletcher's 32-bit checksum
        /// </summary>
        Fletcher32,

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

#if NET8_0_OR_GREATER
        /// <summary>
        /// SHA3-256 hash
        /// </summary>
        SHA3_256,

        /// <summary>
        /// SHA3-384 hash
        /// </summary>
        SHA3_384,

        /// <summary>
        /// SHA3-512 hash
        /// </summary>
        SHA3_512, 

        /// <summary>
        /// SHAKE128 SHA-3 family hash
        /// </summary>
        /// <remarks>Outputs a 256-bit (32-byte) hash</remarks>
        SHAKE128,

        /// <summary>
        /// SHAKE256 SHA-3 family hash
        /// </summary>
        /// <remarks>Outputs a 512-bit (64-byte) hash</remarks>
        SHAKE256,
#endif

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