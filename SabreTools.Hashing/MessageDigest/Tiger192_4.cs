namespace SabreTools.Hashing.MessageDigest
{
    /// <summary>
    /// 4-pass variant of Tiger-192
    /// </summary>
    public class Tiger192_4 : TigerHashBase
    {
        public Tiger192_4() : base()
        {
            _passes = 4;
            _padStart = 0x01;
        }
    }
}