namespace SabreTools.Hashing
{
    /// <summary>
    /// Available hashing and checksumming types
    /// </summary>
    public static class HashType
    {
        /// <summary>
        /// Mark Adler's 32-bit checksum
        /// </summary>
        public const string Adler32 = "Adler32";

#if NET7_0_OR_GREATER
        /// <summary>
        /// BLAKE3 512-bit digest
        /// </summary>
        public const string BLAKE3 = "BLAKE3";
#endif

        #region CRC

        #region CRC-1

        /// <summary>
        /// CRC 1-bit checksum (CRC-1/ZERO [Parity bit with 0 start])
        /// </summary>
        public const string CRC1_ZERO = "CRC1_ZERO";

        /// <summary>
        /// CRC 1-bit checksum (CRC-1/ONE [Parity bit with 1 start])
        /// </summary>
        public const string CRC1_ONE = "CRC1_ONE";

        #endregion

        #region CRC-3

        /// <summary>
        /// CRC 3-bit checksum (CRC-3/GSM)
        /// </summary>
        public const string CRC3_GSM = "CRC3_GSM";

        /// <summary>
        /// CRC 3-bit checksum (CRC-3/ROHC)
        /// </summary>
        public const string CRC3_ROHC = "CRC3_ROHC";

        #endregion

        #region CRC-4

        /// <summary>
        /// CRC 4-bit checksum (CRC-4/G-704 [CRC-4/ITU])
        /// </summary>
        public const string CRC4_G704 = "CRC4_G704";

        /// <summary>
        /// CRC 4-bit checksum (CRC-4/INTERLAKEN)
        /// </summary>
        public const string CRC4_INTERLAKEN = "CRC4_INTERLAKEN";

        #endregion

        #region CRC-5

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/EPC-C1G2 [CRC-5/EPC])
        /// </summary>
        public const string CRC5_EPCC1G2 = "CRC5_EPCC1G2";

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/G-704 [CRC-5/ITU])
        /// </summary>
        public const string CRC5_G704 = "CRC5_G704";

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/USB)
        /// </summary>
        public const string CRC5_USB = "CRC5_USB";

        #endregion

        #region CRC-6

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/CDMA2000-A)
        /// </summary>
        public const string CRC6_CDMA2000A = "CRC6_CDMA2000A";

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/CDMA2000-B)
        /// </summary>
        public const string CRC6_CDMA2000B = "CRC6_CDMA2000B";

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/DARC)
        /// </summary>
        public const string CRC6_DARC = "CRC6_DARC";

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/G-704 [CRC-6/ITU])
        /// </summary>
        public const string CRC6_G704 = "CRC6_G704";

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/GSM)
        /// </summary>
        public const string CRC6_GSM = "CRC6_GSM";

        #endregion

        #region CRC-7

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/MMC [CRC-7])
        /// </summary>
        public const string CRC7_MMC = "CRC7_MMC";

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/ROHC)
        /// </summary>
        public const string CRC7_ROHC = "CRC7_ROHC";

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/UMTS)
        /// </summary>
        public const string CRC7_UMTS = "CRC7_UMTS";

        #endregion

        #region CRC-8

        /// <summary>
        /// CRC 8-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC8_SMBUS"/>
        public const string CRC8 = "CRC8";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/AUTOSAR)
        /// </summary>
        public const string CRC8_AUTOSAR = "CRC8_AUTOSAR";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/BLUETOOTH)
        /// </summary>
        public const string CRC8_BLUETOOTH = "CRC8_BLUETOOTH";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/CDMA2000)
        /// </summary>
        public const string CRC8_CDMA2000 = "CRC8_CDMA2000";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/DARC)
        /// </summary>
        public const string CRC8_DARC = "CRC8_DARC";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/DVB-S2)
        /// </summary>
        public const string CRC8_DVBS2 = "CRC8_DVBS2";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/GSM-A)
        /// </summary>
        public const string CRC8_GSMA = "CRC8_GSMA";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/GSM-B)
        /// </summary>
        public const string CRC8_GSMB = "CRC8_GSMB";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/HITAG)
        /// </summary>
        public const string CRC8_HITAG = "CRC8_HITAG";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/I-432-1 [CRC-8/ITU])
        /// </summary>
        public const string CRC8_I4321 = "CRC8_I4321";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/I-CODE)
        /// </summary>
        public const string CRC8_ICODE = "CRC8_ICODE";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/LTE)
        /// </summary>
        public const string CRC8_LTE = "CRC8_LTE";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/MAXIM-DOW [CRC-8/MAXIM, DOW-CRC])
        /// </summary>
        public const string CRC8_MAXIMDOW = "CRC8_MAXIMDOW";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/MIFARE-MAD)
        /// </summary>
        public const string CRC8_MIFAREMAD = "CRC8_MIFAREMAD";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/NRSC-5)
        /// </summary>
        public const string CRC8_NRSC5 = "CRC8_NRSC5";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/OPENSAFETY)
        /// </summary>
        public const string CRC8_OPENSAFETY = "CRC8_OPENSAFETY";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/ROHC)
        /// </summary>
        public const string CRC8_ROHC = "CRC8_ROHC";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/SAE-J1850)
        /// </summary>
        public const string CRC8_SAEJ1850 = "CRC8_SAEJ1850";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/SMBUS [CRC-8])
        /// </summary>
        public const string CRC8_SMBUS = "CRC8_SMBUS";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/TECH-3250 [CRC-8/AES, CRC-8/EBU])
        /// </summary>
        public const string CRC8_TECH3250 = "CRC8_TECH3250";

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/WCDMA)
        /// </summary>
        public const string CRC8_WCDMA = "CRC8_WCDMA";

        #endregion

        #region CRC-10

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/ATM [CRC-10, CRC-10/I-610])
        /// </summary>
        public const string CRC10_ATM = "CRC10_ATM";

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/CDMA2000)
        /// </summary>
        public const string CRC10_CDMA2000 = "CRC10_CDMA2000";

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/GSM)
        /// </summary>
        public const string CRC10_GSM = "CRC10_GSM";

        #endregion

        #region CRC-11

        /// <summary>
        /// CRC 11-bit checksum (CRC-11/FLEXRAY [CRC-11])
        /// </summary>
        public const string CRC11_FLEXRAY = "CRC11_FLEXRAY";

        /// <summary>
        /// CRC 11-bit checksum (CRC-11/UMTS)
        /// </summary>
        public const string CRC11_UMTS = "CRC11_UMTS";

        #endregion

        #region CRC-12

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/CDMA2000)
        /// </summary>
        public const string CRC12_CDMA2000 = "CRC12_CDMA2000";

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/DECT [X-CRC-12])
        /// </summary>
        public const string CRC12_DECT = "CRC12_DECT";

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/GSM)
        /// </summary>
        public const string CRC12_GSM = "CRC12_GSM";

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/UMTS [CRC-12/3GPP])
        /// </summary>
        public const string CRC12_UMTS = "CRC12_UMTS";

        #endregion

        #region CRC-13

        /// <summary>
        /// CRC 13-bit checksum (CRC-13/BBC)
        /// </summary>
        public const string CRC13_BBC = "CRC13_BBC";

        #endregion

        #region CRC-14

        /// <summary>
        /// CRC 14-bit checksum (CRC-14/DARC)
        /// </summary>
        public const string CRC14_DARC = "CRC14_DARC";

        /// <summary>
        /// CRC 14-bit checksum (CRC-14/GSM)
        /// </summary>
        public const string CRC14_GSM = "CRC14_GSM";

        #endregion

        #region CRC-15

        /// <summary>
        /// CRC 15-bit checksum (CRC-15/CAN [CRC-15])
        /// </summary>
        public const string CRC15_CAN = "CRC15_CAN";

        /// <summary>
        /// CRC 15-bit checksum (CRC-15/MPT1327)
        /// </summary>
        public const string CRC15_MPT1327 = "CRC15_MPT1327";

        #endregion

        #region CRC-16

        /// <summary>
        /// CRC 16-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC16_ARC"/>
        public const string CRC16 = "CRC16";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/ARC [ARC, CRC-16, CRC-16/LHA, CRC-IBM])
        /// </summary>
        public const string CRC16_ARC = "CRC16_ARC";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/CDMA2000)
        /// </summary>
        public const string CRC16_CDMA2000 = "CRC16_CDMA2000";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/CMS)
        /// </summary>
        public const string CRC16_CMS = "CRC16_CMS";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DDS-110)
        /// </summary>
        public const string CRC16_DDS110 = "CRC16_DDS110";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DECT-R [R-CRC-16])
        /// </summary>
        public const string CRC16_DECTR = "CRC16_DECTR";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DECT-X [X-CRC-16])
        /// </summary>
        public const string CRC16_DECTX = "CRC16_DECTX";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DNP)
        /// </summary>
        public const string CRC16_DNP = "CRC16_DNP";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/EN-13757)
        /// </summary>
        public const string CRC16_EN13757 = "CRC16_EN13757";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/GENIBUS [CRC-16/DARC, CRC-16/EPC, CRC-16/EPC-C1G2, CRC-16/I-CODE])
        /// </summary>
        public const string CRC16_GENIBUS = "CRC16_GENIBUS";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/GSM)
        /// </summary>
        public const string CRC16_GSM = "CRC16_GSM";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/IBM-3740 [CRC-16/AUTOSAR, CRC-16/CCITT-FALSE])
        /// </summary>
        public const string CRC16_IBM3740 = "CRC16_IBM3740";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/IBM-SDLC [CRC-16/ISO-HDLC, CRC-16/ISO-IEC-14443-3-B, CRC-16/X-25, CRC-B, X-25])
        /// </summary>
        public const string CRC16_IBMSDLC = "CRC16_IBMSDLC";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/ISO-IEC-14443-3-A [CRC-A])
        /// </summary>
        public const string CRC16_ISOIEC144433A = "CRC16_ISOIEC144433A";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/KERMIT [CRC-16/BLUETOOTH, CRC-16/CCITT, CRC-16/CCITT-TRUE, CRC-16/V-41-LSB, CRC-CCITT, KERMIT])
        /// </summary>
        public const string CRC16_KERMIT = "CRC16_KERMIT";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/LJ1200)
        /// </summary>
        public const string CRC16_LJ1200 = "CRC16_LJ1200";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/M17)
        /// </summary>
        public const string CRC16_M17 = "CRC16_M17";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MAXIM-DOW [CRC-16/MAXIM])
        /// </summary>
        public const string CRC16_MAXIMDOW = "CRC16_MAXIMDOW";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MCRF4XX)
        /// </summary>
        public const string CRC16_MCRF4XX = "CRC16_MCRF4XX";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MODBUS [MODBUS])
        /// </summary>
        public const string CRC16_MODBUS = "CRC16_MODBUS";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/NRSC-5)
        /// </summary>
        public const string CRC16_NRSC5 = "CRC16_NRSC5";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/OPENSAFETY-A)
        /// </summary>
        public const string CRC16_OPENSAFETYA = "CRC16_OPENSAFETYA";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/OPENSAFETY-B)
        /// </summary>
        public const string CRC16_OPENSAFETYB = "CRC16_OPENSAFETYB";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/PROFIBUS [CRC-16/IEC-61158-2])
        /// </summary>
        public const string CRC16_PROFIBUS = "CRC16_PROFIBUS";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/RIELLO)
        /// </summary>
        public const string CRC16_RIELLO = "CRC16_RIELLO";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/SPI-FUJITSU [CRC-16/AUG-CCITT])
        /// </summary>
        public const string CRC16_SPIFUJITSU = "CRC16_SPIFUJITSU";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/T10-DIF)
        /// </summary>
        public const string CRC16_T10DIF = "CRC16_T10DIF";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/TELEDISK)
        /// </summary>
        public const string CRC16_TELEDISK = "CRC16_TELEDISK";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/TMS37157)
        /// </summary>
        public const string CRC16_TMS37157 = "CRC16_TMS37157";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/UMTS [CRC-16/BUYPASS, CRC-16/VERIFONE])
        /// </summary>
        public const string CRC16_UMTS = "CRC16_UMTS";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/USB)
        /// </summary>
        public const string CRC16_USB = "CRC16_USB";

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/XMODEM [CRC-16/ACORN, CRC-16/LTE, CRC-16/V-41-MSB, XMODEM, ZMODEM])
        /// </summary>
        public const string CRC16_XMODEM = "CRC16_XMODEM";

        #endregion

        #region CRC-17

        /// <summary>
        /// CRC 17-bit checksum (CRC-17/CAN-FD)
        /// </summary>
        public const string CRC17_CANFD = "CRC17_CANFD";

        #endregion

        #region CRC-21

        /// <summary>
        /// CRC 21-bit checksum (CRC-21/CAN-FD)
        /// </summary>
        public const string CRC21_CANFD = "CRC21_CANFD";

        #endregion

        #region CRC-24

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/BLE)
        /// </summary>
        public const string CRC24_BLE = "CRC24_BLE";

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/FLEXRAY-A)
        /// </summary>
        public const string CRC24_FLEXRAYA = "CRC24_FLEXRAYA";

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/FLEXRAY-B)
        /// </summary>
        public const string CRC24_FLEXRAYB = "CRC24_FLEXRAYB";

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/INTERLAKEN)
        /// </summary>
        public const string CRC24_INTERLAKEN = "CRC24_INTERLAKEN";

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/LTE-A)
        /// </summary>
        public const string CRC24_LTEA = "CRC24_LTEA";

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/LTE-B)
        /// </summary>
        public const string CRC24_LTEB = "CRC24_LTEB";

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/OPENPGP)
        /// </summary>
        public const string CRC24_OPENPGP = "CRC24_OPENPGP";

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/OS-9)
        /// </summary>
        public const string CRC24_OS9 = "CRC24_OS9";

        #endregion

        #region CRC-30

        /// <summary>
        /// CRC 30-bit checksum (CRC-30/CDMA)
        /// </summary>
        public const string CRC30_CDMA = "CRC30_CDMA";

        #endregion

        #region CRC-31

        /// <summary>
        /// CRC 31-bit checksum (CRC-31/PHILIPS)
        /// </summary>
        public const string CRC31_PHILIPS = "CRC31_PHILIPS";

        #endregion

        #region CRC-32

        /// <summary>
        /// CRC 32-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC32_ISOHDLC"/>
        public const string CRC32 = "CRC32";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/AIXM)
        /// </summary>
        public const string CRC32_AIXM = "CRC32_AIXM";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/AUTOSAR)
        /// </summary>
        public const string CRC32_AUTOSAR = "CRC32_AUTOSAR";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/BASE91-D)
        /// </summary>
        public const string CRC32_BASE91D = "CRC32_BASE91D";

        /// <summary>
        /// CRC 32-bit checksum (BZIP2)
        /// </summary>
        public const string CRC32_BZIP2 = "CRC32_BZIP2";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/CD-ROM-EDC)
        /// </summary>
        public const string CRC32_CDROMEDC = "CRC32_CDROMEDC";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/CKSUM)
        /// </summary>
        public const string CRC32_CKSUM = "CRC32_CKSUM";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/DVD-ROM-EDC)
        /// </summary>
        public const string CRC32_DVDROMEDC = "CRC32_DVDROMEDC";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/ISCSI)
        /// </summary>
        public const string CRC32_ISCSI = "CRC32_ISCSI";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/ISO-HDLC)
        /// </summary>
        public const string CRC32_ISOHDLC = "CRC32_ISOHDLC";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/JAMCRC)
        /// </summary>
        public const string CRC32_JAMCRC = "CRC32_JAMCRC";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/MEF)
        /// </summary>
        public const string CRC32_MEF = "CRC32_MEF";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/MPEG-2)
        /// </summary>
        public const string CRC32_MPEG2 = "CRC32_MPEG2";

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/XFER)
        /// </summary>
        public const string CRC32_XFER = "CRC32_XFER";

        #endregion

        #region CRC-40

        /// <summary>
        /// CRC 40-bit checksum (CRC-40/GSM)
        /// </summary>
        public const string CRC40_GSM = "CRC40_GSM";

        #endregion

        #region CRC-64

        /// <summary>
        /// CRC 64-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC64_ECMA182"/>
        public const string CRC64 = "CRC64";

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/ECMA-182, Microsoft implementation)
        /// </summary>
        public const string CRC64_ECMA182 = "CRC64_ECMA182";

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/GO-ISO)
        /// </summary>
        public const string CRC64_GOISO = "CRC64_GOISO";

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/MS)
        /// </summary>
        public const string CRC64_MS = "CRC64_MS";

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/NVME)
        /// </summary>
        public const string CRC64_NVME = "CRC64_NVME";

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/REDIS)
        /// </summary>
        public const string CRC64_REDIS = "CRC64_REDIS";

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/WE)
        /// </summary>
        public const string CRC64_WE = "CRC64_WE";

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/XZ)
        /// </summary>
        public const string CRC64_XZ = "CRC64_XZ";

        #endregion

        #endregion

        #region Fletcher

        /// <summary>
        /// John G. Fletcher's 16-bit checksum
        /// </summary>
        public const string Fletcher16 = "Fletcher16";

        /// <summary>
        /// John G. Fletcher's 32-bit checksum
        /// </summary>
        public const string Fletcher32 = "Fletcher32";

        /// <summary>
        /// John G. Fletcher's 64-bit checksum
        /// </summary>
        public const string Fletcher64 = "Fletcher64";

        #endregion

        #region FNV

        /// <summary>
        /// FNV hash (Variant 0, 32-bit)
        /// </summary>
        public const string FNV0_32 = "FNV0_32";

        /// <summary>
        /// FNV hash (Variant 0, 64-bit)
        /// </summary>
        public const string FNV0_64 = "FNV0_64";

        /// <summary>
        /// FNV hash (Variant 1, 32-bit)
        /// </summary>
        public const string FNV1_32 = "FNV1_32";

        /// <summary>
        /// FNV hash (Variant 1, 64-bit)
        /// </summary>
        public const string FNV1_64 = "FNV1_64";

        /// <summary>
        /// FNV hash (Variant 1a, 32-bit)
        /// </summary>
        public const string FNV1a_32 = "FNV1a_32";

        /// <summary>
        /// FNV hash (Variant 1a, 64-bit)
        /// </summary>
        public const string FNV1a_64 = "FNV1a_64";

        #endregion

        /// <summary>
        /// Custom checksum used by MEKA
        /// </summary>
        public const string MekaCrc = "MekaCrc";

        #region Message Digest

        /// <summary>
        /// MD2 message-digest algorithm
        /// </summary>
        public const string MD2 = "MD2";

        /// <summary>
        /// MD4 message-digest algorithm
        /// </summary>
        public const string MD4 = "MD4";

        /// <summary>
        /// MD5 message-digest algorithm
        /// </summary>
        public const string MD5 = "MD5";

        #endregion

        #region RIPEMD

        /// <summary>
        /// RIPEMD-128 hash
        /// </summary>
        public const string RIPEMD128 = "RIPEMD128";

        /// <summary>
        /// RIPEMD-160 hash
        /// </summary>
        public const string RIPEMD160 = "RIPEMD160";

        /// <summary>
        /// RIPEMD-256 hash
        /// </summary>
        public const string RIPEMD256 = "RIPEMD256";

        /// <summary>
        /// RIPEMD-320 hash
        /// </summary>
        public const string RIPEMD320 = "RIPEMD320";

        #endregion

        #region SHA

        /// <summary>
        /// SHA-1 hash
        /// </summary>
        public const string SHA1 = "SHA1";

        /// <summary>
        /// SHA-256 hash
        /// </summary>
        public const string SHA256 = "SHA256";

        /// <summary>
        /// SHA-384 hash
        /// </summary>
        public const string SHA384 = "SHA384";

        /// <summary>
        /// SHA-512 hash
        /// </summary>
        public const string SHA512 = "SHA512";

#if NET8_0_OR_GREATER
        /// <summary>
        /// SHA3-256 hash
        /// </summary>
        public const string SHA3_256 = "SHA3_256";

        /// <summary>
        /// SHA3-384 hash
        /// </summary>
        public const string SHA3_384 = "SHA3_384";

        /// <summary>
        /// SHA3-512 hash
        /// </summary>
        public const string SHA3_512 = "SHA3_512";

        /// <summary>
        /// SHAKE128 SHA-3 family hash
        /// </summary>
        /// <remarks>Outputs a 256-bit (32-byte) hash</remarks>
        public const string SHAKE128 = "SHAKE128";

        /// <summary>
        /// SHAKE256 SHA-3 family hash
        /// </summary>
        /// <remarks>Outputs a 512-bit (64-byte) hash</remarks>
        public const string SHAKE256 = "SHAKE256";
#endif

        #endregion

        /// <summary>
        /// spamsum fuzzy hash
        /// </summary>
        public const string SpamSum = "SpamSum";

        #region Tiger

        /// <summary>
        /// Tiger 128-bit hash, 3 passes
        /// </summary>
        public const string Tiger128_3 = "Tiger128_3";

        /// <summary>
        /// Tiger 128-bit hash, 4 passes
        /// </summary>
        public const string Tiger128_4 = "Tiger128_4";

        /// <summary>
        /// Tiger 160-bit hash, 3 passes
        /// </summary>
        public const string Tiger160_3 = "Tiger160_3";

        /// <summary>
        /// Tiger 160-bit hash, 4 passes
        /// </summary>
        public const string Tiger160_4 = "Tiger160_4";

        /// <summary>
        /// Tiger 192-bit hash, 3 passes
        /// </summary>
        public const string Tiger192_3 = "Tiger192_3";

        /// <summary>
        /// Tiger 192-bit hash, 4 passes
        /// </summary>
        public const string Tiger192_4 = "Tiger192_4";

        /// <summary>
        /// Tiger2 128-bit hash, 3 passes
        /// </summary>
        public const string Tiger2_128_3 = "Tiger2_128_3";

        /// <summary>
        /// Tiger2 128-bit hash, 4 passes
        /// </summary>
        public const string Tiger2_128_4 = "Tiger2_128_4";

        /// <summary>
        /// Tiger2 160-bit hash, 3 passes
        /// </summary>
        public const string Tiger2_160_3 = "Tiger2_160_3";

        /// <summary>
        /// Tiger2 160-bit hash, 4 passes
        /// </summary>
        public const string Tiger2_160_4 = "Tiger2_160_4";

        /// <summary>
        /// Tiger2 192-bit hash, 3 passes
        /// </summary>
        public const string Tiger2_192_3 = "Tiger2_192_3";

        /// <summary>
        /// Tiger2 192-bit hash, 4 passes
        /// </summary>
        public const string Tiger2_192_4 = "Tiger2_192_4";

        #endregion

        #region xxHash

        /// <summary>
        /// xxHash32 hash
        /// </summary>
        public const string XxHash32 = "XxHash32";

        /// <summary>
        /// xxHash64 hash
        /// </summary>
        public const string XxHash64 = "XxHash64";

#if NET462_OR_GREATER || NETCOREAPP
        /// <summary>
        /// XXH3 64-bit hash
        /// </summary>
        public const string XxHash3 = "XxHash3";

        /// <summary>
        /// XXH128 128-bit hash
        /// </summary>
        public const string XxHash128 = "XxHash128";
#endif

        #endregion

        /// <summary>
        /// All supported hashes
        /// </summary>
        /// TODO: This shouldn't be hardcoded
        public static readonly string[] AllHashes =
        [
            Adler32,

    #if NET7_0_OR_GREATER
            BLAKE3,
    #endif

            CRC1_ZERO,
            CRC1_ONE,

            CRC3_GSM,
            CRC3_ROHC,

            CRC4_G704,
            CRC4_INTERLAKEN,

            CRC5_EPCC1G2,
            CRC5_G704,
            CRC5_USB,

            CRC6_CDMA2000A,
            CRC6_CDMA2000B,
            CRC6_DARC,
            CRC6_G704,
            CRC6_GSM,

            CRC7_MMC,
            CRC7_ROHC,
            CRC7_UMTS,

            CRC8,
            CRC8_AUTOSAR,
            CRC8_BLUETOOTH,
            CRC8_CDMA2000,
            CRC8_DARC,
            CRC8_DVBS2,
            CRC8_GSMA,
            CRC8_GSMB,
            CRC8_HITAG,
            CRC8_I4321,
            CRC8_ICODE,
            CRC8_LTE,
            CRC8_MAXIMDOW,
            CRC8_MIFAREMAD,
            CRC8_NRSC5,
            CRC8_OPENSAFETY,
            CRC8_ROHC,
            CRC8_SAEJ1850,
            CRC8_SMBUS,
            CRC8_TECH3250,
            CRC8_WCDMA,

            CRC10_ATM,
            CRC10_CDMA2000,
            CRC10_GSM,

            CRC11_FLEXRAY,
            CRC11_UMTS,

            CRC12_CDMA2000,
            CRC12_DECT,
            CRC12_GSM,
            CRC12_UMTS,

            CRC13_BBC,

            CRC14_DARC,
            CRC14_GSM,

            CRC15_CAN,
            CRC15_MPT1327,

            CRC16,
            CRC16_ARC,
            CRC16_CDMA2000,
            CRC16_CMS,
            CRC16_DDS110,
            CRC16_DECTR,
            CRC16_DECTX,
            CRC16_DNP,
            CRC16_EN13757,
            CRC16_GENIBUS,
            CRC16_GSM,
            CRC16_IBM3740,
            CRC16_IBMSDLC,
            CRC16_ISOIEC144433A,
            CRC16_KERMIT,
            CRC16_LJ1200,
            CRC16_M17,
            CRC16_MAXIMDOW,
            CRC16_MCRF4XX,
            CRC16_MODBUS,
            CRC16_NRSC5,
            CRC16_OPENSAFETYA,
            CRC16_OPENSAFETYB,
            CRC16_PROFIBUS,
            CRC16_RIELLO,
            CRC16_SPIFUJITSU,
            CRC16_T10DIF,
            CRC16_TELEDISK,
            CRC16_TMS37157,
            CRC16_UMTS,
            CRC16_USB,
            CRC16_XMODEM,

            CRC17_CANFD,

            CRC21_CANFD,

            CRC24_BLE,
            CRC24_FLEXRAYA,
            CRC24_FLEXRAYB,
            CRC24_INTERLAKEN,
            CRC24_LTEA,
            CRC24_LTEB,
            CRC24_OPENPGP,
            CRC24_OS9,

            CRC30_CDMA,

            CRC31_PHILIPS,

            CRC32,
            CRC32_AIXM,
            CRC32_AUTOSAR,
            CRC32_BASE91D,
            CRC32_BZIP2,
            CRC32_CDROMEDC,
            CRC32_CKSUM,
            CRC32_DVDROMEDC,
            CRC32_ISCSI,
            CRC32_ISOHDLC,
            CRC32_JAMCRC,
            CRC32_MEF,
            CRC32_MPEG2,
            CRC32_XFER,

            CRC40_GSM,

            CRC64,
            CRC64_ECMA182,
            CRC64_GOISO,
            CRC64_MS,
            CRC64_NVME,
            CRC64_REDIS,
            CRC64_WE,
            CRC64_XZ,

            Fletcher16,
            Fletcher32,
            Fletcher64,

            FNV0_32,
            FNV0_64,
            FNV1_32,
            FNV1_64,
            FNV1a_32,
            FNV1a_64,

            MekaCrc,

            MD2,
            MD4,
            MD5,

            RIPEMD128,
            RIPEMD160,
            RIPEMD256,
            RIPEMD320,

            SHA1,
            SHA256,
            SHA384,
            SHA512,
#if NET8_0_OR_GREATER
            SHA3_256,
            SHA3_384,
            SHA3_512,
            SHAKE128,
            SHAKE256,
#endif

            SpamSum,

            Tiger128_3,
            Tiger128_4,
            Tiger160_3,
            Tiger160_4,
            Tiger192_3,
            Tiger192_4,

            Tiger2_128_3,
            Tiger2_128_4,
            Tiger2_160_3,
            Tiger2_160_4,
            Tiger2_192_3,
            Tiger2_192_4,

            XxHash32,
            XxHash64,
#if NET462_OR_GREATER || NETCOREAPP
            XxHash3,
            XxHash128,
#endif
        ];
    }
}
