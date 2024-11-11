using static SabreTools.Hashing.Checksum.Constants;

namespace SabreTools.Hashing.Checksum
{
    public class FNV0_64 : FnvBase<ulong>
    {
        public FNV0_64()
        {
            _basis = 0;
            _prime = FNV64Prime;
            Reset();
        }

        /// <inheritdoc/>
        public override void TransformBlock(byte[] data, int offset, int length)
        {
            while (length > 0)
            {
                _hash *= _prime;
                _hash ^= data[offset++];
                length--;
            }
        }
    }
}