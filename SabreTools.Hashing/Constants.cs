namespace SabreTools.Hashing
{
    public static class Constants
    {
        #region 0-byte file constants

        public const long SizeZero = 0;
        public const string CRCZero = "00000000";
        public static readonly byte[] CRCZeroBytes =        [0x00, 0x00, 0x00, 0x00];
        public const string MD5Zero = "d41d8cd98f00b204e9800998ecf8427e";
        public static readonly byte[] MD5ZeroBytes =        [ 0xd4, 0x1d, 0x8c, 0xd9,
                                                              0x8f, 0x00, 0xb2, 0x04,
                                                              0xe9, 0x80, 0x09, 0x98,
                                                              0xec, 0xf8, 0x42, 0x7e ];
        public const string SHA1Zero = "da39a3ee5e6b4b0d3255bfef95601890afd80709";
        public static readonly byte[] SHA1ZeroBytes =       [ 0xda, 0x39, 0xa3, 0xee,
                                                              0x5e, 0x6b, 0x4b, 0x0d,
                                                              0x32, 0x55, 0xbf, 0xef,
                                                              0x95, 0x60, 0x18, 0x90,
                                                              0xaf, 0xd8, 0x07, 0x09 ];
        public const string SHA256Zero = "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad";
        public static readonly byte[] SHA256ZeroBytes =     [ 0xba, 0x78, 0x16, 0xbf,
                                                              0x8f, 0x01, 0xcf, 0xea,
                                                              0x41, 0x41, 0x40, 0xde,
                                                              0x5d, 0xae, 0x22, 0x23,
                                                              0xb0, 0x03, 0x61, 0xa3,
                                                              0x96, 0x17, 0x7a, 0x9c,
                                                              0xb4, 0x10, 0xff, 0x61,
                                                              0xf2, 0x00, 0x15, 0xad ];
        public const string SHA384Zero = "cb00753f45a35e8bb5a03d699ac65007272c32ab0eded1631a8b605a43ff5bed8086072ba1e7cc2358baeca134c825a7";
        public static readonly byte[] SHA384ZeroBytes =     [ 0xcb, 0x00, 0x75, 0x3f,
                                                              0x45, 0xa3, 0x5e, 0x8b,
                                                              0xb5, 0xa0, 0x3d, 0x69,
                                                              0x9a, 0xc6, 0x50, 0x07,
                                                              0x27, 0x2c, 0x32, 0xab,
                                                              0x0e, 0xde, 0xd1, 0x63,
                                                              0x1a, 0x8b, 0x60, 0x5a,
                                                              0x43, 0xff, 0x5b, 0xed,
                                                              0x80, 0x86, 0x07, 0x2b,
                                                              0xa1, 0xe7, 0xcc, 0x23,
                                                              0x58, 0xba, 0xec, 0xa1,
                                                              0x34, 0xc8, 0x25, 0xa7 ];
        public const string SHA512Zero = "ddaf35a193617abacc417349ae20413112e6fa4e89a97ea20a9eeee64b55d39a2192992a274fc1a836ba3c23a3feebbd454d4423643ce80e2a9ac94fa54ca49f";
        public static readonly byte[] SHA512ZeroBytes =     [ 0xdd, 0xaf, 0x35, 0xa1,
                                                              0x93, 0x61, 0x7a, 0xba,
                                                              0xcc, 0x41, 0x73, 0x49,
                                                              0xae, 0x20, 0x41, 0x31,
                                                              0x12, 0xe6, 0xfa, 0x4e,
                                                              0x89, 0xa9, 0x7e, 0xa2,
                                                              0x0a, 0x9e, 0xee, 0xe6,
                                                              0x4b, 0x55, 0xd3, 0x9a,
                                                              0x21, 0x92, 0x99, 0x2a,
                                                              0x27, 0x4f, 0xc1, 0xa8,
                                                              0x36, 0xba, 0x3c, 0x23,
                                                              0xa3, 0xfe, 0xeb, 0xbd,
                                                              0x45, 0x4d, 0x44, 0x23,
                                                              0x64, 0x3c, 0xe8, 0x0e,
                                                              0x2a, 0x9a, 0xc9, 0x4f,
                                                              0xa5, 0x4c, 0xa4, 0x9f ];
        public const string SpamSumZero = "QXX";
        public static readonly byte[] SpamSumZeroBytes =    [0x51, 0x58, 0x58];

        #endregion

        #region Hash string length constants

        public const int CRCLength = 8;
        public const int MD5Length = 32;
        public const int SHA1Length = 40;
        public const int SHA256Length = 64;
        public const int SHA384Length = 96;
        public const int SHA512Length = 128;

        #endregion
    }
}