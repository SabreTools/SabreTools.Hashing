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

        #region CRC

        #region CRC-1

        /// <summary>
        /// CRC 1-bit checksum (CRC-1/ZERO [Parity bit with 0 start])
        /// </summary>
        CRC1_ZERO,

        /// <summary>
        /// CRC 1-bit checksum (CRC-1/ONE [Parity bit with 1 start])
        /// </summary>
        CRC1_ONE,

        #endregion

        #region CRC-3

        /// <summary>
        /// CRC 3-bit checksum (CRC-3/GSM)
        /// </summary>
        CRC3_GSM,

        /// <summary>
        /// CRC 3-bit checksum (CRC-3/ROHC)
        /// </summary>
        CRC3_ROHC,

        #endregion

        #region CRC-4

        /// <summary>
        /// CRC 4-bit checksum (CRC-4/G-704 [CRC-4/ITU])
        /// </summary>
        CRC4_G704,

        /// <summary>
        /// CRC 4-bit checksum (CRC-4/INTERLAKEN)
        /// </summary>
        CRC4_INTERLAKEN,

        #endregion

        #region CRC-5

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/EPC-C1G2 [CRC-5/EPC])
        /// </summary>
        CRC5_EPCC1G2,

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/G-704 [CRC-5/ITU])
        /// </summary>
        CRC5_G704,

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/USB)
        /// </summary>
        CRC5_USB,

        #endregion

        #region CRC-6

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/CDMA2000-A)
        /// </summary>
        CRC6_CDMA2000A,

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/CDMA2000-B)
        /// </summary>
        CRC6_CDMA2000B,

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/DARC)
        /// </summary>
        CRC6_DARC,

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/G-704 [CRC-6/ITU])
        /// </summary>
        CRC6_G704,

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/GSM)
        /// </summary>
        CRC6_GSM,

        #endregion

        #region CRC-7

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/MMC [CRC-7])
        /// </summary>
        CRC7_MMC,

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/ROHC)
        /// </summary>
        CRC7_ROHC,

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/UMTS)
        /// </summary>
        CRC7_UMTS,

        #endregion

        #region CRC-8

        /// <summary>
        /// CRC 8-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC8_SMBUS"/>
        CRC8,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/AUTOSAR)
        /// </summary>
        CRC8_AUTOSAR,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/BLUETOOTH)
        /// </summary>
        CRC8_BLUETOOTH,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/CDMA2000)
        /// </summary>
        CRC8_CDMA2000,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/DARC)
        /// </summary>
        CRC8_DARC,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/DVB-S2)
        /// </summary>
        CRC8_DVBS2,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/GSM-A)
        /// </summary>
        CRC8_GSMA,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/GSM-B)
        /// </summary>
        CRC8_GSMB,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/HITAG)
        /// </summary>
        CRC8_HITAG,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/I-432-1 [CRC-8/ITU])
        /// </summary>
        CRC8_I4321,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/I-CODE)
        /// </summary>
        CRC8_ICODE,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/LTE)
        /// </summary>
        CRC8_LTE,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/MAXIM-DOW [CRC-8/MAXIM, DOW-CRC])
        /// </summary>
        CRC8_MAXIMDOW,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/MIFARE-MAD)
        /// </summary>
        CRC8_MIFAREMAD,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/NRSC-5)
        /// </summary>
        CRC8_NRSC5,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/OPENSAFETY)
        /// </summary>
        CRC8_OPENSAFETY,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/ROHC)
        /// </summary>
        CRC8_ROHC,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/SAE-J1850)
        /// </summary>
        CRC8_SAEJ1850,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/SMBUS [CRC-8])
        /// </summary>
        CRC8_SMBUS,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/TECH-3250 [CRC-8/AES, CRC-8/EBU])
        /// </summary>
        CRC8_TECH3250,

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/WCDMA)
        /// </summary>
        CRC8_WCDMA,

        #endregion

        #region CRC-10

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/ATM [CRC-10, CRC-10/I-610])
        /// </summary>
        CRC10_ATM,

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/CDMA2000)
        /// </summary>
        CRC10_CDMA2000,

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/GSM)
        /// </summary>
        CRC10_GSM,

        #endregion

        #region CRC-11

        /// <summary>
        /// CRC 11-bit checksum (CRC-11/FLEXRAY [CRC-11])
        /// </summary>
        CRC11_FLEXRAY,

        /// <summary>
        /// CRC 11-bit checksum (CRC-11/UMTS)
        /// </summary>
        CRC11_UMTS,

        #endregion

        #region CRC-12

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/CDMA2000)
        /// </summary>
        CRC12_CDMA2000,

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/DECT [X-CRC-12])
        /// </summary>
        CRC12_DECT,

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/GSM)
        /// </summary>
        CRC12_GSM,

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/UMTS [CRC-12/3GPP])
        /// </summary>
        CRC12_UMTS,

        #endregion

        #region CRC-13

        /// <summary>
        /// CRC 13-bit checksum (CRC-13/BBC)
        /// </summary>
        CRC13_BBC,

        #endregion

        #region CRC-14

        /// <summary>
        /// CRC 14-bit checksum (CRC-14/DARC)
        /// </summary>
        CRC14_DARC,

        /// <summary>
        /// CRC 14-bit checksum (CRC-14/GSM)
        /// </summary>
        CRC14_GSM,

        #endregion

        #region CRC-15

        /// <summary>
        /// CRC 15-bit checksum (CRC-15/CAN [CRC-15])
        /// </summary>
        CRC15_CAN,

        /// <summary>
        /// CRC 15-bit checksum (CRC-15/MPT1327)
        /// </summary>
        CRC15_MPT1327,

        #endregion

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
        CRC16_ISOIEC144433A,

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
        CRC16_RIELLO,

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
        CRC16_TMS37157,

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
        CRC24_BLE,

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
        /// CRC 32-bit checksum (CRC-32/DVD-ROM-EDC)
        /// </summary>
        CRC32_DVDROMEDC,

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

        /// <summary>
        /// John G. Fletcher's 64-bit checksum
        /// </summary>
        Fletcher64,

        #endregion

        #region FNV

        /// <summary>
        /// FNV hash (Variant 0, 32-bit)
        /// </summary>
        FNV0_32,

        /// <summary>
        /// FNV hash (Variant 0, 64-bit)
        /// </summary>
        FNV0_64,

        /// <summary>
        /// FNV hash (Variant 1, 32-bit)
        /// </summary>
        FNV1_32,

        /// <summary>
        /// FNV hash (Variant 1, 64-bit)
        /// </summary>
        FNV1_64,

        /// <summary>
        /// FNV hash (Variant 1a, 32-bit)
        /// </summary>
        FNV1a_32,

        /// <summary>
        /// FNV hash (Variant 1a, 64-bit)
        /// </summary>
        FNV1a_64,

        #endregion

        /// <summary>
        /// Custom checksum used by MEKA
        /// </summary>
        MekaCrc,

        #region Message Digest

        /// <summary>
        /// MD2 message-digest algorithm
        /// </summary>
        MD2,

        /// <summary>
        /// MD4 message-digest algorithm
        /// </summary>
        MD4,

        /// <summary>
        /// MD5 message-digest algorithm
        /// </summary>
        MD5,

        #endregion

        #region RIPEMD

        /// <summary>
        /// RIPEMD-128 hash
        /// </summary>
        RIPEMD128,

        /// <summary>
        /// RIPEMD-160 hash
        /// </summary>
        RIPEMD160,

        /// <summary>
        /// RIPEMD-256 hash
        /// </summary>
        RIPEMD256,

        /// <summary>
        /// RIPEMD-320 hash
        /// </summary>
        RIPEMD320,

        #endregion

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

        #region Tiger

        /// <summary>
        /// Tiger 128-bit hash, 3 passes
        /// </summary>
        Tiger128_3,

        /// <summary>
        /// Tiger 128-bit hash, 4 passes
        /// </summary>
        Tiger128_4,

        /// <summary>
        /// Tiger 160-bit hash, 3 passes
        /// </summary>
        Tiger160_3,

        /// <summary>
        /// Tiger 160-bit hash, 4 passes
        /// </summary>
        Tiger160_4,

        /// <summary>
        /// Tiger 192-bit hash, 3 passes
        /// </summary>
        Tiger192_3,

        /// <summary>
        /// Tiger 192-bit hash, 4 passes
        /// </summary>
        Tiger192_4,

        /// <summary>
        /// Tiger2 128-bit hash, 3 passes
        /// </summary>
        Tiger2_128_3,

        /// <summary>
        /// Tiger2 128-bit hash, 4 passes
        /// </summary>
        Tiger2_128_4,

        /// <summary>
        /// Tiger2 160-bit hash, 3 passes
        /// </summary>
        Tiger2_160_3,

        /// <summary>
        /// Tiger2 160-bit hash, 4 passes
        /// </summary>
        Tiger2_160_4,

        /// <summary>
        /// Tiger2 192-bit hash, 3 passes
        /// </summary>
        Tiger2_192_3,

        /// <summary>
        /// Tiger2 192-bit hash, 4 passes
        /// </summary>
        Tiger2_192_4,

        #endregion

        #region xxHash

        /// <summary>
        /// xxHash32 hash
        /// </summary>
        XxHash32,

        /// <summary>
        /// xxHash64 hash
        /// </summary>
        XxHash64,

#if NET462_OR_GREATER || NETCOREAPP
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
