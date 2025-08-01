using System;

namespace SabreTools.Hashing.CryptographicHash
{
    /// <summary>
    /// 4-pass variant of Tiger-160
    /// </summary>
    public class Tiger160_4 : TigerHashBase
    {
        /// <inheritdoc/>
        public override int HashSize => 160;

        public Tiger160_4() : base()
        {
            _passes = 4;
            _padStart = 0x01;
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
