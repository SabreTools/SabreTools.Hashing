using System;
using static SabreTools.Hashing.SpamSum.Constants;

namespace SabreTools.Hashing.SpamSum
{
    /// <see href="https://github.com/ssdeep-project/ssdeep/blob/master/fuzzy.c"/>
    internal class FuzzyState
    {
        public ulong TotalSize { get; set; }

        public ulong FixedSize { get; set; }

        public ulong ReduceBorder { get; set; }

        public uint BHStart { get; set; }

        public uint BHEnd { get; set; }

        public uint BHEndLimit { get; set; }

        public uint Flags { get; set; }

        public uint RollMask { get; set; }

        public BlockhashContext[] BH { get; set; }

        public RollState Roll { get; set; }

        public byte LastH { get; set; }

        public FuzzyState()
        {
            BH = new BlockhashContext[NUM_BLOCKHASHES];
            for (int i = 0; i < NUM_BLOCKHASHES; i++)
            {
                BH[i] = new BlockhashContext();
            }

            Roll = new RollState();
        }

        public void TryForkBlockhash()
        {
            uint obh, nbh;
            if (BHEnd <= 0)
                throw new Exception("assert(BHEnd > 0)");

            obh = BHEnd - 1;
            if (BHEnd <= BHEndLimit)
            {
                nbh = obh + 1;
                BH[nbh].H = BH[obh].H;
                BH[nbh].HalfH = BH[obh].HalfH;
                BH[nbh].Digest[0] = 0x00;
                BH[nbh].HalfDigest = 0x00;
                BH[nbh].DIndex = 0;
                ++BHEnd;
            }
            else if (BHEnd == NUM_BLOCKHASHES
                && ((Flags & FUZZY_STATE_NEED_LASTHASH) == 0))
            {
                Flags |= FUZZY_STATE_NEED_LASTHASH;
                LastH = BH[obh].H;
            }
        }

        public void TryReduceBlockhash()
        {
            if (BHStart >= BHEnd)
                throw new Exception("assert(BHStart < BHEnd)");

            // Need at least two working hashes.
            if (BHEnd - BHStart < 2)
                return;

            // Initial blocksize estimate would select this or a smaller blocksize.
            if (ReduceBorder >= (((Flags & FUZZY_STATE_SIZE_FIXED) != 0) ? FixedSize : TotalSize))
                return;

            // Estimate adjustment would select this blocksize.
            if (BH[BHStart + 1].DIndex < SPAMSUM_LENGTH / 2)
                return;

            // At this point we are clearly no longer interested in the
            // start_blocksize. Get rid of it.
            ++BHStart;
            ReduceBorder *= 2;
            RollMask = (RollMask * 2) + 1;
        }
    }
}
