using System.Collections.Generic;

namespace SabreTools.Hashing
{
    /// <summary>
    /// Zero-byte / empty hash
    /// </summary>
    public static class ZeroHash
    {
        #region Shortcuts for Common Hashes

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