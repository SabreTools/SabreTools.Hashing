using System;
using System.Text.RegularExpressions;

namespace SabreTools.Hashing.SpamSum;

internal static class Comparisons
{
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
        if (first == null || second == null)
            return -1;

        // Each SpamSum string starts with its block size before the first semicolon.
        // Verify it's there and return otherwise.

        int firstPrefixIndex = first.IndexOf(':');
        if (firstPrefixIndex == -1)
            return -1;
        if (!uint.TryParse(first.Substring(0, firstPrefixIndex), out uint firstBlockSize))
            return -1;

        int secondPrefixIndex = second.IndexOf(':');
        if (secondPrefixIndex == -1)
            return -1;
        if (!uint.TryParse(second.Substring(0, secondPrefixIndex), out uint secondBlockSize))
            return -1;

        // Check if blocksizes don't match. Each spamSum is broken up into two blocks. fuzzy_compare allows you to
        // compare if one block in one hash is the same size as one block in the other hash, even if the other two are
        // non-matching, so that's also checked for.
        if (firstBlockSize != secondBlockSize &&
            (firstBlockSize > uint.MaxValue / 2 || firstBlockSize * 2 != secondBlockSize) &&
            (firstBlockSize % 2 == 1 || firstBlockSize / 2 != secondBlockSize))
        {
            return 0;
        }

        // Get the spamSum strings starting past the blocksize prefix.
        first = first.Substring(firstPrefixIndex + 1);
        second = second.Substring(secondPrefixIndex + 1);

        // Make sure there's something there
        if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second))
            return -1;

        // Split each spamSum into two blocks.
        // Unclear why the second blocks must end before commas, but it is what fuzzy_compare does.
        // If a spamSum doesn't have two parts past the prefix, it's malformed and must be returned.

        var tempSplit = first.Split(':');
        var firstBlockOne = tempSplit[0];
        if (tempSplit.Length == 1 || string.IsNullOrEmpty(tempSplit[1]))
            return -1;
        var firstBlockTwo = tempSplit[1].Split(',')[0];

        tempSplit = second.Split(':');
        var secondBlockOne = tempSplit[0];
        if (tempSplit.Length == 1 || string.IsNullOrEmpty(tempSplit[1]))
            return -1;
        var secondBlockTwo = tempSplit[1].Split(',')[0];

        // The comments for fuzzy_compare say to "Eliminate any sequences [of the same character] longer than 3".
        // What this actually means is that any sequences of the same character longer than 3 need to be reduced to size 3,
        // i.e. "9AgX87HAAAAAOKG5/Dqj3C2o/jlqW7Yn/nmcwlcKCwA9aJo9FcAKwf" becomes "9AgX87HAAAOKG5/Dqj3C2o/jlqW7Yn/nmcwlcKCwA9aJo9FcAKwf"
        // The reason for doing this is that these sequences contain very little info, so cutting them down helps with
        // part of scoring the strings later.
        var r = new Regex("(.)(?<=\\1\\1\\1\\1)", RegexOptions.Compiled);

        firstBlockOne = r.Replace(firstBlockOne, string.Empty);
        firstBlockTwo = r.Replace(firstBlockTwo, string.Empty);
        secondBlockOne = r.Replace(secondBlockOne, string.Empty);
        secondBlockTwo = r.Replace(secondBlockTwo, string.Empty);

        // Return 100 immediately if both spamSums are identical.
        if (firstBlockSize == secondBlockSize
            && firstBlockOne.Length == secondBlockOne.Length
            && firstBlockTwo.Length == secondBlockTwo.Length)
        {
            if (firstBlockOne == secondBlockOne && firstBlockTwo == secondBlockTwo)
                return 100;
        }

        // Choose different scoring combinations depending on block sizes present.
        if (firstBlockSize <= uint.MaxValue / 2)
        {
            if (firstBlockSize == secondBlockSize)
            {
                var score1 = ScoreStrings(firstBlockOne, secondBlockOne, firstBlockSize);
                var score2 = ScoreStrings(firstBlockTwo, secondBlockTwo, firstBlockSize * 2);
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
