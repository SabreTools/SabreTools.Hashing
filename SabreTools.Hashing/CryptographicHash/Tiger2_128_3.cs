using System;

namespace SabreTools.Hashing.CryptographicHash
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
        protected override byte[] HashFinal()
        {
            byte[] hash = base.HashFinal();
            byte[] trimmedHash = new byte[16];
            Array.Copy(hash, trimmedHash, 16);
            return trimmedHash;
        }
    }
}