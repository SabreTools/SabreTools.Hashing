using static SabreTools.Hashing.Checksum.Constants;

namespace SabreTools.Hashing.Checksum
{
    public class FNV0_32 : FnvBase<uint>
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
            for (int i = offset; length > 0; i++, length--)
            {
                _hash = (_hash * _prime) ^ data[i];
            }
        }
    }
}