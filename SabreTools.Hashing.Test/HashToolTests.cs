using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Get an array of all hash types
        /// </summary>
        public static List<object[]> AllHashTypes
        {
            get
            {
                var values = Enum.GetValues(typeof(HashType));
                var set = new List<object[]>();
                foreach (var value in values)
                {
                    set.Add([value]);
                }

                return set;
            }
        }

        [Fact]
        public void GetStandardHashesFileTest()
        {
            bool gotHashes = HashTool.GetStandardHashes(_hashFilePath, out long actualSize, out string? crc32, out string? md5, out string? sha1);

            Assert.True(gotHashes);
            TestHelper.ValidateSize(actualSize);
            TestHelper.ValidateHash(HashType.CRC32, crc32);
            TestHelper.ValidateHash(HashType.MD5, md5);
            TestHelper.ValidateHash(HashType.SHA1, sha1);
        }

        [Fact]
        public void GetStandardHashesArrayTest()
        {
            byte[] fileBytes = File.ReadAllBytes(_hashFilePath);
            bool gotHashes = HashTool.GetStandardHashes(fileBytes, out long actualSize, out string? crc32, out string? md5, out string? sha1);

            Assert.True(gotHashes);
            TestHelper.ValidateSize(actualSize);
            TestHelper.ValidateHash(HashType.CRC32, crc32);
            TestHelper.ValidateHash(HashType.MD5, md5);
            TestHelper.ValidateHash(HashType.SHA1, sha1);
        }

        [Fact]
        public void GetStandardHashesStreamTest()
        {
            var fileStream = File.OpenRead(_hashFilePath);
            bool gotHashes = HashTool.GetStandardHashes(fileStream, out long actualSize, out string? crc32, out string? md5, out string? sha1);

            Assert.True(gotHashes);
            TestHelper.ValidateSize(actualSize);
            TestHelper.ValidateHash(HashType.CRC32, crc32);
            TestHelper.ValidateHash(HashType.MD5, md5);
            TestHelper.ValidateHash(HashType.SHA1, sha1);
        }

        [Fact]
        public void GetFileHashesParallelTest()
        {
            var hashDict = HashTool.GetFileHashes(_hashFilePath);
            TestHelper.ValidateHashes(hashDict);
        }

        [Theory]
        [MemberData(nameof(AllHashTypes))]
        public void GetFileHashesSerialTest(HashType hashType)
        {
            var hashValue = HashTool.GetFileHash(_hashFilePath, hashType);
            TestHelper.ValidateHash(hashType, hashValue);
        }

        [Fact]
        public void GetFileHashesAndSizeTest()
        {
            var hashDict = HashTool.GetFileHashesAndSize(_hashFilePath, out long actualSize);
            TestHelper.ValidateSize(actualSize);
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
        public void GetByteArrayHashesAndSizeTest()
        {
            byte[] fileBytes = File.ReadAllBytes(_hashFilePath);
            var hashDict = HashTool.GetByteArrayHashesAndSize(fileBytes, out long actualSize);
            TestHelper.ValidateSize(actualSize);
            TestHelper.ValidateHashes(hashDict);
        }

        [Fact]
        public void GetStreamHashesTest()
        {
            var fileStream = File.OpenRead(_hashFilePath);
            var hashDict = HashTool.GetStreamHashes(fileStream);
            TestHelper.ValidateHashes(hashDict);
        }

        [Fact]
        public void GetStreamHashesAndSizeTest()
        {
            var fileStream = File.OpenRead(_hashFilePath);
            var hashDict = HashTool.GetStreamHashesAndSize(fileStream, out long actualSize);
            TestHelper.ValidateSize(actualSize);
            TestHelper.ValidateHashes(hashDict);
        }
    }
}