namespace SabreTools.Hashing
{
    /// <summary>
    /// Represents a single hashing or checksumming type definition
    /// </summary>
    public sealed class HashType
    {
        #region Properties

        /// <summary>
        /// Name of hash or checksum type
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Long description of checksum type
        /// </summary>
        public readonly string Description;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private HashType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        #endregion

        #region Static Definitions

        /// <summary>
        /// Mark Adler's 32-bit checksum
        /// </summary>
        public static readonly HashType Adler32 = new("Adler32", "Mark Adler's 32-bit checksum");

#if NET7_0_OR_GREATER
        /// <summary>
        /// BLAKE3 512-bit digest
        /// </summary>
        public static readonly HashType BLAKE3 = new("BLAKE3", "BLAKE3 512-bit digest");
#endif

        #region CRC

        #region CRC-1

        /// <summary>
        /// CRC 1-bit checksum (CRC-1/ZERO [Parity bit with 0 start])
        /// </summary>
        public static readonly HashType CRC1_ZERO = new("CRC1_ZERO", "CRC-1/ZERO [Parity bit with 0 start]");

        /// <summary>
        /// CRC 1-bit checksum (CRC-1/ONE [Parity bit with 1 start])
        /// </summary>
        public static readonly HashType CRC1_ONE = new ("CRC1_ONE", "CRC-1/ONE [Parity bit with 1 start]");

        #endregion

        #region CRC-3

        /// <summary>
        /// CRC 3-bit checksum (CRC-3/GSM)
        /// </summary>
        public static readonly HashType CRC3_GSM = new("CRC3_GSM", "CRC-3/GSM");

        /// <summary>
        /// CRC 3-bit checksum (CRC-3/ROHC)
        /// </summary>
        public static readonly HashType CRC3_ROHC = new ("CRC3_ROHC", "CRC-3/ROHC");

        #endregion

        #region CRC-4

        /// <summary>
        /// CRC 4-bit checksum (CRC-4/G-704 [CRC-4/ITU])
        /// </summary>
        public static readonly HashType CRC4_G704 = new("CRC4_G704", "CRC-4/G-704 [CRC-4/ITU]");

        /// <summary>
        /// CRC 4-bit checksum (CRC-4/INTERLAKEN)
        /// </summary>
        public static readonly HashType CRC4_INTERLAKEN = new ("CRC4_INTERLAKEN", "CRC-4/INTERLAKEN");

        #endregion

        #region CRC-5

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/EPC-C1G2 [CRC-5/EPC])
        /// </summary>
        public static readonly HashType CRC5_EPCC1G2 = new("CRC5_EPCC1G2", "CRC-5/EPC-C1G2 [CRC-5/EPC]");

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/G-704 [CRC-5/ITU])
        /// </summary>
        public static readonly HashType CRC5_G704 = new("CRC5_G704", "CRC-5/G-704 [CRC-5/ITU]");

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/USB)
        /// </summary>
        public static readonly HashType CRC5_USB = new("CRC5_USB", "CRC-5/USB");

        #endregion

        #region CRC-6

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/CDMA2000-A)
        /// </summary>
        public static readonly HashType CRC6_CDMA2000A = new("CRC6_CDMA2000A", "CRC-6/CDMA2000-A");

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/CDMA2000-B)
        /// </summary>
        public static readonly HashType CRC6_CDMA2000B = new("CRC6_CDMA2000B", "CRC-6/CDMA2000-B");

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/DARC)
        /// </summary>
        public static readonly HashType CRC6_DARC = new("CRC6_DARC", "CRC-6/DARC");

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/G-704 [CRC-6/ITU])
        /// </summary>
        public static readonly HashType CRC6_G704 = new ("CRC6_G704", "CRC-6/G-704 [CRC-6/ITU]");

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/GSM)
        /// </summary>
        public static readonly HashType CRC6_GSM = new ("CRC6_GSM", "CRC-6/GSM");

        #endregion

        #region CRC-7

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/MMC [CRC-7])
        /// </summary>
        public static readonly HashType CRC7_MMC = new ("CRC7_MMC", "CRC-7/MMC [CRC-7]");

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/ROHC)
        /// </summary>
        public static readonly HashType CRC7_ROHC = new ("CRC7_ROHC", "CRC-7/ROHC");

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/UMTS)
        /// </summary>
        public static readonly HashType CRC7_UMTS = new ("CRC7_UMTS", "CRC-7/UMTS");

        #endregion

        #region CRC-8

        /// <summary>
        /// CRC 8-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC8_SMBUS"/>
        public static readonly HashType CRC8 = new("CRC8", "CRC-8");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/AUTOSAR)
        /// </summary>
        public static readonly HashType CRC8_AUTOSAR = new("CRC8_AUTOSAR", "CRC-8/AUTOSAR");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/BLUETOOTH)
        /// </summary>
        public static readonly HashType CRC8_BLUETOOTH = new("CRC8_BLUETOOTH", "CRC-8/BLUETOOTH");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/CDMA2000)
        /// </summary>
        public static readonly HashType CRC8_CDMA2000 = new ("CRC8_CDMA2000", "CRC-8/CDMA2000");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/DARC)
        /// </summary>
        public static readonly HashType CRC8_DARC = new ("CRC8_DARC", "CRC-8/DARC");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/DVB-S2)
        /// </summary>
        public static readonly HashType CRC8_DVBS2 = new ("CRC8_DVBS2", "CRC-8/DVB-S2");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/GSM-A)
        /// </summary>
        public static readonly HashType CRC8_GSMA = new ("CRC8_GSMA", "CRC-8/GSM-A");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/GSM-B)
        /// </summary>
        public static readonly HashType CRC8_GSMB = new ("CRC8_GSMB", "CRC-8/GSM-B");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/HITAG)
        /// </summary>
        public static readonly HashType CRC8_HITAG = new ("CRC8_HITAG", "CRC-8/HITAG");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/I-432-1 [CRC-8/ITU])
        /// </summary>
        public static readonly HashType CRC8_I4321 = new ("CRC8_I4321", "CRC-8/I-432-1 [CRC-8/ITU]");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/I-CODE)
        /// </summary>
        public static readonly HashType CRC8_ICODE = new ("CRC8_ICODE", "CRC-8/I-CODE");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/LTE)
        /// </summary>
        public static readonly HashType CRC8_LTE = new("CRC8_LTE", "CRC-8/LTE");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/MAXIM-DOW [CRC-8/MAXIM, DOW-CRC])
        /// </summary>
        public static readonly HashType CRC8_MAXIMDOW = new ("CRC8_MAXIMDOW", "CRC-8/MAXIM-DOW [CRC-8/MAXIM, DOW-CRC]");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/MIFARE-MAD)
        /// </summary>
        public static readonly HashType CRC8_MIFAREMAD = new ("CRC8_MIFAREMAD", "CRC-8/MIFARE-MAD");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/NRSC-5)
        /// </summary>
        public static readonly HashType CRC8_NRSC5 = new ("CRC8_NRSC5", "CRC-8/NRSC-5");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/OPENSAFETY)
        /// </summary>
        public static readonly HashType CRC8_OPENSAFETY = new ("CRC8_OPENSAFETY", "CRC-8/OPENSAFETY");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/ROHC)
        /// </summary>
        public static readonly HashType CRC8_ROHC = new ("CRC8_ROHC", "CRC-8/ROHC");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/SAE-J1850)
        /// </summary>
        public static readonly HashType CRC8_SAEJ1850 = new ("CRC8_SAEJ1850", "CRC-8/SAE-J1850");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/SMBUS [CRC-8])
        /// </summary>
        public static readonly HashType CRC8_SMBUS = new ("CRC8_SMBUS", "CRC-8/SMBUS [CRC-8]");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/TECH-3250 [CRC-8/AES, CRC-8/EBU])
        /// </summary>
        public static readonly HashType CRC8_TECH3250 = new ("CRC8_TECH3250", "CRC-8/TECH-3250 [CRC-8/AES, CRC-8/EBU]");

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/WCDMA)
        /// </summary>
        public static readonly HashType CRC8_WCDMA = new ("CRC8_WCDMA", "CRC-8/WCDMA");

        #endregion

        #region CRC-10

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/ATM [CRC-10, CRC-10/I-610])
        /// </summary>
        public static readonly HashType CRC10_ATM = new ("CRC10_ATM", "CRC-10/ATM [CRC-10, CRC-10/I-610]");

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/CDMA2000)
        /// </summary>
        public static readonly HashType CRC10_CDMA2000 = new ("CRC10_CDMA2000", "CRC-10/CDMA2000");

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/GSM)
        /// </summary>
        public static readonly HashType CRC10_GSM = new ("CRC10_GSM", "CRC-10/GSM");

        #endregion

        #region CRC-11

        /// <summary>
        /// CRC 11-bit checksum (CRC-11/FLEXRAY [CRC-11])
        /// </summary>
        public static readonly HashType CRC11_FLEXRAY = new ("CRC11_FLEXRAY", "CRC-11/FLEXRAY [CRC-11]");

        /// <summary>
        /// CRC 11-bit checksum (CRC-11/UMTS)
        /// </summary>
        public static readonly HashType CRC11_UMTS = new ("CRC11_UMTS", "CRC-11/UMTS");

        #endregion

        #region CRC-12

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/CDMA2000)
        /// </summary>
        public static readonly HashType CRC12_CDMA2000 = new ("CRC12_CDMA2000", "CRC-12/CDMA2000");

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/DECT [X-CRC-12])
        /// </summary>
        public static readonly HashType CRC12_DECT = new ("CRC12_DECT", "CRC-12/DECT [X-CRC-12]");

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/GSM)
        /// </summary>
        public static readonly HashType CRC12_GSM = new ("CRC12_GSM", "CRC-12/GSM");

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/UMTS [CRC-12/3GPP])
        /// </summary>
        public static readonly HashType CRC12_UMTS = new ("CRC12_UMTS", "CRC-12/UMTS [CRC-12/3GPP]");

        #endregion

        #region CRC-13

        /// <summary>
        /// CRC 13-bit checksum (CRC-13/BBC)
        /// </summary>
        public static readonly HashType CRC13_BBC = new("CRC13_BBC", "CRC-13/BBC");

        #endregion

        #region CRC-14

        /// <summary>
        /// CRC 14-bit checksum (CRC-14/DARC)
        /// </summary>
        public static readonly HashType CRC14_DARC = new("CRC14_DARC", "CRC-14/DARC");

        /// <summary>
        /// CRC 14-bit checksum (CRC-14/GSM)
        /// </summary>
        public static readonly HashType CRC14_GSM = new("CRC14_GSM", "CRC-14/GSM");

        #endregion

        #region CRC-15

        /// <summary>
        /// CRC 15-bit checksum (CRC-15/CAN [CRC-15])
        /// </summary>
        public static readonly HashType CRC15_CAN = new("CRC15_CAN", "CRC-15/CAN [CRC-15]");

        /// <summary>
        /// CRC 15-bit checksum (CRC-15/MPT1327)
        /// </summary>
        public static readonly HashType CRC15_MPT1327 = new("CRC15_MPT1327", "CRC-15/MPT1327");

        #endregion

        #region CRC-16

        /// <summary>
        /// CRC 16-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC16_ARC"/>
        public static readonly HashType CRC16 = new("CRC16", "CRC-16");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/ARC [ARC, CRC-16, CRC-16/LHA, CRC-IBM])
        /// </summary>
        public static readonly HashType CRC16_ARC = new("CRC16_ARC", "CRC-16/ARC [ARC, CRC-16, CRC-16/LHA, CRC-IBM]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/CDMA2000)
        /// </summary>
        public static readonly HashType CRC16_CDMA2000 = new("CRC16_CDMA2000", "CRC-16/CDMA2000");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/CMS)
        /// </summary>
        public static readonly HashType CRC16_CMS = new("CRC16_CMS", "CRC-16/CMS");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DDS-110)
        /// </summary>
        public static readonly HashType CRC16_DDS110 = new("CRC16_DDS110", "CRC-16/DDS-110");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DECT-R [R-CRC-16])
        /// </summary>
        public static readonly HashType CRC16_DECTR = new("CRC16_DECTR", "CRC-16/DECT-R [R-CRC-16]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DECT-X [X-CRC-16])
        /// </summary>
        public static readonly HashType CRC16_DECTX = new("CRC16_DECTX", "CRC-16/DECT-X [X-CRC-16]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DNP)
        /// </summary>
        public static readonly HashType CRC16_DNP = new("CRC16_DNP", "CRC-16/DNP");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/EN-13757)
        /// </summary>
        public static readonly HashType CRC16_EN13757 = new("CRC16_EN13757", "CRC-16/EN-13757");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/GENIBUS [CRC-16/DARC, CRC-16/EPC, CRC-16/EPC-C1G2, CRC-16/I-CODE])
        /// </summary>
        public static readonly HashType CRC16_GENIBUS = new("CRC16_GENIBUS", "CRC-16/GENIBUS [CRC-16/DARC, CRC-16/EPC, CRC-16/EPC-C1G2, CRC-16/I-CODE]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/GSM)
        /// </summary>
        public static readonly HashType CRC16_GSM = new("CRC16_GSM", "CRC-16/GSM");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/IBM-3740 [CRC-16/AUTOSAR, CRC-16/CCITT-FALSE])
        /// </summary>
        public static readonly HashType CRC16_IBM3740 = new("CRC16_IBM3740", "CRC-16/IBM-3740 [CRC-16/AUTOSAR, CRC-16/CCITT-FALSE]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/IBM-SDLC [CRC-16/ISO-HDLC, CRC-16/ISO-IEC-14443-3-B, CRC-16/X-25, CRC-B, X-25])
        /// </summary>
        public static readonly HashType CRC16_IBMSDLC = new("CRC16_IBMSDLC", "CRC-16/IBM-SDLC [CRC-16/ISO-HDLC, CRC-16/ISO-IEC-14443-3-B, CRC-16/X-25, CRC-B, X-25]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/ISO-IEC-14443-3-A [CRC-A])
        /// </summary>
        public static readonly HashType CRC16_ISOIEC144433A = new("CRC16_ISOIEC144433A", "CRC-16/ISO-IEC-14443-3-A [CRC-A]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/KERMIT [CRC-16/BLUETOOTH, CRC-16/CCITT, CRC-16/CCITT-TRUE, CRC-16/V-41-LSB, CRC-CCITT, KERMIT])
        /// </summary>
        public static readonly HashType CRC16_KERMIT = new("CRC16_KERMIT", "CRC-16/KERMIT [CRC-16/BLUETOOTH, CRC-16/CCITT, CRC-16/CCITT-TRUE, CRC-16/V-41-LSB, CRC-CCITT, KERMIT]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/LJ1200)
        /// </summary>
        public static readonly HashType CRC16_LJ1200 = new("CRC16_LJ1200", "CRC-16/LJ1200");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/M17)
        /// </summary>
        public static readonly HashType CRC16_M17 = new("CRC16_M17", "CRC-16/M17");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MAXIM-DOW [CRC-16/MAXIM])
        /// </summary>
        public static readonly HashType CRC16_MAXIMDOW = new("CRC16_MAXIMDOW", "CRC-16/MAXIM-DOW [CRC-16/MAXIM]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MCRF4XX)
        /// </summary>
        public static readonly HashType CRC16_MCRF4XX = new("CRC16_MCRF4XX", "CRC-16/MCRF4XX");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MODBUS [MODBUS])
        /// </summary>
        public static readonly HashType CRC16_MODBUS = new("CRC16_MODBUS", "CRC-16/MODBUS [MODBUS]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/NRSC-5)
        /// </summary>
        public static readonly HashType CRC16_NRSC5 = new("CRC16_NRSC5", "CRC-16/NRSC-5");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/OPENSAFETY-A)
        /// </summary>
        public static readonly HashType CRC16_OPENSAFETYA = new("CRC16_OPENSAFETYA", "CRC-16/OPENSAFETY-A");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/OPENSAFETY-B)
        /// </summary>
        public static readonly HashType CRC16_OPENSAFETYB = new("CRC16_OPENSAFETYB", "CRC-16/OPENSAFETY-B");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/PROFIBUS [CRC-16/IEC-61158-2])
        /// </summary>
        public static readonly HashType CRC16_PROFIBUS = new("CRC16_PROFIBUS", "CRC-16/PROFIBUS [CRC-16/IEC-61158-2]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/RIELLO)
        /// </summary>
        public static readonly HashType CRC16_RIELLO = new("CRC16_RIELLO", "CRC-16/RIELLO");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/SPI-FUJITSU [CRC-16/AUG-CCITT])
        /// </summary>
        public static readonly HashType CRC16_SPIFUJITSU = new("CRC16_SPIFUJITSU", "CRC-16/SPI-FUJITSU [CRC-16/AUG-CCITT]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/T10-DIF)
        /// </summary>
        public static readonly HashType CRC16_T10DIF = new("CRC16_T10DIF", "CRC-16/T10-DIF");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/TELEDISK)
        /// </summary>
        public static readonly HashType CRC16_TELEDISK = new("CRC16_TELEDISK", "CRC-16/TELEDISK");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/TMS37157)
        /// </summary>
        public static readonly HashType CRC16_TMS37157 = new("CRC16_TMS37157", "CRC-16/TMS37157");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/UMTS [CRC-16/BUYPASS, CRC-16/VERIFONE])
        /// </summary>
        public static readonly HashType CRC16_UMTS = new("CRC16_UMTS", "CRC-16/UMTS [CRC-16/BUYPASS, CRC-16/VERIFONE]");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/USB)
        /// </summary>
        public static readonly HashType CRC16_USB = new("CRC16_USB", "CRC-16/USB");

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/XMODEM [CRC-16/ACORN, CRC-16/LTE, CRC-16/V-41-MSB, XMODEM, ZMODEM])
        /// </summary>
        public static readonly HashType CRC16_XMODEM = new("CRC16_XMODEM", "CRC-16/XMODEM [CRC-16/ACORN, CRC-16/LTE, CRC-16/V-41-MSB, XMODEM, ZMODEM]");

        #endregion

        #region CRC-17

        /// <summary>
        /// CRC 17-bit checksum (CRC-17/CAN-FD)
        /// </summary>
        public static readonly HashType CRC17_CANFD = new("CRC17_CANFD", "CRC-17/CAN-FD");

        #endregion

        #region CRC-21

        /// <summary>
        /// CRC 21-bit checksum (CRC-21/CAN-FD)
        /// </summary>
        public static readonly HashType CRC21_CANFD = new("CRC21_CANFD", "CRC-21/CAN-FD");

        #endregion

        #region CRC-24

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/BLE)
        /// </summary>
        public static readonly HashType CRC24_BLE = new("CRC24_BLE", "CRC-24/BLE");

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/FLEXRAY-A)
        /// </summary>
        public static readonly HashType CRC24_FLEXRAYA = new("CRC24_FLEXRAYA", "CRC-24/FLEXRAY-A");

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/FLEXRAY-B)
        /// </summary>
        public static readonly HashType CRC24_FLEXRAYB = new("CRC24_FLEXRAYB", "CRC-24/FLEXRAY-B");

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/INTERLAKEN)
        /// </summary>
        public static readonly HashType CRC24_INTERLAKEN = new("CRC24_INTERLAKEN", "CRC-24/INTERLAKEN");

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/LTE-A)
        /// </summary>
        public static readonly HashType CRC24_LTEA = new("CRC24_LTEA", "CRC-24/LTE-A");

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/LTE-B)
        /// </summary>
        public static readonly HashType CRC24_LTEB = new("CRC24_LTEB", "CRC-24/LTE-B");

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/OPENPGP)
        /// </summary>
        public static readonly HashType CRC24_OPENPGP = new("CRC24_OPENPGP", "CRC-24/OPENPGP");

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/OS-9)
        /// </summary>
        public static readonly HashType CRC24_OS9 = new("CRC24_OS9", "CRC-24/OS-9");

        #endregion

        #region CRC-30

        /// <summary>
        /// CRC 30-bit checksum (CRC-30/CDMA)
        /// </summary>
        public static readonly HashType CRC30_CDMA = new("CRC30_CDMA", "CRC-30/CDMA");

        #endregion

        #region CRC-31

        /// <summary>
        /// CRC 31-bit checksum (CRC-31/PHILIPS)
        /// </summary>
        public static readonly HashType CRC31_PHILIPS = new("CRC31_PHILIPS", "CRC-31/PHILIPS");

        #endregion

        #region CRC-32

        /// <summary>
        /// CRC 32-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC32_ISOHDLC"/>
        public static readonly HashType CRC32 = new("CRC32", "CRC-32");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/AIXM)
        /// </summary>
        public static readonly HashType CRC32_AIXM = new("CRC32_AIXM", "CRC-32/AIXM");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/AUTOSAR)
        /// </summary>
        public static readonly HashType CRC32_AUTOSAR = new("CRC32_AUTOSAR", "CRC-32/AUTOSAR");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/BASE91-D)
        /// </summary>
        public static readonly HashType CRC32_BASE91D = new("CRC32_BASE91D", "CRC-32/BASE91-D");

        /// <summary>
        /// CRC 32-bit checksum (BZIP2)
        /// </summary>
        public static readonly HashType CRC32_BZIP2 = new("CRC32_BZIP2", "BZIP2");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/CD-ROM-EDC)
        /// </summary>
        public static readonly HashType CRC32_CDROMEDC = new("CRC32_CDROMEDC", "CRC-32/CD-ROM-EDC");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/CKSUM)
        /// </summary>
        public static readonly HashType CRC32_CKSUM = new("CRC32_CKSUM", "CRC-32/CKSUM");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/DVD-ROM-EDC)
        /// </summary>
        public static readonly HashType CRC32_DVDROMEDC = new("CRC32_DVDROMEDC", "CRC-32/DVD-ROM-EDC");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/ISCSI)
        /// </summary>
        public static readonly HashType CRC32_ISCSI = new("CRC32_ISCSI", "CRC-32/ISCSI");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/ISO-HDLC)
        /// </summary>
        public static readonly HashType CRC32_ISOHDLC = new("CRC32_ISOHDLC", "CRC-32/ISO-HDLC");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/JAMCRC)
        /// </summary>
        public static readonly HashType CRC32_JAMCRC = new("CRC32_JAMCRC", "CRC-32/JAMCRC");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/MEF)
        /// </summary>
        public static readonly HashType CRC32_MEF = new("CRC32_MEF", "CRC-32/MEF");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/MPEG-2)
        /// </summary>
        public static readonly HashType CRC32_MPEG2 = new("CRC32_MPEG2", "CRC-32/MPEG-2");

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/XFER)
        /// </summary>
        public static readonly HashType CRC32_XFER = new("CRC32_XFER", "CRC-32/XFER");

        #endregion

        #region CRC-40

        /// <summary>
        /// CRC 40-bit checksum (CRC-40/GSM)
        /// </summary>
        public static readonly HashType CRC40_GSM = new("CRC40_GSM", "CRC-40/GSM");

        #endregion

        #region CRC-64

        /// <summary>
        /// CRC 64-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC64_ECMA182"/>
        public static readonly HashType CRC64 = new("CRC64", "CRC-64");

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/ECMA-182, Microsoft implementation)
        /// </summary>
        public static readonly HashType CRC64_ECMA182 = new("CRC64_ECMA182", "CRC-64/ECMA-182, Microsoft implementation");

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/GO-ISO)
        /// </summary>
        public static readonly HashType CRC64_GOISO = new("CRC64_GOISO", "CRC-64/GO-ISO");

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/MS)
        /// </summary>
        public static readonly HashType CRC64_MS = new("CRC64_MS", "CRC-64/MS");

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/NVME)
        /// </summary>
        public static readonly HashType CRC64_NVME = new("CRC64_NVME", "CRC-64/NVME");

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/REDIS)
        /// </summary>
        public static readonly HashType CRC64_REDIS = new("CRC64_REDIS", "CRC-64/REDIS");

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/WE)
        /// </summary>
        public static readonly HashType CRC64_WE = new("CRC64_WE", "CRC-64/WE");

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/XZ)
        /// </summary>
        public static readonly HashType CRC64_XZ = new("CRC64_XZ", "CRC-64/XZ");

        #endregion

        #endregion

        #region Fletcher

        /// <summary>
        /// John G. Fletcher's 16-bit checksum
        /// </summary>
        public static readonly HashType Fletcher16 = new("Fletcher16", "John G. Fletcher's 16-bit checksum");

        /// <summary>
        /// John G. Fletcher's 32-bit checksum
        /// </summary>
        public static readonly HashType Fletcher32 = new("Fletcher32", "John G. Fletcher's 32-bit checksum");

        /// <summary>
        /// John G. Fletcher's 64-bit checksum
        /// </summary>
        public static readonly HashType Fletcher64 = new("Fletcher64", "John G. Fletcher's 64-bit checksum");

        #endregion

        #region FNV

        /// <summary>
        /// FNV hash (Variant 0, 32-bit)
        /// </summary>
        public static readonly HashType FNV0_32 = new("FNV0_32", "FNV hash (Variant 0, 32-bit)");

        /// <summary>
        /// FNV hash (Variant 0, 64-bit)
        /// </summary>
        public static readonly HashType FNV0_64 = new("FNV0_64", "FNV hash (Variant 0, 64-bit)");

        /// <summary>
        /// FNV hash (Variant 1, 32-bit)
        /// </summary>
        public static readonly HashType FNV1_32 = new("FNV1_32", "FNV hash (Variant 1, 32-bit)");

        /// <summary>
        /// FNV hash (Variant 1, 64-bit)
        /// </summary>
        public static readonly HashType FNV1_64 = new("FNV1_64", "FNV hash (Variant 1, 64-bit)");

        /// <summary>
        /// FNV hash (Variant 1a, 32-bit)
        /// </summary>
        public static readonly HashType FNV1a_32 = new("FNV1a_32", "FNV hash (Variant 1a, 32-bit)");

        /// <summary>
        /// FNV hash (Variant 1a, 64-bit)
        /// </summary>
        public static readonly HashType FNV1a_64 = new("FNV1a_64", "FNV hash (Variant 1a, 64-bit)");

        #endregion

        /// <summary>
        /// Custom checksum used by MEKA
        /// </summary>
        public static readonly HashType MekaCrc = new("MekaCrc", "Custom MEKA checksum");

        #region Message Digest

        /// <summary>
        /// MD2 message-digest algorithm
        /// </summary>
        public static readonly HashType MD2 = new("MD2", "MD2 message-digest algorithm");

        /// <summary>
        /// MD4 message-digest algorithm
        /// </summary>
        public static readonly HashType MD4 = new("MD4", "MD4 message-digest algorithm");

        /// <summary>
        /// MD5 message-digest algorithm
        /// </summary>
        public static readonly HashType MD5 = new("MD5", "MD5 message-digest algorithm");

        #endregion

        #region RIPEMD

        /// <summary>
        /// RIPEMD-128 hash
        /// </summary>
        public static readonly HashType RIPEMD128 = new("RIPEMD128", "RIPE 128-bit message digest");

        /// <summary>
        /// RIPEMD-160 hash
        /// </summary>
        public static readonly HashType RIPEMD160 = new("RIPEMD160", "RIPE 160-bit message digest");

        /// <summary>
        /// RIPEMD-256 hash
        /// </summary>
        public static readonly HashType RIPEMD256 = new("RIPEMD256", "RIPE 256-bit message digest");

        /// <summary>
        /// RIPEMD-320 hash
        /// </summary>
        public static readonly HashType RIPEMD320 = new("RIPEMD320", "RIPE 320-bit message digest");

        #endregion

        #region SHA

        /// <summary>
        /// SHA-1 hash
        /// </summary>
        public static readonly HashType SHA1 = new("SHA1", "SHA-1 hash");

        /// <summary>
        /// SHA-256 hash
        /// </summary>
        public static readonly HashType SHA256 = new("SHA256", "SHA-256 hash");

        /// <summary>
        /// SHA-384 hash
        /// </summary>
        public static readonly HashType SHA384 = new("SHA384", "SHA-384 hash");

        /// <summary>
        /// SHA-512 hash
        /// </summary>
        public static readonly HashType SHA512 = new("SHA512", "SHA-512 hash");

#if NET8_0_OR_GREATER
        /// <summary>
        /// SHA3-256 hash
        /// </summary>
        public static readonly HashType SHA3_256 = new("SHA3_256", "SHA3-256 hash");

        /// <summary>
        /// SHA3-384 hash
        /// </summary>
        public static readonly HashType SHA3_384 = new("SHA3_384", "SHA3-384 hash");

        /// <summary>
        /// SHA3-512 hash
        /// </summary>
        public static readonly HashType SHA3_512 = new("SHA3_512", "SHA3-512 hash");

        /// <summary>
        /// SHAKE128 SHA-3 family hash
        /// </summary>
        /// <remarks>Outputs a 256-bit (32-byte) hash</remarks>
        public static readonly HashType SHAKE128 = new("SHAKE128", "SHAKE128 SHA-3 family hash (256-bit)");

        /// <summary>
        /// SHAKE256 SHA-3 family hash
        /// </summary>
        /// <remarks>Outputs a 512-bit (64-byte) hash</remarks>
        public static readonly HashType SHAKE256 = new("SHAKE256", "SHAKE256 SHA-3 family hash (512-bit)");
#endif

        #endregion

        /// <summary>
        /// spamsum fuzzy hash
        /// </summary>
        public static readonly HashType SpamSum = new("SpamSum", "spamsum fuzzy hash");

        #region Tiger

        /// <summary>
        /// Tiger 128-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger128_3 = new("Tiger128_3", "Tiger 128-bit hash, 3 passes");

        /// <summary>
        /// Tiger 128-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger128_4 = new("Tiger128_4", "Tiger 128-bit hash, 4 passes");

        /// <summary>
        /// Tiger 160-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger160_3 = new("Tiger160_3", "Tiger 160-bit hash, 3 passes");

        /// <summary>
        /// Tiger 160-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger160_4 = new("Tiger160_4", "Tiger 160-bit hash, 4 passes");

        /// <summary>
        /// Tiger 192-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger192_3 = new("Tiger192_3", "Tiger 192-bit hash, 3 passes");

        /// <summary>
        /// Tiger 192-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger192_4 = new("Tiger192_4", "Tiger 192-bit hash, 4 passes");

        /// <summary>
        /// Tiger2 128-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger2_128_3 = new("Tiger2_128_3", "Tiger2 128-bit hash, 3 passes");

        /// <summary>
        /// Tiger2 128-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger2_128_4 = new("Tiger2_128_4", "Tiger2 128-bit hash, 4 passes");

        /// <summary>
        /// Tiger2 160-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger2_160_3 = new("Tiger2_160_3", "Tiger2 160-bit hash, 3 passes");

        /// <summary>
        /// Tiger2 160-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger2_160_4 = new("Tiger2_160_4", "Tiger2 160-bit hash, 4 passes");

        /// <summary>
        /// Tiger2 192-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger2_192_3 = new("Tiger2_192_3", "Tiger2 192-bit hash, 3 passes");

        /// <summary>
        /// Tiger2 192-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger2_192_4 = new("Tiger2_192_4", "Tiger2 192-bit hash, 4 passes");

        #endregion

        #region xxHash

        /// <summary>
        /// xxHash32 hash
        /// </summary>
        public static readonly HashType XxHash32 = new("XxHash32", "xxHash32 hash");

        /// <summary>
        /// xxHash64 hash
        /// </summary>
        public static readonly HashType XxHash64 = new("XxHash64", "xxHash64 hash");

#if NET462_OR_GREATER || NETCOREAPP
        /// <summary>
        /// XXH3 64-bit hash
        /// </summary>
        public static readonly HashType XxHash3 = new("XxHash3", "XXH3 64-bit hash");

        /// <summary>
        /// XXH128 128-bit hash
        /// </summary>
        public static readonly HashType XxHash128 = new("XxHash128", "XXH128 128-bit hash");
#endif

        #endregion

        /// <summary>
        /// All supported hashes
        /// </summary>
        /// TODO: This shouldn't be hardcoded
        public static readonly HashType[] AllHashes =
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

        #endregion
    }
}
