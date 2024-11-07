namespace SabreTools.Hashing.XxHash
{
    /// <summary>
    /// Structure for XXH3 streaming API.
    /// </summary>
    /// <see href="https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h"/> 
    internal class XXH3State
    {
        /// <summary>
        /// Accumulator lanes
        /// </summary>
        public ulong[] AccumulatorLanes { get; } = new ulong[8];

        /// <summary>
        /// Used to store a custom secret generated from a seed.
        /// </summary>
        public byte[] CustomSecret { get; } = new byte[Constants.XXH3_SECRET_DEFAULT_SIZE];

        /// <summary>
        /// The internal buffer. <see cref="XXH32State.PartialReadBuffer"/>
        /// </summary>
        public byte[] Buffer { get; } = new byte[Constants.XXH3_INTERNALBUFFER_SIZE];

        /// <summary>
        /// The amount of memory in <see cref="Buffer"/>, <see cref="XXH32State.Memsize"/> 
        /// </summary>
        public uint BufferedSize { get; set; }

        /// <summary>
        /// Reserved field. Needed for padding on 64-bit.
        /// </summary>
        public uint UseSeed { get; set; }

        /// <summary>
        /// Number or stripes processed.
        /// </summary>
        public ulong StripesSoFar { get; set; }

        /// <summary>
        /// Total length hashed. 64-bit even on 32-bit targets.
        /// </summary>
        public ulong TotalLength { get; set; }

        /// <summary>
        /// Number of stripes per block.
        /// </summary>
        public ulong StripesPerBlock { get; set; }

        /// <summary>
        /// Size of <see cref="CustomSecret"/> or <see cref="ExtSecret">
        /// </summary>
        public ulong SecretLimit { get; set; }

        /// <summary>
        /// Seed for _withSeed variants. Must be zero otherwise, @see XXH3_INITSTATE()
        /// </summary>
        public ulong Seed { get; set; }

        /// <summary>
        /// Reserved field.
        /// </summary>
        public ulong Reserved64 { get; set; }

        /// <summary>
        /// Reference to an external secret for the _withSecret variants, NULL
        /// for other variants.
        /// </summary>
        /// <remarks>There may be some padding at the end due to alignment on 64 bytes</remarks>
        public byte[]? ExtSecret { get; set; }
    }
}