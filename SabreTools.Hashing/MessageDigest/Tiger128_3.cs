using System;

namespace SabreTools.Hashing.MessageDigest
{
    /// <summary>
    /// 3-pass variant of Tiger-128
    /// </summary>
    public class Tiger128_3 : TigerHashBase
    {
        public Tiger128_3() : base()
        {
            _passes = 3;
            _padStart = 0x01;
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