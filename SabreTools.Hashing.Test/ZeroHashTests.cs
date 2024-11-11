using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SabreTools.Hashing.Test
{
    public class ZeroHashTests
    {
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

        [Theory]
        [MemberData(nameof(AllHashTypes))]
        public void GetZeroByteHashes(HashType hashType)
        {
            var expected = ZeroHash.GetBytes(hashType);
            var actual = HashTool.GetByteArrayHashArray([], hashType);

            Assert.NotNull(actual);
            Assert.Equal(expected.Length, actual.Length);
            Assert.True(actual.SequenceEqual(expected));
        }

        [Theory]
        [MemberData(nameof(AllHashTypes))]
        public void GetZeroStringHashes(HashType hashType)
        {
            var expected = ZeroHash.GetString(hashType);
            var actual = HashTool.GetByteArrayHash([], hashType);
            Assert.Equal(expected, actual);
        }
    }
}