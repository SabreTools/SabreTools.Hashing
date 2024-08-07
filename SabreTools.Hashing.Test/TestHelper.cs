using System.Collections.Generic;
using Xunit;

namespace SabreTools.Hashing.Test
{
    /// <summary>
    /// Helper class for tests
    /// </summary>
    internal static class TestHelper
    {
        #region Known File Information

        private const long _hashFileSize = 125;
        private const string _adler32 = "08562d95";
#if NET7_0_OR_GREATER
        private const string _blake3 = "d4bd7ca6f1ebea9580d9381106b248eb5b6069170d0bfd00b17d659fcd10dcdc";
#endif
        private const string _crc16_ccitt = "482d";
        private const string _crc16_ibm = "7573";
        private const string _crc32 = "ba02a660";
        private const string _crc64 = "a0e0009c18b5338d";
        private const string _crc64_reversed = "fb49044e8331f6e5";
        private const string _fletcher16 = "46c1";
        private const string _fletcher32 = "073f2d94";
        private const string _md5 = "b722871eaa950016296184d026c5dec9";
#if NETFRAMEWORK
        private const string _ripemd160 = "346361e1d7fdb836650cecdb842b0dbe660eed66";
#endif
        private const string _sha1 = "eea1ee2d801d830c4bdad4df3c8da6f9f52d1a9f";
        private const string _sha256 = "fdb02dee8c319c52087382c45f099c90d0b6cc824850aff28c1bfb2884b7b855";
        private const string _sha384 = "e276c49618fff25bc1fe2e0659cd0ef0e7c1186563b063e07c52323b9899f3ce9b091be04d6208444b3ef1265e879074";
        private const string _sha512 = "15d69514eb628c2403e945a7cafd1d27e557f6e336c69b63ea17e7ed9d256cc374ee662f09305836d6de37fdae59d83883b982aa8446e4ff26346b6b6b50b240";
#if NET8_0_OR_GREATER
        private const string _sha3_256 = "1d76459e68c865b5911ada5104067cc604c5c60b345c4e81b3905e916a43c868";
        private const string _sha3_384 = "1bcbed87b73f25c0adf486c3afbf0ea3105763c387af3f8b2bd79b0a1964d42832b1d7c6a2225f9153ead26f442e8b67";
        private const string _sha3_512 = "89852144df37c58d01f5912124f1942dd00bac0346eb3971943416699c3094cff087fb42c356019c3d91f8e8f55b9254c8caec48e9414af6817297d06725ffeb";
        private const string _shake128 = "e5f88d0db79a71c39490beb9ebac21eaf4a5d6368438fca20f5e4ce77cfee9aa";
        private const string _shake256 = "24d9e83198bbc7baf4dcd293bfc35ae3fff05399786c37318f1b1ef85f41970c66926f8a2a1f912d96e2d8e45535af88a301a1c200697437c1a65d7e980344bc";
#endif
        private const string _spamsum = "3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL";
#if NET462_OR_GREATER || NETCOREAPP
        private const string _xxhash32 = "8e331daa";
        private const string _xxhash64 = "082bf6f0a49e1e18";
        private const string _xxhash3 = "040474eb0eda9ff2";
        private const string _xxhash128 = "d934b4b4a5e1e11baeef8012fbcd11e8";
#endif

        #endregion

        /// <summary>
        /// Validate the hashes in a hash dictionary
        /// </summary>
        public static void ValidateHashes(Dictionary<HashType, string?>? hashDict)
        {
            Assert.NotNull(hashDict);
            Assert.Equal(_adler32, hashDict![HashType.Adler32]);
#if NET7_0_OR_GREATER
            Assert.Equal(_blake3, hashDict[HashType.BLAKE3]);
#endif
            Assert.Equal(_crc16_ccitt, hashDict[HashType.CRC16_CCITT]);
            Assert.Equal(_crc16_ibm, hashDict[HashType.CRC16_IBM]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_ISO]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Naive]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Optimized]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Parallel]);
            Assert.Equal(_crc64, hashDict[HashType.CRC64]);
            Assert.Equal(_crc64_reversed, hashDict[HashType.CRC64_Reversed]);
            Assert.Equal(_fletcher16, hashDict[HashType.Fletcher16]);
            Assert.Equal(_fletcher32, hashDict[HashType.Fletcher32]);
            Assert.Equal(_md5, hashDict[HashType.MD5]);
            Assert.Equal(_sha1, hashDict[HashType.SHA1]);
            Assert.Equal(_sha256, hashDict[HashType.SHA256]);
            Assert.Equal(_sha384, hashDict[HashType.SHA384]);
            Assert.Equal(_sha512, hashDict[HashType.SHA512]);
#if NET8_0_OR_GREATER
            if (System.Security.Cryptography.SHA3_256.IsSupported)
                Assert.Equal(_sha3_256, hashDict[HashType.SHA3_256]);
            if (System.Security.Cryptography.SHA3_384.IsSupported)
                Assert.Equal(_sha3_384, hashDict[HashType.SHA3_384]);
            if (System.Security.Cryptography.SHA3_512.IsSupported)
                Assert.Equal(_sha3_512, hashDict[HashType.SHA3_512]);
            if (System.Security.Cryptography.Shake128.IsSupported)
                Assert.Equal(_shake128, hashDict[HashType.SHAKE128]);
            if (System.Security.Cryptography.Shake256.IsSupported)
                Assert.Equal(_shake256, hashDict[HashType.SHAKE256]);
#endif
            Assert.Equal(_spamsum, hashDict[HashType.SpamSum]);
#if NET462_OR_GREATER || NETCOREAPP
            Assert.Equal(_xxhash32, hashDict[HashType.XxHash32]);
            Assert.Equal(_xxhash64, hashDict[HashType.XxHash64]);
            Assert.Equal(_xxhash3, hashDict[HashType.XxHash3]);
            Assert.Equal(_xxhash128, hashDict[HashType.XxHash128]);
#endif
        }
    }
}