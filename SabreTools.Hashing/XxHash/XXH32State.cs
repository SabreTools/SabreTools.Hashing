namespace SabreTools.Hashing.XxHash
{
    /// <summary>
    /// Structure for XXH32 streaming API.
    /// </summary>
    /// <see href="https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h"/> 
    internal class XXH32State
    {
        /// <summary>
        /// Total length hashed, modulo 2^32
        /// </summary>
        public uint TotalLength { get; set; }

        /// <summary>
        /// Whether the hash is >= 16 (handles @ref total_len_32 overflow)
        /// </summary>
        public uint LargeLength { get; set; }

        /// <summary>
        /// Accumulator lanes
        /// </summary>
        public uint[] AccumulatorLanes { get; } = new uint[4];

        /// <summary>
        /// Internal buffer for partial reads. Treated as unsigned char[16].
        /// </summary>
        public uint[] PartialReadBuffer { get; } = new uint[4];

        /// <summary>
        /// Amount of data in <see cref="PartialReadBuffer">
        /// </summary>
        public uint Memsize { get; set; }

        /// <summary>
        /// Reserved field. Do not read nor write to it.
        /// </summary>
        public uint Reserved { get; set; }
    }
}