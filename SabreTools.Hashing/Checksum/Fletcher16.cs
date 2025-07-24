namespace SabreTools.Hashing.Checksum
{
    /// <see href="https://en.wikipedia.org/wiki/Fletcher%27s_checksum#Optimizations"/> 
    public class Fletcher16 : ChecksumBase<ushort>
    {
        /// <inheritdoc/>
        public override int HashSize => 16;

        public Fletcher16()
        {
            Initialize();
        }

        /// <inheritdoc/>
        protected override void HashCore(byte[] data, int offset, int length)
        {
            // Split the existing hash
            uint c0 = (uint)(_hash & 0x00ff);
            uint c1 = (uint)((_hash >> 8) & 0x00ff);

            // Found by solving for c1 overflow:
            // n > 0 and n * (n+1) / 2 * (2^8-1) < (2^32-1).
            while (length > 0)
            {
                int blocklen = length;
                if (blocklen > 5802)
                    blocklen = 5802;

                length -= blocklen;
                do
                {
                    c0 += data[offset++];
                    c1 += c0;
                } while (--blocklen > 0);

                c0 %= 255;
                c1 %= 255;
            }

            // Return recombined sums
            _hash = (ushort)((c1 << 8) | c0);
        }
    }
}
