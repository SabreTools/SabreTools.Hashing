using System;
using static SabreTools.Hashing.RipeMD.Constants;

namespace SabreTools.Hashing.RipeMD
{
    // TODO: Determine if unrolled version of Round is more efficient
    internal class RipeMD160
    {
        /// <summary>
        /// Leftmost 32 bits
        /// </summary>
        private uint _y0;

        /// <summary>
        /// Second from left 32 bits
        /// </summary>
        private uint _y1;

        /// <summary>
        /// Third from left 32 bits
        /// </summary>
        private uint _y2;

        /// <summary>
        /// Fourth from left 32 bits
        /// </summary>
        private uint _y3;

        /// <summary>
        /// Fifth from left 32 bits
        /// </summary>
        private uint _y4;

        /// <summary>
        /// Total number of bytes processed
        /// </summary>
        private ulong _totalBytes;

        /// <summary>
        /// Internal byte buffer to accumulate before <see cref="_z"/> 
        /// </summary>
        private readonly byte[] _preZ = new byte[4];

        /// <summary>
        /// Current pointer to <see cref="_preZ"/> 
        /// </summary>
        private int _preZPtr;

        /// <summary>
        /// Internal UInt32 buffer for processing
        /// </summary>
        private readonly uint[] _z = new uint[16];

        /// <summary>
        /// Current pointer to <see cref="_z"/> 
        /// </summary>
        private int _zPtr;

        public RipeMD160()
        {
            Reset();
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
        {
            // Reset the seed values
            _y0 = RMD160Y0;
            _y1 = RMD160Y1;
            _y2 = RMD160Y2;
            _y3 = RMD160Y3;
            _y4 = RMD160Y4;

            // Reset the byte count
            _totalBytes = 0;

            // Reset the pre-Z buffer
            Array.Clear(_preZ, 0, _preZ.Length);
            _preZPtr = 0;

            // Reset the accumulator
            Array.Clear(_z, 0, _z.Length);
            _zPtr = 0;
        }

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
        {
            // Increment the processed byte count
            _totalBytes += (ulong)length;

            // Fill the pre-Z buffer first if partially full
            if (_preZPtr > 0)
            {
                // Read as many bytes as possible to either fill or exhaust
                int preZRead = Math.Min(4 - _preZPtr, length);
                Array.Copy(data, offset, _preZ, _preZPtr, preZRead);
                offset += preZRead;
                length -= preZRead;

                // Process the pre-Z buffer if necessary
                if (_preZPtr == 4)
                {
                    AppendUInt32(_preZ);
                    _preZPtr = 0;
                }

                // Exit if there are no more bytes
                if (length == 0)
                    return;
            }

            // Process 4-byte blocks
            while (length >= 4)
            {
                // Get the next 4 bytes
                var temp = new byte[4];
                Array.Copy(data, offset, temp, 0, 4);

                // Process the array
                AppendUInt32(temp);

                // Set bytes as processed
                offset += 4;
                length -= 4;
            }

            // Fill the pre-Z buffer with the remainder
            if (length > 0)
            {
                Array.Copy(data, offset, _preZ, 0, length);
                _preZPtr = length;
            }
        }

        /// <summary>
        /// End the hashing process
        /// </summary>
        public void Terminate()
        {
            // Pad the block
            TransformBlock([128], 0, 1);

            // Add zero bytes until pre-Z is cleared
            while (_preZPtr != 0)
            {
                TransformBlock([0], 0, 1);
            }

            // Handle if the accumulator is nearly full
            if (_zPtr > 14)
                Round();

            // Split the block count
            ulong width = _totalBytes << 3;
            _z[14] = (uint)(width & 0xffffffff);
            _z[15] = (uint)(width >> 32);

            // Run a final round
            Round();
        }

        /// <summary>
        /// Get the current value of the hash
        /// </summary>
        /// <remarks>
        /// If <see cref="Terminate"/> has not been run, this value
        /// will not be accurate for the processed bytes so far.
        /// </remarks>
        public byte[] GetHash()
        {
            var hash = new byte[20];
            int hashOffset = 0;

            // Y0
            byte[] segment = BitConverter.GetBytes(_y0);
            Array.Reverse(segment);
            Array.Copy(segment, 0, hash, hashOffset, 4);
            hashOffset += 4;

            // Y1
            segment = BitConverter.GetBytes(_y1);
            Array.Reverse(segment);
            Array.Copy(segment, 0, hash, hashOffset, 4);
            hashOffset += 4;

            // Y2
            segment = BitConverter.GetBytes(_y2);
            Array.Reverse(segment);
            Array.Copy(segment, 0, hash, hashOffset, 4);
            hashOffset += 4;

            // Y3
            segment = BitConverter.GetBytes(_y3);
            Array.Reverse(segment);
            Array.Copy(segment, 0, hash, hashOffset, 4);
            hashOffset += 4;

            // Y4
            segment = BitConverter.GetBytes(_y4);
            Array.Reverse(segment);
            Array.Copy(segment, 0, hash, hashOffset, 4);

            // Reset the state and return
            Reset();
            return hash;
        }

        /// <summary>
        /// Append a block of 4 bytes, processing the set if necessary
        /// </summary>
        private void AppendUInt32(byte[] data)
        {
            // Read in the next 4 bytes as a little-endian UInt32
            _z[_zPtr++] = (uint)data[0] + data[1] << 8
                              + data[2] << 16 + data[3] << 24;

            // If the accumulator is full, perform an update round
            if (_zPtr == 16)
                Round();
        }

        /// <summary>
        /// Perform one round of updates on the cached values
        /// </summary>
        private void Round()
        {
            // Setup values
            uint x0 = _y0, xp0 = _y0;
            uint x1 = _y1, xp1 = _y1;
            uint x2 = _y2, xp2 = _y2;
            uint x3 = _y3, xp3 = _y3;
            uint x4 = _y4, xp4 = _y4;

            // Loop and process
            for (int i = 0; i <= 79; i++)
            {
                // Get the round seeds
                uint ci = i switch
                {
                    <= 15 => RMD160Round00To15,
                    <= 31 => RMD160Round16To31,
                    <= 47 => RMD160Round32To47,
                    <= 63 => RMD160Round48To63,
                    <= 79 => RMD160Round64To79,
                    _ => throw new ArgumentOutOfRangeException(nameof(i)),
                };
                uint cpi = i switch
                {
                    <= 15 => RMD160RoundPrime00To15,
                    <= 31 => RMD160RoundPrime16To31,
                    <= 47 => RMD160RoundPrime32To47,
                    <= 63 => RMD160RoundPrime48To63,
                    <= 79 => RMD160RoundPrime64To79,
                    _ => throw new ArgumentOutOfRangeException(nameof(i)),
                };

                // Step A
                uint ws = RotateLeft(x0 + RoundOperation(x1, x2, x3, i) + _z[RMD160Ai[i]] + ci, RMD160Ti[i]) + x4;

                // Step B
                x0 = x4;
                x4 = x3;
                x3 = RotateLeft(x2, 10);
                x2 = x1;
                x1 = ws;

                // Step C
                ws = RotateLeft(xp0 + RoundOperation(xp1, xp2, xp3, 79 - i) + _z[RMD160Api[i]] + cpi, RMD160Tpi[i]) + xp4;

                // Step D
                xp0 = xp4;
                xp4 = xp3;
                xp3 = RotateLeft(xp2, 10);
                xp2 = xp1;
                xp1 = ws;
            }

            // Avalanche values
            uint wo = _y0;
            _y0 = _y1 + x2 + xp3;
            _y1 = _y2 + x3 + xp4;
            _y2 = _y3 + x4 + xp0;
            _y3 = _y4 + x0 + xp1;
            _y4 = wo + x1 + xp2;

            // Reset the buffer
            Array.Clear(_z, 0, _z.Length);
            _zPtr = 0;
        }

        /// <summary>
        /// To facilitate software implementation, the round-function Φ is described in terms of operations on 32-bit words.
        /// A sequence of functions g0, g1, …, g79 is used in this round-function, where each function g i, 0 ≤ i ≤ 79, takes
        /// three words X0, X1 and X2 as input and produces a single word as output.
        /// </summary>
        private static uint RoundOperation(uint x0, uint x1, uint x2, int round)
        {
            // [0, 15]
            if (0 <= round && round <= 15)
                return x0 ^ x1 ^ x2;

            // [16, 31]
            else if (16 <= round && round <= 31)
                return (x0 & x1) | (~x0 & x2);

            // [32, 47]
            else if (32 <= round && round <= 47)
                return (x0 | ~x1) ^ x2;

            // [48, 63]
            else if (48 <= round && round <= 63)
                return (x0 & x2) | (x1 & ~x2);

            // [64, 79]
            else if (64 <= round && round <= 79)
                return x0 ^ (x1 | ~x2);

            // Invalid
            throw new ArgumentOutOfRangeException(nameof(round));
        }

        /// <summary>
        /// 32-bit rotate left
        /// </summary>
        private static uint RotateLeft(uint value, int shift)
            => (value << shift) | (value >> (32 - shift));
    }
}