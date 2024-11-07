namespace SabreTools.Hashing.Crc
{
    /// <see href="https://reveng.sourceforge.io/crc-catalogue/all.htm#crc.legend"/>
    internal static class StandardDefinitions
    {
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