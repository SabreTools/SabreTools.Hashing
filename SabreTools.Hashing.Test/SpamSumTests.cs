using System.Collections.Generic;
using Xunit;

namespace SabreTools.Hashing.Test;

public class SpamSumTests
{
    [Theory]
    [InlineData("3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL",
        "3:hMCERJAFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:huRJdxZENFs+rPSromekL", 41)]
    [InlineData("12:Y+VH/3Ckg3xqMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMn:xHqVwMMMMMMMMMMMMMMMMMMMMMMMMMM0", 
        "12:Oqkg3xqMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMu:OqVwMMMMMMMMMMMMMMMMMMMMMMMMMMMd", 44)]
    [InlineData("6:l+lq/MtlM8pJ0gt6lXWogE61UlT1Uqj1akMD5n:l+l6Mtl/n0gtOXmEuUl5UqpakM9n", 
        "6:mTj3qJskr+V+1o21+n0rtD2noPWKlAyjllZmMt6120EK+wlsS6T1oLwXuk4tk7:m/bk/1oQrJL3jTu20EK+wlsp5oO4tk7", 0)]
    public void FuzzyComparesTheory(string stringOne, string stringTwo, int expected)
    {
        var result = SpamSum.SpamSum.FuzzyCompare(stringOne, stringTwo);
        Assert.Equal(expected, result);
    }
}