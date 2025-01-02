using static SabreTools.Hashing.NonCryptographicHash.Constants;

namespace SabreTools.Hashing.NonCryptographicHash
{
    public class FNV0_32 : FnvBase<uint>
    {
        public FNV0_32()
        {
            _basis = 0;
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