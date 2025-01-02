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
    }
}