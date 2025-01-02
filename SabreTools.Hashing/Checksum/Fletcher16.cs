namespace SabreTools.Hashing.Checksum
{
    /// <see href="https://en.wikipedia.org/wiki/Fletcher%27s_checksum#Optimizations"/> 
    public class Fletcher16 : ChecksumBase<ushort>
    {
        public Fletcher16()
        {
            Initialize();
        }

        /// <inheritdoc/>
        public override void HashCore(byte[] data, int offset, int length)
        {
            // Split the existing hash
            uint c0 = (uint)(_hash & 0x00FF);
            uint c1 = (uint)(_hash << 16);

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

            _hash = (ushort)(c1 << 8 | c0);
        }
    }
}