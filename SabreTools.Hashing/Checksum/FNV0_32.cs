using static SabreTools.Hashing.Checksum.Constants;

namespace SabreTools.Hashing.Checksum
{
    internal class FNV0_32 : FnvBase<uint>
    {
        public FNV0_32()
        {
            _basis = 0;
            _prime = FNV32Prime;
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