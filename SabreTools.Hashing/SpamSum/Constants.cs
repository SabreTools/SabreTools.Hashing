namespace SabreTools.Hashing.SpamSum
{
    /// <see href="https://download.samba.org/pub/unpacked/junkcode/spamsum/spamsum.c"/> 
    internal static class Constants
    {
        /// <summary>
        /// The output is a string of length 64 in base64
        /// </summary>
        public const int SPAMSUM_LENGTH = 64;

        public const int MIN_BLOCKSIZE = 3;

        public const uint HASH_PRIME = 0x01000193;

        public const uint HASH_INIT = 0x28021967;

        public const uint ROLLING_WINDOW = 7;

        public const string Base64Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
    }
}