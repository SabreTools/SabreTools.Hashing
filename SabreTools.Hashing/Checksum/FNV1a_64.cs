using static SabreTools.Hashing.Checksum.Constants;

namespace SabreTools.Hashing.Checksum
{
    public class FNV1a_64 : FnvBase<ulong>
    {
        public FNV1a_64()
        {
            _basis = FNV64Basis;
            _prime = FNV64Prime;
            Initialize();
        }

        /// <inheritdoc/>
        public override void HashCore(byte[] data, int offset, int length)
        {
            for (int i = offset; length > 0; i++, length--)
            {
                _hash = (_hash ^ data[i]) * _prime;
            }
        }
    }
}