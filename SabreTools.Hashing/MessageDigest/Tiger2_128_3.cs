using System;

namespace SabreTools.Hashing.MessageDigest
{
    /// <summary>
    /// 3-pass variant of Tiger2-128
    /// </summary>
    public class Tiger2_128_3 : TigerHashBase
    {
        public Tiger2_128_3() : base()
        {
            _passes = 3;
            _padStart = 0x80;
        }

        /// <inheritdoc/>
        public override byte[] GetHash()
        {
            byte[] hash = base.GetHash();
            byte[] trimmedHash = new byte[16];
            Array.Copy(hash, trimmedHash, 16);
            return trimmedHash;
        }
    }
}