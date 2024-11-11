using System;

namespace SabreTools.Hashing.MessageDigest
{
    /// <summary>
    /// 4-pass variant of Tiger-160
    /// </summary>
    public class Tiger160_4 : TigerHashBase
    {
        public Tiger160_4() : base()
        {
            _passes = 4;
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