namespace SabreTools.Hashing.Crc
{
    /// <see href="https://reveng.sourceforge.io/crc-catalogue/all.htm#crc.legend"/>
    public static class StandardDefinitions
    {
        #region CRC-3

        /// <summary>
        /// CRC-3/GSM
        /// </summary>
        public static readonly CrcDefinition CRC3_GSM = new()
        {
            Name = "CRC-3/GSM",
            Width = 3,
            Poly = 0x3,
            Init = 0x0,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x7,
        };

        /// <summary>
        /// CRC-3/ROHC
        /// </summary>
        public static readonly CrcDefinition CRC3_ROHC = new()
        {
            Name = "CRC-3/ROHC",
            Width = 3,
            Poly = 0x3,
            Init = 0x7,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0,
        };

        #endregion

        #region CRC-4

        /// <summary>
        /// CRC-4/G-704 [CRC-4/ITU]
        /// </summary>
        public static readonly CrcDefinition CRC4_G704 = new()
        {
            Name = "CRC-4/G-704",
            Width = 4,
            Poly = 0x3,
            Init = 0x0,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0,
        };

        /// <summary>
        /// CRC-4/INTERLAKEN
        /// </summary>
        public static readonly CrcDefinition CRC4_INTERLAKEN = new()
        {
            Name = "CRC-4/INTERLAKEN",
            Width = 4,
            Poly = 0x3,
            Init = 0xf,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xf,
        };

        #endregion

        #region CRC-5

        /// <summary>
        /// CRC-5/EPC-C1G2 [CRC-5/EPC]
        /// </summary>
        public static readonly CrcDefinition CRC5_EPCC1G2 = new()
        {
            Name = "CRC-5/EPC-C1G2",
            Width = 5,
            Poly = 0x09,
            Init = 0x09,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-5/G-704 [CRC-5/ITU]
        /// </summary>
        public static readonly CrcDefinition CRC5_G704 = new()
        {
            Name = "CRC-5/G-704",
            Width = 5,
            Poly = 0x15,
            Init = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-5/USB
        /// </summary>
        public static readonly CrcDefinition CRC5_USB = new()
        {
            Name = "CRC-5/USB",
            Width = 5,
            Poly = 0x05,
            Init = 0x1f,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x1f,
        };

        #endregion

        #region CRC-6

        /// <summary>
        /// CRC-6/CDMA2000-A
        /// </summary>
        public static readonly CrcDefinition CRC6_CDMA2000A = new()
        {
            Name = "CRC-6/CDMA2000-A",
            Width = 6,
            Poly = 0x27,
            Init = 0x3f,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-6/CDMA2000-B
        /// </summary>
        public static readonly CrcDefinition CRC6_CDMA2000B = new()
        {
            Name = "CRC-6/CDMA2000-B",
            Width = 6,
            Poly = 0x07,
            Init = 0x3f,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-6/DARC
        /// </summary>
        public static readonly CrcDefinition CRC6_DARC = new()
        {
            Name = "CRC-6/DARC",
            Width = 6,
            Poly = 0x19,
            Init = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-6/G-704 [CRC-6/ITU]
        /// </summary>
        public static readonly CrcDefinition CRC6_G704 = new()
        {
            Name = "CRC-6/G-704",
            Width = 6,
            Poly = 0x03,
            Init = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-6/GSM
        /// </summary>
        public static readonly CrcDefinition CRC6_GSM = new()
        {
            Name = "CRC-6/GSM",
            Width = 6,
            Poly = 0x2f,
            Init = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x3f,
        };

        #endregion

        #region CRC-7

        /// <summary>
        /// CRC-7/MMC [CRC-7]
        /// </summary>
        public static readonly CrcDefinition CRC7_MMC = new()
        {
            Name = "CRC-7/MMC",
            Width = 7,
            Poly = 0x09,
            Init = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-7/ROHC
        /// </summary>
        public static readonly CrcDefinition CRC7_ROHC = new()
        {
            Name = "CRC-7/ROHC",
            Width = 7,
            Poly = 0x4f,
            Init = 0x7f,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-7/UMTS
        /// </summary>
        public static readonly CrcDefinition CRC7_UMTS = new()
        {
            Name = "CRC-7/UMTS",
            Width = 7,
            Poly = 0x45,
            Init = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        #endregion

        #region CRC-8

        /// <summary>
        /// CRC-8/AUTOSAR
        /// </summary>
        public static readonly CrcDefinition CRC8_AUTOSAR = new()
        {
            Name = "CRC-8/AUTOSAR",
            Width = 8,
            Poly = 0x2f,
            Init = 0xff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xff,
        };

        /// <summary>
        /// CRC-8/BLUETOOTH
        /// </summary>
        public static readonly CrcDefinition CRC8_BLUETOOTH = new()
        {
            Name = "CRC-8/BLUETOOTH",
            Width = 8,
            Poly = 0xa7,
            Init = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/CDMA2000
        /// </summary>
        public static readonly CrcDefinition CRC8_CDMA2000 = new()
        {
            Name = "CRC-8/CDMA2000",
            Width = 8,
            Poly = 0x9b,
            Init = 0xff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/DARC
        /// </summary>
        public static readonly CrcDefinition CRC8_DARC = new()
        {
            Name = "CRC-8/DARC",
            Width = 8,
            Poly = 0x39,
            Init = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/DVB-S2
        /// </summary>
        public static readonly CrcDefinition CRC8_DVBS2 = new()
        {
            Name = "CRC-8/DVB-S2",
            Width = 8,
            Poly = 0xd5,
            Init = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/GSM-A
        /// </summary>
        public static readonly CrcDefinition CRC8_GSMA = new()
        {
            Name = "CRC-8/GSM-A",
            Width = 8,
            Poly = 0x1d,
            Init = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/GSM-B
        /// </summary>
        public static readonly CrcDefinition CRC8_GSMB = new()
        {
            Name = "CRC-8/GSM-B",
            Width = 8,
            Poly = 0x49,
            Init = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xff,
        };

        /// <summary>
        /// CRC-8/HITAG
        /// </summary>
        public static readonly CrcDefinition CRC8_HITAG = new()
        {
            Name = "CRC-8/HITAG",
            Width = 8,
            Poly = 0x1d,
            Init = 0xff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/I-432-1 [CRC-8/ITU]
        /// </summary>
        public static readonly CrcDefinition CRC8_I4321 = new()
        {
            Name = "CRC-8/I-432-1",
            Width = 8,
            Poly = 0x07,
            Init = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x55,
        };

        /// <summary>
        /// CRC-8/I-CODE
        /// </summary>
        public static readonly CrcDefinition CRC8_ICODE = new()
        {
            Name = "CRC-8/I-CODE",
            Width = 8,
            Poly = 0x1d,
            Init = 0xfd,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/LTE
        /// </summary>
        public static readonly CrcDefinition CRC8_LTE = new()
        {
            Name = "CRC-8/LTE",
            Width = 8,
            Poly = 0x9b,
            Init = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/MAXIM-DOW [CRC-8/MAXIM, DOW-CRC]
        /// </summary>
        public static readonly CrcDefinition CRC8_MAXIMDOW = new()
        {
            Name = "CRC-8/MAXIM-DOW",
            Width = 8,
            Poly = 0x31,
            Init = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/MIFARE-MAD
        /// </summary>
        public static readonly CrcDefinition CRC8_MIFAREMAD = new()
        {
            Name = "CRC-8/MIFARE-MAD",
            Width = 8,
            Poly = 0x1d,
            Init = 0xc7,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/NRSC-5
        /// </summary>
        public static readonly CrcDefinition CRC8_NRSC5 = new()
        {
            Name = "CRC-8/NRSC-5",
            Width = 8,
            Poly = 0x31,
            Init = 0xff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/OPENSAFETY
        /// </summary>
        public static readonly CrcDefinition CRC8_OPENSAFETY = new()
        {
            Name = "CRC-8/OPENSAFETY",
            Width = 8,
            Poly = 0x2f,
            Init = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/ROHC
        /// </summary>
        public static readonly CrcDefinition CRC8_ROHC = new()
        {
            Name = "CRC-8/ROHC",
            Width = 8,
            Poly = 0x07,
            Init = 0xff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/SAE-J1850
        /// </summary>
        public static readonly CrcDefinition CRC8_SAEJ1850 = new()
        {
            Name = "CRC-8/SAE-J1850",
            Width = 8,
            Poly = 0x1d,
            Init = 0xff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xff,
        };

        /// <summary>
        /// CRC-8/SMBUS [CRC-8]
        /// </summary>
        public static readonly CrcDefinition CRC8_SMBUS = new()
        {
            Name = "CRC-8/SMBUS",
            Width = 8,
            Poly = 0x07,
            Init = 0x00,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/TECH-3250 [CRC-8/AES, CRC-8/EBU]
        /// </summary>
        public static readonly CrcDefinition CRC8_TECH3250 = new()
        {
            Name = "CRC-8/TECH-3250",
            Width = 8,
            Poly = 0x1d,
            Init = 0xff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00,
        };

        /// <summary>
        /// CRC-8/WCDMA
        /// </summary>
        public static readonly CrcDefinition CRC8_WCDMA = new()
        {
            Name = "CRC-8/WCDMA",
            Width = 8,
            Poly = 0x9b,
            Init = 0x00,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00,
        };

        #endregion

        #region CRC-10

        /// <summary>
        /// CRC-10/ATM [CRC-10, CRC-10/I-610]
        /// </summary>
        public static readonly CrcDefinition CRC10_ATM = new()
        {
            Name = "CRC-10/ATM",
            Width = 10,
            Poly = 0x233,
            Init = 0x000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000,
        };

        /// <summary>
        /// CRC-10/CDMA2000
        /// </summary>
        public static readonly CrcDefinition CRC10_CDMA2000 = new()
        {
            Name = "CRC-10/CDMA2000",
            Width = 10,
            Poly = 0x3d9,
            Init = 0x3ff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000,
        };

        /// <summary>
        /// CRC-10/GSM
        /// </summary>
        public static readonly CrcDefinition CRC10_GSM = new()
        {
            Name = "CRC-10/GSM",
            Width = 10,
            Poly = 0x175,
            Init = 0x000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x3ff,
        };

        #endregion

        #region CRC-11

        /// <summary>
        /// CRC-11/FLEXRAY [CRC-11]
        /// </summary>
        public static readonly CrcDefinition CRC11_FLEXRAY = new()
        {
            Name = "CRC-11/FLEXRAY",
            Width = 11,
            Poly = 0x385,
            Init = 0x01a,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000,
        };

        /// <summary>
        /// CRC-11/UMTS
        /// </summary>
        public static readonly CrcDefinition CRC11_UMTS = new()
        {
            Name = "CRC-11/UMTS",
            Width = 11,
            Poly = 0x307,
            Init = 0x000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000,
        };

        #endregion

        #region CRC-12

        /// <summary>
        /// CRC-12/CDMA2000
        /// </summary>
        public static readonly CrcDefinition CRC12_CDMA2000 = new()
        {
            Name = "CRC-12/CDMA2000",
            Width = 12,
            Poly = 0xf13,
            Init = 0xfff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000,
        };

        /// <summary>
        /// CRC-12/DECT [X-CRC-12]
        /// </summary>
        public static readonly CrcDefinition CRC12_DECT = new()
        {
            Name = "CRC-12/DECT",
            Width = 12,
            Poly = 0x80f,
            Init = 0x000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000,
        };

        /// <summary>
        /// CRC-12/GSM
        /// </summary>
        public static readonly CrcDefinition CRC12_GSM = new()
        {
            Name = "CRC-12/GSM",
            Width = 12,
            Poly = 0xd31,
            Init = 0x000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xfff,
        };

        /// <summary>
        /// CRC-12/UMTS [CRC-12/3GPP]
        /// </summary>
        public static readonly CrcDefinition CRC12_UMTS = new()
        {
            Name = "CRC-12/UMTS",
            Width = 12,
            Poly = 0x80f,
            Init = 0x000,
            ReflectIn = false,
            ReflectOut = true,
            XorOut = 0x000,
        };

        #endregion

        #region CRC-13

        /// <summary>
        /// CRC-13/BBC
        /// </summary>
        public static readonly CrcDefinition CRC13_BBC = new()
        {
            Name = "CRC-13/BBC",
            Width = 13,
            Poly = 0x1cf5,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        #endregion

        #region CRC-14

        /// <summary>
        /// CRC-14/DARC
        /// </summary>
        public static readonly CrcDefinition CRC14_DARC = new()
        {
            Name = "CRC-14/DARC",
            Width = 14,
            Poly = 0x0805,
            Init = 0x0000,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-14/GSM
        /// </summary>
        public static readonly CrcDefinition CRC14_GSM = new()
        {
            Name = "CRC-14/GSM",
            Width = 14,
            Poly = 0x202d,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x3fff,
        };

        #endregion

        #region CRC-15

        /// <summary>
        /// CRC-15/CAN [CRC-15]
        /// </summary>
        public static readonly CrcDefinition CRC15_CAN = new()
        {
            Name = "CRC-15/CAN",
            Width = 15,
            Poly = 0x4599,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-15/MPT1327
        /// </summary>
        public static readonly CrcDefinition CRC15_MPT1327 = new()
        {
            Name = "CRC-15/MPT1327",
            Width = 15,
            Poly = 0x6815,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0001,
        };

        #endregion

        #region CRC-16

        /// <summary>
        /// CRC-16/ARC [ARC, CRC-16, CRC-16/LHA, CRC-IBM]
        /// </summary>
        public static readonly CrcDefinition CRC16_ARC = new()
        {
            Name = "CRC-16/ARC",
            Width = 16,
            Poly = 0x8005,
            Init = 0x0000,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/CDMA2000
        /// </summary>
        public static readonly CrcDefinition CRC16_CDMA2000 = new()
        {
            Name = "CRC-16/CDMA2000",
            Width = 16,
            Poly = 0xc867,
            Init = 0xffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/CMS
        /// </summary>
        public static readonly CrcDefinition CRC16_CMS = new()
        {
            Name = "CRC-16/CMS",
            Width = 16,
            Poly = 0x8005,
            Init = 0xffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/DDS-110
        /// </summary>
        public static readonly CrcDefinition CRC16_DDS110 = new()
        {
            Name = "CRC-16/DDS-110",
            Width = 16,
            Poly = 0x8005,
            Init = 0x800d,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/DECT-R [R-CRC-16]
        /// </summary>
        public static readonly CrcDefinition CRC16_DECTR = new()
        {
            Name = "CRC-16/DECT-R",
            Width = 16,
            Poly = 0x0589,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0001,
        };

        /// <summary>
        /// CRC-16/DECT-X [X-CRC-16]
        /// </summary>
        public static readonly CrcDefinition CRC16_DECTX = new()
        {
            Name = "CRC-16/DECT-X ",
            Width = 16,
            Poly = 0x0589,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/DNP
        /// </summary>
        public static readonly CrcDefinition CRC16_DNP = new()
        {
            Name = "CRC-16/DNP",
            Width = 16,
            Poly = 0x3d65,
            Init = 0x0000,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffff,
        };

        /// <summary>
        /// CRC-16/EN-13757
        /// </summary>
        public static readonly CrcDefinition CRC16_EN13757 = new()
        {
            Name = "CRC-16/EN-13757",
            Width = 16,
            Poly = 0x3d65,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffff,
        };

        /// <summary>
        /// CRC-16/GENIBUS [CRC-16/DARC, CRC-16/EPC, CRC-16/EPC-C1G2, CRC-16/I-CODE]
        /// </summary>
        public static readonly CrcDefinition CRC16_GENIBUS = new()
        {
            Name = "CRC-16/GENIBUS",
            Width = 16,
            Poly = 0x1021,
            Init = 0xffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffff,
        };

        /// <summary>
        /// CRC-16/GSM
        /// </summary>
        public static readonly CrcDefinition CRC16_GSM = new()
        {
            Name = "CRC-16/GSM",
            Width = 16,
            Poly = 0x1021,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffff,
        };

        /// <summary>
        /// CRC-16/IBM-3740 [CRC-16/AUTOSAR, CRC-16/CCITT-FALSE]
        /// </summary>
        public static readonly CrcDefinition CRC16_IBM3740 = new()
        {
            Name = "CRC-16/IBM-3740",
            Width = 16,
            Poly = 0x1021,
            Init = 0xffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/IBM-SDLC [CRC-16/ISO-HDLC, CRC-16/ISO-IEC-14443-3-B, CRC-16/X-25, CRC-B, X-25]
        /// </summary>
        public static readonly CrcDefinition CRC16_IBMSDLC = new()
        {
            Name = "CRC-16/IBM-SDLC",
            Width = 16,
            Poly = 0x1021,
            Init = 0xffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffff,
        };

        /// <summary>
        /// CRC-16/ISO-IEC-14443-3-A [CRC-A]
        /// </summary>
        public static readonly CrcDefinition CRC16_ISOIEC144433A = new()
        {
            Name = "CRC-16/ISO-IEC-14443-3-A",
            Width = 16,
            Poly = 0x1021,
            Init = 0xc6c6,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/KERMIT [CRC-16/BLUETOOTH, CRC-16/CCITT, CRC-16/CCITT-TRUE, CRC-16/V-41-LSB, CRC-CCITT, KERMIT]
        /// </summary>
        public static readonly CrcDefinition CRC16_KERMIT = new()
        {
            Name = "CRC-16/KERMIT",
            Width = 16,
            Poly = 0x1021,
            Init = 0x0000,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/LJ1200
        /// </summary>
        public static readonly CrcDefinition CRC16_LJ1200 = new()
        {
            Name = "CRC-16/LJ1200",
            Width = 16,
            Poly = 0x6f63,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/M17
        /// </summary>
        public static readonly CrcDefinition CRC16_M17 = new()
        {
            Name = "CRC-16/M17",
            Width = 16,
            Poly = 0x5935,
            Init = 0xffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/MAXIM-DOW [CRC-16/MAXIM]
        /// </summary>
        public static readonly CrcDefinition CRC16_MAXIMDOW = new()
        {
            Name = "CRC-16/MAXIM-DOW [CRC-16/MAXIM]",
            Width = 16,
            Poly = 0x8005,
            Init = 0x0000,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffff,
        };

        /// <summary>
        /// CRC-16/MCRF4XX
        /// </summary>
        public static readonly CrcDefinition CRC16_MCRF4XX = new()
        {
            Name = "CRC-16/MCRF4XX",
            Width = 16,
            Poly = 0x1021,
            Init = 0xffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/MODBUS [MODBUS]
        /// </summary>
        public static readonly CrcDefinition CRC16_MODBUS = new()
        {
            Name = "CRC-16/MODBUS",
            Width = 16,
            Poly = 0x8005,
            Init = 0xffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/NRSC-5
        /// </summary>
        public static readonly CrcDefinition CRC16_NRSC5 = new()
        {
            Name = "CRC-16/NRSC-5",
            Width = 16,
            Poly = 0x080b,
            Init = 0xffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/OPENSAFETY-A
        /// </summary>
        public static readonly CrcDefinition CRC16_OPENSAFETYA = new()
        {
            Name = "CRC-16/OPENSAFETY-A",
            Width = 16,
            Poly = 0x5935,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/OPENSAFETY-B
        /// </summary>
        public static readonly CrcDefinition CRC16_OPENSAFETYB = new()
        {
            Name = "CRC-16/OPENSAFETY-B",
            Width = 16,
            Poly = 0x755b,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/PROFIBUS [CRC-16/IEC-61158-2]
        /// </summary>
        public static readonly CrcDefinition CRC16_PROFIBUS = new()
        {
            Name = "CRC-16/PROFIBUS",
            Width = 16,
            Poly = 0x1dcf,
            Init = 0xffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffff,
        };

        /// <summary>
        /// CRC-16/RIELLO
        /// </summary>
        public static readonly CrcDefinition CRC16_RIELLO = new()
        {
            Name = "CRC-16/RIELLO",
            Width = 16,
            Poly = 0x1021,
            Init = 0xb2aa,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/SPI-FUJITSU [CRC-16/AUG-CCITT]
        /// </summary>
        public static readonly CrcDefinition CRC16_SPIFUJITSU = new()
        {
            Name = "CRC-16/SPI-FUJITSU",
            Width = 16,
            Poly = 0x1021,
            Init = 0x1d0f,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/T10-DIF
        /// </summary>
        public static readonly CrcDefinition CRC16_T10DIF = new()
        {
            Name = "CRC-16/T10-DIF",
            Width = 16,
            Poly = 0x8bb7,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/TELEDISK
        /// </summary>
        public static readonly CrcDefinition CRC16_TELEDISK = new()
        {
            Name = "CRC-16/TELEDISK",
            Width = 16,
            Poly = 0xa097,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/TMS37157
        /// </summary>
        public static readonly CrcDefinition CRC16_TMS37157 = new()
        {
            Name = "CRC-16/TMS37157",
            Width = 16,
            Poly = 0x1021,
            Init = 0x89ec,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/UMTS [CRC-16/BUYPASS, CRC-16/VERIFONE]
        /// </summary>
        public static readonly CrcDefinition CRC16_UMTS = new()
        {
            Name = "CRC-16/UMTS",
            Width = 16,
            Poly = 0x8005,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        /// <summary>
        /// CRC-16/USB
        /// </summary>
        public static readonly CrcDefinition CRC16_USB = new()
        {
            Name = "CRC-16/USB",
            Width = 16,
            Poly = 0x8005,
            Init = 0xffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffff,
        };

        /// <summary>
        /// CRC-16/XMODEM [CRC-16/ACORN, CRC-16/LTE, CRC-16/V-41-MSB, XMODEM, ZMODEM]
        /// </summary>
        public static readonly CrcDefinition CRC16_XMODEM = new()
        {
            Name = "CRC-16/XMODEM",
            Width = 16,
            Poly = 0x1021,
            Init = 0x0000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000,
        };

        #endregion

        #region CRC-17

        /// <summary>
        /// CRC-17/CAN-FD
        /// </summary>
        public static readonly CrcDefinition CRC17_CANFD = new()
        {
            Name = "CRC-17/CAN-FD",
            Width = 17,
            Poly = 0x1685b,
            Init = 0x00000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00000,
        };

        #endregion

        #region CRC-21

        /// <summary>
        /// CRC-21/CAN-FD
        /// </summary>
        public static readonly CrcDefinition CRC21_CANFD = new()
        {
            Name = "CRC-21/CAN-FD",
            Width = 21,
            Poly = 0x102899,
            Init = 0x000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000000,
        };

        #endregion

        #region CRC-24

        /// <summary>
        /// CRC-24/BLE
        /// </summary>
        public static readonly CrcDefinition CRC24_BLE = new()
        {
            Name = "CRC-24/BLE",
            Width = 24,
            Poly = 0x00065b,
            Init = 0x555555,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x000000,
        };

        /// <summary>
        /// CRC-24/FLEXRAY-A
        /// </summary>
        public static readonly CrcDefinition CRC24_FLEXRAYA = new()
        {
            Name = "CRC-24/FLEXRAY-A",
            Width = 24,
            Poly = 0x5d6dcb,
            Init = 0xfedcba,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000000,
        };

        /// <summary>
        /// CRC-24/FLEXRAY-B
        /// </summary>
        public static readonly CrcDefinition CRC24_FLEXRAYB = new()
        {
            Name = "CRC-24/FLEXRAY-B",
            Width = 24,
            Poly = 0x5d6dcb,
            Init = 0xabcdef,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000000,
        };

        /// <summary>
        /// CRC-24/INTERLAKEN
        /// </summary>
        public static readonly CrcDefinition CRC24_INTERLAKEN = new()
        {
            Name = "CRC-24/INTERLAKEN",
            Width = 24,
            Poly = 0x328b63,
            Init = 0xffffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffffff,
        };

        /// <summary>
        /// CRC-24/LTE-A
        /// </summary>
        public static readonly CrcDefinition CRC24_LTEA = new()
        {
            Name = "CRC-24/LTE-A",
            Width = 24,
            Poly = 0x864cfb,
            Init = 0x000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000000,
        };

        /// <summary>
        /// CRC-24/LTE-B
        /// </summary>
        public static readonly CrcDefinition CRC24_LTEB = new()
        {
            Name = "CRC-24/LTE-B",
            Width = 24,
            Poly = 0x800063,
            Init = 0x000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000000,
        };

        /// <summary>
        /// CRC-24/OPENPGP
        /// </summary>
        public static readonly CrcDefinition CRC24_OPENPGP = new()
        {
            Name = "CRC-24/OPENPGP",
            Width = 24,
            Poly = 0x864cfb,
            Init = 0xb704ce,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x000000,
        };

        /// <summary>
        /// CRC-24/OS-9
        /// </summary>
        public static readonly CrcDefinition CRC24_OS9 = new()
        {
            Name = "CRC-24/OS-9",
            Width = 24,
            Poly = 0x800063,
            Init = 0xffffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffffff,
        };

        #endregion

        #region CRC-30

        /// <summary>
        /// CRC-30/CDMA
        /// </summary>
        public static readonly CrcDefinition CRC30_CDMA = new()
        {
            Name = "CRC-30/CDMA",
            Width = 30,
            Poly = 0x2030b9c7,
            Init = 0x3fffffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x3fffffff,
        };

        #endregion

        #region CRC-31

        /// <summary>
        /// CRC-31/PHILIPS
        /// </summary>
        public static readonly CrcDefinition CRC31_PHILIPS = new()
        {
            Name = "CRC-31/PHILIPS",
            Width = 31,
            Poly = 0x04c11db7,
            Init = 0x7fffffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x7fffffff,
        };

        #endregion

        #region CRC-32

        /// <summary>
        /// CRC-32/AIXM
        /// </summary>
        public static readonly CrcDefinition CRC32_AIXM = new()
        {
            Name = "CRC-32/AIXM",
            Width = 32,
            Poly = 0x814141ab,
            Init = 0x00000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00000000,
        };

        /// <summary>
        /// CRC-32/AUTOSAR
        /// </summary>
        public static readonly CrcDefinition CRC32_AUTOSAR = new()
        {
            Name = "CRC-32/AUTOSAR",
            Width = 32,
            Poly = 0xf4acfb13,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffff,
        };

        /// <summary>
        /// CRC-32/BASE91-D
        /// </summary>
        public static readonly CrcDefinition CRC32_BASE91D = new()
        {
            Name = "CRC-32/BASE91-D",
            Width = 32,
            Poly = 0xa833982b,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffff,
        };

        /// <summary>
        /// CRC-32/BZIP2
        /// </summary>
        public static readonly CrcDefinition CRC32_BZIP2 = new()
        {
            Name = "CRC-32/BZIP2",
            Width = 32,
            Poly = 0x04c11db7,
            Init = 0xffffffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffffffff,
        };

        /// <summary>
        /// CRC-32/CD-ROM-EDC
        /// </summary>
        public static readonly CrcDefinition CRC32_CDROMEDC = new()
        {
            Name = "CRC-32/CD-ROM-EDC",
            Width = 32,
            Poly = 0x8001801b,
            Init = 0x00000000,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00000000,
        };

        /// <summary>
        /// CRC-32/CKSUM
        /// </summary>
        public static readonly CrcDefinition CRC32_CKSUM = new()
        {
            Name = "CRC-32/CKSUM",
            Width = 32,
            Poly = 0x04c11db7,
            Init = 0x00000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffffffff,
        };

        /// <summary>
        /// CRC-32/ISCSI
        /// </summary>
        public static readonly CrcDefinition CRC32_ISCSI = new()
        {
            Name = "CRC-32/ISCSI",
            Width = 32,
            Poly = 0x1edc6f41,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffff,
        };

        /// <summary>
        /// CRC-32/ISO-HDLC
        /// </summary>
        public static readonly CrcDefinition CRC32_ISOHDLC = new()
        {
            Name = "CRC-32/ISO-HDLC",
            Width = 32,
            Poly = 0x04c11db7,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffff,
        };

        /// <summary>
        /// CRC-32/JAMCRC
        /// </summary>
        public static readonly CrcDefinition CRC32_JAMCRC = new()
        {
            Name = "CRC-32/JAMCRC",
            Width = 32,
            Poly = 0x04c11db7,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00000000,
        };

        /// <summary>
        /// CRC-32/MEF
        /// </summary>
        public static readonly CrcDefinition CRC32_MEF = new()
        {
            Name = "CRC-32/MEF",
            Width = 32,
            Poly = 0x741b8cd7,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00000000,
        };

        /// <summary>
        /// CRC-32/MPEG-2
        /// </summary>
        public static readonly CrcDefinition CRC32_MPEG2 = new()
        {
            Name = "CRC-32/MPEG-2",
            Width = 32,
            Poly = 0x04c11db7,
            Init = 0xffffffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00000000,
        };

        /// <summary>
        /// CRC-32/XFER
        /// </summary>
        public static readonly CrcDefinition CRC32_XFER = new()
        {
            Name = "CRC-32/XFER",
            Width = 32,
            Poly = 0x000000af,
            Init = 0x00000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00000000,
        };

        #endregion

        #region CRC-40

        /// <summary>
        /// CRC-40/GSM
        /// </summary>
        public static readonly CrcDefinition CRC40_GSM = new()
        {
            Name = "CRC-40/GSM",
            Width = 40,
            Poly = 0x0004820009,
            Init = 0x0000000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffffffffff,
        };

        #endregion

        #region CRC-64

        /// <summary>
        /// CRC-64/ECMA-182
        /// </summary>
        public static readonly CrcDefinition CRC64_ECMA182 = new()
        {
            Name = "CRC-64/ECMA-182",
            Width = 64,
            Poly = 0x42f0e1eba9ea3693,
            Init = 0x0000000000000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000000000000000,
        };

        /// <summary>
        /// CRC-64/GO-ISO
        /// </summary>
        public static readonly CrcDefinition CRC64_GOISO = new()
        {
            Name = "CRC-64/GO-ISO",
            Width = 64,
            Poly = 0x000000000000001b,
            Init = 0xffffffffffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffffffffffff,
        };

        /// <summary>
        /// CRC-64/MS
        /// </summary>
        public static readonly CrcDefinition CRC64_MS = new()
        {
            Name = "CRC-64/MS",
            Width = 64,
            Poly = 0x259c84cba6426349,
            Init = 0xffffffffffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000000000000000,
        };

        /// <summary>
        /// CRC-64/NVME
        /// </summary>
        public static readonly CrcDefinition CRC64_NVME = new()
        {
            Name = "CRC-64/NVME",
            Width = 64,
            Poly = 0xad93d23594c93659,
            Init = 0xffffffffffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffffffffffff,
        };

        /// <summary>
        /// CRC-64/REDIS
        /// </summary>
        public static readonly CrcDefinition CRC64_REDIS = new()
        {
            Name = "CRC-64/REDIS",
            Width = 64,
            Poly = 0xad93d23594c935a9,
            Init = 0x0000000000000000,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000000000000000,
        };

        /// <summary>
        /// CRC-64/WE
        /// </summary>
        public static readonly CrcDefinition CRC64_WE = new()
        {
            Name = "CRC-64/WE",
            Width = 64,
            Poly = 0x42f0e1eba9ea3693,
            Init = 0xffffffffffffffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffffffffffffffff,
        };

        /// <summary>
        /// CRC-64/XZ
        /// </summary>
        public static readonly CrcDefinition CRC64_XZ = new()
        {
            Name = "CRC-64/XZ",
            Width = 64,
            Poly = 0x42f0e1eba9ea3693,
            Init = 0xffffffffffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffffffffffff,
        };

        #endregion
    }
}