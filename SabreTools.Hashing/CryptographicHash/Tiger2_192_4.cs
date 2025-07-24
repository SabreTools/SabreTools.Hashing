namespace SabreTools.Hashing.CryptographicHash
{
    /// <summary>
    /// 4-pass variant of Tiger2-192
    /// </summary>
    public class Tiger2_192_4 : TigerHashBase
    {
        /// <inheritdoc/>
        public override int HashSize => 192;

        public Tiger2_192_4() : base()
        {
            _passes = 4;
            _padStart = 0x80;
        }
    }
}
