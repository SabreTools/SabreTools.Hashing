using System;
using System.Linq;
using Xunit;

namespace SabreTools.Hashing.Test
{
    public class ZeroHashTests
    {
        /// <summary>
        /// Get an array of all hash types
        /// </summary>
        public static TheoryData<string> AllHashTypes
        {
            get
            {
                var set = new TheoryData<string>();
                foreach (var value in HashType.AllHashes)
                {
                    set.Add(value);
                }

                return set;
            }
        }

        [Theory]
        [MemberData(nameof(AllHashTypes))]
        public void GetZeroByteHashes(string hashType)
        {
            var expected = ZeroHash.GetBytes(hashType);
            var actual = HashTool.GetByteArrayHashArray([], hashType);

            Assert.NotNull(actual);
            Assert.Equal(expected.Length, actual.Length);
            Assert.True(actual.SequenceEqual(expected));
        }

        [Theory]
        [MemberData(nameof(AllHashTypes))]
        public void GetZeroStringHashes(string hashType)
        {
            var expected = ZeroHash.GetString(hashType);
            var actual = HashTool.GetByteArrayHash([], hashType);
            Assert.Equal(expected, actual);
        }
    }
}
