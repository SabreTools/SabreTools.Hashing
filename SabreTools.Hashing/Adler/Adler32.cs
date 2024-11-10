using System;
using static SabreTools.Hashing.Adler.Constants;

namespace SabreTools.Hashing.Adler
{
    /// <see href="https://github.com/madler/zlib/blob/v1.2.11/adler32.c"/> 
    public class Adler32
    {
        /// <summary>
        /// The current value of the hash
        /// </summary>
        private uint _hash;

        public Adler32()
        {
            Reset();
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
        {
            _hash = 1;
        }

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
        {
            uint n;

            // Split Adler-32 into component sums
            uint sum2 = (_hash >> 16) & 0xffff;
            _hash &= 0xffff;

            // In case user likes doing a byte at a time, keep it fast
            if (length == 1)
            {
                _hash += data[offset + 0];
                if (_hash >= BASE)
                    _hash -= BASE;

                sum2 += _hash;
                if (sum2 >= BASE)
                    sum2 -= BASE;

                _hash |= sum2 << 16;
                return;
            }

            // In case short lengths are provided, keep it somewhat fast
            if (length < 16)
            {
                while (length-- > 0)
                {
                    _hash += data[offset]++;
                    sum2 += _hash;
                }

                if (_hash >= BASE)
                    _hash -= BASE;

                // Only added so many BASE's
                sum2 %= BASE;
                _hash |= sum2 << 16;
                return;
            }

            // Do length NMAX blocks -- requires just one modulo operation
            while (length >= NMAX)
            {
                // NMAX is divisible by 16
                length -= NMAX;
                n = NMAX / 16;
                do
                {
                    // 16 sums unrolled
                    _hash += data[offset + 0]; sum2 += _hash;
                    _hash += data[offset + 1]; sum2 += _hash;
                    _hash += data[offset + 2]; sum2 += _hash;
                    _hash += data[offset + 3]; sum2 += _hash;
                    _hash += data[offset + 4]; sum2 += _hash;
                    _hash += data[offset + 5]; sum2 += _hash;
                    _hash += data[offset + 6]; sum2 += _hash;
                    _hash += data[offset + 7]; sum2 += _hash;
                    _hash += data[offset + 8]; sum2 += _hash;
                    _hash += data[offset + 9]; sum2 += _hash;
                    _hash += data[offset + 10]; sum2 += _hash;
                    _hash += data[offset + 11]; sum2 += _hash;
                    _hash += data[offset + 12]; sum2 += _hash;
                    _hash += data[offset + 13]; sum2 += _hash;
                    _hash += data[offset + 14]; sum2 += _hash;
                    _hash += data[offset + 15]; sum2 += _hash;

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

                    _hash += data[offset + 0]; sum2 += _hash;
                    _hash += data[offset + 1]; sum2 += _hash;
                    _hash += data[offset + 2]; sum2 += _hash;
                    _hash += data[offset + 3]; sum2 += _hash;
                    _hash += data[offset + 4]; sum2 += _hash;
                    _hash += data[offset + 5]; sum2 += _hash;
                    _hash += data[offset + 6]; sum2 += _hash;
                    _hash += data[offset + 7]; sum2 += _hash;
                    _hash += data[offset + 8]; sum2 += _hash;
                    _hash += data[offset + 9]; sum2 += _hash;
                    _hash += data[offset + 10]; sum2 += _hash;
                    _hash += data[offset + 11]; sum2 += _hash;
                    _hash += data[offset + 12]; sum2 += _hash;
                    _hash += data[offset + 13]; sum2 += _hash;
                    _hash += data[offset + 14]; sum2 += _hash;
                    _hash += data[offset + 15]; sum2 += _hash;

                    offset += 16;
                }

                while (length-- > 0)
                {
                    _hash += data[offset++];
                    sum2 += _hash;
                }

                _hash %= BASE;
                sum2 %= BASE;
            }

            // Return recombined sums
            _hash |= sum2 << 16;
        }

        /// <summary>
        /// Finalize the hash and return as a byte array
        /// </summary>
        public byte[] Finalize()
        {
            return BitConverter.GetBytes(_hash);
        }
    }
}