using static SabreTools.Hashing.Checksum.Constants;

namespace SabreTools.Hashing.Checksum
{
    internal class FNV1_32 : FnvBase<uint>
    {
        public FNV1_32()
        {
            _basis = FNV32Basis;
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