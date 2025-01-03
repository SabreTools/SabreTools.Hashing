using System;
using System.Text;
using static SabreTools.Hashing.SpamSum.Constants;

namespace SabreTools.Hashing.SpamSum
{
    /// <see href="https://download.samba.org/pub/unpacked/junkcode/spamsum/spamsum.c"/> 
    public class SpamSum
    {
        private RollState _rollState = new();

        /// <summary>
        /// Take a message of length 'length' and return a string representing a hash of that message,
        /// prefixed by the selected blocksize
        /// </summary>
        public byte[]? GetHash(byte[] input, uint length, uint blockSize)
        {
            int p;
            uint h, h2, h3;
            uint j, k;

            // Guess a reasonable block size
            if (blockSize == 0)
            {
                blockSize = MIN_BLOCKSIZE;
                while (blockSize * SPAMSUM_LENGTH < length)
                {
                    blockSize *= 2;
                }
            }

            byte[] ret = new byte[SPAMSUM_LENGTH + SPAMSUM_LENGTH / 2 + 20];
            byte[] ret2 = new byte[SPAMSUM_LENGTH / 2 + 1];

        again:
            // Clear the arrays
#if NET6_0_OR_GREATER
            Array.Clear(ret);
            Array.Clear(ret2);
#else
            Array.Clear(ret, 0, ret.Length);
            Array.Clear(ret2, 0, ret2.Length);
#endif

            // The first part of the spamsum signature is the blocksize
            string blockSizeStr = $"{blockSize}:";
            byte[] blockSizeArr = Encoding.ASCII.GetBytes(blockSizeStr);
            Array.Copy(blockSizeArr, ret, blockSizeArr.Length);
            p = blockSizeArr.Length;

            k = j = 0;
            h3 = h2 = HASH_INIT;
            h = RollReset();

            for (uint i = 0; i < length; i++)
            {
                // At each character we update the rolling hash and the normal hash.
                // When the rolling hash hits the reset value then we emit the normal
                // hash as a element of the signature and reset both hashes
                h = RollHash(input[i]);
                h2 = SumHash(input[i], h2);
                h3 = SumHash(input[i], h3);

                if (h % blockSize == (blockSize - 1))
                {
                    // We have hit a reset point. We now emit a
                    // hash which is based on all chacaters in the
                    // piece of the message between the last reset
                    // point and this one
                    ret[p + j] = (byte)Base64Alphabet[(int)(h2 % 64)];
                    if (j < SPAMSUM_LENGTH - 1)
                    {
                        // We can have a problem with the tail
                        // overflowing. The easiest way to
                        // cope with this is to only reset the
                        // second hash if we have room for
                        // more characters in our
                        // signature. This has the effect of
                        // combining the last few pieces of
                        // the message into a single piece
                        h2 = HASH_INIT;
                        j++;
                    }
                }

                // This produces a second signature with a block size
                // of block_size*2. By producing dual signatures in
                // this way the effect of small changes in the message
                // size near a block size boundary is greatly reduced.
                if (h % (blockSize * 2) == ((blockSize * 2) - 1))
                {
                    ret2[k] = (byte)Base64Alphabet[(int)(h3 % 64)];
                    if (k < SPAMSUM_LENGTH / 2 - 1)
                    {
                        h3 = HASH_INIT;
                        k++;
                    }
                }
            }

            // If we have anything left then add it to the end. This
            // ensures that the last part of the message is always
            // considered
            if (h != 0)
            {
                ret[p + j] = (byte)Base64Alphabet[(int)(h2 % 64)];
                ret2[k] = (byte)Base64Alphabet[(int)(h3 % 64)];
            }

            ret[p + ++j] = (byte)':';
            Array.Copy(ret2, 0, ret, p + ++j, ret2.Length);

            // Our blocksize guess may have been way off - repeat if necessary
            if (blockSize == 0 && blockSize > MIN_BLOCKSIZE && j < SPAMSUM_LENGTH / 2)
            {
                blockSize /= 2;
                goto again;
            }

            return ret;
        }

        /// <summary>
        /// A rolling hash, based on the Adler checksum. By using a rolling hash
        /// we can perform auto resynchronisation after inserts/deletes.
        /// 
        /// Internally, H1 is the sum of the bytes in the window and H2
        /// is the sum of the bytes times the index.
        /// 
        /// H3 is a shift/xor based rolling hash, and is mostly needed to ensure that
        /// we can cope with large blocksize values.
        /// </summary>
        private uint RollHash(byte c)
        {
            _rollState.H2 -= _rollState.H1;
            _rollState.H2 += ROLLING_WINDOW * c;

            _rollState.H1 += c;
            _rollState.H1 -= _rollState.Window[_rollState.N % ROLLING_WINDOW];

            _rollState.Window[_rollState.N % ROLLING_WINDOW] = c;
            _rollState.N++;

            _rollState.H3 = (_rollState.H3 << 5) & 0xFFFFFFFF;
            _rollState.H3 ^= c;

            return _rollState.H1 + _rollState.H2 + _rollState.H3;
        }

        /// <summary>
        /// Reset the state of the rolling hash and return the initial rolling hash value
        /// </summary>
        private uint RollReset()
        {
            _rollState = new RollState();
            return 0;
        }

        /// <summary>
        /// A simple non-rolling hash, based on the FNV hash
        /// </summary>
        private static uint SumHash(byte c, uint h)
        {
            h *= HASH_PRIME;
            h ^= c;
            return h;
        }
    }
}