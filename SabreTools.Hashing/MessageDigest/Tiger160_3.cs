using System;

namespace SabreTools.Hashing.MessageDigest
{
    /// <summary>
    /// 3-pass variant of Tiger-160
    /// </summary>
    public class Tiger160_3 : TigerHashBase
    {
        public Tiger160_3() : base()
        {
            _passes = 3;
            _padStart = 0x01;
        }

        /// <inheritdoc/>
        public override byte[] GetHash()
        {
            byte[] hash = base.GetHash();
            byte[] trimmedHash = new byte[20];
            Array.Copy(hash, trimmedHash, 20);
            return trimmedHash;
        }
    }
}