using System;
using System.IO;
using Xunit;

namespace SabreTools.Hashing.Test
{
    public class HashToolTests
    {
        /// <summary>
        /// Path to the uncompressed file to hash
        /// </summary>
        private static readonly string _hashFilePath
            = Path.Combine(Environment.CurrentDirectory, "TestData", "file-to-hash.bin");

        #region Known File Information

        private const long _hashFileSize = 125;
        private const string _crc32 = "ba02a660";
        private const string _md5 = "b722871eaa950016296184d026c5dec9";
        private const string _sha1 = "eea1ee2d801d830c4bdad4df3c8da6f9f52d1a9f";

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
            TestHelper.ValidateHashes(hashDict);
        }

        [Fact]
        public void GetFileHashesAndSizeTest()
        {
            var hashDict = HashTool.GetFileHashesAndSize(_hashFilePath, out long actualSize);
            Assert.Equal(_hashFileSize, actualSize);
            TestHelper.ValidateHashes(hashDict);
        }

        [Fact]
        public void GetByteArrayHashesTest()
        {
            byte[] fileBytes = File.ReadAllBytes(_hashFilePath);
            var hashDict = HashTool.GetByteArrayHashes(fileBytes);
            TestHelper.ValidateHashes(hashDict);
        }

        [Fact]
        public void GetStreamHashesTest()
        {
            var fileStream = File.OpenRead(_hashFilePath);
            var hashDict = HashTool.GetStreamHashes(fileStream);
            TestHelper.ValidateHashes(hashDict);
        }
    }
}