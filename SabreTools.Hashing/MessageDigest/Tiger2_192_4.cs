namespace SabreTools.Hashing.MessageDigest
{
    /// <summary>
    /// 4-pass variant of Tiger2-192
    /// </summary>
    public class Tiger2_192_4 : TigerHashBase
    {
        public Tiger2_192_4() : base()
        {
            _passes = 4;
            _padStart = 0x80;
        }
    }
}