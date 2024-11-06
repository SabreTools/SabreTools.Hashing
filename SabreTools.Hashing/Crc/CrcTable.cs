namespace SabreTools.Hashing.Crc
{
    internal class CrcTable
    {
        /// <summary>
        /// Optimized implementation table
        /// </summary>
        public readonly ulong[,] OptTable;

        /// <summary>
        /// Number of bits to process at a time
        /// </summary>
        public readonly int ProcessBits;

        /// <summary>
        /// Bit shift based on the CRC width
        /// </summary>
        public readonly int BitShift;

        /// <summary>
        /// Bit mask based on the CRC width
        /// </summary>
        public readonly ulong BitMask;

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
            ProcessBits = _definition.Width < 8 ? 1 : 8;
            BitShift = _definition.Width - ProcessBits;
            BitMask = 1UL << (_definition.Width - 1);

            // Initialize the internal tables
            OptTable = new ulong[SliceCount, 1 << ProcessBits];

            // Build the standard table
            for (int i = 0; i < 1 << ProcessBits; i++)
            {
                // Get the starting value for this index
                ulong point = (ulong)i;
                if (ProcessBits > 1 && def.ReflectIn)
                    point = BitOperations.ReverseBits(point, ProcessBits);

                // Shift to account for storage
                point <<= _definition.Width - ProcessBits;

                // Accumulate the value
                for (int j = 0; j < 8; j++)
                {
                    if ((point & BitMask) > 0)
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
            // Per-byte processing
            if (_definition.Width >= 8)
            {
                if (_definition.ReflectIn)
                    hash = (hash >> 8) ^ OptTable[0, (byte)hash ^ data[offset]];
                else
                    hash = (hash << 8) ^ OptTable[0, ((byte)(hash >> BitShift)) ^ data[offset]];
            }

            // Per-bit processing
            else
            {
                for (int b = 0; b < 8; b++)
                {
                    if (_definition.ReflectIn)
                        hash = (hash >> 1) ^ OptTable[0, (byte)(hash & 1) ^ ((byte)(data[offset] >> b) & 1)];
                    else
                        hash = (hash << 1) ^ OptTable[0, (byte)((hash >> BitShift) & 1) ^ ((byte)(data[offset] >> (7 - b)) & 1)];
                }
            }
        }
    }
}