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

        #region CRC-16

        /// <summary>
        /// CRC 16-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC16_ARC"/> 
        CRC16,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/ARC [ARC, CRC-16, CRC-16/LHA, CRC-IBM])
        /// </summary>
        CRC16_ARC,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/CDMA2000)
        /// </summary>
        CRC16_CDMA2000,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/CMS)
        /// </summary>
        CRC16_CMS,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DDS-110)
        /// </summary>
        CRC16_DDS110,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DECT-R [R-CRC-16])
        /// </summary>
        CRC16_DECTR,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DECT-X [X-CRC-16])
        /// </summary>
        CRC16_DECTX,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DNP)
        /// </summary>
        CRC16_DNP,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/EN-13757)
        /// </summary>
        CRC16_EN13757,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/GENIBUS [CRC-16/DARC, CRC-16/EPC, CRC-16/EPC-C1G2, CRC-16/I-CODE])
        /// </summary>
        CRC16_GENIBUS,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/GSM)
        /// </summary>
        CRC16_GSM,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/IBM-3740 [CRC-16/AUTOSAR, CRC-16/CCITT-FALSE])
        /// </summary>
        CRC16_IBM3740,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/IBM-SDLC [CRC-16/ISO-HDLC, CRC-16/ISO-IEC-14443-3-B, CRC-16/X-25, CRC-B, X-25])
        /// </summary>
        CRC16_IBMSDLC,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/ISO-IEC-14443-3-A [CRC-A])
        /// </summary>
        //CRC16_ISOIEC144433A, // Disabled until incorrect hashes can be fixed

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/KERMIT [CRC-16/BLUETOOTH, CRC-16/CCITT, CRC-16/CCITT-TRUE, CRC-16/V-41-LSB, CRC-CCITT, KERMIT])
        /// </summary>
        CRC16_KERMIT,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/LJ1200)
        /// </summary>
        CRC16_LJ1200,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/M17)
        /// </summary>
        CRC16_M17,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MAXIM-DOW [CRC-16/MAXIM])
        /// </summary>
        CRC16_MAXIMDOW,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MCRF4XX)
        /// </summary>
        CRC16_MCRF4XX,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MODBUS [MODBUS])
        /// </summary>
        CRC16_MODBUS,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/NRSC-5)
        /// </summary>
        CRC16_NRSC5,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/OPENSAFETY-A)
        /// </summary>
        CRC16_OPENSAFETYA,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/OPENSAFETY-B)
        /// </summary>
        CRC16_OPENSAFETYB,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/PROFIBUS [CRC-16/IEC-61158-2])
        /// </summary>
        CRC16_PROFIBUS,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/RIELLO)
        /// </summary>
        //CRC16_RIELLO, // Disabled until incorrect hashes can be fixed

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/SPI-FUJITSU [CRC-16/AUG-CCITT])
        /// </summary>
        CRC16_SPIFUJITSU,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/T10-DIF)
        /// </summary>
        CRC16_T10DIF,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/TELEDISK)
        /// </summary>
        CRC16_TELEDISK,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/TMS37157)
        /// </summary>
        //CRC16_TMS37157, // Disabled until incorrect hashes can be fixed

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/UMTS [CRC-16/BUYPASS, CRC-16/VERIFONE])
        /// </summary>
        CRC16_UMTS,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/USB)
        /// </summary>
        CRC16_USB,

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/XMODEM [CRC-16/ACORN, CRC-16/LTE, CRC-16/V-41-MSB, XMODEM, ZMODEM])
        /// </summary>
        CRC16_XMODEM,

        #endregion

        #region CRC-17

        /// <summary>
        /// CRC 17-bit checksum (CRC-17/CAN-FD)
        /// </summary>
        CRC17_CANFD,

        #endregion

        #region CRC-21

        /// <summary>
        /// CRC 21-bit checksum (CRC-21/CAN-FD)
        /// </summary>
        CRC21_CANFD,

        #endregion

        #region CRC-24

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/BLE)
        /// </summary>
        //CRC24_BLE, // Disabled until incorrect hashes can be fixed

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/FLEXRAY-A)
        /// </summary>
        CRC24_FLEXRAYA,

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/FLEXRAY-B)
        /// </summary>
        CRC24_FLEXRAYB,

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/INTERLAKEN)
        /// </summary>
        CRC24_INTERLAKEN,

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/LTE-A)
        /// </summary>
        CRC24_LTEA,

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/LTE-B)
        /// </summary>
        CRC24_LTEB,

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/OPENPGP)
        /// </summary>
        CRC24_OPENPGP,

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/OS-9)
        /// </summary>
        CRC24_OS9,

        #endregion

        #region CRC-30

        /// <summary>
        /// CRC 30-bit checksum (CRC-30/CDMA)
        /// </summary>
        CRC30_CDMA,

        #endregion

        #region CRC-31

        /// <summary>
        /// CRC 31-bit checksum (CRC-31/PHILIPS)
        /// </summary>
        CRC31_PHILIPS,

        #endregion

        #region CRC-32

        /// <summary>
        /// CRC 32-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC32_ISOHDLC"/> 
        CRC32,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/AIXM)
        /// </summary>
        CRC32_AIXM,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/AUTOSAR)
        /// </summary>
        CRC32_AUTOSAR,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/BASE91-D)
        /// </summary>
        CRC32_BASE91D,

        /// <summary>
        /// CRC 32-bit checksum (BZIP2)
        /// </summary>
        CRC32_BZIP2,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/CD-ROM-EDC)
        /// </summary>
        CRC32_CDROMEDC,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/CKSUM)
        /// </summary>
        CRC32_CKSUM,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/ISCSI)
        /// </summary>
        CRC32_ISCSI,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/ISO-HDLC)
        /// </summary>
        CRC32_ISOHDLC,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/JAMCRC)
        /// </summary>
        CRC32_JAMCRC,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/MEF)
        /// </summary>
        CRC32_MEF,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/MPEG-2)
        /// </summary>
        CRC32_MPEG2,

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/XFER)
        /// </summary>
        CRC32_XFER,

        #endregion

        #region CRC-40

        /// <summary>
        /// CRC 40-bit checksum (CRC-40/GSM)
        /// </summary>
        CRC40_GSM,

        #endregion

        #region CRC-64

        /// <summary>
        /// CRC 64-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC64_ECMA182"/> 
        CRC64,

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/ECMA-182, Microsoft implementation)
        /// </summary>
        CRC64_ECMA182,

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/GO-ISO)
        /// </summary>
        CRC64_GOISO,

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/MS)
        /// </summary>
        CRC64_MS,

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/NVME)
        /// </summary>
        CRC64_NVME,

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/REDIS)
        /// </summary>
        CRC64_REDIS,

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/WE)
        /// </summary>
        CRC64_WE,

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/XZ)
        /// </summary>
        CRC64_XZ,

        #endregion

        #region Fletcher

        /// <summary>
        /// John G. Fletcher's 16-bit checksum
        /// </summary>
        Fletcher16,

        /// <summary>
        /// John G. Fletcher's 32-bit checksum
        /// </summary>
        Fletcher32,

        #endregion

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

        #region SHA

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

        #endregion

        /// <summary>
        /// spamsum fuzzy hash
        /// </summary>
        SpamSum,

        #region xxHash

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

        #endregion
    }
}