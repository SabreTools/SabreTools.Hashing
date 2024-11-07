namespace SabreTools.Hashing.XxHash
{
    /// <summary>
    /// Structure for XXH64 streaming API.
    /// </summary>
    /// <see href="https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h"/> 
    internal class XXH64State
    {
        /// <summary>
        /// Total length hashed. This is always 64-bit.
        /// </summary>
        public ulong TotalLength { get; set; }

        /// <summary>
        /// Accumulator lanes
        /// </summary>
        public ulong[] AccumulatorLanes { get; } = new ulong[4];

        /// <summary>
        /// Internal buffer for partial reads. Treated as unsigned char[16].
        /// </summary>
        public ulong[] PartialReadBuffer { get; } = new ulong[4];

        /// <summary>
        /// Amount of data in <see cref="PartialReadBuffer">
        /// </summary>
        public uint Memsize { get; set; }

        /// <summary>
        /// Reserved field, needed for padding anyways
        /// </summary>
        public uint Reserved32 { get; set; }

        /// <summary>
        /// Reserved field. Do not read nor write to it.
        /// </summary>
        public ulong Reserved64 { get; set; }
    }
}