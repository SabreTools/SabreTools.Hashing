using System;
using System.IO;
using System.IO.Compression;
using Xunit;

namespace SabreTools.Hashing.Test
{
    public class CompressedStreamTests
    {
        /// <summary>
        /// Path to PKZIP archive containing a single compressed file to hash
        /// </summary>
        private static readonly string _singleGzipFilePath
            = Path.Combine(Environment.CurrentDirectory, "TestData", "file-to-hash.bin.gz");

        /// <summary>
        /// Path to PKZIP archive containing a single compressed file to hash
        /// </summary>
        private static readonly string _singleZipFilePath
            = Path.Combine(Environment.CurrentDirectory, "TestData", "file-to-hash.zip");

        /// <summary>
        /// Path to PKZIP archive containing a multiple compressed files to hash
        /// </summary>
        private static readonly string _multiZipFilePath
            = Path.Combine(Environment.CurrentDirectory, "TestData", "file-to-hash-multi.zip");

        [Fact]
        public void GetSingleGzipStreamHashesTest()
        {
            var gzipStream = new GZipStream(File.OpenRead(_singleGzipFilePath), CompressionMode.Decompress);
            var hashDict = HashTool.GetStreamHashesAndSize(gzipStream, out long actualSize);
            TestHelper.ValidateSize(actualSize);
            TestHelper.ValidateHashes(hashDict);
        }

        [Fact]
        public void GetSingleDeflateStreamHashesTest()
        {
            var zipFile = ZipFile.OpenRead(_singleZipFilePath);
            var fileStream = zipFile.Entries[0].Open();
            var hashDict = HashTool.GetStreamHashesAndSize(fileStream, out long actualSize);
            TestHelper.ValidateSize(actualSize);
            TestHelper.ValidateHashes(hashDict);
        }

        [Fact]
        public void GetMultiDeflateStreamHashesTest()
        {
            var zipFile = ZipFile.OpenRead(_multiZipFilePath);

            for (int i = 0; i < zipFile.Entries.Count; i++)
            {
                var fileStream = zipFile.Entries[i].Open();
                var hashDict = HashTool.GetStreamHashesAndSize(fileStream, out long actualSize);
                TestHelper.ValidateSize(actualSize);
                TestHelper.ValidateHashes(hashDict);
            }
        }
    }
}