using System.Collections.Generic;
using Xunit;

namespace SabreTools.Hashing.Test;

// TODO: More thorough testing on the various things like block size checks, but this is likely enough as it is.
public class SpamSumTests
{
    [Theory]
    [InlineData("3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL",
        "3:hMCERJAFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:huRJdxZENFs+rPSromekL", 41)] // Basic small data tests
    [InlineData("12:Y+VH/3Ckg3xqMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMn:xHqVwMMMMMMMMMMMMMMMMMMMMMMMMMM0", 
        "12:Oqkg3xqMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMu:OqVwMMMMMMMMMMMMMMMMMMMMMMMMMMMd", 44)]
    [InlineData("6:l+lq/MtlM8pJ0gt6lXWogE61UlT1Uqj1akMD5n:l+l6Mtl/n0gtOXmEuUl5UqpakM9n", 
        "6:mTj3qJskr+V+1o21+n0rtD2noPWKlAyjllZmMt6120EK+wlsS6T1oLwXuk4tk7:m/bk/1oQrJL3jTu20EK+wlsp5oO4tk7", 0)]
    [InlineData("196608:Gbxf3F4OQK3IuUGM8Ylv1kqCLuDKeo5cRld6iZL6HAGpX7g08WCWDc4NNgs4NEv:qcgxU+UxR2gl5qAGpXjHDcCNgs4N","196608:EqKRzGWxtDOadbDCbZStQxNy+fox3UgOYorlhjolL0K1WJj5lYA:EbNf76db9xNVox3MRlh+sf", 0)] // Basic large data tests
    [InlineData("24576:p+QxhkAcV6cUdRxczoy3NmO0ne3HFVjSeQ229SVjeONr+v:YQ/q6baz5Nqe3H2eQzStBa","24576:fCQxhkAcV6cUdRxczoyVQQFDSVRNihk24vXDj20sq:6Q/q6bazwMgRNihk24jtsq", 54)]
    [InlineData("3:hMCERJAFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:huRJdxZENFs+rPSromekL",
        "3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL", 41)] // Basic small mirror data tests
    [InlineData("12:Oqkg3xqMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMu:OqVwMMMMMMMMMMMMMMMMMMMMMMMMMMMd","12:Y+VH/3Ckg3xqMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMMn:xHqVwMMMMMMMMMMMMMMMMMMMMMMMMMM0", 44)]
    [InlineData("6:mTj3qJskr+V+1o21+n0rtD2noPWKlAyjllZmMt6120EK+wlsS6T1oLwXuk4tk7:m/bk/1oQrJL3jTu20EK+wlsp5oO4tk7","6:l+lq/MtlM8pJ0gt6lXWogE61UlT1Uqj1akMD5n:l+l6Mtl/n0gtOXmEuUl5UqpakM9n", 0)]
    [InlineData("196608:EqKRzGWxtDOadbDCbZStQxNy+fox3UgOYorlhjolL0K1WJj5lYA:EbNf76db9xNVox3MRlh+sf","196608:Gbxf3F4OQK3IuUGM8Ylv1kqCLuDKeo5cRld6iZL6HAGpX7g08WCWDc4NNgs4NEv:qcgxU+UxR2gl5qAGpXjHDcCNgs4N", 0)] // Basic large mirror data tests
    [InlineData("24576:fCQxhkAcV6cUdRxczoyVQQFDSVRNihk24vXDj20sq:6Q/q6bazwMgRNihk24jtsq","24576:p+QxhkAcV6cUdRxczoy3NmO0ne3HFVjSeQ229SVjeONr+v:YQ/q6baz5Nqe3H2eQzStBa", 54)]
    [InlineData("500:AAAAAAAAAAAAAAAAAAAAAAAAyENFACBE+rW6Tj7SMQmK:4","500:AAAyENFACBE+rW6Tj7SMQmK:4", 100)] // Test duplicate sequence truncation
    [InlineData("500:7SMQmKa:3","500:7SMQmKr:3", 0)] // Test rolling window - larger than 7
    [InlineData("500:7QmKa:3","500:7QmKr:3", 0)] // Test rolling window - smaller than 7
    [InlineData("9287:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL","5893:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL", 0)] // Test different blocksizes
    [InlineData("3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL","3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL", 100)]
    [InlineData(null,null, -1)]
    [InlineData("3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL",null, -1)]
    [InlineData(null,"3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL", -1)]
    [InlineData("","", -1)]
    [InlineData("3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL","", -1)]
    [InlineData("","3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL", -1)]
    public void FuzzyCompareTest(string? stringOne, string? stringTwo, int expected)
    {
        var result = SpamSum.SpamSum.FuzzyCompare(stringOne, stringTwo);
        Assert.Equal(expected, result);
    }
}