namespace SabreTools.Hashing.XxHash
{
    // Handle unused private fields
    #pragma warning disable CS0169
    #pragma warning disable CS0414
    #pragma warning disable CS0649

    /// <summary>
    /// Structure for XXH3 streaming API.
    /// </summary>
    /// <see href="https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h"/> 
    internal class XXH3_64State
    {
        /// <summary>
        /// Accumulator lanes
        /// </summary>
        private readonly ulong[] _acc = new ulong[8];

        /// <summary>
        /// Used to store a custom secret generated from a seed.
        /// </summary>
        private readonly byte[] _customSecret = new byte[Constants.XXH3_SECRET_DEFAULT_SIZE];

        /// <summary>
        /// The internal buffer. <see cref="XXH32State._mem32"/>
        /// </summary>
        private readonly byte[] _buffer = new byte[Constants.XXH3_INTERNALBUFFER_SIZE];

        /// <summary>
        /// The amount of memory in <see cref="_buffer"/>, <see cref="XXH32State._memsize"/> 
        /// </summary>
        private uint _bufferedSize;

        /// <summary>
        /// Reserved field. Needed for padding on 64-bit.
        /// </summary>
        private uint _useSeed;

        /// <summary>
        /// Number or stripes processed.
        /// </summary>
        private ulong _stripesSoFar;

        /// <summary>
        /// Total length hashed. 64-bit even on 32-bit targets.
        /// </summary>
        private ulong _totalLength;

        /// <summary>
        /// Number of stripes per block.
        /// </summary>
        private ulong _stripesPerBlock;

        /// <summary>
        /// Size of <see cref="_customSecret"/> or <see cref="_extSecret">
        /// </summary>
        private ulong _secretLimit;

        /// <summary>
        /// Seed for _withSeed variants. Must be zero otherwise, @see XXH3_INITSTATE()
        /// </summary>
        private ulong _seed;

        /// <summary>
        /// Reference to an external secret for the _withSecret variants, NULL
        /// for other variants.
        /// </summary>
        /// <remarks>There may be some padding at the end due to alignment on 64 bytes</remarks>
        private byte[]? _extSecret = null;

        public XXH3_64State()
        {
            // TODO: XXH3_64bits
        }

        /// <param name="seed">The 64-bit seed to alter the hash result predictably.</param>
        public XXH3_64State(ulong seed)
        {
            // TODO: XXH3_64bits_withSeed
        }

        /// <param name="secret">The secret data.</param>
        public XXH3_64State(byte[] secret)
        {
            // TODO: XXH3_64bits_withSecret
        }

        /// <summary>
        /// Resets to begin a new hash
        /// </summary>
        /// <param name="seed">The 64-bit seed to alter the hash result predictably.</param>
        public void Reset()
        {
            // TODO: XXH3_64bits_reset
        }

        /// <summary>
        /// Resets to begin a new hash
        /// </summary>
        /// <param name="seed">The 64-bit seed to alter the hash result predictably.</param>
        public void Reset(ulong seed)
        {
            // TODO: XXH3_64bits_reset_withSeed
        }

        /// <summary>
        /// Resets to begin a new hash
        /// </summary>
        /// <param name="secret">The secret data.</param>
        public void Reset(byte[] secret)
        {
            // TODO: XXH3_64bits_reset_withSecret
        }
    
        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
        {
            // TODO: XXH3_64bits_update
        }

        /// <summary>
        /// Returns the calculated hash value
        /// </summary>
        /// <returns>The calculated 64-bit xxHash64 value from that state.</returns>
        public ulong Digest()
        {
            // TODO: XXH3_64bits_digest
            return ulong.MaxValue;
        }
    }
}