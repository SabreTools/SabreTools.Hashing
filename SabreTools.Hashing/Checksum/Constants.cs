namespace SabreTools.Hashing.Checksum
{
    internal static class Constants
    {
        #region Adler-32

        /// <summary>
        /// Largest prime smaller than 65536
        /// </summary>
        public const ushort A32BASE = 65521;

        /// <summary>
        /// NMAX is the largest n such that 255n(n+1)/2 + (n+1)(<see cref="A32BASE">-1) <= 2^32-1
        /// </summary>
        public const ushort A32NMAX = 5552;

        #endregion
    }
}