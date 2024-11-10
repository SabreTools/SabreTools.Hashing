namespace SabreTools.Hashing.Checksum
{
    internal static class Constants
    {
        #region Adler-32 / Fletcher-32

        /// <summary>
        /// Largest prime smaller than 65536
        /// </summary>
        public const ushort A32BASE = 65521;

        /// <summary>
        /// NMAX is the largest n such that 255n(n+1)/2 + (n+1)(<see cref="A32BASE">-1) <= 2^32-1
        /// </summary>
        public const ushort A32NMAX = 5552;

        /// <summary>
        /// Max value for a single half of a Fletcher-32 checksum
        /// </summary>
        public const ushort F32BASE = 0xffff;

        /// <summary>
        /// Max value for a single half of a Fletcher-64 checksum
        /// </summary>
        public const uint F64BASE = 0xffffffff;

        #endregion
    }
}