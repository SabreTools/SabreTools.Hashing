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

        /// <summary>
        /// The 64-bit seed to alter the hash's output predictably.
        /// </summary>
        private ulong _seed;

        public XXH64State()
        {
            _seed = 0;
        }

        /// <param name="seed">The 64-bit seed to alter the hash result predictably.</param>
        public XXH64State(ulong seed)
        {
            _seed = seed;
        }
    
        /// <summary>
        /// Resets to begin a new hash
        /// </summary>
        /// <param name="seed">The 64-bit seed to alter the hash result predictably.</param>
        public void Reset(ulong seed)
        {
            // TODO: XXH64_reset function
        }
    
        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
        {
            // TODO: XXH64_update function
        }

        /// <summary>
        /// Returns the calculated hash value
        /// </summary>
        /// <returns>The calculated 64-bit xxHash64 value from that state.</returns>
        public ulong Digest()
        {
            // TODO: XXH64_update
            return ulong.MaxValue;
        }
    }
}