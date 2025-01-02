namespace SabreTools.Hashing.CryptographicHash
{
    /// <summary>
    /// 3-pass variant of Tiger2-192
    /// </summary>
    public class Tiger2_192_3 : TigerHashBase
    {
        /// <inheritdoc/>
        public override int HashSize => 192;

        public Tiger2_192_3() : base()
        {
            _passes = 3;
            _padStart = 0x80;
        }
    }
}