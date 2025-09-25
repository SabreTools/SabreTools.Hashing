using System.Collections.Generic;

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
        public static byte[] CRC32Arr => _bytes[HashType.CRC32];

        /// <summary>
        /// Zero-byte MD5 hash
        /// </summary>
        public static byte[] MD5Arr => _bytes[HashType.MD5];

        /// <summary>
        /// Zero-byte SHA-1 hash
        /// </summary>
        public static byte[] SHA1Arr => _bytes[HashType.SHA1];

        /// <summary>
        /// Zero-byte SHA-256 hash
        /// </summary>
        public static byte[] SHA256Arr => _bytes[HashType.SHA256];

        /// <summary>
        /// Zero-byte SHA-384 hash
        /// </summary>
        public static byte[] SHA384Arr => _bytes[HashType.SHA384];

        /// <summary>
        /// Zero-byte SHA-512 hash
        /// </summary>
        public static byte[] SHA512Arr => _bytes[HashType.SHA512];

        /// <summary>
        /// Zero-byte SpamSum fuzzy hash
        /// </summary>
        public static byte[] SpamSumArr => _bytes[HashType.SpamSum];

        #endregion

        #region Shortcuts for Common Hash Strings

        /// <summary>
        /// Zero-byte CRC-32 checksum
        /// </summary>
        public static string CRC32Str => _strings[HashType.CRC32];

        /// <summary>
        /// Zero-byte MD5 hash
        /// </summary>
        public static string MD5Str => _strings[HashType.MD5];

        /// <summary>
        /// Zero-byte SHA-1 hash
        /// </summary>
        public static string SHA1Str => _strings[HashType.SHA1];

        /// <summary>
        /// Zero-byte SHA-256 hash
        /// </summary>
        public static string SHA256Str => _strings[HashType.SHA256];

        /// <summary>
        /// Zero-byte SHA-384 hash
        /// </summary>
        public static string SHA384Str => _strings[HashType.SHA384];

        /// <summary>
        /// Zero-byte SHA-512 hash
        /// </summary>
        public static string SHA512Str => _strings[HashType.SHA512];

        /// <summary>
        /// Zero-byte SpamSum fuzzy hash
        /// </summary>
        public static string SpamSumStr => _strings[HashType.SpamSum];

        #endregion

        /// <summary>
        /// Set of all known 0-byte outputs as strings
        /// </summary>
        private static readonly Dictionary<HashType, byte[]> _bytes = new()
        {
            {HashType.Adler32, [0x00, 0x00, 0x00, 0x01]},

#if NET7_0_OR_GREATER
            {HashType.BLAKE3, [0xaf, 0x13, 0x49, 0xb9, 0xf5, 0xf9, 0xa1, 0xa6,
                               0xa0, 0x40, 0x4d, 0xea, 0x36, 0xdc, 0xc9, 0x49,
                               0x9b, 0xcb, 0x25, 0xc9, 0xad, 0xc1, 0x12, 0xb7,
                               0xcc, 0x9a, 0x93, 0xca, 0xe4, 0x1f, 0x32, 0x62]},
#endif

            {HashType.CRC1_ZERO, [0x00]},
            {HashType.CRC1_ONE, [0x01]},

            {HashType.CRC3_GSM, [0x07]},
            {HashType.CRC3_ROHC, [0x07]},

            {HashType.CRC4_G704, [0x00]},
            {HashType.CRC4_INTERLAKEN, [0x00]},

            {HashType.CRC5_EPCC1G2, [0x09]},
            {HashType.CRC5_G704, [0x00]},
            {HashType.CRC5_USB, [0x00]},

            {HashType.CRC6_CDMA2000A, [0x3f]},
            {HashType.CRC6_CDMA2000B, [0x3f]},
            {HashType.CRC6_DARC, [0x00]},
            {HashType.CRC6_G704, [0x00]},
            {HashType.CRC6_GSM, [0x3f]},

            {HashType.CRC7_MMC, [0x00]},
            {HashType.CRC7_ROHC, [0x7f]},
            {HashType.CRC7_UMTS, [0x00]},

            {HashType.CRC8, [0x00]},
            {HashType.CRC8_AUTOSAR, [0x00]},
            {HashType.CRC8_BLUETOOTH, [0x00]},
            {HashType.CRC8_CDMA2000, [0xff]},
            {HashType.CRC8_DARC, [0x00]},
            {HashType.CRC8_DVBS2, [0x00]},
            {HashType.CRC8_GSMA, [0x00]},
            {HashType.CRC8_GSMB, [0xff]},
            {HashType.CRC8_HITAG, [0xff]},
            {HashType.CRC8_I4321, [0x55]},
            {HashType.CRC8_ICODE, [0xfd]},
            {HashType.CRC8_LTE, [0x00]},
            {HashType.CRC8_MAXIMDOW, [0x00]},
            {HashType.CRC8_MIFAREMAD, [0xc7]},
            {HashType.CRC8_NRSC5, [0xff]},
            {HashType.CRC8_OPENSAFETY, [0x00]},
            {HashType.CRC8_ROHC, [0xff]},
            {HashType.CRC8_SAEJ1850, [0x00]},
            {HashType.CRC8_SMBUS, [0x00]},
            {HashType.CRC8_TECH3250, [0xff]},
            {HashType.CRC8_WCDMA, [0x00]},

            {HashType.CRC10_ATM, [0x00, 0x00]},
            {HashType.CRC10_CDMA2000, [0x03, 0xff]},
            {HashType.CRC10_GSM, [0x03, 0xff]},

            {HashType.CRC11_FLEXRAY, [0x00, 0x1a]},
            {HashType.CRC11_UMTS, [0x00, 0x00]},

            {HashType.CRC12_CDMA2000, [0x0f, 0xff]},
            {HashType.CRC12_DECT, [0x00, 0x00]},
            {HashType.CRC12_GSM, [0x0f, 0xff]},
            {HashType.CRC12_UMTS, [0x00, 0x00]},

            {HashType.CRC13_BBC, [0x00, 0x00]},

            {HashType.CRC14_DARC, [0x00, 0x00]},
            {HashType.CRC14_GSM, [0x3f, 0xff]},

            {HashType.CRC15_CAN, [0x00, 0x00]},
            {HashType.CRC15_MPT1327, [0x00, 0x01]},

            {HashType.CRC16, [0x00, 0x00]},
            {HashType.CRC16_ARC, [0x00, 0x00]},
            {HashType.CRC16_CDMA2000, [0xff, 0xff]},
            {HashType.CRC16_CMS, [0xff, 0xff]},
            {HashType.CRC16_DDS110, [0x80, 0x0d]},
            {HashType.CRC16_DECTR, [0x00, 0x01]},
            {HashType.CRC16_DECTX, [0x00, 0x00]},
            {HashType.CRC16_DNP, [0xff, 0xff]},
            {HashType.CRC16_EN13757, [0xff, 0xff]},
            {HashType.CRC16_GENIBUS, [0x00, 0x00]},
            {HashType.CRC16_GSM, [0xff, 0xff]},
            {HashType.CRC16_IBM3740, [0xff, 0xff]},
            {HashType.CRC16_IBMSDLC, [0x00, 0x00]},
            {HashType.CRC16_ISOIEC144433A, [0x63, 0x63]},
            {HashType.CRC16_KERMIT, [0x00, 0x00]},
            {HashType.CRC16_LJ1200, [0x00, 0x00]},
            {HashType.CRC16_M17, [0xff, 0xff]},
            {HashType.CRC16_MAXIMDOW, [0xff, 0xff]},
            {HashType.CRC16_MCRF4XX, [0xff, 0xff]},
            {HashType.CRC16_MODBUS, [0xff, 0xff]},
            {HashType.CRC16_NRSC5, [0xff, 0xff]},
            {HashType.CRC16_OPENSAFETYA, [0x00, 0x00]},
            {HashType.CRC16_OPENSAFETYB, [0x00, 0x00]},
            {HashType.CRC16_PROFIBUS, [0x00, 0x00]},
            {HashType.CRC16_RIELLO, [0x55, 0x4d]},
            {HashType.CRC16_SPIFUJITSU, [0x1d, 0x0f]},
            {HashType.CRC16_T10DIF, [0x00, 0x00]},
            {HashType.CRC16_TELEDISK, [0x00, 0x00]},
            {HashType.CRC16_TMS37157, [0x37, 0x91]},
            {HashType.CRC16_UMTS, [0x00, 0x00]},
            {HashType.CRC16_USB, [0x00, 0x00]},
            {HashType.CRC16_XMODEM, [0x00, 0x00]},

            {HashType.CRC17_CANFD, [0x00, 0x00, 0x00]},

            {HashType.CRC21_CANFD, [0x00, 0x00, 0x00]},

            {HashType.CRC24_BLE, [0xaa, 0xaa, 0xaa]},
            {HashType.CRC24_FLEXRAYA, [0xfe, 0xdc, 0xba]},
            {HashType.CRC24_FLEXRAYB, [0xab, 0xcd, 0xef]},
            {HashType.CRC24_INTERLAKEN, [0x00, 0x00, 0x00]},
            {HashType.CRC24_LTEA, [0x00, 0x00, 0x00]},
            {HashType.CRC24_LTEB, [0x00, 0x00, 0x00]},
            {HashType.CRC24_OPENPGP, [0xb7, 0x04, 0xce]},
            {HashType.CRC24_OS9, [0x00, 0x00, 0x00]},

            {HashType.CRC30_CDMA, [0x00, 0x00, 0x00, 0x00]},

            {HashType.CRC31_PHILIPS, [0x00, 0x00, 0x00, 0x00]},

            {HashType.CRC32, [0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC32_AIXM, [0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC32_AUTOSAR, [0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC32_BASE91D, [0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC32_BZIP2, [0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC32_CDROMEDC, [0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC32_CKSUM, [0xff, 0xff, 0xff, 0xff]},
            {HashType.CRC32_ISCSI, [0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC32_ISOHDLC, [0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC32_JAMCRC, [0xff, 0xff, 0xff, 0xff]},
            {HashType.CRC32_MEF, [0xff, 0xff, 0xff, 0xff]},
            {HashType.CRC32_MPEG2, [0xff, 0xff, 0xff, 0xff]},
            {HashType.CRC32_XFER, [0x00, 0x00, 0x00, 0x00]},

            {HashType.CRC40_GSM, [0xff, 0xff, 0xff, 0xff, 0xff]},

            {HashType.CRC64, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC64_ECMA182, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC64_GOISO, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC64_MS, [0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff]},
            {HashType.CRC64_NVME, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC64_REDIS, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC64_WE, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]},
            {HashType.CRC64_XZ, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]},

            {HashType.Fletcher16, [0x00, 0x00]},
            {HashType.Fletcher32, [0x00, 0x00, 0x00, 0x00]},
            {HashType.Fletcher64, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]},

            {HashType.FNV0_32, [0x00, 0x00, 0x00, 0x00]},
            {HashType.FNV0_64, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]},
            {HashType.FNV1_32, [0x81, 0x1c, 0x9d, 0xc5]},
            {HashType.FNV1_64, [0xcb, 0xf2, 0x9c, 0xe4, 0x84, 0x22, 0x23, 0x25]},
            {HashType.FNV1a_32, [0x81, 0x1c, 0x9d, 0xc5]},
            {HashType.FNV1a_64, [0xcb, 0xf2, 0x9c, 0xe4, 0x84, 0x22, 0x23, 0x25]},

            {HashType.MekaCrc, [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00]},

            {HashType.MD2, [0x83, 0x50, 0xe5, 0xa3, 0xe2, 0x4c, 0x15, 0x3d,
                            0xf2, 0x27, 0x5c, 0x9f, 0x80, 0x69, 0x27, 0x73]},
            {HashType.MD4, [0x31, 0xd6, 0xcf, 0xe0, 0xd1, 0x6a, 0xe9, 0x31,
                            0xb7, 0x3c, 0x59, 0xd7, 0xe0, 0xc0, 0x89, 0xc0]},
            {HashType.MD5, [0xd4, 0x1d, 0x8c, 0xd9, 0x8f, 0x00, 0xb2, 0x04,
                            0xe9, 0x80, 0x09, 0x98, 0xec, 0xf8, 0x42, 0x7e]},

            {HashType.RIPEMD128, [0xcd, 0xf2, 0x62, 0x13, 0xa1, 0x50, 0xdc, 0x3e,
                                  0xcb, 0x61, 0x0f, 0x18, 0xf6, 0xb3, 0x8b, 0x46]},
            {HashType.RIPEMD160, [0x9c, 0x11, 0x85, 0xa5, 0xc5, 0xe9, 0xfc, 0x54,
                                  0x61, 0x28, 0x08, 0x97, 0x7e, 0xe8, 0xf5, 0x48,
                                  0xb2, 0x25, 0x8d, 0x31]},
            {HashType.RIPEMD256, [0x02, 0xba, 0x4c, 0x4e, 0x5f, 0x8e, 0xcd, 0x18,
                                  0x77, 0xfc, 0x52, 0xd6, 0x4d, 0x30, 0xe3, 0x7a,
                                  0x2d, 0x97, 0x74, 0xfb, 0x1e, 0x5d, 0x02, 0x63,
                                  0x80, 0xae, 0x01, 0x68, 0xe3, 0xc5, 0x52, 0x2d]},
            {HashType.RIPEMD320, [0x22, 0xd6, 0x5d, 0x56, 0x61, 0x53, 0x6c, 0xdc,
                                  0x75, 0xc1, 0xfd, 0xf5, 0xc6, 0xde, 0x7b, 0x41,
                                  0xb9, 0xf2, 0x73, 0x25, 0xeb, 0xc6, 0x1e, 0x85,
                                  0x57, 0x17, 0x7d, 0x70, 0x5a, 0x0e, 0xc8, 0x80,
                                  0x15, 0x1c, 0x3a, 0x32, 0xa0, 0x08, 0x99, 0xb8]},

            {HashType.SHA1, [0xda, 0x39, 0xa3, 0xee, 0x5e, 0x6b, 0x4b, 0x0d,
                             0x32, 0x55, 0xbf, 0xef, 0x95, 0x60, 0x18, 0x90,
                             0xaf, 0xd8, 0x07, 0x09]},
            {HashType.SHA256, [0xe3, 0xb0, 0xc4, 0x42, 0x98, 0xfc, 0x1c, 0x14,
                               0x9a, 0xfb, 0xf4, 0xc8, 0x99, 0x6f, 0xb9, 0x24,
                               0x27, 0xae, 0x41, 0xe4, 0x64, 0x9b, 0x93, 0x4c,
                               0xa4, 0x95, 0x99, 0x1b, 0x78, 0x52, 0xb8, 0x55]},
            {HashType.SHA384, [0x38, 0xb0, 0x60, 0xa7, 0x51, 0xac, 0x96, 0x38,
                               0x4c, 0xd9, 0x32, 0x7e, 0xb1, 0xb1, 0xe3, 0x6a,
                               0x21, 0xfd, 0xb7, 0x11, 0x14, 0xbe, 0x07, 0x43,
                               0x4c, 0x0c, 0xc7, 0xbf, 0x63, 0xf6, 0xe1, 0xda,
                               0x27, 0x4e, 0xde, 0xbf, 0xe7, 0x6f, 0x65, 0xfb,
                               0xd5, 0x1a, 0xd2, 0xf1, 0x48, 0x98, 0xb9, 0x5b]},
            {HashType.SHA512, [0xcf, 0x83, 0xe1, 0x35, 0x7e, 0xef, 0xb8, 0xbd,
                               0xf1, 0x54, 0x28, 0x50, 0xd6, 0x6d, 0x80, 0x07,
                               0xd6, 0x20, 0xe4, 0x05, 0x0b, 0x57, 0x15, 0xdc,
                               0x83, 0xf4, 0xa9, 0x21, 0xd3, 0x6c, 0xe9, 0xce,
                               0x47, 0xd0, 0xd1, 0x3c, 0x5d, 0x85, 0xf2, 0xb0,
                               0xff, 0x83, 0x18, 0xd2, 0x87, 0x7e, 0xec, 0x2f,
                               0x63, 0xb9, 0x31, 0xbd, 0x47, 0x41, 0x7a, 0x81,
                               0xa5, 0x38, 0x32, 0x7a, 0xf9, 0x27, 0xda, 0x3e]},
#if NET8_0_OR_GREATER
            {HashType.SHA3_256, [0xa7, 0xff, 0xc6, 0xf8, 0xbf, 0x1e, 0xd7, 0x66,
                                 0x51, 0xc1, 0x47, 0x56, 0xa0, 0x61, 0xd6, 0x62,
                                 0xf5, 0x80, 0xff, 0x4d, 0xe4, 0x3b, 0x49, 0xfa,
                                 0x82, 0xd8, 0x0a, 0x4b, 0x80, 0xf8, 0x43, 0x4a]},
            {HashType.SHA3_384, [0x0c, 0x63, 0xa7, 0x5b, 0x84, 0x5e, 0x4f, 0x7d,
                                 0x01, 0x10, 0x7d, 0x85, 0x2e, 0x4c, 0x24, 0x85,
                                 0xc5, 0x1a, 0x50, 0xaa, 0xaa, 0x94, 0xfc, 0x61,
                                 0x99, 0x5e, 0x71, 0xbb, 0xee, 0x98, 0x3a, 0x2a,
                                 0xc3, 0x71, 0x38, 0x31, 0x26, 0x4a, 0xdb, 0x47,
                                 0xfb, 0x6b, 0xd1, 0xe0, 0x58, 0xd5, 0xf0, 0x04]},
            {HashType.SHA3_512, [0xa6, 0x9f, 0x73, 0xcc, 0xa2, 0x3a, 0x9a, 0xc5,
                                 0xc8, 0xb5, 0x67, 0xdc, 0x18, 0x5a, 0x75, 0x6e,
                                 0x97, 0xc9, 0x82, 0x16, 0x4f, 0xe2, 0x58, 0x59,
                                 0xe0, 0xd1, 0xdc, 0xc1, 0x47, 0x5c, 0x80, 0xa6,
                                 0x15, 0xb2, 0x12, 0x3a, 0xf1, 0xf5, 0xf9, 0x4c,
                                 0x11, 0xe3, 0xe9, 0x40, 0x2c, 0x3a, 0xc5, 0x58,
                                 0xf5, 0x00, 0x19, 0x9d, 0x95, 0xb6, 0xd3, 0xe3,
                                 0x01, 0x75, 0x85, 0x86, 0x28, 0x1d, 0xcd, 0x26]},
            {HashType.SHAKE128, [0x7f, 0x9c, 0x2b, 0xa4, 0xe8, 0x8f, 0x82, 0x7d,
                                 0x61, 0x60, 0x45, 0x50, 0x76, 0x05, 0x85, 0x3e,
                                 0xd7, 0x3b, 0x80, 0x93, 0xf6, 0xef, 0xbc, 0x88,
                                 0xeb, 0x1a, 0x6e, 0xac, 0xfa, 0x66, 0xef, 0x26]},
            {HashType.SHAKE256, [0x46, 0xb9, 0xdd, 0x2b, 0x0b, 0xa8, 0x8d, 0x13,
                                 0x23, 0x3b, 0x3f, 0xeb, 0x74, 0x3e, 0xeb, 0x24,
                                 0x3f, 0xcd, 0x52, 0xea, 0x62, 0xb8, 0x1b, 0x82,
                                 0xb5, 0x0c, 0x27, 0x64, 0x6e, 0xd5, 0x76, 0x2f,
                                 0xd7, 0x5d, 0xc4, 0xdd, 0xd8, 0xc0, 0xf2, 0x00,
                                 0xcb, 0x05, 0x01, 0x9d, 0x67, 0xb5, 0x92, 0xf6,
                                 0xfc, 0x82, 0x1c, 0x49, 0x47, 0x9a, 0xb4, 0x86,
                                 0x40, 0x29, 0x2e, 0xac, 0xb3, 0xb7, 0xc4, 0xbe]},
#endif

            {HashType.SpamSum, [0x33, 0x3a, 0x3a, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                                0x00, 0x00, 0x00, 0x00]},

            {HashType.Tiger128_3, [0x32, 0x93, 0xac, 0x63, 0x0c, 0x13, 0xf0, 0x24,
                                   0x5f, 0x92, 0xbb, 0xb1, 0x76, 0x6e, 0x16, 0x16]},
            {HashType.Tiger128_4, [0x24, 0xcc, 0x78, 0xa7, 0xf6, 0xff, 0x35, 0x46,
                                   0xe7, 0x98, 0x4e, 0x59, 0x69, 0x5c, 0xa1, 0x3d]},
            {HashType.Tiger160_3, [0x32, 0x93, 0xac, 0x63, 0x0c, 0x13, 0xf0, 0x24,
                                   0x5f, 0x92, 0xbb, 0xb1, 0x76, 0x6e, 0x16, 0x16,
                                   0x7a, 0x4e, 0x58, 0x49]},
            {HashType.Tiger160_4, [0x24, 0xcc, 0x78, 0xa7, 0xf6, 0xff, 0x35, 0x46,
                                   0xe7, 0x98, 0x4e, 0x59, 0x69, 0x5c, 0xa1, 0x3d,
                                   0x80, 0x4e, 0x0b, 0x68]},
            {HashType.Tiger192_3, [0x32, 0x93, 0xac, 0x63, 0x0c, 0x13, 0xf0, 0x24,
                                   0x5f, 0x92, 0xbb, 0xb1, 0x76, 0x6e, 0x16, 0x16,
                                   0x7a, 0x4e, 0x58, 0x49, 0x2d, 0xde, 0x73, 0xf3]},
            {HashType.Tiger192_4, [0x24, 0xcc, 0x78, 0xa7, 0xf6, 0xff, 0x35, 0x46,
                                   0xe7, 0x98, 0x4e, 0x59, 0x69, 0x5c, 0xa1, 0x3d,
                                   0x80, 0x4e, 0x0b, 0x68, 0x6e, 0x25, 0x51, 0x94]},
            {HashType.Tiger2_128_3, [0x44, 0x41, 0xbe, 0x75, 0xf6, 0x01, 0x87, 0x73,
                                     0xc2, 0x06, 0xc2, 0x27, 0x45, 0x37, 0x4b, 0x92]},
            {HashType.Tiger2_128_4, [0x6a, 0x72, 0x01, 0xa4, 0x7a, 0xac, 0x20, 0x65,
                                     0x91, 0x38, 0x11, 0x17, 0x55, 0x53, 0x48, 0x9a]},
            {HashType.Tiger2_160_3, [0x44, 0x41, 0xbe, 0x75, 0xf6, 0x01, 0x87, 0x73,
                                     0xc2, 0x06, 0xc2, 0x27, 0x45, 0x37, 0x4b, 0x92,
                                     0x4a, 0xa8, 0x31, 0x3f]},
            {HashType.Tiger2_160_4, [0x6a, 0x72, 0x01, 0xa4, 0x7a, 0xac, 0x20, 0x65,
                                     0x91, 0x38, 0x11, 0x17, 0x55, 0x53, 0x48, 0x9a,
                                     0xdd, 0x0f, 0x8b, 0x99]},
            {HashType.Tiger2_192_3, [0x44, 0x41, 0xbe, 0x75, 0xf6, 0x01, 0x87, 0x73,
                                     0xc2, 0x06, 0xc2, 0x27, 0x45, 0x37, 0x4b, 0x92,
                                     0x4a, 0xa8, 0x31, 0x3f, 0xef, 0x91, 0x9f, 0x41]},
            {HashType.Tiger2_192_4, [0x6a, 0x72, 0x01, 0xa4, 0x7a, 0xac, 0x20, 0x65,
                                     0x91, 0x38, 0x11, 0x17, 0x55, 0x53, 0x48, 0x9a,
                                     0xdd, 0x0f, 0x8b, 0x99, 0xe6, 0x5a, 0x09, 0x55]},

            {HashType.XxHash32, [0x02, 0xcc, 0x5d, 0x05]},
            {HashType.XxHash64, [0xef, 0x46, 0xdb, 0x37, 0x51, 0xd8, 0xe9, 0x99]},
#if NET462_OR_GREATER || NETCOREAPP
            {HashType.XxHash3, [0x2d, 0x06, 0x80, 0x05, 0x38, 0xd3, 0x94, 0xc2]},
            {HashType.XxHash128, [0x99, 0xaa, 0x06, 0xd3, 0x01, 0x47, 0x98, 0xd8,
                                  0x60, 0x01, 0xc3, 0x24, 0x46, 0x8d, 0x49, 0x7f]},
#endif
        };

        /// <summary>
        /// Set of all known 0-byte outputs as strings
        /// </summary>
        private static readonly Dictionary<HashType, string> _strings = new()
        {
            {HashType.Adler32, "00000001"},

#if NET7_0_OR_GREATER
            {HashType.BLAKE3, "af1349b9f5f9a1a6a0404dea36dcc9499bcb25c9adc112b7cc9a93cae41f3262"},
#endif

            {HashType.CRC1_ZERO, "0"},
            {HashType.CRC1_ONE, "1"},

            {HashType.CRC3_GSM, "7"},
            {HashType.CRC3_ROHC, "7"},

            {HashType.CRC4_G704, "0"},
            {HashType.CRC4_INTERLAKEN, "0"},

            {HashType.CRC5_EPCC1G2, "09"},
            {HashType.CRC5_G704, "00"},
            {HashType.CRC5_USB, "00"},

            {HashType.CRC6_CDMA2000A, "3f"},
            {HashType.CRC6_CDMA2000B, "3f"},
            {HashType.CRC6_DARC, "00"},
            {HashType.CRC6_G704, "00"},
            {HashType.CRC6_GSM, "3f"},

            {HashType.CRC7_MMC, "00"},
            {HashType.CRC7_ROHC, "7f"},
            {HashType.CRC7_UMTS, "00"},

            {HashType.CRC8, "00"},
            {HashType.CRC8_AUTOSAR, "00"},
            {HashType.CRC8_BLUETOOTH, "00"},
            {HashType.CRC8_CDMA2000, "ff"},
            {HashType.CRC8_DARC, "00"},
            {HashType.CRC8_DVBS2, "00"},
            {HashType.CRC8_GSMA, "00"},
            {HashType.CRC8_GSMB, "ff"},
            {HashType.CRC8_HITAG, "ff"},
            {HashType.CRC8_I4321, "55"},
            {HashType.CRC8_ICODE, "fd"},
            {HashType.CRC8_LTE, "00"},
            {HashType.CRC8_MAXIMDOW, "00"},
            {HashType.CRC8_MIFAREMAD, "c7"},
            {HashType.CRC8_NRSC5, "ff"},
            {HashType.CRC8_OPENSAFETY, "00"},
            {HashType.CRC8_ROHC, "ff"},
            {HashType.CRC8_SAEJ1850, "00"},
            {HashType.CRC8_SMBUS, "00"},
            {HashType.CRC8_TECH3250, "ff"},
            {HashType.CRC8_WCDMA, "00"},

            {HashType.CRC10_ATM, "000"},
            {HashType.CRC10_CDMA2000, "3ff"},
            {HashType.CRC10_GSM, "3ff"},

            {HashType.CRC11_FLEXRAY, "01a"},
            {HashType.CRC11_UMTS, "000"},

            {HashType.CRC12_CDMA2000, "fff"},
            {HashType.CRC12_DECT, "000"},
            {HashType.CRC12_GSM, "fff"},
            {HashType.CRC12_UMTS, "000"},

            {HashType.CRC13_BBC, "0000"},

            {HashType.CRC14_DARC, "0000"},
            {HashType.CRC14_GSM, "3fff"},

            {HashType.CRC15_CAN, "0000"},
            {HashType.CRC15_MPT1327, "0001"},

            {HashType.CRC16, "0000"},
            {HashType.CRC16_ARC, "0000"},
            {HashType.CRC16_CDMA2000, "ffff"},
            {HashType.CRC16_CMS, "ffff"},
            {HashType.CRC16_DDS110, "800d"},
            {HashType.CRC16_DECTR, "0001"},
            {HashType.CRC16_DECTX, "0000"},
            {HashType.CRC16_DNP, "ffff"},
            {HashType.CRC16_EN13757, "ffff"},
            {HashType.CRC16_GENIBUS, "0000"},
            {HashType.CRC16_GSM, "ffff"},
            {HashType.CRC16_IBM3740, "ffff"},
            {HashType.CRC16_IBMSDLC, "0000"},
            {HashType.CRC16_ISOIEC144433A, "6363"},
            {HashType.CRC16_KERMIT, "0000"},
            {HashType.CRC16_LJ1200, "0000"},
            {HashType.CRC16_M17, "ffff"},
            {HashType.CRC16_MAXIMDOW, "ffff"},
            {HashType.CRC16_MCRF4XX, "ffff"},
            {HashType.CRC16_MODBUS, "ffff"},
            {HashType.CRC16_NRSC5, "ffff"},
            {HashType.CRC16_OPENSAFETYA, "0000"},
            {HashType.CRC16_OPENSAFETYB, "0000"},
            {HashType.CRC16_PROFIBUS, "0000"},
            {HashType.CRC16_RIELLO, "554d"},
            {HashType.CRC16_SPIFUJITSU, "1d0f"},
            {HashType.CRC16_T10DIF, "0000"},
            {HashType.CRC16_TELEDISK, "0000"},
            {HashType.CRC16_TMS37157, "3791"},
            {HashType.CRC16_UMTS, "0000"},
            {HashType.CRC16_USB, "0000"},
            {HashType.CRC16_XMODEM, "0000"},

            {HashType.CRC17_CANFD, "00000"},

            {HashType.CRC21_CANFD, "000000"},

            {HashType.CRC24_BLE, "aaaaaa"},
            {HashType.CRC24_FLEXRAYA, "fedcba"},
            {HashType.CRC24_FLEXRAYB, "abcdef"},
            {HashType.CRC24_INTERLAKEN, "000000"},
            {HashType.CRC24_LTEA, "000000"},
            {HashType.CRC24_LTEB, "000000"},
            {HashType.CRC24_OPENPGP, "b704ce"},
            {HashType.CRC24_OS9, "000000"},

            {HashType.CRC30_CDMA, "00000000"},

            {HashType.CRC31_PHILIPS, "00000000"},

            {HashType.CRC32, "00000000"},
            {HashType.CRC32_AIXM, "00000000"},
            {HashType.CRC32_AUTOSAR, "00000000"},
            {HashType.CRC32_BASE91D, "00000000"},
            {HashType.CRC32_BZIP2, "00000000"},
            {HashType.CRC32_CDROMEDC, "00000000"},
            {HashType.CRC32_CKSUM, "ffffffff"},
            {HashType.CRC32_ISCSI, "00000000"},
            {HashType.CRC32_ISOHDLC, "00000000"},
            {HashType.CRC32_JAMCRC, "ffffffff"},
            {HashType.CRC32_MEF, "ffffffff"},
            {HashType.CRC32_MPEG2, "ffffffff"},
            {HashType.CRC32_XFER, "00000000"},

            {HashType.CRC40_GSM, "ffffffffff"},

            {HashType.CRC64, "0000000000000000"},
            {HashType.CRC64_ECMA182, "0000000000000000"},
            {HashType.CRC64_GOISO, "0000000000000000"},
            {HashType.CRC64_MS, "ffffffffffffffff"},
            {HashType.CRC64_NVME, "0000000000000000"},
            {HashType.CRC64_REDIS, "0000000000000000"},
            {HashType.CRC64_WE, "0000000000000000"},
            {HashType.CRC64_XZ, "0000000000000000"},

            {HashType.Fletcher16, "0000"},
            {HashType.Fletcher32, "00000000"},
            {HashType.Fletcher64, "0000000000000000"},

            {HashType.FNV0_32, "00000000"},
            {HashType.FNV0_64, "0000000000000000"},
            {HashType.FNV1_32, "811c9dc5"},
            {HashType.FNV1_64, "cbf29ce484222325"},
            {HashType.FNV1a_32, "811c9dc5"},
            {HashType.FNV1a_64, "cbf29ce484222325"},

            {HashType.MekaCrc, "0000000000000000"},

            {HashType.MD2, "8350e5a3e24c153df2275c9f80692773"},
            {HashType.MD4, "31d6cfe0d16ae931b73c59d7e0c089c0"},
            {HashType.MD5, "d41d8cd98f00b204e9800998ecf8427e"},

            {HashType.RIPEMD128, "cdf26213a150dc3ecb610f18f6b38b46"},
            {HashType.RIPEMD160, "9c1185a5c5e9fc54612808977ee8f548b2258d31"},
            {HashType.RIPEMD256, "02ba4c4e5f8ecd1877fc52d64d30e37a2d9774fb1e5d026380ae0168e3c5522d"},
            {HashType.RIPEMD320, "22d65d5661536cdc75c1fdf5c6de7b41b9f27325ebc61e8557177d705a0ec880151c3a32a00899b8"},

            {HashType.SHA1, "da39a3ee5e6b4b0d3255bfef95601890afd80709"},
            {HashType.SHA256, "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855"},
            {HashType.SHA384, "38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b"},
            {HashType.SHA512, "cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e"},
#if NET8_0_OR_GREATER
            {HashType.SHA3_256, "a7ffc6f8bf1ed76651c14756a061d662f580ff4de43b49fa82d80a4b80f8434a"},
            {HashType.SHA3_384, "0c63a75b845e4f7d01107d852e4c2485c51a50aaaa94fc61995e71bbee983a2ac3713831264adb47fb6bd1e058d5f004"},
            {HashType.SHA3_512, "a69f73cca23a9ac5c8b567dc185a756e97c982164fe25859e0d1dcc1475c80a615b2123af1f5f94c11e3e9402c3ac558f500199d95b6d3e301758586281dcd26"},
            {HashType.SHAKE128, "7f9c2ba4e88f827d616045507605853ed73b8093f6efbc88eb1a6eacfa66ef26"},
            {HashType.SHAKE256, "46b9dd2b0ba88d13233b3feb743eeb243fcd52ea62b81b82b50c27646ed5762fd75dc4ddd8c0f200cb05019d67b592f6fc821c49479ab48640292eacb3b7c4be"},
#endif

            {HashType.SpamSum, "3::"},

            {HashType.Tiger128_3, "3293ac630c13f0245f92bbb1766e1616"},
            {HashType.Tiger128_4, "24cc78a7f6ff3546e7984e59695ca13d"},
            {HashType.Tiger160_3, "3293ac630c13f0245f92bbb1766e16167a4e5849"},
            {HashType.Tiger160_4, "24cc78a7f6ff3546e7984e59695ca13d804e0b68"},
            {HashType.Tiger192_3, "3293ac630c13f0245f92bbb1766e16167a4e58492dde73f3"},
            {HashType.Tiger192_4, "24cc78a7f6ff3546e7984e59695ca13d804e0b686e255194"},
            {HashType.Tiger2_128_3, "4441be75f6018773c206c22745374b92"},
            {HashType.Tiger2_128_4, "6a7201a47aac2065913811175553489a"},
            {HashType.Tiger2_160_3, "4441be75f6018773c206c22745374b924aa8313f"},
            {HashType.Tiger2_160_4, "6a7201a47aac2065913811175553489add0f8b99"},
            {HashType.Tiger2_192_3, "4441be75f6018773c206c22745374b924aa8313fef919f41"},
            {HashType.Tiger2_192_4, "6a7201a47aac2065913811175553489add0f8b99e65a0955"},

            {HashType.XxHash32, "02cc5d05"},
            {HashType.XxHash64, "ef46db3751d8e999"},
#if NET462_OR_GREATER || NETCOREAPP
            {HashType.XxHash3, "2d06800538d394c2"},
            {HashType.XxHash128, "99aa06d3014798d86001c324468d497f"},
#endif
        };

        /// <summary>
        /// Get the 0-byte value for <paramref name="hashType"/> as a byte array
        /// </summary>
        /// <param name="hashType">Hash type to get the value for</param>
        /// <returns>Non-empty array containing the value on success, empty array on failure</returns>
        public static byte[] GetBytes(HashType hashType)
        {
            if (!_strings.ContainsKey(hashType))
                return [];

            return _bytes[hashType];
        }

        /// <summary>
        /// Get the 0-byte value for <paramref name="hashType"/> as a string
        /// </summary>
        /// <param name="hashType">Hash type to get the value for</param>
        /// <returns>Non-empty string containing the value on success, empty string on failure</returns>
        public static string GetString(HashType hashType)
        {
            if (!_strings.ContainsKey(hashType))
                return string.Empty;

            return _strings[hashType];
        }
    }
}
