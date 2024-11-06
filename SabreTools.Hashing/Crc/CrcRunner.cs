using System;

namespace SabreTools.Hashing.Crc
{
    internal class CrcRunner
    {
        /// <summary>
        /// Definition used to create the runner
        /// </summary>
        private readonly CrcDefinition _definition;

        /// <summary>
        /// Table used for calculation steps
        /// </summary>
        private readonly CrcTable _table;

        /// <summary>
        /// The current value of the hash
        /// </summary>
        private ulong _hash;

        public CrcRunner(CrcDefinition def)
        {
            // Check for a valid bit width
            if (def.Width < 0 || def.Width > 64)
                throw new ArgumentOutOfRangeException(nameof(def));

            _definition = def;
            _table = new CrcTable(def);
            _hash = def.Init;
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
        {
            _hash = _definition.Init;
        }

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
        {
            // Empty data just returns
            if (data.Length == 0)
                return;

            // Check for valid offset and length
            if (offset > data.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
            else if (offset + length > data.Length)
                throw new ArgumentOutOfRangeException(nameof(length));

            // // Try transforming fast first
            // if (TransformBlockFast(data, offset, length))
            //     return;

            // Process the data byte-wise
            for (int i = offset; i < offset + length; i++)
            {
                _table.PerformChecksumStep(ref _hash, data, i);
            }
        }

        /// <summary>
        /// Finalize the hash and return as a byte array
        /// </summary>
        public byte[] Finalize()
        {
            // Create a copy of the hash
            ulong localHash = _hash;

            // Handle mutual reflection
            if (_definition.ReflectIn ^ _definition.ReflectOut)
                localHash = BitOperations.ReverseBits(localHash, _definition.Width);

            // Handle XOR
            localHash ^= _definition.XorOut;

            // Process the value and return
            return BitOperations.ClampValueToBytes(localHash, _definition.Width);
        }

        /// <summary>
        /// Perform an optimized transform step
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        private bool TransformBlockFast(byte[] data, int offset, int length)
        {
            // Check for optimizable transformation
            if (_definition.Width != 32 || !_definition.ReflectIn)
                return false;

            for (; (offset & 7) != 0 && length != 0; length--)
            {
                _table.PerformChecksumStep(ref _hash, data, offset++);
            }

            if (length >= 8)
            {
                int end = (length - 8) & ~7;
                length -= end;
                end += offset;

                while (offset != end)
                {
                    _hash ^= (ulong)(
                          (data[offset + 0]      )
                        + (data[offset + 1] << 8 )
                        + (data[offset + 2] << 16)
                        + (data[offset + 3] << 24)
                        + (data[offset + 4] << 32)
                        + (data[offset + 5] << 40)
                        + (data[offset + 6] << 48)
                        + (data[offset + 7] << 56));
                    offset += 8;

                    _hash = _table.OptTable[7, (byte)(_hash      )]
                          ^ _table.OptTable[6, (byte)(_hash >>= 8)]
                          ^ _table.OptTable[5, (byte)(_hash >>= 8)]
                          ^ _table.OptTable[4, (byte)(_hash >>= 8)]
                          ^ _table.OptTable[3, (byte)(_hash >>= 8)]
                          ^ _table.OptTable[2, (byte)(_hash >>= 8)]
                          ^ _table.OptTable[1, (byte)(_hash >>= 8)]
                          ^ _table.OptTable[0, (byte)(_hash >>  8)];
                }
            }

            while (length-- != 0)
            {
                _table.PerformChecksumStep(ref _hash, data, offset++);
            }

            return true;
        }
    }
}