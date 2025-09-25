using System;
using System.Text.RegularExpressions;

namespace SabreTools.Hashing.SpamSum;

internal static class Comparisons
{
    /// <summary>
    /// Compares how similar two SpamSums are to each other. Implements ssdeep's fuzzy_compare.
    /// </summary>
    /// <param name="firstHash">First hash to compare</param>
    /// <param name="secondHash">Second hash to compare</param>
    /// <returns>-1 on validity failure, 0 if they're not comparable, score from 0 (least similar) to 100 (most similar) otherwise.</returns>
    /// <see href="https://github.com/ssdeep-project/ssdeep/blob/df3b860f8918261b3faeec9c7d2c8a241089e6e6/fuzzy.c#L860"/> 
    public static int FuzzyCompare(string? firstHash, string? secondHash)
    {
        if (firstHash == null || secondHash == null)
            return -1;

        // Each SpamSum string starts with its block size before the first semicolon. Verify it's there and return
        // otherwise.
        var stringOnePrefixIndex = firstHash.IndexOf(':');
        if (stringOnePrefixIndex == -1)
            return -1;
        if (!uint.TryParse(firstHash.Substring(0, stringOnePrefixIndex), out uint blockSizeOne))
            return -1;
        var stringTwoPrefixIndex = secondHash.IndexOf(':');
        if (stringTwoPrefixIndex == -1)
            return -1;
        if (!uint.TryParse(secondHash.Substring(0, stringTwoPrefixIndex), out uint blockSizeTwo))
            return -1;

        // Check if blocksizes don't match. Each spamSum is broken up into two blocks. fuzzy_compare allows you to
        // compare if one block in one hash is the same size as one block in the other hash, even if the other two are
        // non-matching, so that's also checked for.
        if (blockSizeOne != blockSizeTwo &&
            (blockSizeOne > uint.MaxValue / 2 || blockSizeOne * 2 != blockSizeTwo) &&
            (blockSizeOne % 2 == 1 || blockSizeOne / 2 != blockSizeTwo))
            return 0;

        // Get the spamSum strings starting past the blocksize prefix.
        var stringOnePrefixOnwards = firstHash.Substring(stringOnePrefixIndex + 1);
        var stringTwoPrefixOnwards = secondHash.Substring(stringTwoPrefixIndex + 1);

        // Make sure there's something there
        if (string.IsNullOrEmpty(stringOnePrefixOnwards) || string.IsNullOrEmpty(stringTwoPrefixOnwards))
            return -1;

        // Split each spamSum into two blocks.
        // Unclear why the second blocks must end before commas, but it is what fuzzy_compare does.
        // If a spamSum doesn't have two parts past the prefix, it's malformed and must be returned.

        var tempSplit = stringOnePrefixOnwards.Split(':');
        var stringOneBlockOne = tempSplit[0];
        if (tempSplit.Length == 1 || string.IsNullOrEmpty(tempSplit[1]))
            return -1;
        var stringOneBlockTwo = tempSplit[1].Split(',')[0];
        tempSplit = stringTwoPrefixOnwards.Split(':');
        var stringTwoBlockOne = tempSplit[0];
        if (tempSplit.Length == 1 || string.IsNullOrEmpty(tempSplit[1]))
            return -1;
        var stringTwoBlockTwo = tempSplit[1].Split(',')[0];

        // The comments for fuzzy_compare say to "Eliminate any sequences [of the same character] longer than 3".
        // What this actually means is that any sequences of the same character longer than 3 need to be reduced to size 3,
        // i.e. "9AgX87HAAAAAOKG5/Dqj3C2o/jlqW7Yn/nmcwlcKCwA9aJo9FcAKwf" becomes "9AgX87HAAAOKG5/Dqj3C2o/jlqW7Yn/nmcwlcKCwA9aJo9FcAKwf"
        // The reason for doing this is that these sequences contain very little info, so cutting them down helps with
        // part of scoring the strings later.
        Regex r = new Regex("(.)(?<=\\1\\1\\1\\1)", RegexOptions.Compiled);

        stringOneBlockOne = r.Replace(stringOneBlockOne, string.Empty);
        stringOneBlockTwo = r.Replace(stringOneBlockTwo, string.Empty);
        stringTwoBlockOne = r.Replace(stringTwoBlockOne, string.Empty);
        stringTwoBlockTwo = r.Replace(stringTwoBlockTwo, string.Empty);


        // Return 100 immediately if both spamSums are identical.
        if (blockSizeOne == blockSizeTwo && stringOneBlockOne.Length == stringTwoBlockOne.Length &&
            stringOneBlockTwo.Length == stringTwoBlockTwo.Length)
            if (stringOneBlockOne == stringTwoBlockOne && stringOneBlockTwo == stringTwoBlockTwo)
                return 100;

        // Choose different scoring combinations depending on block sizes present.
        uint score;
        if (blockSizeOne <= uint.MaxValue / 2)
        {
            if (blockSizeOne == blockSizeTwo)
            {
                var score1 = ScoreStrings(stringOneBlockOne, stringTwoBlockOne, blockSizeOne);
                var score2 = ScoreStrings(stringOneBlockTwo, stringTwoBlockTwo, blockSizeOne * 2);
                score = Math.Max(score1, score2);
            }
            else if (blockSizeOne * 2 == blockSizeTwo)
                score = ScoreStrings(stringTwoBlockOne, stringOneBlockTwo, blockSizeTwo);
            else
                score = ScoreStrings(stringOneBlockOne, stringTwoBlockTwo, blockSizeOne);
        }
        else
        {
            if (blockSizeOne == blockSizeTwo)
                score = ScoreStrings(stringOneBlockOne, stringTwoBlockOne, blockSizeOne);
            else if (blockSizeOne % 2 == 0 && blockSizeOne / 2 == blockSizeTwo)
                score = ScoreStrings(stringOneBlockOne, stringTwoBlockTwo, blockSizeOne);
            else
                score = 0;
        }

        return (int)score;
    }

    /// <summary>
    /// Checks whether the two SpamSum strings have a common substring of 7 or more characters (as defined in fuzzy_compare's ROLLING_WINDOW size).
    /// </summary>
    /// <param name="stringOne">First string to score</param>
    /// <param name="stringTwo">Second string to score</param>
    /// <returns>False if there is no common substring of 7 or more characters, true if there is.</returns>

    private static bool HasCommmonSubstring(string stringOne, string stringTwo)
    {
        var stringOneLength = stringOne.Length;
        var stringTwoLength = stringTwo.Length;
        var largestSubstring = 0;

        for (var i = 0; i < stringOneLength; i++)
            for (var j = 0; j < stringTwoLength; j++)
            {
                var currentIndex = 0;
                while ((i + currentIndex) < stringOneLength && (j + currentIndex) < stringTwoLength && stringOne[i + currentIndex] == stringTwo[j + currentIndex])
                    currentIndex++;

                largestSubstring = Math.Max(largestSubstring, currentIndex);
            }

        if (largestSubstring >= 7)
            return true;
        
        return false;
    }

    /// <summary>
    /// Compares how similar two SpamSums are to each other. Implements ssdeep's fuzzy_compare.
    /// </summary>
    /// <param name="stringOne">First string to score</param>
    /// <param name="stringTwo">Second string to score</param>
    /// <param name="blockSize">Current blocksize</param>
    /// <returns>-1 on validity failure, 0 if they're not comparable, score from 0 (least similar) to 100 (most similar) otherwise.</returns>
    private static uint ScoreStrings(string stringOne, string stringTwo, uint blockSize)
    {
        if (!HasCommmonSubstring(stringOne, stringTwo))
            return 0;

        const uint maxLength = 64;
        const uint insertCost = 1;
        const uint removeCost = 1;
        const uint replaceCost = 2;

        var traverseOne = new uint[maxLength + 1];
        var traverseTwo = new uint[maxLength + 1];
        uint[] tempArray;

        uint indexOne, indexTwo;
        for (indexTwo = 0; indexTwo <= stringTwo.Length; indexTwo++)
            traverseOne[indexTwo] = indexTwo * removeCost;
        for (indexOne = 0; indexOne < stringOne.Length; indexOne++)
        {
            traverseTwo[0] = (indexOne + 1) * insertCost;
            for (indexTwo = 0; indexTwo < stringTwo.Length; indexTwo++)
            {
                var costA = traverseOne[indexTwo + 1] + insertCost;
                var costD = traverseTwo[indexTwo] + removeCost;
                var costR = traverseOne[indexTwo] + (stringOne[(int)indexOne] == stringTwo[(int)indexTwo] ? 0 : replaceCost);
                traverseTwo[indexTwo + 1] = Math.Min(Math.Min(costA, costD), costR);
            }

            tempArray = traverseOne;
            traverseOne = traverseTwo;
            traverseTwo = tempArray;
        }

        long score = traverseOne[stringTwo.Length];

        const int spamSumLength = 64;
        const int rollingWindow = 7;
        const int minBlocksize = 3;
        score = (score * spamSumLength) / (stringOne.Length + stringTwo.Length);

        // Currently, the score ranges from 0-64 (64 being the length of a spamsum), with 0 being the strongest match
        // and 64 being the weakest match.

        // Change scale to 0-100
        score = (100 * score) / spamSumLength;

        // Invert scale so 0 is the weakest possible match and 100 is the strongest
        score = 100 - score;
        
        // Compensate for small blocksizes, so match isn't reported as overly strong.
        if (blockSize >= (99 + rollingWindow) / rollingWindow * minBlocksize)
            return (uint)score;
        if (score > blockSize / minBlocksize * Math.Min(stringOne.Length, stringTwo.Length))
            score = blockSize / minBlocksize * Math.Min(stringOne.Length, stringTwo.Length);

        return (uint)score;
    }
}