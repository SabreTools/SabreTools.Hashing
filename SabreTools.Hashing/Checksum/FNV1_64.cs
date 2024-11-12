using static SabreTools.Hashing.Checksum.Constants;

namespace SabreTools.Hashing.Checksum
{
    public class FNV1_64 : FnvBase<ulong>
    {
        public FNV1_64()
        {
            _basis = FNV64Basis;
            _prime = FNV64Prime;
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