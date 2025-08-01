using System;

namespace SabreTools.Hashing.CryptographicHash
{
    /// <summary>
    /// 3-pass variant of Tiger2-160
    /// </summary>
    public class Tiger2_160_3 : TigerHashBase
    {
        /// <inheritdoc/>
        public override int HashSize => 160;

        public Tiger2_160_3() : base()
        {
            _passes = 3;
            _padStart = 0x80;
        }

        /// <inheritdoc/>
        protected override byte[] HashFinal()
        {
            byte[] hash = base.HashFinal();
            byte[] trimmedHash = new byte[20];
            Array.Copy(hash, trimmedHash, 20);
            return trimmedHash;
        }
    }
}
