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

        /// <summary>
        /// The 32-bit seed to alter the hash's output predictably.
        /// </summary>
        private uint _seed;

        public XXH32State()
        {
            _seed = 0;
        }

        /// <param name="seed">The 32-bit seed to alter the hash result predictably.</param>
        public XXH32State(uint seed)
        {
            _seed = seed;
        }
    
        /// <summary>
        /// Resets to begin a new hash
        /// </summary>
        /// <param name="seed">The 32-bit seed to alter the hash result predictably.</param>
        public void Reset(uint seed)
        {
            // TODO: XXH32_reset function
        }
    
        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
        {
            // TODO: XXH32_update function
        }

        /// <summary>
        /// Returns the calculated hash value
        /// </summary>
        /// <returns>The calculated 32-bit xxHash32 value from that state.</returns>
        public uint Digest()
        {
            // TODO: XXH32_digest
            return uint.MaxValue;
        }
    }
}