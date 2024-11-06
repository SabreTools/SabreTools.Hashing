namespace SabreTools.Hashing.Crc
{
    /// <see href="https://reveng.sourceforge.io/crc-catalogue/all.htm#crc.legend"/>
    internal static class StandardDefinitions
    {
        #region CRC-32

        // CRC-32/AIXM
        public static readonly CrcDefinition CRC32_AIXM = new()
        {
            Width = 32,
            Poly = 0x814141ab,
            Init = 0x00000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00000000,
        };

        // CRC-32/AUTOSAR
        public static readonly CrcDefinition CRC32_AUTOSAR = new()
        {
            Width = 32,
            Poly = 0xf4acfb13,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffff,
        };

        // CRC-32/BASE91-D
        public static readonly CrcDefinition CRC32_BASE91D = new()
        {
            Width = 32,
            Poly = 0xa833982b,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffff,
        };

        // CRC-32/BZIP2
        public static readonly CrcDefinition CRC32_BZIP2 = new()
        {
            Width = 32,
            Poly = 0x04c11db7,
            Init = 0xffffffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffffffff,
        };

        // CRC-32/CD-ROM-EDC
        public static readonly CrcDefinition CRC32_CDROMEDC = new()
        {
            Width = 32,
            Poly = 0x8001801b,
            Init = 0x00000000,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00000000,
        };

        // CRC-32/CKSUM
        public static readonly CrcDefinition CRC32_CKSUM = new()
        {
            Width = 32,
            Poly = 0x04c11db7,
            Init = 0x00000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffffffff,
        };

        // CRC-32/ISCSI
        public static readonly CrcDefinition CRC32_ISCSI = new()
        {
            Width = 32,
            Poly = 0x1edc6f41,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffff,
        };

        // CRC-32/ISO-HDLC
        public static readonly CrcDefinition CRC32_ISOHDLC = new()
        {
            Width = 32,
            Poly = 0x04c11db7,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffff,
        };

        // CRC-32/JAMCRC
        public static readonly CrcDefinition CRC32_JAMCRC = new()
        {
            Width = 32,
            Poly = 0x04c11db7,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00000000,
        };

        // CRC-32/MEF
        public static readonly CrcDefinition CRC32_MEF = new()
        {
            Width = 32,
            Poly = 0x741b8cd7,
            Init = 0xffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x00000000,
        };

        // CRC-32/MPEG-2
        public static readonly CrcDefinition CRC32_MPEG2 = new()
        {
            Width = 32,
            Poly = 0x04c11db7,
            Init = 0xffffffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00000000,
        };

        // CRC-32/XFER
        public static readonly CrcDefinition CRC32_XFER = new()
        {
            Width = 32,
            Poly = 0x000000af,
            Init = 0x00000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x00000000,
        };

        #endregion

        #region CRC-64

        // CRC-64/ECMA-182
        public static readonly CrcDefinition CRC64_ECMA182 = new()
        {
            Width = 64,
            Poly = 0x42f0e1eba9ea3693,
            Init = 0x0000000000000000,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0x0000000000000000,
        };

        // CRC-64/GO-ISO
        public static readonly CrcDefinition CRC64_GOISO = new()
        {
            Width = 64,
            Poly = 0x000000000000001b,
            Init = 0xffffffffffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffffffffffff,
        };

        // CRC-64/MS
        public static readonly CrcDefinition CRC64_MS = new()
        {
            Width = 64,
            Poly = 0x259c84cba6426349,
            Init = 0xffffffffffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000000000000000,
        };

        // CRC-64/NVME
        public static readonly CrcDefinition CRC64_NVME = new()
        {
            Width = 64,
            Poly = 0xad93d23594c93659,
            Init = 0xffffffffffffffff,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0xffffffffffffffff,
        };

        // CRC-64/REDIS
        public static readonly CrcDefinition CRC64_REDIS = new()
        {
            Width = 64,
            Poly = 0xad93d23594c935a9,
            Init = 0x0000000000000000,
            ReflectIn = true,
            ReflectOut = true,
            XorOut = 0x0000000000000000,
        };

        // CRC-64/WE
        public static readonly CrcDefinition CRC64_WE = new()
        {
            Width = 64,
            Poly = 0x42f0e1eba9ea3693,
            Init = 0xffffffffffffffff,
            ReflectIn = false,
            ReflectOut = false,
            XorOut = 0xffffffffffffffff,
        };

        // CRC-64/XZ
        public static readonly CrcDefinition CRC64_XZ = new()
        {
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