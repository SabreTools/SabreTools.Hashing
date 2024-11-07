namespace SabreTools.Hashing.XxHash
{
    // https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h
    internal static class Constants
    {
        #region XXH32

        public const uint XXH_PRIME32_1 = 0x9E3779B1;

        public const uint XXH_PRIME32_2 = 0x85EBCA77;

        public const uint XXH_PRIME32_3 = 0xC2B2AE3D;

        public const uint XXH_PRIME32_4 = 0x27D4EB2F;

        public const uint XXH_PRIME32_5 = 0x165667B1;

        #endregion

        #region XXH64

        public const ulong XXH_PRIME64_1 = 0x9E3779B185EBCA87;

        public const ulong XXH_PRIME64_2 = 0xC2B2AE3D27D4EB4F;

        public const ulong XXH_PRIME64_3 = 0x165667B19E3779F9;

        public const ulong XXH_PRIME64_4 = 0x85EBCA77C2B2AE63;

        public const ulong XXH_PRIME64_5 = 0x27D4EB2F165667C5;

        #endregion

        #region XXH3

        /// <summary>
        /// Pseudorandom secret taken directly from FARSH.
        /// </summary>
        public static readonly byte[] XXH3_kSecret =
        [
            0xb8, 0xfe, 0x6c, 0x39, 0x23, 0xa4, 0x4b, 0xbe, 0x7c, 0x01, 0x81, 0x2c, 0xf7, 0x21, 0xad, 0x1c,
            0xde, 0xd4, 0x6d, 0xe9, 0x83, 0x90, 0x97, 0xdb, 0x72, 0x40, 0xa4, 0xa4, 0xb7, 0xb3, 0x67, 0x1f,
            0xcb, 0x79, 0xe6, 0x4e, 0xcc, 0xc0, 0xe5, 0x78, 0x82, 0x5a, 0xd0, 0x7d, 0xcc, 0xff, 0x72, 0x21,
            0xb8, 0x08, 0x46, 0x74, 0xf7, 0x43, 0x24, 0x8e, 0xe0, 0x35, 0x90, 0xe6, 0x81, 0x3a, 0x26, 0x4c,
            0x3c, 0x28, 0x52, 0xbb, 0x91, 0xc3, 0x00, 0xcb, 0x88, 0xd0, 0x65, 0x8b, 0x1b, 0x53, 0x2e, 0xa3,
            0x71, 0x64, 0x48, 0x97, 0xa2, 0x0d, 0xf9, 0x4e, 0x38, 0x19, 0xef, 0x46, 0xa9, 0xde, 0xac, 0xd8,
            0xa8, 0xfa, 0x76, 0x3f, 0xe3, 0x9c, 0x34, 0x3f, 0xf9, 0xdc, 0xbb, 0xc7, 0xc7, 0x0b, 0x4f, 0x1d,
            0x8a, 0x51, 0xe0, 0x4b, 0xcd, 0xb4, 0x59, 0x31, 0xc8, 0x9f, 0x7e, 0xc9, 0xd9, 0x78, 0x73, 0x64,
            0xea, 0xc5, 0xac, 0x83, 0x34, 0xd3, 0xeb, 0xc3, 0xc5, 0x81, 0xa0, 0xff, 0xfa, 0x13, 0x63, 0xeb,
            0x17, 0x0d, 0xdd, 0x51, 0xb7, 0xf0, 0xda, 0x49, 0xd3, 0x16, 0x55, 0x26, 0x29, 0xd4, 0x68, 0x9e,
            0x2b, 0x16, 0xbe, 0x58, 0x7d, 0x47, 0xa1, 0xfc, 0x8f, 0xf8, 0xb8, 0xd1, 0x7a, 0xd0, 0x31, 0xce,
            0x45, 0xcb, 0x3a, 0x8f, 0x95, 0x16, 0x04, 0x28, 0xaf, 0xd7, 0xfb, 0xca, 0xbb, 0x4b, 0x40, 0x7e,
        ];

        public const ulong PRIME_MX1 = 0x165667919E3779F9;

        public const ulong PRIME_MX2 = 0x9FB21C651E98DF25;

        /// <summary>
        /// The size of the internal XXH3 buffer.
        /// This is the optimal update size for incremental hashing.
        /// </summary>
        public const int XXH3_INTERNALBUFFER_SIZE = 256;

        /// <summary>
        /// Default size of the secret buffer (and <see cref="XXH3_kSecret"/>).
        /// This is the size used in <see cref="XXH3_kSecret"/> and the seeded functions.
        /// Not to be confused with @ref XXH3_SECRET_SIZE_MIN.
        /// </summary>
        public const int XXH3_SECRET_DEFAULT_SIZE = 192;

        #endregion
    }
}