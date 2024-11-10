namespace SabreTools.Hashing.Adler
{
    internal static class Constants
    {
        /// <summary>
        /// Largest prime smaller than 65536
        /// </summary>
        public const ushort BASE = 65521;

        /// <summary>
        /// NMAX is the largest n such that 255n(n+1)/2 + (n+1)(<see cref="BASE">-1) <= 2^32-1
        /// </summary>
        public const ushort NMAX = 5552;
    }
}