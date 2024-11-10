using System;
using static SabreTools.Hashing.HashOperations;
using static SabreTools.Hashing.Tiger.Constants;

namespace SabreTools.Hashing.Tiger
{
    /// <see href="https://biham.cs.technion.ac.il/Reports/Tiger//>
    public abstract class TigerHash
    {
        /// <summary>
        /// Number of passes (minimum 3)
        /// </summary>
        protected int _passes;

        /// <summary>
        /// Set of 3 64-bit numbers representing the hash state
        /// </summary>
        private readonly ulong[] _state = new ulong[3];

        /// <summary>
        /// Total number of bytes processed
        /// </summary>
        private long _totalBytes;

        /// <summary>
        /// Internal byte buffer to accumulate before <see cref="_block"/> 
        /// </summary>
        private readonly byte[] _buffer = new byte[64];

        /// <summary>
        /// Internal UInt64 buffer for processing
        /// </summary>
        private readonly ulong[] _block = new ulong[8];

        public TigerHash()
        {
            Reset();
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
        {
            // Reset the seed values
            _state[0] = SeedA;
            _state[1] = SeedB;
            _state[2] = SeedC;

            // Reset the byte count
            _totalBytes = 0;

            // Reset the buffers
            Array.Clear(_buffer, 0, _buffer.Length);
            Array.Clear(_block, 0, _block.Length);
        }

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
        {
            // Figure out how much buffer is needed
            int bufferLen = (int)(_totalBytes & 0x3f);

            // Increment the processed byte count
            _totalBytes += length;

            // If there is buffer to fill and it will meet the limit
            if (bufferLen > 0 && bufferLen + length >= 64)
            {
                // Fill the buffer from the input
                Array.Copy(data, offset, _buffer, bufferLen, 64 - bufferLen);

                // Set the new values
                offset += 64 - bufferLen;
                length -= 64 - bufferLen;

                // Split the buffer for the round
                for (int i = 0; i < 8; i++)
                {
                    _block[i] = ReadLE64(_buffer, i * 8);
                }

                // Run the round
                Compress();
                bufferLen = 0;
            }

            /// Process any standalone blocks
            while (length >= 64)
            {
                // Fill the buffer from the input
                Array.Copy(data, offset, _buffer, 0, 64);

                // Set the new values
                offset += 64;
                length -= 64;

                // Split the buffer for the round
                for (int i = 0; i < 8; i++)
                {
                    _block[i] = ReadLE64(_buffer, i * 8);
                }

                // Run the round
                Compress();
            }

            // Save the remainder in the buffer
            if (length > 0)
                Array.Copy(data, offset, _buffer, bufferLen, length);
        }

        /// <summary>
        /// End the hashing process
        /// </summary>
        public void Terminate()
        {
            // Determine the pad length
            int padLength = 64 - (int)(_totalBytes & 0x3f);
            if (padLength <= 8)
                padLength += 64;

            // Get the total byte count in bits
            long totalBitCount = _totalBytes * 8;

            // Prebuild the padding
            var padding = new byte[padLength];
            padding[0] = 0x01;
            padding[padLength - 1] = (byte)((totalBitCount >> 56) & 0xff);
            padding[padLength - 2] = (byte)((totalBitCount >> 48) & 0xff);
            padding[padLength - 3] = (byte)((totalBitCount >> 40) & 0xff);
            padding[padLength - 4] = (byte)((totalBitCount >> 32) & 0xff);
            padding[padLength - 5] = (byte)((totalBitCount >> 24) & 0xff);
            padding[padLength - 6] = (byte)((totalBitCount >> 16) & 0xff);
            padding[padLength - 7] = (byte)((totalBitCount >> 8) & 0xff);
            padding[padLength - 8] = (byte)((totalBitCount >> 0) & 0xff);

            // Pad the block
            TransformBlock(padding, 0, padding.Length);
        }

        /// <summary>
        /// Get the current value of the hash
        /// </summary>
        /// <remarks>
        /// If <see cref="Terminate"/> has not been run, this value
        /// will not be accurate for the processed bytes so far.
        /// </remarks>
        public virtual byte[] GetHash()
        {
            var hash = new byte[24];
            int hashOffset = 0;

            // Assemble the hash array
            for (int i = 0; i < _state.Length; i++)
            {
                byte[] segment = BitConverter.GetBytes(_state[i]);
                Array.Copy(segment, 0, hash, hashOffset, 8);
                hashOffset += 8;
            }

            // Reset the state and return
            Reset();
            return hash;
        }

        /// <summary>
        /// Perform one round of updates on the cached values
        /// </summary>
        private void Compress()
        {
            // Save current values [save_abc]
            ulong aa = _state[0];
            ulong bb = _state[1];
            ulong cc = _state[2];

            // Pass 1 [pass(a, b, c, 5)]
            Pass(ref _state[0], ref _state[1], ref _state[2], 5);

            // Avalanche [key_schedule]
            KeySchedule();

            // Pass 2 [pass(c, a, b, 7)]
            Pass(ref _state[2], ref _state[0], ref _state[1], 7);

            // Avalanche [key_schedule]
            KeySchedule();

            // Pass 3 [pass(b, c, a, 9)]
            Pass(ref _state[1], ref _state[2], ref _state[0], 9);

            // Perform correct set of extra passes
            for (int pass_no = 3; pass_no < _passes; pass_no++)
            {
                // Avalanche [key_schedule]
                KeySchedule();

                // Pass N [pass(a, b, c, 9)]
                Pass(ref _state[0], ref _state[1], ref _state[2], 9);

                // Rotate
                ulong tmpa = _state[0];
                _state[0] = _state[2];
                _state[2] = _state[1];
                _state[1] = tmpa;
            }

            // Update stored values [feedforward]
            _state[0] ^= aa;
            _state[1] -= bb;
            _state[2] += cc;
        }

        /// <summary>
        /// pass(a,b,c,mul)
        /// </summary>
        private void Pass(ref ulong a, ref ulong b, ref ulong c, int mul)
        {
            Round(ref a, ref b, ref c, _block[0], mul);
            Round(ref b, ref c, ref a, _block[1], mul);
            Round(ref c, ref a, ref b, _block[2], mul);
            Round(ref a, ref b, ref c, _block[3], mul);
            Round(ref b, ref c, ref a, _block[4], mul);
            Round(ref c, ref a, ref b, _block[5], mul);
            Round(ref a, ref b, ref c, _block[6], mul);
            Round(ref b, ref c, ref a, _block[7], mul);
        }

        /// <summary>
        /// round(a,b,c,x,mul)
        /// </summary>
        private static void Round(ref ulong a, ref ulong b, ref ulong c, ulong x, int mul)
        {
            c ^= x;

            a -= Table[((c >> (0 * 8)) & 0xFF) + (0 * 256)]
               ^ Table[((c >> (2 * 8)) & 0xFF) + (1 * 256)]
               ^ Table[((c >> (4 * 8)) & 0xFF) + (2 * 256)]
               ^ Table[((c >> (6 * 8)) & 0xFF) + (3 * 256)];
            b += Table[((c >> (1 * 8)) & 0xFF) + (3 * 256)]
               ^ Table[((c >> (3 * 8)) & 0xFF) + (2 * 256)]
               ^ Table[((c >> (5 * 8)) & 0xFF) + (1 * 256)]
               ^ Table[((c >> (7 * 8)) & 0xFF) + (0 * 256)];

            unchecked { b *= (ulong)mul; }
        }

        /// <summary>
        /// key_schedule
        /// </summary>
        private void KeySchedule()
        {
            _block[0] -= _block[7] ^ 0xA5A5A5A5A5A5A5A5;
            _block[1] ^= _block[0];
            _block[2] += _block[1];
            _block[3] -= _block[2] ^ ((~_block[1]) << 19);
            _block[4] ^= _block[3];
            _block[5] += _block[4];
            _block[6] -= _block[5] ^ ((~_block[4]) >> 23);
            _block[7] ^= _block[6];
            _block[0] += _block[7];
            _block[1] -= _block[0] ^ ((~_block[7]) << 19);
            _block[2] ^= _block[1];
            _block[3] += _block[2];
            _block[4] -= _block[3] ^ ((~_block[2]) >> 23);
            _block[5] ^= _block[4];
            _block[6] += _block[5];
            _block[7] -= _block[6] ^ 0x0123456789ABCDEF;
        }
    }

    /// <summary>
    /// 3-pass variant of Tiger-192
    /// </summary>
    public class Tiger192_3 : TigerHash
    {
        public Tiger192_3() : base()
        {
            _passes = 3;
        }
    }

    /// <summary>
    /// 4-pass variant of Tiger-192
    /// </summary>
    public class Tiger192_4 : TigerHash
    {
        public Tiger192_4() : base()
        {
            _passes = 4;
        }
    }

    /// <summary>
    /// 3-pass variant of Tiger-160
    /// </summary>
    public class Tiger160_3 : TigerHash
    {
        public Tiger160_3() : base()
        {
            _passes = 3;
        }

        /// <inheritdoc/>
        public override byte[] GetHash()
        {
            byte[] hash = base.GetHash();
            byte[] trimmedHash = new byte[20];
            Array.Copy(hash, trimmedHash, 20);
            return trimmedHash;
        }
    }

    /// <summary>
    /// 4-pass variant of Tiger-160
    /// </summary>
    public class Tiger160_4 : TigerHash
    {
        public Tiger160_4() : base()
        {
            _passes = 4;
        }

        /// <inheritdoc/>
        public override byte[] GetHash()
        {
            byte[] hash = base.GetHash();
            byte[] trimmedHash = new byte[20];
            Array.Copy(hash, trimmedHash, 20);
            return trimmedHash;
        }
    }

    /// <summary>
    /// 3-pass variant of Tiger-128
    /// </summary>
    public class Tiger128_3 : TigerHash
    {
        public Tiger128_3() : base()
        {
            _passes = 3;
        }

        /// <inheritdoc/>
        public override byte[] GetHash()
        {
            byte[] hash = base.GetHash();
            byte[] trimmedHash = new byte[16];
            Array.Copy(hash, trimmedHash, 16);
            return trimmedHash;
        }
    }

    /// <summary>
    /// 4-pass variant of Tiger-128
    /// </summary>
    public class Tiger128_4 : TigerHash
    {
        public Tiger128_4() : base()
        {
            _passes = 4;
        }

        /// <inheritdoc/>
        public override byte[] GetHash()
        {
            byte[] hash = base.GetHash();
            byte[] trimmedHash = new byte[16];
            Array.Copy(hash, trimmedHash, 16);
            return trimmedHash;
        }
    }
}