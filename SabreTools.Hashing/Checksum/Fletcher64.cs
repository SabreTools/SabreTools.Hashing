using static SabreTools.Hashing.Checksum.Constants;

namespace SabreTools.Hashing.Checksum
{
    /// <see href="https://en.wikipedia.org/wiki/Fletcher%27s_checksum#Optimizations"/>
    /// <remarks>Uses an Adler-32-like implementation instead of the above</remarks>
    public class Fletcher64 : ChecksumBase<ulong>
    {
        /// <inheritdoc/>
        public override int HashSize => 64;

        public Fletcher64()
        {
            Initialize();
        }

        /// <inheritdoc/>
        protected override void HashCore(byte[] data, int offset, int length)
        {
            // Split Fletcher-64 into component sums
            ulong c0 = _hash & 0xffffffff;
            ulong c1 = (_hash >> 32) & 0xffffffff;

            // In case user likes doing a byte at a time, keep it fast
            if (length == 1)
            {
                c0 += data[offset];
                if (c0 >= F64BASE)
                    c0 -= F64BASE;

                c1 += c0;
                if (c1 >= F64BASE)
                    c1 -= F64BASE;

                _hash = (c1 << 32) | c0;
                return;
            }

            // In case short lengths are provided, keep it somewhat fast
            if (length < 16)
            {
                while (length-- > 0)
                {
                    c0 += data[offset]++;
                    c1 += c0;
                }

                if (c0 >= F64BASE)
                    c0 -= F64BASE;

                // Only added so many BASE's
                c1 %= F64BASE;
                _hash = (c1 << 32) | c0;
                return;
            }

            // Do length NMAX blocks -- requires just one modulo operation
            while (length >= A32NMAX)
            {
                // NMAX is divisible by 16
                length -= A32NMAX;
                uint n = A32NMAX / 16;
                do
                {
                    c0 += data[offset + 0]; c1 += c0;
                    c0 += data[offset + 1]; c1 += c0;
                    c0 += data[offset + 2]; c1 += c0;
                    c0 += data[offset + 3]; c1 += c0;
                    c0 += data[offset + 4]; c1 += c0;
                    c0 += data[offset + 5]; c1 += c0;
                    c0 += data[offset + 6]; c1 += c0;
                    c0 += data[offset + 7]; c1 += c0;
                    c0 += data[offset + 8]; c1 += c0;
                    c0 += data[offset + 9]; c1 += c0;
                    c0 += data[offset + 10]; c1 += c0;
                    c0 += data[offset + 11]; c1 += c0;
                    c0 += data[offset + 12]; c1 += c0;
                    c0 += data[offset + 13]; c1 += c0;
                    c0 += data[offset + 14]; c1 += c0;
                    c0 += data[offset + 15]; c1 += c0;

                    offset += 16;
                } while (--n > 0);
            }

            // Do remaining bytes (less than NMAX, still just one modulo)
            if (length > 0)
            {
                // Avoid modulos if none remaining
                while (length >= 16)
                {
                    length -= 16;

                    c0 += data[offset + 0]; c1 += c0;
                    c0 += data[offset + 1]; c1 += c0;
                    c0 += data[offset + 2]; c1 += c0;
                    c0 += data[offset + 3]; c1 += c0;
                    c0 += data[offset + 4]; c1 += c0;
                    c0 += data[offset + 5]; c1 += c0;
                    c0 += data[offset + 6]; c1 += c0;
                    c0 += data[offset + 7]; c1 += c0;
                    c0 += data[offset + 8]; c1 += c0;
                    c0 += data[offset + 9]; c1 += c0;
                    c0 += data[offset + 10]; c1 += c0;
                    c0 += data[offset + 11]; c1 += c0;
                    c0 += data[offset + 12]; c1 += c0;
                    c0 += data[offset + 13]; c1 += c0;
                    c0 += data[offset + 14]; c1 += c0;
                    c0 += data[offset + 15]; c1 += c0;

                    offset += 16;
                }

                while (length-- > 0)
                {
                    c0 += data[offset++];
                    c1 += c0;
                }

                c0 %= F64BASE;
                c1 %= F64BASE;
            }

            // Return recombined sums
            _hash = (c1 << 32) | c0;
        }
    }
}
