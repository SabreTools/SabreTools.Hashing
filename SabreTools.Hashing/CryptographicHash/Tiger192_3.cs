namespace SabreTools.Hashing.CryptographicHash
{
    /// <summary>
    /// 3-pass variant of Tiger-192
    /// </summary>
    public class Tiger192_3 : TigerHashBase
    {
        /// <inheritdoc/>
        public override int HashSize => 192;

        public Tiger192_3() : base()
        {
            _passes = 3;
            _padStart = 0x01;
        }
    }
}
