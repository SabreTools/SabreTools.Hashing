using System;
using System.Text.RegularExpressions;

namespace SabreTools.Hashing.SpamSum;

// -1 on validity failure, 0 if they're not comparable, score from 0 (least similar) to 100 (most similar) otherwise.
internal static class Comparisons
{
    public static int FuzzyCompare(string? firstHash, string? secondHash)
    {
        const int spamSumLength = 64;
        uint score = 0;
        string? stringOneBlockOne, stringOneBlockTwo, stringTwoBlockOne, stringTwoBlockTwo;
        string? stringOnePrefixOnwards, stringTwoPrefixOnwards;
        int stringOnePrefixIndex, stringTwoPrefixIndex;

        if (firstHash == null || secondHash == null)
            return -1;

        // Each SpamSum string starts with its block size before the first semicolon. Verify it's there and return
        // otherwise.
        stringOnePrefixIndex = firstHash.IndexOf(':');
        if (stringOnePrefixIndex == -1)
            return -1;
        if (!uint.TryParse(firstHash.Substring(0, stringOnePrefixIndex), out uint blockSizeOne))
            return -1;
        stringTwoPrefixIndex = secondHash.IndexOf(':');
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
        stringOnePrefixOnwards = firstHash.Substring(stringOnePrefixIndex + 1);
        stringTwoPrefixOnwards = firstHash.Substring(stringTwoPrefixIndex + 1);

        // Make sure there's something there
        if (string.IsNullOrEmpty(stringOnePrefixOnwards) || string.IsNullOrEmpty(stringTwoPrefixOnwards))
            return -1;

        // Split each spamSum into two blocks.
        // Unclear why the second blocks must end before commas, but it is what fuzzy_compare does.
        // If a spamSum doesn't have two parts past the prefix, it's malformed and must be returned.

        var tempSplit = stringOnePrefixOnwards.Split(':');
        stringOneBlockOne = tempSplit[0];
        if (tempSplit.Length == 1 || string.IsNullOrEmpty(tempSplit[1]))
            return -1;
        stringOneBlockTwo = tempSplit[1].Split(',')[0];
        tempSplit = stringTwoPrefixOnwards.Split(':');
        stringTwoBlockOne = tempSplit[0];
        if (tempSplit.Length == 1 || string.IsNullOrEmpty(tempSplit[1]))
            return -1;
        stringTwoBlockTwo = tempSplit[1].Split(',')[0];

        // The comments for fuzzy_compare say to "Eliminate any sequences [of the same character] longer than 3".
        // What this actually means is that any sequences of the same character longer than 3 need to be reduced to size 3,
        // i.e. "9AgX87HAAAAAOKG5/Dqj3C2o/jlqW7Yn/nmcwlcKCwA9aJo9FcAKwf" becomes "9AgX87HAAAOKG5/Dqj3C2o/jlqW7Yn/nmcwlcKCwA9aJo9FcAKwf"
        // The reason for doing this is that these sequences contain very little info, so cutting them down helps with
        // part of scoring the strings later.

        Regex r = new Regex("(.)(?<=\\1\\1\\1\\1)", RegexOptions.Compiled);

        stringOneBlockOne = r.Replace(stringOneBlockOne, String.Empty);
        stringOneBlockTwo = r.Replace(stringOneBlockTwo, String.Empty);
        stringTwoBlockOne = r.Replace(stringTwoBlockOne, String.Empty);
        stringTwoBlockTwo = r.Replace(stringTwoBlockTwo, String.Empty);


        // Return 100 immediately if both spamSums are identical.
        if (blockSizeOne == blockSizeTwo && stringOneBlockOne.Length == stringTwoBlockOne.Length &&
            stringOneBlockTwo.Length == stringTwoBlockTwo.Length)
            if (stringOneBlockOne == stringTwoBlockOne && stringOneBlockTwo == stringTwoBlockTwo)
                return 100;

        // each signature has a string for two block sizes. We now
        // choose how to combine the two block sizes. We checked above
        // that they have at least one block size in common
        if (blockSizeOne <= uint.MaxValue / 2)
        {
            if (blockSizeOne == blockSizeTwo)
            {
                uint score1, score2;
                score1 = ScoreStrings(stringOneBlockOne, stringTwoBlockOne, blockSizeOne);
                score2 = ScoreStrings(stringOneBlockTwo, stringTwoBlockTwo, blockSizeOne * 2);
                score = Math.Max(score1, score2);
            }
            else if (blockSizeOne * 2 == blockSizeTwo)
            {
                score = ScoreStrings(stringTwoBlockOne, stringOneBlockTwo, blockSizeTwo);
            }
            else
            {
                score = ScoreStrings(stringOneBlockOne, stringTwoBlockTwo, blockSizeOne);
            }
        }
        else
        {
            if (blockSizeOne == blockSizeTwo)
            {
                score = ScoreStrings(stringOneBlockOne, stringTwoBlockOne, blockSizeOne);
            }
            else if (blockSizeOne % 2 == 0 && blockSizeOne / 2 == blockSizeTwo)
            {
                score = ScoreStrings(stringOneBlockOne, stringTwoBlockTwo, blockSizeOne);
            }
            else
            {
                score = 0;
            }
        }

        return (int)score;
    }


    private static bool HasCommmonSubstring(string stringOne, string stringTwo)
    {
        int stringOneLength = stringOne.Length;
        int stringTwoLength = stringTwo.Length;

        int largestSubstring = 0;

        for (int i = 0; i < stringOneLength; i++)
        {
            for (int j = 0; j < stringTwoLength; j++)
            {
                int curr = 0;
                while ((i + curr) < stringOneLength && (j + curr) < stringTwoLength
                                                    && stringOne[i + curr] == stringTwo[j + curr])
                {
                    curr++;
                }

                largestSubstring = Math.Max(largestSubstring, curr);
            }
        }

        if (largestSubstring >= 7)
            return true;
        else
            return false;
    }

    private static uint ScoreStrings(string stringOne, string stringTwo, uint blockSize)
    {
        if (!HasCommmonSubstring(stringOne, stringTwo))
            return 0;

        uint EDIT_DISTN_MAXLEN = 64;
        uint EDIT_DISTN_INSERT_COST = 1;
        uint EDIT_DISTN_REMOVE_COST = 1;
        uint EDIT_DISTN_REPLACE_COST = 2;

        var t1 = new uint[EDIT_DISTN_MAXLEN + 1];
        var t2 = new uint[EDIT_DISTN_MAXLEN + 1];
        var t3 = new uint[EDIT_DISTN_MAXLEN + 1];

        uint i1, i2;
        for (i2 = 0; i2 <= stringTwo.Length; i2++)
            t1[i2] = i2 * EDIT_DISTN_REMOVE_COST;
        for (i1 = 0; i1 < stringOne.Length; i1++)
        {
            t2[0] = (i1 + 1) * EDIT_DISTN_INSERT_COST;
            for (i2 = 0; i2 < stringTwo.Length; i2++)
            {
                uint cost_a = t1[i2 + 1] + EDIT_DISTN_INSERT_COST;
                uint cost_d = t2[i2] + EDIT_DISTN_REMOVE_COST;
                uint cost_r = t1[i2] + (stringOne[(int)i1] == stringTwo[(int)i2] ? 0 : EDIT_DISTN_REPLACE_COST);
                t2[i2 + 1] = Math.Min(Math.Min(cost_a, cost_d), cost_r);
            }

            t3 = t1;
            t1 = t2;
            t2 = t3;
        }

        long score = t1[stringTwo.Length];

        const int spamSumLength = 64;
        const int rollingWindow = 7;
        const int minBlocksize = 3;
        score = (score * spamSumLength) / (stringOne.Length + stringTwo.Length);

        // at this stage the score occurs roughly on a 0-SPAMSUM_LENGTH scale,
        // with 0 being a good match and SPAMSUM_LENGTH being a complete
        // mismatch

        // rescale to a 0-100 scale (friendlier to humans)
        score = (100 * score) / spamSumLength;

        // now re-scale on a 0-100 scale with 0 being a poor match and
        // 100 being a excellent match.
        score = 100 - score;

        //  printf ("s1len: %"PRIu32"  s2len: %"PRIu32"\n", (uint32_t)s1len, (uint32_t)s2len);

        // when the blocksize is small we don't want to exaggerate the match size
        if (blockSize >= (99 + rollingWindow) / rollingWindow * minBlocksize)
            return (uint)score;
        if (score > blockSize / minBlocksize * Math.Min(stringOne.Length, stringTwo.Length))
        {
            score = blockSize / minBlocksize * Math.Min(stringOne.Length, stringTwo.Length);
        }

        return (uint)score;
    }
}