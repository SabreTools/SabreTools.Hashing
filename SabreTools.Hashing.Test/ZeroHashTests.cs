using System;
using System.Collections.Generic;
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
        public void GetZeroHashes(HashType hashType)
        {
            var expected = ZeroHash.GetString(hashType);
            var actual = HashTool.GetByteArrayHash([], hashType);
            Assert.Equal(expected, actual);
        }
    }
}