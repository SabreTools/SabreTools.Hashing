using System;

namespace SabreTools.Hashing.CryptographicHash
{
    /// <summary>
    /// 3-pass variant of Tiger-128
    /// </summary>
    public class Tiger128_3 : TigerHashBase
    {
        /// <inheritdoc/>
        public override int HashSize => 128;

        public Tiger128_3() : base()
        {
            _passes = 3;
            _padStart = 0x01;
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