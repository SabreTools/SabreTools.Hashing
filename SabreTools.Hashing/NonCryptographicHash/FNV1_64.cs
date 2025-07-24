using static SabreTools.Hashing.NonCryptographicHash.Constants;

namespace SabreTools.Hashing.NonCryptographicHash
{
    public class FNV1_64 : FnvBase<ulong>
    {
        /// <inheritdoc/>
        public override int HashSize => 64;

        public FNV1_64()
        {
            _basis = FNV64Basis;
            _prime = FNV64Prime;
            Initialize();
        }

        /// <inheritdoc/>
        protected override void HashCore(byte[] data, int offset, int length)
        {
            for (int i = offset; length > 0; i++, length--)
            {
                _hash = (_hash * _prime) ^ data[i];
            }
        }
    }
}
