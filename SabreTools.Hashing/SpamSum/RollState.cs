using static SabreTools.Hashing.SpamSum.Constants;

namespace SabreTools.Hashing.SpamSum
{
    /// <see href="https://github.com/ssdeep-project/ssdeep/blob/master/fuzzy.c"/> 
    internal class RollState
    {
        public byte[] Window { get; set; }

        public uint H1 { get; set; }

        public uint H2 { get; set; }

        public uint H3 { get; set; }

        public uint N { get; set; }

        public RollState()
        {
            Window = new byte[ROLLING_WINDOW];
        }

        /// <summary>
        /// A rolling hash, based on the Adler checksum. By using a rolling hash
        /// we can perform auto resynchronisation after inserts/deletes.
        /// 
        /// Internally, H1 is the sum of the bytes in the window and H2
        /// is the sum of the bytes times the index.
        /// 
        /// H3 is a shift/xor based rolling hash, and is mostly needed to ensure that
        /// we can cope with large blocksize values.
        /// </summary>
        public void RollHash(byte c)
        {
            H2 -= H1;
            H2 += ROLLING_WINDOW * c;

            H1 += c;
            H1 -= Window[N % ROLLING_WINDOW];

            Window[N % ROLLING_WINDOW] = c;
            N++;

            // The original spamsum AND'ed this value with 0xFFFFFFFF which
            // in theory should have no effect. This AND has been removed
            // for performance (jk)
            H3 <<= 5;
            H3 ^= c;
        }
    
        /// <summary>
        /// Return the current rolling sum
        /// </summary>
        public uint RollSum() => H1 + H2 + H3;
    }
}