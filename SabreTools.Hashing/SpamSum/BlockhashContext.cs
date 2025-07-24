using static SabreTools.Hashing.SpamSum.Constants;

namespace SabreTools.Hashing.SpamSum
{
    /// <summary>
    /// A blockhash contains a signature state for a specific (implicit) blocksize.
    /// The blocksize is given by <see cref="SSDEEP_BS(uint)"/>
    /// </summary>
    /// <see href="https://github.com/ssdeep-project/ssdeep/blob/master/fuzzy.c"/> 
    internal class BlockhashContext
    {
        /// <summary>
        /// Current digest length
        /// </summary>
        public uint DIndex { get; set; }

        /// <summary>
        /// Current message digest
        /// </summary>
        public byte[] Digest { get; set; }

        /// <summary>
        /// Digest value at <see cref="HalfH"/> 
        /// </summary>
        public byte HalfDigest { get; set; }

        /// <summary>
        /// Partial FNV hash
        /// </summary>
        public byte H { get; set; }

        /// <summary>
        /// Partial FNV hash reset after <see cref="Digest"/> is
        /// <see cref="SPAMSUM_LENGTH"/> / 2 long. This is needed
        /// to be able to truncate digest for the second output hash
        /// to stay compatible with ssdeep output.
        /// </summary>
        public byte HalfH { get; set; }

        public BlockhashContext()
        {
            Digest = new byte[SPAMSUM_LENGTH];
        }
    }
}
