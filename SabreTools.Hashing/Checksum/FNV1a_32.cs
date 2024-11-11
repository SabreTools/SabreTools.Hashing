using static SabreTools.Hashing.Checksum.Constants;

namespace SabreTools.Hashing.Checksum
{
    internal class FNV1a_32 : FnvBase<uint>
    {
        public FNV1a_32()
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
                _hash ^= data[offset++];
                _hash *= _prime;
                length--;
            }
        }
    }
}