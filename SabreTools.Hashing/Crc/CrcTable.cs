namespace SabreTools.Hashing.Crc
{
    internal class CrcTable
    {
        /// <summary>
        /// Indicates if CRC should be processed bitwise instead of bytewise
        /// </summary>
        private readonly bool _processBitwise;

        /// <summary>
        /// Number of bits to process at a time
        /// </summary>
        private readonly int _processBits;

        /// <summary>
        /// Bit shift based on the CRC width
        /// </summary>
        private readonly int _bitShift;

        /// <summary>
        /// Bit mask based on the CRC width
        /// </summary>
        private readonly ulong _bitMask;

        /// <summary>
        /// Mapping table
        /// </summary>
        private readonly ulong[,] _table;

        /// <summary>
        /// Definition used to build the table
        /// </summary>
        private readonly CrcDefinition _definition;

        /// <summary>
        /// Number of slices in the optimized table
        /// </summary>
        private const int SliceCount = 8;

        public CrcTable(CrcDefinition def)
        {
            // Set the accessible fields
            _definition = def;
            _processBitwise = _definition.Width < 8;
            _processBits = _processBitwise ? 1 : 8;
            _bitShift = _definition.Width - _processBits;
            _bitMask = 1UL << (_definition.Width - 1);

            // Initialize the internal
            _table = new ulong[SliceCount, 1 << _processBits];

            // Build the standard table
            for (uint i = 0; i < (1 << _processBits); i++)
            {
                // Get the starting value for this index
                ulong point = i;
                if (!_processBitwise && def.ReflectIn)
                    point = BitOperations.ReverseBits(point, _processBits);

                // Shift to account for storage
                point <<= _definition.Width - _processBits;

                // Accumulate the value
                for (int j = 0; j < _processBits; j++)
                {
                    if ((point & _bitMask) > 0UL)
                        point = (point << 1) ^ def.Poly;
                    else
                        point <<= 1;
                }

                // Reflect if necessary
                if (def.ReflectIn)
                    point = BitOperations.ReverseBits(point, def.Width);

                // Shift back to account for storage
                point &= ulong.MaxValue >> (64 - def.Width);

                // Assign to the table
                _table[0, i] = point;
            }

            // Skip building the optimized table for bitwise processing
            if (_processBitwise)
                return;

            // Build the optimized table for non-bitwise processing
            for (int i = 1; i < SliceCount; i++)
            {
                // Build each slice from the previous
                for (int j = 0; j < 1 << _processBits; j++)
                {
                    ulong last = _table[i - 1, j];
                    if (_definition.ReflectIn)
                        _table[i, j] = (last >> _processBits) ^ _table[0, (byte)last];
                    else
                        _table[i, j] = (last << _processBits) ^ _table[0, (byte)(last >> _bitShift)];
                }
            }
        }

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="hash">Current hash value, updated on run</param>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(ref ulong hash, byte[] data, int offset, int length)
        {
            // Empty data just returns
            if (data.Length == 0)
                return;

            // Check for valid offset and length
            if (offset > data.Length)
                throw new System.ArgumentOutOfRangeException(nameof(offset));
            else if (offset + length > data.Length)
                throw new System.ArgumentOutOfRangeException(nameof(length));

            // Try transforming fast first
            if (TransformBlockFast(ref hash, data, offset, length))
                return;

            // Process the data byte-wise
            for (int i = offset; i < offset + length; i++)
            {
                PerformChecksumStep(ref hash, data, i);
            }
        }

        /// <summary>
        /// Perform a single checksum step
        /// </summary>
        /// <param name="hash">Current hash value, updated on run</param>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the data to process</param>
        private void PerformChecksumStep(ref ulong hash, byte[] data, int offset)
        {
            // Per-bit processing
            if (_processBitwise)
            {
                for (int b = 0; b < 8; b++)
                {
                    if (_definition.ReflectIn)
                        hash = (hash >> 1) ^ _table[0, (byte)(hash & 1) ^ ((byte)(data[offset] >> b) & 1)];
                    else
                        hash = (hash << 1) ^ _table[0, (byte)((hash >> _bitShift) & 1) ^ ((byte)(data[offset] >> (7 - b)) & 1)];
                }
            }

            // Per-byte processing
            else
            {
                if (_definition.ReflectIn)
                    hash = (hash >> 8) ^ _table[0, (byte)hash ^ data[offset]];
                else
                    hash = (hash << 8) ^ _table[0, ((byte)(hash >> _bitShift)) ^ data[offset]];
            }
        }

        /// <summary>
        /// Perform an optimized transform step
        /// </summary>
        private bool TransformBlockFast(ref ulong hash, byte[] data, int offset, int length)
        {
            // Bitwise transformations are not optimized
            if (_processBitwise)
                return false;

            // Only certain widths can be optimized
            if (System.Array.IndexOf([16, 24, 32, 64], _definition.Width) == -1)
                return false;

            // All reflection-in implementations share an optimized path
            if (_definition.ReflectIn)
            {
                TransformBlockFastReflect(ref hash, data, offset, length);
                return true;
            }

            // CRC-32 with no reflection-in has can be optimized
            if (_definition.Width == 32 && !_definition.ReflectIn)
            {
                TransformBlockFastNoReflect(ref hash, data, offset, length);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Optimized transformation for 16/24/32/64-bit CRC with reflection
        /// </summary>
        private void TransformBlockFastReflect(ref ulong hash, byte[] data, int offset, int length)
        {
            // Process on a copy of the hash
            ulong local = hash;

            // Process aligned data
            if (length > 4)
            {
                long end = offset + (length & ~(uint)3);
                length &= 3;

                while (offset < end)
                {
                    ulong low = local ^ (uint)(
                          (data[offset + 0]      )
                        + (data[offset + 1] << 8 )
                        + (data[offset + 2] << 16)
                        + (data[offset + 3] << 24));
                    offset += 4;

                    local = _table[3, (byte)(low      )]
                          ^ _table[2, (byte)(low >> 8 )]
                          ^ _table[1, (byte)(low >> 16)]
                          ^ _table[0, (byte)(low >> 24)]
                          ^ local >> 32;
                }
            }

            // Process unaligned data
            while (length-- != 0)
            {
                PerformChecksumStep(ref local, data, offset++);
            }

            // Assign the new hash value
            hash = local;
        }
    
        /// <summary>
        /// Optimized transformation for 32-bit CRC with no reflection
        /// </summary>
        private void TransformBlockFastNoReflect(ref ulong hash, byte[] data, int offset, int length)
        {
            // Process on a copy of the hash
            ulong local = hash;

            // Process aligned data
            if (length > 4)
            {
                long end = offset + (length & ~(uint)3);
                length &= 3;

                while (offset < end)
                {
                    ulong low = local ^ (uint)(
                          (data[offset + 3]      )
                        + (data[offset + 2] << 8 )
                        + (data[offset + 1] << 16)
                        + (data[offset + 0] << 24));
                    offset += 4;

                    local = _table[0, (byte)(low      )]
                          ^ _table[1, (byte)(low >> 8 )]
                          ^ _table[2, (byte)(low >> 16)]
                          ^ _table[3, (byte)(low >> 24)];
                }
            }

            // Process unaligned data
            while (length-- != 0)
            {
                PerformChecksumStep(ref local, data, offset++);
            }

            // Assign the new hash value
            hash = local;
        }
    }
}