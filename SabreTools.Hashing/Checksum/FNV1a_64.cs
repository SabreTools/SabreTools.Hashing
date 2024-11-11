using static SabreTools.Hashing.Checksum.Constants;

namespace SabreTools.Hashing.Checksum
{
    public class FNV1a_64 : FnvBase<ulong>
    {
        public FNV1a_64()
        {
            _basis = FNV64Basis;
            _prime = FNV64Prime;
            Reset();
        }

        /// <inheritdoc/>
        public override void TransformBlock(byte[] data, int offset, int length)
        {
            while (length > 0)
            {
                _hash ^= data[offset++];
                _hash *= _prime;
                length--;
            }
        }
    }
}