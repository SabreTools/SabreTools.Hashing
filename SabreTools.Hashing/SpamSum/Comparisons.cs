using System;
using System.Text.RegularExpressions;

namespace SabreTools.Hashing.SpamSum;

internal static class Comparisons
{
    /// <summary>
    /// Regex to reduce any sequences longer than 3
    /// </summary>
    private static Regex _reduceRegex = new("(.)(?<=\\1\\1\\1\\1)", RegexOptions.Compiled);

    /// <summary>
    /// Compares how similar two SpamSums are to each other
    /// </summary>
    /// <param name="first">First hash to compare</param>
    /// <param name="second">Second hash to compare</param>
    /// <returns>-1 on validity failure, 0 if they're not comparable, score from 0 (least similar) to 100 (most similar) otherwise.</returns>
    /// <remarks>Implements ssdeep's fuzzy_compare</remarks>
    /// <see href="https://github.com/ssdeep-project/ssdeep/blob/df3b860f8918261b3faeec9c7d2c8a241089e6e6/fuzzy.c#L860"/> 
    public static int FuzzyCompare(string? first, string? second)
    {
        // If either input is invalid
        if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second))
            return -1;

        // Split the string into 3 parts for processing
        var firstSplit = first!.Split(':');
        var secondSplit = second!.Split(':');
        if (firstSplit.Length != 3 || secondSplit.Length != 3)
            return -1;

        // If any of the required parts are empty
        if (firstSplit[0].Length == 0 || firstSplit[2].Length == 0)
            return -1;
        if (secondSplit[0].Length == 0 || secondSplit[2].Length == 0)
            return -1;

        // Each SpamSum string starts with its block size before the first semicolon.
        if (!uint.TryParse(firstSplit[0], out uint firstBlockSize))
            return -1;
        if (!uint.TryParse(secondSplit[0], out uint secondBlockSize))
            return -1;

        // Check if blocksizes don't match. Each spamSum is broken up into two blocks.
        // fuzzy_compare allows you to compare if one block in one hash is the same
        // size as one block in the other hash, even if the other two are non-matching,
        // so that's also checked for.
        if (firstBlockSize != secondBlockSize
            && (firstBlockSize > uint.MaxValue / 2 || firstBlockSize * 2 != secondBlockSize)
            && (firstBlockSize % 2 == 1 || firstBlockSize / 2 != secondBlockSize))
        {
            return 0;
        }

        // Ensure only second block data before a comma is used
        string firstBlockTwo = firstSplit[2].Split(',')[0];
        string secondBlockTwo = secondSplit[2].Split(',')[0];

        // Reduce any sequences longer than 3
        // These sequences contain very little info and can be reduced as a result
        string firstBlockOne = _reduceRegex.Replace(firstSplit[1], string.Empty);
        firstBlockTwo = _reduceRegex.Replace(firstBlockTwo, string.Empty);
        string secondBlockOne = _reduceRegex.Replace(secondSplit[1], string.Empty);
        secondBlockTwo = _reduceRegex.Replace(secondBlockTwo, string.Empty);

        // Return 100 immediately if both spamSums are identical.
        if (firstBlockSize == secondBlockSize && firstBlockOne == secondBlockOne && firstBlockTwo == secondBlockTwo)
            return 100;

        // Choose different scoring combinations depending on block sizes present.
        if (firstBlockSize <= uint.MaxValue / 2)
        {
            if (firstBlockSize == secondBlockSize)
            {
                uint score1 = ScoreStrings(firstBlockOne, secondBlockOne, firstBlockSize);
                uint score2 = ScoreStrings(firstBlockTwo, secondBlockTwo, firstBlockSize * 2);
                return (int)Math.Max(score1, score2);
            }
            else if (firstBlockSize * 2 == secondBlockSize)
            {
                return (int)ScoreStrings(secondBlockOne, firstBlockTwo, secondBlockSize);
            }
            else
            {
                return (int)ScoreStrings(firstBlockOne, secondBlockTwo, firstBlockSize);
            }
        }
        else
        {
            if (firstBlockSize == secondBlockSize)
                return (int)ScoreStrings(firstBlockOne, secondBlockOne, firstBlockSize);
            else if (firstBlockSize % 2 == 0 && firstBlockSize / 2 == secondBlockSize)
                return (int)ScoreStrings(firstBlockOne, secondBlockTwo, firstBlockSize);
            else
                return 0;
        }
    }

    /// <summary>
    /// Checks whether the two SpamSum strings have a common substring of 7 or more characters (as defined in fuzzy_compare's ROLLING_WINDOW size).
    /// </summary>
    /// <param name="first">First string to score</param>
    /// <param name="second">Second string to score</param>
    /// <returns>False if there is no common substring of 7 or more characters, true if there is.</returns>
    private static bool HasCommmonSubstring(string first, string second)
    {
        var firstLength = first.Length;
        var secondLength = second.Length;
        var largestSubstring = 0;

        for (var i = 0; i < firstLength; i++)
        {
            for (var j = 0; j < secondLength; j++)
            {
                var currentIndex = 0;
                while ((i + currentIndex) < firstLength && (j + currentIndex) < secondLength && first[i + currentIndex] == second[j + currentIndex])
                {
                    currentIndex++;
                }

                largestSubstring = Math.Max(largestSubstring, currentIndex);
            }
        }

        if (largestSubstring >= 7)
            return true;

        return false;
    }

    /// <summary>
    /// Compares how similar two SpamSums are to each other. Implements ssdeep's fuzzy_compare.
    /// </summary>
    /// <param name="first">First string to score</param>
    /// <param name="second">Second string to score</param>
    /// <param name="blockSize">Current blocksize</param>
    /// <returns>-1 on validity failure, 0 if they're not comparable, score from 0 (least similar) to 100 (most similar) otherwise.</returns>
    private static uint ScoreStrings(string first, string second, uint blockSize)
    {
        if (!HasCommmonSubstring(first, second))
            return 0;

        const uint maxLength = 64;
        const uint insertCost = 1;
        const uint removeCost = 1;
        const uint replaceCost = 2;

        var firstTraverse = new uint[maxLength + 1];
        var secondTravers = new uint[maxLength + 1];

        for (uint secondIndex = 0; secondIndex <= second.Length; secondIndex++)
        {
            firstTraverse[secondIndex] = secondIndex * removeCost;
        }

        for (uint firstIndex = 0; firstIndex < first.Length; firstIndex++)
        {
            secondTravers[0] = (firstIndex + 1) * insertCost;
            for (uint secondIndex = 0; secondIndex < second.Length; secondIndex++)
            {
                var costA = firstTraverse[secondIndex + 1] + insertCost;
                var costD = secondTravers[secondIndex] + removeCost;
                var costR = firstTraverse[secondIndex] + (first[(int)firstIndex] == second[(int)secondIndex] ? 0 : replaceCost);
                secondTravers[secondIndex + 1] = Math.Min(Math.Min(costA, costD), costR);
            }

            var tempArray = firstTraverse;
            firstTraverse = secondTravers;
            secondTravers = tempArray;
        }

        long score = firstTraverse[second.Length];

        const int spamSumLength = 64;
        const int rollingWindow = 7;
        const int minBlocksize = 3;
        score = (score * spamSumLength) / (first.Length + second.Length);

        // Currently, the score ranges from 0-64 (64 being the length of a spamsum), with 0 being the strongest match
        // and 64 being the weakest match.

        // Change scale to 0-100
        score = (100 * score) / spamSumLength;

        // Invert scale so 0 is the weakest possible match and 100 is the strongest
        score = 100 - score;

        // Compensate for small blocksizes, so match isn't reported as overly strong.
        if (blockSize >= (99 + rollingWindow) / rollingWindow * minBlocksize)
            return (uint)score;
        if (score > blockSize / minBlocksize * Math.Min(first.Length, second.Length))
            score = blockSize / minBlocksize * Math.Min(first.Length, second.Length);

        return (uint)score;
    }
}
