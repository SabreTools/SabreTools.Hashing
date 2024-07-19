using System;
using System.IO;
using System.IO.Compression;
using Xunit;

namespace SabreTools.Hashing.Test
{
    public class CompressedStreamTests
    {
        private static readonly string _singleFilePath = Path.Combine(Environment.CurrentDirectory, "TestData", "file-to-hash.zip");

        private static readonly string _multiFilePath = Path.Combine(Environment.CurrentDirectory, "TestData", "file-to-hash-multi.zip");

        [Fact]
        public void GetSingleStreamHashesTest()
        {
            var zipFile = ZipFile.OpenRead(_singleFilePath);
            var fileStream = zipFile.Entries[0].Open();
            var hashDict = HashTool.GetStreamHashes(fileStream);
            TestHelper.ValidateHashes(hashDict);
        }

        [Fact]
        public void GetMultiStreamHashesTest()
        {
            var zipFile = ZipFile.OpenRead(_multiFilePath);

            for (int i = 0; i < zipFile.Entries.Count; i++)
            {
                var fileStream = zipFile.Entries[i].Open();
                var hashDict = HashTool.GetStreamHashes(fileStream);
                TestHelper.ValidateHashes(hashDict);
            }
        }
    }
}