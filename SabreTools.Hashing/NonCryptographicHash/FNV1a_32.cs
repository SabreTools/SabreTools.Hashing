using static SabreTools.Hashing.NonCryptographicHash.Constants;

namespace SabreTools.Hashing.NonCryptographicHash
{
    public class FNV1a_32 : FnvBase<uint>
    {
        public FNV1a_32()
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
                _hash = (_hash ^ data[i]) * _prime;
            }
        }
    }
}