namespace SabreTools.Hashing.NonCryptographicHash
{
    internal static class Constants
    {
        #region FNV

        public const uint FNV32Basis = 0x811c9dc5;
        public const ulong FNV64Basis = 0xcbf29ce484222325;

        public const uint FNV32Prime = 0x01000193;
        public const ulong FNV64Prime = 0x00000100000001b3;

        #endregion

        #region xxHash-32

        public const uint XXH_PRIME32_1 = 0x9E3779B1;

        public const uint XXH_PRIME32_2 = 0x85EBCA77;

        public const uint XXH_PRIME32_3 = 0xC2B2AE3D;

        public const uint XXH_PRIME32_4 = 0x27D4EB2F;

        public const uint XXH_PRIME32_5 = 0x165667B1;

        #endregion

        #region xxHash-64

        public const ulong XXH_PRIME64_1 = 0x9E3779B185EBCA87;

        public const ulong XXH_PRIME64_2 = 0xC2B2AE3D27D4EB4F;

        public const ulong XXH_PRIME64_3 = 0x165667B19E3779F9;

        public const ulong XXH_PRIME64_4 = 0x85EBCA77C2B2AE63;

        public const ulong XXH_PRIME64_5 = 0x27D4EB2F165667C5;

        #endregion
    }
}