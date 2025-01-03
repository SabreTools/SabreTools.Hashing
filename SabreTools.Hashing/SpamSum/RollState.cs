using static SabreTools.Hashing.SpamSum.Constants;

namespace SabreTools.Hashing.SpamSum
{
    /// <see href="https://download.samba.org/pub/unpacked/junkcode/spamsum/spamsum.c"/> 
    public class RollState
    {
        public readonly byte[] Window = new byte[ROLLING_WINDOW];

        public uint H1;

        public uint H2;

        public uint H3;

        public uint N;
    }
}