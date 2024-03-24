using System;
using System.IO;
using Xunit;

namespace SabreTools.Hashing.Test
{
    public class HashToolTests
    {
        private static readonly string _hashFilePath = Path.Combine(Environment.CurrentDirectory, "TestData", "file-to-hash.bin");

        #region Known File Information

        private const long _hashFileSize = 125;
        private const string _blake3 = "d4bd7ca6f1ebea9580d9381106b248eb5b6069170d0bfd00b17d659fcd10dcdc";
        private const string _crc32 = "ba02a660";
        private const string _crc64 = "a0e0009c18b5338d";
        private const string _md5 = "b722871eaa950016296184d026c5dec9";
        private const string _ripemd160 = "346361e1d7fdb836650cecdb842b0dbe660eed66";
        private const string _sha1 = "eea1ee2d801d830c4bdad4df3c8da6f9f52d1a9f";
        private const string _sha256 = "fdb02dee8c319c52087382c45f099c90d0b6cc824850aff28c1bfb2884b7b855";
        private const string _sha384 = "e276c49618fff25bc1fe2e0659cd0ef0e7c1186563b063e07c52323b9899f3ce9b091be04d6208444b3ef1265e879074";
        private const string _sha512 = "15d69514eb628c2403e945a7cafd1d27e557f6e336c69b63ea17e7ed9d256cc374ee662f09305836d6de37fdae59d83883b982aa8446e4ff26346b6b6b50b240";
        private const string _spamsum = "3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL";
        private const string _xxhash32 = "8e331daa";
        private const string _xxhash64 = "082bf6f0a49e1e18";
        private const string _xxhash3 = "040474eb0eda9ff2";
        private const string _xxhash128 = "d934b4b4a5e1e11baeef8012fbcd11e8";

        #endregion

        [Fact]
        public void GetStandardHashesTest()
        {
            bool gotHashes = HashTool.GetStandardHashes(_hashFilePath, out long actualSize, out string? crc32, out string? md5, out string? sha1);

            Assert.True(gotHashes);
            Assert.Equal(_hashFileSize, actualSize);
            Assert.Equal(_crc32, crc32);
            Assert.Equal(_md5, md5);
            Assert.Equal(_sha1, sha1);
        }

        [Fact]
        public void GetFileHashesTest()
        {
            var hashDict = HashTool.GetFileHashes(_hashFilePath);

            Assert.NotNull(hashDict);
#if NET7_0_OR_GREATER
            Assert.Equal(_blake3, hashDict![HashType.BLAKE3]);
#endif
            Assert.Equal(_crc32, hashDict![HashType.CRC32]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Naive]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Optimized]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Parallel]);
#if NET462_OR_GREATER || NETCOREAPP
            Assert.Equal(_crc64, hashDict[HashType.CRC64]);
#endif
            Assert.Equal(_md5, hashDict[HashType.MD5]);
#if NETFRAMEWORK
            Assert.Equal(_ripemd160, hashDict[HashType.RIPEMD160]);
#endif
            Assert.Equal(_sha1, hashDict[HashType.SHA1]);
            Assert.Equal(_sha256, hashDict[HashType.SHA256]);
            Assert.Equal(_sha384, hashDict[HashType.SHA384]);
            Assert.Equal(_sha512, hashDict[HashType.SHA512]);
            Assert.Equal(_spamsum, hashDict[HashType.SpamSum]);
#if NET462_OR_GREATER || NETCOREAPP
            Assert.Equal(_xxhash32, hashDict[HashType.XxHash32]);
            Assert.Equal(_xxhash64, hashDict[HashType.XxHash64]);
            Assert.Equal(_xxhash3, hashDict[HashType.XxHash3]);
            Assert.Equal(_xxhash128, hashDict[HashType.XxHash128]);
#endif
        }

        [Fact]
        public void GetFileHashesAndSizeTest()
        {
            var hashDict = HashTool.GetFileHashesAndSize(_hashFilePath, out long actualSize);

            Assert.Equal(_hashFileSize, actualSize);
            Assert.NotNull(hashDict);
#if NET7_0_OR_GREATER
            Assert.Equal(_blake3, hashDict![HashType.BLAKE3]);
#endif
            Assert.Equal(_crc32, hashDict![HashType.CRC32]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Naive]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Optimized]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Parallel]);
#if NET462_OR_GREATER || NETCOREAPP
            Assert.Equal(_crc64, hashDict[HashType.CRC64]);
#endif
            Assert.Equal(_md5, hashDict[HashType.MD5]);
            Assert.Equal(_sha1, hashDict[HashType.SHA1]);
            Assert.Equal(_sha256, hashDict[HashType.SHA256]);
            Assert.Equal(_sha384, hashDict[HashType.SHA384]);
            Assert.Equal(_sha512, hashDict[HashType.SHA512]);
            Assert.Equal(_spamsum, hashDict[HashType.SpamSum]);
#if NET462_OR_GREATER || NETCOREAPP
            Assert.Equal(_xxhash32, hashDict[HashType.XxHash32]);
            Assert.Equal(_xxhash64, hashDict[HashType.XxHash64]);
            Assert.Equal(_xxhash3, hashDict[HashType.XxHash3]);
            Assert.Equal(_xxhash128, hashDict[HashType.XxHash128]);
#endif
        }

        [Fact]
        public void GetByteArrayHashesTest()
        {
            byte[] fileBytes = File.ReadAllBytes(_hashFilePath);
            var hashDict = HashTool.GetByteArrayHashes(fileBytes);

            Assert.NotNull(hashDict);
#if NET7_0_OR_GREATER
            Assert.Equal(_blake3, hashDict![HashType.BLAKE3]);
#endif
            Assert.Equal(_crc32, hashDict![HashType.CRC32]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Naive]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Optimized]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Parallel]);
#if NET462_OR_GREATER || NETCOREAPP
            Assert.Equal(_crc64, hashDict[HashType.CRC64]);
#endif
            Assert.Equal(_md5, hashDict[HashType.MD5]);
            Assert.Equal(_sha1, hashDict[HashType.SHA1]);
            Assert.Equal(_sha256, hashDict[HashType.SHA256]);
            Assert.Equal(_sha384, hashDict[HashType.SHA384]);
            Assert.Equal(_sha512, hashDict[HashType.SHA512]);
            Assert.Equal(_spamsum, hashDict[HashType.SpamSum]);
#if NET462_OR_GREATER || NETCOREAPP
            Assert.Equal(_xxhash32, hashDict[HashType.XxHash32]);
            Assert.Equal(_xxhash64, hashDict[HashType.XxHash64]);
            Assert.Equal(_xxhash3, hashDict[HashType.XxHash3]);
            Assert.Equal(_xxhash128, hashDict[HashType.XxHash128]);
#endif
        }

        [Fact]
        public void GetStreamHashesTest()
        {
            var fileStream = File.OpenRead(_hashFilePath);
            var hashDict = HashTool.GetStreamHashes(fileStream);

            Assert.NotNull(hashDict);
#if NET7_0_OR_GREATER
            Assert.Equal(_blake3, hashDict![HashType.BLAKE3]);
#endif
            Assert.Equal(_crc32, hashDict![HashType.CRC32]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Naive]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Optimized]);
            Assert.Equal(_crc32, hashDict[HashType.CRC32_Parallel]);
#if NET462_OR_GREATER || NETCOREAPP
            Assert.Equal(_crc64, hashDict[HashType.CRC64]);
#endif
            Assert.Equal(_md5, hashDict[HashType.MD5]);
            Assert.Equal(_sha1, hashDict[HashType.SHA1]);
            Assert.Equal(_sha256, hashDict[HashType.SHA256]);
            Assert.Equal(_sha384, hashDict[HashType.SHA384]);
            Assert.Equal(_sha512, hashDict[HashType.SHA512]);
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