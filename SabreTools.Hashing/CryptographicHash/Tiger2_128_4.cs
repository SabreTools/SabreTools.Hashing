using System;

namespace SabreTools.Hashing.CryptographicHash
{
    /// <summary>
    /// 4-pass variant of Tiger2-128
    /// </summary>
    public class Tiger2_128_4 : TigerHashBase
    {
        /// <inheritdoc/>
        public override int HashSize => 128;

        public Tiger2_128_4() : base()
        {
            _passes = 4;
            _padStart = 0x80;
        }

        /// <inheritdoc/>
        protected override byte[] HashFinal()
        {
            byte[] hash = base.HashFinal();
            byte[] trimmedHash = new byte[16];
            Array.Copy(hash, trimmedHash, 16);
            return trimmedHash;
        }
    }
}