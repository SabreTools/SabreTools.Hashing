using System;
using System.Text;
using static SabreTools.Hashing.SpamSum.Constants;

namespace SabreTools.Hashing.SpamSum
{
    /// <see href="https://github.com/ssdeep-project/ssdeep/blob/master/fuzzy.c"/> 
    public class SpamSum : System.Security.Cryptography.HashAlgorithm
    {
        private FuzzyState _state;

        public SpamSum()
        {
            _state = new();
            Initialize();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            _state = new FuzzyState
            {
                BHStart = 0,
                BHEnd = 1,
                BHEndLimit = NUM_BLOCKHASHES - 1,
                TotalSize = 0,
                ReduceBorder = MIN_BLOCKSIZE * SPAMSUM_LENGTH,
                Flags = 0,
                RollMask = 0,
            };

            _state.BH[0].H = HASH_INIT;
            _state.BH[0].HalfH = HASH_INIT;
            _state.BH[0].Digest[0] = 0x00;
            _state.BH[0].HalfDigest = 0x00;
            _state.BH[0].DIndex = 0;
        }

        /// <inheritdoc cref="Comparisons.FuzzyCompare(string?, string?)"/>
        public static int FuzzyCompare(string? firstHash, string? secondHash)
            => Comparisons.FuzzyCompare(firstHash, secondHash);

        /// <inheritdoc/>
        protected override void HashCore(byte[] array, int ibStart, int cbSize)
        {
            _state.TotalSize += (ulong)cbSize;
            for (int i = ibStart; i < cbSize; i++)
            {
                ProcessByte(array[i]);
            }
        }

        /// <inheritdoc/>
        protected override byte[] HashFinal()
        {
            string? digest = Finalize(0);
            if (digest == null)
                return [];

            return Encoding.ASCII.GetBytes(digest.TrimEnd('\0'));
        }

        /// <remarks>
        /// Originally named `fuzzy_engine_step`
        /// </remarks>
        private void ProcessByte(byte c)
        {
            // At each character we update the rolling hash and the normal hashes.
            // When the rolling hash hits a reset value then we emit a normal hash
            // as a element of the signature and reset the normal hash.
            _state.Roll.RollHash(c);
            uint horg = _state.Roll.RollSum() + 1;
            uint h = horg / MIN_BLOCKSIZE;

            uint i;
            for (i = _state.BHStart; i < _state.BHEnd; ++i)
            {
                _state.BH[i].H = SumHash(c, _state.BH[i].H);
                _state.BH[i].HalfH = SumHash(c, _state.BH[i].HalfH);
            }

            if ((_state.Flags & FUZZY_STATE_NEED_LASTHASH) != 0)
                _state.LastH = SumHash(c, _state.LastH);

            // 0xffffffff !== -1 (mod 3)
            if (horg == 0)
                return;

            // With growing blocksize almost no runs fail the next test.
            if ((h & _state.RollMask) != 0)
                return;

            // Delay computation of modulo as possible.
            if ((horg % MIN_BLOCKSIZE) != 0)
                return;

            h >>= (int)_state.BHStart;
            i = _state.BHStart;
            do
            {
                // We have hit a reset point. We now emit hashes which are
                // based on all characters in the piece of the message between
                // the last reset point and this one
                if (_state.BH[i].DIndex == 0)
                {
                    // Can only happen 30 times.
                    // First step for this blocksize. Clone next.
                    _state.TryForkBlockhash();
                }

                _state.BH[i].Digest[_state.BH[i].DIndex] = B64[_state.BH[i].H];
                _state.BH[i].HalfDigest = B64[_state.BH[i].HalfH];

                if (_state.BH[i].DIndex < SPAMSUM_LENGTH - 1)
                {
                    // We can have a problem with the tail overflowing. The
                    // easiest way to cope with this is to only reset the
                    // normal hash if we have room for more characters in
                    // our signature. This has the effect of combining the
                    // last few pieces of the message into a single piece
                    _state.BH[i].Digest[++_state.BH[i].DIndex] = 0x00;
                    _state.BH[i].H = HASH_INIT;
                    if (_state.BH[i].DIndex < SPAMSUM_LENGTH / 2)
                    {
                        _state.BH[i].HalfH = HASH_INIT;
                        _state.BH[i].HalfDigest = 0x00;
                    }
                }
                else
                {
                    _state.TryReduceBlockhash();
                }

                if ((h & 1) != 0)
                    break;

                h >>= 1;
            } while (++i < _state.BHEnd);
        }

        /// <summary>
        /// A simple non-rolling hash, based on the FNV hash
        /// </summary>
        private static byte SumHash(byte c, byte h) => SUM_TABLE[h][c & 0x3f];

        /// <remarks>
        /// Originally named `fuzzy_digest`
        /// </remarks>
        private string? Finalize(uint flags)
        {
            uint bi = _state.BHStart;
            uint h = _state.Roll.RollSum();
            int i;

            // Exclude terminating '\0'.
            int remain = FUZZY_MAX_RESULT - 1;

            // Verify that our elimination was not overeager.
            if (bi != 0 && (ulong)SSDEEP_BS(bi) / 2 * SPAMSUM_LENGTH >= _state.TotalSize)
                return null;

            // The input exceeds data types.
            if (_state.TotalSize > SSDEEP_TOTAL_SIZE_MAX)
                return null;

            // Initial blocksize guess.
            while ((ulong)SSDEEP_BS(bi) * SPAMSUM_LENGTH < _state.TotalSize)
            {
                ++bi;
            }

            // Adapt blocksize guess to actual digest length.
            if (bi >= _state.BHEnd)
                bi = _state.BHEnd - 1;

            while (bi > _state.BHStart && _state.BH[bi].DIndex < SPAMSUM_LENGTH / 2)
            {
                --bi;
            }

            if (bi > 0 && _state.BH[bi].DIndex < SPAMSUM_LENGTH / 2)
                return null;

            byte[] result = new byte[FUZZY_MAX_RESULT];
            int resultPtr = 0;

            string prefixStr = $"{(ulong)SSDEEP_BS(bi)}:";
            byte[] prefixArr = Encoding.ASCII.GetBytes(prefixStr);
            Array.Copy(prefixArr, result, prefixArr.Length);

            i = prefixArr.Length;
            if (i >= remain)
                return null;

            remain -= i;
            resultPtr += i;

            i = (int)_state.BH[bi].DIndex;
            if (i > remain)
                return null;

            if ((flags & FUZZY_FLAG_ELIMSEQ) != 0)
                i = EliminateSequences(result, resultPtr, _state.BH[bi].Digest, 0, i);
            else
                Array.Copy(_state.BH[bi].Digest, 0, result, resultPtr, i);

            resultPtr += i;
            remain -= i;
            if (h != 0)
            {
                if (remain <= 0)
                    return null;

                result[resultPtr] = B64[_state.BH[bi].H];
                if ((flags & FUZZY_FLAG_ELIMSEQ) == 0
                    || i < 3
                    || result[resultPtr] != result[resultPtr - 1]
                    || result[resultPtr] != result[resultPtr - 2]
                    || result[resultPtr] != result[resultPtr - 3])
                {
                    ++resultPtr;
                    --remain;
                }
            }
            else if (_state.BH[bi].Digest[_state.BH[bi].DIndex] != '\0')
            {
                if (remain <= 0)
                    return null;

                result[resultPtr] = _state.BH[bi].Digest[_state.BH[bi].DIndex];
                if ((flags & FUZZY_FLAG_ELIMSEQ) == 0
                    || i < 3
                    || result[resultPtr] != result[resultPtr - 1]
                    || result[resultPtr] != result[resultPtr - 2]
                    || result[resultPtr] != result[resultPtr - 3])
                {
                    ++resultPtr;
                    --remain;
                }
            }

            if (remain <= 0)
                return null;

            result[resultPtr++] = (byte)':';
            --remain;

            if (bi < _state.BHEnd - 1)
            {
                ++bi;
                i = (int)_state.BH[bi].DIndex;
                if ((flags & FUZZY_FLAG_NOTRUNC) == 0 && i > SPAMSUM_LENGTH / 2 - 1)
                    i = SPAMSUM_LENGTH / 2 - 1;

                if (i > remain)
                    return null;

                if ((flags & FUZZY_FLAG_ELIMSEQ) != 0)
                    i = EliminateSequences(result, resultPtr, _state.BH[bi].Digest, 0, i);
                else
                    Array.Copy(_state.BH[bi].Digest, 0, result, resultPtr, i);

                resultPtr += i;
                remain -= i;
                if (h != 0)
                {
                    if (remain <= 0)
                        return null;

                    h = (flags & FUZZY_FLAG_NOTRUNC) != 0
                        ? _state.BH[bi].H
                        : _state.BH[bi].HalfH;

                    result[resultPtr] = B64[h];
                    if ((flags & FUZZY_FLAG_ELIMSEQ) == 0
                        || i < 3
                        || result[resultPtr] != result[resultPtr - 1]
                        || result[resultPtr] != result[resultPtr - 2]
                        || result[resultPtr] != result[resultPtr - 3])
                    {
                        ++resultPtr;
                        --remain;
                    }
                }
                else
                {
                    i = (flags & FUZZY_FLAG_NOTRUNC) != 0
                        ? _state.BH[bi].Digest[_state.BH[bi].DIndex]
                        : _state.BH[bi].HalfDigest;

                    if (i != 0x00)
                    {
                        if (remain <= 0)
                            return null;

                        result[resultPtr] = (byte)i;
                        if ((flags & FUZZY_FLAG_ELIMSEQ) == 0
                            || i < 3
                            || result[resultPtr] != result[resultPtr - 1]
                            || result[resultPtr] != result[resultPtr - 2]
                            || result[resultPtr] != result[resultPtr - 3])
                        {
                            ++resultPtr;
                            --remain;
                        }
                    }
                }
            }
            else if (h != 0)
            {
                if (bi != 0 && bi != NUM_BLOCKHASHES - 1)
                    return null;
                if (remain <= 0)
                    return null;

                if (bi == 0)
                    result[resultPtr++] = B64[_state.BH[bi].H];
                else
                    result[resultPtr++] = B64[_state.LastH];

                /* No need to bother with FUZZY_FLAG_ELIMSEQ, because this
                 * digest has length 1. */
                --remain;
            }

            result[resultPtr] = 0x00;
            return Encoding.ASCII.GetString(result);
        }

        /// <remarks>
        /// Originally named `memcpy_eliminate_sequences`
        /// </remarks>
        private static int EliminateSequences(byte[] dst, int dstPtr, byte[] src, int srcPtr, int n)
        {
            int srcend = srcPtr + n;
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n));

            if (srcPtr < srcend) dst[dstPtr++] = src[srcPtr++];
            if (srcPtr < srcend) dst[dstPtr++] = src[srcPtr++];
            if (srcPtr < srcend) dst[dstPtr++] = src[srcPtr++];
            while (srcPtr < srcend)
            {
                if (src[srcPtr] == dst[dstPtr - 1]
                    && src[srcPtr] == dst[dstPtr - 2]
                    && src[srcPtr] == dst[dstPtr - 3])
                {
                    ++srcPtr;
                    --n;
                }
                else
                {
                    dst[dstPtr++] = src[srcPtr++];
                }
            }

            return n;
        }
    }
}
