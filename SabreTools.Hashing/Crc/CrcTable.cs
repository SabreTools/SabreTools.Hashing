namespace SabreTools.Hashing.Crc
{
    internal class CrcTable
    {
        /// <summary>
        /// Optimized implementation table
        /// </summary>
        public readonly ulong[,] OptTable;

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

            // Initialize the internal tables
            OptTable = new ulong[SliceCount, 1 << _processBits];

            // Build the standard table
            for (int i = 0; i < 1 << _processBits; i++)
            {
                // Get the starting value for this index
                ulong point = (ulong)i;
                if (!_processBitwise && def.ReflectIn)
                    point = BitOperations.ReverseBits(point, _processBits);

                // Shift to account for storage
                point <<= _definition.Width - _processBits;

                // Accumulate the value
                for (int j = 0; j < 8; j++)
                {
                    if ((point & _bitMask) > 0)
                        point = (point << 1) ^ def.Poly;
                    else
                        point <<= 1;
                }

                // Reflect if necessary
                if (def.ReflectIn)
                    point = BitOperations.ReverseBits(point, def.Width);

                // Shift back to account for storage
                point &= ulong.MaxValue >> (64 - def.Width);

                // Assign to both tables
                OptTable[0, i] = point;
            }

            // Build the optimized table -- FIX FOR NON-32, NON-REFLECT
            // for (int i = 1; i < SliceCount; i++)
            // {
            //     // Build each slice from the previous
            //     for (int j = 0; j < 1 << ProcessBits; j++)
            //     {
            //         ulong last = OptTable[i - 1, j];
            //         OptTable[i, j] = (last >> ProcessBits) ^ OptTable[0, last & 0xFF];
            //     }
            // }
        }
    
        /// <summary>
        /// Perform a single checksum step
        /// </summary>
        /// <param name="hash">Current hash value, updated on run</param>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the data to process</param>
        public void PerformChecksumStep(ref ulong hash, byte[] data, int offset)
        {
            // Per-bit processing
            if (_processBitwise)
            {
                for (int b = 0; b < 8; b++)
                {
                    if (_definition.ReflectIn)
                        hash = (hash >> _processBits) ^ OptTable[0, (byte)(hash & 1) ^ ((byte)(data[offset] >> b) & 1)];
                    else
                        hash = (hash << _processBits) ^ OptTable[0, (byte)((hash >> _bitShift) & 1) ^ ((byte)(data[offset] >> (7 - b)) & 1)];
                }
            }

            // Per-byte processing
            else
            {
                if (_definition.ReflectIn)
                    hash = (hash >> _processBits) ^ OptTable[0, (byte)hash ^ data[offset]];
                else
                    hash = (hash << _processBits) ^ OptTable[0, ((byte)(hash >> _bitShift)) ^ data[offset]];
            }
        }
    }
}