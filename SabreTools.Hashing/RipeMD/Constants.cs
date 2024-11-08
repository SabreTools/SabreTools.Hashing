namespace SabreTools.Hashing.RipeMD
{
    // <see href="https://cdn.standards.iteh.ai/samples/39876/10f9f9f4bb614eaaaeba7e157e183ca3/ISO-IEC-10118-3-2004.pdf"/>
    internal static class Constants
    {
        #region RIPEMD-128

        public const uint RMD128Round00To15 = 0x00000000;
        public const uint RMD128Round16To31 = 0x5A827999;
        public const uint RMD128Round32To47 = 0x6ED9EBA1;
        public const uint RMD128Round48To63 = 0x8F1BBCDC;

        public const uint RMD128RoundPrime00To15 = 0x50A28BE6;
        public const uint RMD128RoundPrime16To31 = 0x5C4DD124;
        public const uint RMD128RoundPrime32To47 = 0x6D703EF3;
        public const uint RMD128RoundPrime48To63 = 0x00000000;

        public const uint RMD128Y0 = 0x67452301;
        public const uint RMD128Y1 = 0xEFCDAB89;
        public const uint RMD128Y2 = 0x98BADCFE;
        public const uint RMD128Y3 = 0x10325476;

        #endregion

        #region RIPEMD-160

        public const uint RMD160Round00To15 = 0x00000000;
        public const uint RMD160Round16To31 = 0x5A827999;
        public const uint RMD160Round32To47 = 0x6ED9EBA1;
        public const uint RMD160Round48To63 = 0x8F1BBCDC;
        public const uint RMD160Round64To79 = 0xA953FD4E;

        public const uint RMD160RoundPrime00To15 = 0x50A28BE6;
        public const uint RMD160RoundPrime16To31 = 0x5C4DD124;
        public const uint RMD160RoundPrime32To47 = 0x6D703EF3;
        public const uint RMD160RoundPrime48To63 = 0x7A6D76E9;
        public const uint RMD160RoundPrime64To79 = 0x00000000;

        public const uint RMD160Y0 = 0x67452301;
        public const uint RMD160Y1 = 0xEFCDAB89;
        public const uint RMD160Y2 = 0x98BADCFE;
        public const uint RMD160Y3 = 0x10325476;
        public const uint RMD160Y4 = 0xC3D2E1F0;

        #endregion
    }
}