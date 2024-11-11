namespace SabreTools.Hashing.Checksum
{
    internal abstract class FnvBase<T> : ChecksumBase<T> where T : struct
    {
        /// <summary>
        /// Initial value to use
        /// </summary>
        protected T _basis;

        /// <summary>
        /// Round prime to use
        /// </summary>
        protected T _prime;

        /// <inheritdoc/>
        public override void Reset()
        {
            _hash = _basis;
        }
    }
}