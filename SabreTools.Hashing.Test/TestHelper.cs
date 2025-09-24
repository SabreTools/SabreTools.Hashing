using System.Collections.Generic;
using Xunit;

namespace SabreTools.Hashing.Test
{
    /// <summary>
    /// Helper class for tests
    /// </summary>
    /// CRC values confirmed with <see href="https://emn178.github.io/online-tools/crc/"/> 
    internal static class TestHelper
    {
        #region Known File Information

        private const long _hashFileSize = 125;

        private static readonly Dictionary<HashType, string> _knownHashes = new()
        {
            {HashType.Adler32, "08562d95"},

#if NET7_0_OR_GREATER
            {HashType.BLAKE3, "d4bd7ca6f1ebea9580d9381106b248eb5b6069170d0bfd00b17d659fcd10dcdc"},
#endif

            {HashType.CRC1_ZERO, "0"},
            {HashType.CRC1_ONE, "1"},

            {HashType.CRC3_GSM, "4"},
            {HashType.CRC3_ROHC, "3"},

            {HashType.CRC4_G704, "6"},
            {HashType.CRC4_INTERLAKEN, "0"},

            {HashType.CRC5_EPCC1G2, "1f"},
            {HashType.CRC5_G704, "06"},
            {HashType.CRC5_USB, "0a"},

            {HashType.CRC6_CDMA2000A, "3c"},
            {HashType.CRC6_CDMA2000B, "12"},
            {HashType.CRC6_DARC, "0f"},
            {HashType.CRC6_G704, "09"},
            {HashType.CRC6_GSM, "21"},

            {HashType.CRC7_MMC, "2f"},
            {HashType.CRC7_ROHC, "68"},
            {HashType.CRC7_UMTS, "66"},

            {HashType.CRC8, "fc"},
            {HashType.CRC8_AUTOSAR, "ca"},
            {HashType.CRC8_BLUETOOTH, "00"},
            {HashType.CRC8_CDMA2000, "2d"},
            {HashType.CRC8_DARC, "35"},
            {HashType.CRC8_DVBS2, "5c"},
            {HashType.CRC8_GSMA, "d8"},
            {HashType.CRC8_GSMB, "f3"},
            {HashType.CRC8_HITAG, "aa"},
            {HashType.CRC8_I4321, "a9"},
            {HashType.CRC8_ICODE, "61"},
            {HashType.CRC8_LTE, "d7"},
            {HashType.CRC8_MAXIMDOW, "bd"},
            {HashType.CRC8_MIFAREMAD, "9b"},
            {HashType.CRC8_NRSC5, "e2"},
            {HashType.CRC8_OPENSAFETY, "fc"},
            {HashType.CRC8_ROHC, "17"},
            {HashType.CRC8_SAEJ1850, "55"},
            {HashType.CRC8_SMBUS, "fc"},
            {HashType.CRC8_TECH3250, "7d"},
            {HashType.CRC8_WCDMA, "c6"},

            {HashType.CRC10_ATM, "26b"},
            {HashType.CRC10_CDMA2000, "14f"},
            {HashType.CRC10_GSM, "0e7"},

            {HashType.CRC11_FLEXRAY, "18b"},
            {HashType.CRC11_UMTS, "347"},

            {HashType.CRC12_CDMA2000, "f9c"},
            {HashType.CRC12_DECT, "d62"},
            {HashType.CRC12_GSM, "975"},
            {HashType.CRC12_UMTS, "46b"},

            {HashType.CRC13_BBC, "074f"},

            {HashType.CRC14_DARC, "0add"},
            {HashType.CRC14_GSM, "0c7d"},

            {HashType.CRC15_CAN, "66c3"},
            {HashType.CRC15_MPT1327, "013b"},

            {HashType.CRC16, "7573"},
            {HashType.CRC16_ARC, "7573"},
            {HashType.CRC16_CDMA2000, "8b5f"},
            {HashType.CRC16_CMS, "1a37"},
            {HashType.CRC16_DDS110, "241d"},
            {HashType.CRC16_DECTR, "7390"},
            {HashType.CRC16_DECTX, "7391"},
            {HashType.CRC16_DNP, "4bbb"},
            {HashType.CRC16_EN13757, "e28b"},
            {HashType.CRC16_GENIBUS, "b65d"},
            {HashType.CRC16_GSM, "482d"},
            {HashType.CRC16_IBM3740, "49a2"},
            {HashType.CRC16_IBMSDLC, "4f52"},
            {HashType.CRC16_ISOIEC144433A, "85cd"},
            {HashType.CRC16_KERMIT, "bed2"},
            {HashType.CRC16_LJ1200, "3533"},
            {HashType.CRC16_M17, "5223"},
            {HashType.CRC16_MAXIMDOW, "8a8c"},
            {HashType.CRC16_MCRF4XX, "b0ad"},
            {HashType.CRC16_MODBUS, "9e54"},
            {HashType.CRC16_NRSC5, "4857"},
            {HashType.CRC16_OPENSAFETYA, "abcd"},
            {HashType.CRC16_OPENSAFETYB, "76f4"},
            {HashType.CRC16_PROFIBUS, "3099"},
            {HashType.CRC16_RIELLO, "23e0"},
            {HashType.CRC16_SPIFUJITSU, "f98b"},
            {HashType.CRC16_T10DIF, "2642"},
            {HashType.CRC16_TELEDISK, "7e05"},
            {HashType.CRC16_TMS37157, "dba0"},
            {HashType.CRC16_UMTS, "fee0"},
            {HashType.CRC16_USB, "61ab"},
            {HashType.CRC16_XMODEM, "b7d2"},

            {HashType.CRC17_CANFD, "0706d"},

            {HashType.CRC21_CANFD, "117d4b"},

            {HashType.CRC24_BLE, "2969f2"},
            {HashType.CRC24_FLEXRAYA, "ce9dc7"},
            {HashType.CRC24_FLEXRAYB, "0f49d7"},
            {HashType.CRC24_INTERLAKEN, "fb4725"},
            {HashType.CRC24_LTEA, "675e55"},
            {HashType.CRC24_LTEB, "c91203"},
            {HashType.CRC24_OPENPGP, "0c6012"},
            {HashType.CRC24_OS9, "610e21"},

            {HashType.CRC30_CDMA, "2ce682b2"},

            {HashType.CRC31_PHILIPS, "247c3cbe"},

            {HashType.CRC32, "ba02a660"},
            {HashType.CRC32_AIXM, "6174a75a"},
            {HashType.CRC32_AUTOSAR, "c050428e"},
            {HashType.CRC32_BASE91D, "e741ba25"},
            {HashType.CRC32_BZIP2, "18aa4603"},
            {HashType.CRC32_CDROMEDC, "b8ced467"},
            {HashType.CRC32_CKSUM, "f27b3c27"},
            {HashType.CRC32_ISCSI, "544d37db"},
            {HashType.CRC32_ISOHDLC, "ba02a660"},
            {HashType.CRC32_JAMCRC, "45fd599f"},
            {HashType.CRC32_MEF, "d9d98444"},
            {HashType.CRC32_MPEG2, "e755b9fc"},
            {HashType.CRC32_XFER, "55bdf222"},

            {HashType.CRC40_GSM, "c9843306eb"},

            {HashType.CRC64, "8d33b5189c00e0a0"},
            {HashType.CRC64_ECMA182, "8d33b5189c00e0a0"},
            {HashType.CRC64_GOISO, "6c3bf747ccfa1e3b"},
            {HashType.CRC64_MS, "799edc0db430d7be"},
            {HashType.CRC64_NVME, "9242023bbcf6bbf9"},
            {HashType.CRC64_REDIS, "408dab12b9f45dad"},
            {HashType.CRC64_WE, "91812be748f941c4"},
            {HashType.CRC64_XZ, "fb49044e8331f6e5"},

            {HashType.Fletcher16, "46c1"},
            {HashType.Fletcher32, "073f2d94"},
            {HashType.Fletcher64, "000b073400002d94"},

            {HashType.FNV0_32, "33d28b00"},
            {HashType.FNV0_64, "778e818addd23280"},
            {HashType.FNV1_32, "ac09cbeb"},
            {HashType.FNV1_64, "23229308c1f9252b"},
            {HashType.FNV1a_32, "9086769b"},
            {HashType.FNV1a_64, "399dd1cd965b73db"},

            {HashType.MD2, "362e1a6931668e6a9de5c159c52c71b5"},
            {HashType.MD4, "61bef59d7a754874fccbd67b4ec2fb10"},
            {HashType.MD5, "b722871eaa950016296184d026c5dec9"},

            {HashType.RIPEMD128, "6356cc18225245de3ca9afcb4fa22ce6"},
            {HashType.RIPEMD160, "346361e1d7fdb836650cecdb842b0dbe660eed66"},
            {HashType.RIPEMD256, "c2fe11922529651bc615be3d8a296820b6681ecaed5ce051439c86bf3d942276"},
            {HashType.RIPEMD320, "a523bec87b0738f89d8ae5cf0edd3ee9c7b9811f1051e32893e32e820db33841b9d5042e738d20c9"},

            {HashType.SHA1, "eea1ee2d801d830c4bdad4df3c8da6f9f52d1a9f"},
            {HashType.SHA256, "fdb02dee8c319c52087382c45f099c90d0b6cc824850aff28c1bfb2884b7b855"},
            {HashType.SHA384, "e276c49618fff25bc1fe2e0659cd0ef0e7c1186563b063e07c52323b9899f3ce9b091be04d6208444b3ef1265e879074"},
            {HashType.SHA512, "15d69514eb628c2403e945a7cafd1d27e557f6e336c69b63ea17e7ed9d256cc374ee662f09305836d6de37fdae59d83883b982aa8446e4ff26346b6b6b50b240"},
#if NET8_0_OR_GREATER
            {HashType.SHA3_256, "1d76459e68c865b5911ada5104067cc604c5c60b345c4e81b3905e916a43c868"},
            {HashType.SHA3_384, "1bcbed87b73f25c0adf486c3afbf0ea3105763c387af3f8b2bd79b0a1964d42832b1d7c6a2225f9153ead26f442e8b67"},
            {HashType.SHA3_512, "89852144df37c58d01f5912124f1942dd00bac0346eb3971943416699c3094cff087fb42c356019c3d91f8e8f55b9254c8caec48e9414af6817297d06725ffeb"},
            {HashType.SHAKE128, "e5f88d0db79a71c39490beb9ebac21eaf4a5d6368438fca20f5e4ce77cfee9aa"},
            {HashType.SHAKE256, "24d9e83198bbc7baf4dcd293bfc35ae3fff05399786c37318f1b1ef85f41970c66926f8a2a1f912d96e2d8e45535af88a301a1c200697437c1a65d7e980344bc"},
#endif

            {HashType.SpamSum, "3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL"},

            {HashType.Tiger128_3, "afc7ac1c3c031b675562f917b59f0885"},
            {HashType.Tiger128_4, "e7609126923009f733cd0fcbc5a733fa"},
            {HashType.Tiger160_3, "afc7ac1c3c031b675562f917b59f088533405e1a"},
            {HashType.Tiger160_4, "e7609126923009f733cd0fcbc5a733fa4f4ccf7a"},
            {HashType.Tiger192_3, "afc7ac1c3c031b675562f917b59f088533405e1a2f72912d"},
            {HashType.Tiger192_4, "e7609126923009f733cd0fcbc5a733fa4f4ccf7ab7c0e2a3"},
            {HashType.Tiger2_128_3, "b26271774e66519b1c746f210e0be05c"},
            {HashType.Tiger2_128_4, "f1df540d3f2521b87a957c9b2b00fc7c"},
            {HashType.Tiger2_160_3, "b26271774e66519b1c746f210e0be05c4fd9efde"},
            {HashType.Tiger2_160_4, "f1df540d3f2521b87a957c9b2b00fc7c589306dc"},
            {HashType.Tiger2_192_3, "b26271774e66519b1c746f210e0be05c4fd9efde26e46e89"},
            {HashType.Tiger2_192_4, "f1df540d3f2521b87a957c9b2b00fc7c589306dcf094acb5"},

            {HashType.XxHash32, "aa1d338e"},
            {HashType.XxHash64, "181e9ea4f0f62b08"},
#if NET462_OR_GREATER || NETCOREAPP
            {HashType.XxHash3, "f29fda0eeb740404"},
            {HashType.XxHash128, "e811cdfb1280efae1be1e1a5b4b434d9"},
#endif
        };

        public static readonly Dictionary<(string, string), int> _knownCompareHashes = new()
        {
            {
                ("3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL",
                    "3:hMCERJAFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:huRJdxZENFs+rPSromekL"),
                41
            },
            {
                ("12:Y+VH/3Ckg3xqMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMn:xHqVwMMMMMMMMMMMMMMMMMMMMMMMMMM0", 
                    "12:Oqkg3xqMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMu:OqVwMMMMMMMMMMMMMMMMMMMMMMMMMMMd"),
                44 
            },
            {
                ("6:l+lq/MtlM8pJ0gt6lXWogE61UlT1Uqj1akMD5n:l+l6Mtl/n0gtOXmEuUl5UqpakM9n", 
                    "6:mTj3qJskr+V+1o21+n0rtD2noPWKlAyjllZmMt6120EK+wlsS6T1oLwXuk4tk7:m/bk/1oQrJL3jTu20EK+wlsp5oO4tk7"),
                0
            },
        };
        #endregion

        /// <summary>
        /// Validate the hashes in a hash dictionary
        /// </summary>
        public static void ValidateHashes(Dictionary<HashType, string?>? hashDict)
        {
            Assert.NotNull(hashDict);
            foreach (var hashType in _knownHashes.Keys)
            {
                ValidateHash(hashType, hashDict![hashType]);
            }
        }

        /// <summary>
        /// Validate a single hash
        /// </summary>
        public static void ValidateHash(HashType hashType, string? hashValue)
            => Assert.Equal(_knownHashes[hashType], hashValue);

        /// <summary>
        /// Validate the file size
        /// </summary>
        public static void ValidateSize(long fileSize)
            => Assert.Equal(_hashFileSize, fileSize);
        
        /// <summary>
        /// Validate the compared SpamSum hashes in a dictionary
        /// </summary>
        public static void ValidateFuzzyCompares(Dictionary<(string, string), int> compareDict)
        {
            Assert.NotNull(compareDict);
            foreach (var hashes in _knownCompareHashes.Keys)
            {
                ValidateFuzzyCompare(hashes, compareDict![hashes]);
            }
        }

        /// <summary>
        /// Validate a single pair of compared SpamSum hashes
        /// </summary>
        public static void ValidateFuzzyCompare((string, string) hashes, int score)
            => Assert.Equal(_knownCompareHashes[hashes], score);
    }
}