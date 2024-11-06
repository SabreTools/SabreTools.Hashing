namespace SabreTools.Hashing.Crc
{
    internal class CrcTable
    {
        /// <summary>
        /// Optimized implementation table
        /// </summary>
        public readonly ulong[,] OptTable;

        /// <summary>
        /// Number of slices in the optimized table
        /// </summary>
        private const int SliceCount = 8;

        public CrcTable(CrcDefinition def)
        {
            // Get required process variables
            int bitWidth = def.Width < 8 ? 1 : 8;
            ulong msb = 1UL << (def.Width - 1);

            // Initialize the internal tables
            OptTable = new ulong[SliceCount, 1 << bitWidth];

            // Build the standard table
            for (int i = 0; i < 1 << bitWidth; i++)
            {
                // Get the starting value for this index
                ulong point = (ulong)i;
                if (bitWidth > 1 && def.ReflectIn)
                    point = BitOperations.ReverseBits(point, bitWidth);

                // Shift to account for storage
                point <<= def.Width - bitWidth;

                // Accumulate the value
                for (int j = 0; j < 8; j++)
                {
                    if ((point & msb) > 0)
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

            // Build the optimized table
            for (int i = 1; i < SliceCount; i++)
            {
                // Build each slice from the previous
                for (int j = 0; j < 1 << bitWidth; j++)
                {
                    ulong last = OptTable[i - 1, j];
                    OptTable[i, j] = last >> 8 ^ OptTable[0, last & 0xFF];
                }
            }
        }
    }
}