using static SabreTools.Hashing.NonCryptographicHash.Constants;

namespace SabreTools.Hashing.NonCryptographicHash
{
    public class FNV1_32 : FnvBase<uint>
    {
        /// <inheritdoc/>
        public override int HashSize => 32;

        public FNV1_32()
        {
            _basis = FNV32Basis;
            _prime = FNV32Prime;
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