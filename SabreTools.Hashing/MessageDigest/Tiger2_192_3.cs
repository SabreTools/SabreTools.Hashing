namespace SabreTools.Hashing.MessageDigest
{
    /// <summary>
    /// 3-pass variant of Tiger2-192
    /// </summary>
    public class Tiger2_192_3 : TigerHashBase
    {
        public Tiger2_192_3() : base()
        {
            _passes = 3;
            _padStart = 0x80;
        }
    }
}