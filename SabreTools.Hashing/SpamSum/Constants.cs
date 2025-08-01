using System.Text;

namespace SabreTools.Hashing.SpamSum
{
    /// <see href="github.com/ssdeep-project/ssdeep/blob/master/fuzzy.c"/> 
    /// <see href="github.com/ssdeep-project/ssdeep/blob/master/fuzzy.h"/> 
    internal static class Constants
    {
        /// <summary>
        /// fuzzy_digest flag indicating to eliminate sequences of more than
        /// three identical characters
        /// </summary>
        public const uint FUZZY_FLAG_ELIMSEQ = 1;

        /// <summary>
        /// fuzzy_digest flag indicating not to truncate the second part to
        /// SPAMSUM_LENGTH / 2 characters.
        /// </summary>
        public const uint FUZZY_FLAG_NOTRUNC = 2;

        /// <summary>
        /// Length of an individual fuzzy hash signature component.
        /// </summary>
        public const int SPAMSUM_LENGTH = 64;

        /// <summary>
        /// The longest possible length for a fuzzy hash signature
        /// (without the filename)
        /// </summary>
        public const int FUZZY_MAX_RESULT = 2 * SPAMSUM_LENGTH + 20;

        public const uint ROLLING_WINDOW = 7;

        public const uint MIN_BLOCKSIZE = 3;

        public const byte HASH_INIT = 0x27;

        public const int NUM_BLOCKHASHES = 31;

        public const uint FUZZY_STATE_NEED_LASTHASH = 1;

        public const uint FUZZY_STATE_SIZE_FIXED = 2;

        public static uint SSDEEP_BS(uint index) => MIN_BLOCKSIZE << (int)index;

        public static ulong SSDEEP_TOTAL_SIZE_MAX
            => (ulong)SSDEEP_BS(NUM_BLOCKHASHES - 1) * SPAMSUM_LENGTH;

        public static readonly byte[] B64 = Encoding.ASCII.GetBytes("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/");

        #region Precomputed Tables

        /// <summary>
        /// Precomputed patrial FNV hash table
        /// </summary>
        public static readonly byte[][] SUM_TABLE = // [64][64]
        [
            [ // 0x00
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f,
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f,
                0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f,
                0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f,
            ],
            [ // 0x01
                0x13, 0x12, 0x11, 0x10, 0x17, 0x16, 0x15, 0x14, 0x1b, 0x1a, 0x19, 0x18, 0x1f, 0x1e, 0x1d, 0x1c,
                0x03, 0x02, 0x01, 0x00, 0x07, 0x06, 0x05, 0x04, 0x0b, 0x0a, 0x09, 0x08, 0x0f, 0x0e, 0x0d, 0x0c,
                0x33, 0x32, 0x31, 0x30, 0x37, 0x36, 0x35, 0x34, 0x3b, 0x3a, 0x39, 0x38, 0x3f, 0x3e, 0x3d, 0x3c,
                0x23, 0x22, 0x21, 0x20, 0x27, 0x26, 0x25, 0x24, 0x2b, 0x2a, 0x29, 0x28, 0x2f, 0x2e, 0x2d, 0x2c,
            ],
            [ // 0x02
                0x26, 0x27, 0x24, 0x25, 0x22, 0x23, 0x20, 0x21, 0x2e, 0x2f, 0x2c, 0x2d, 0x2a, 0x2b, 0x28, 0x29,
                0x36, 0x37, 0x34, 0x35, 0x32, 0x33, 0x30, 0x31, 0x3e, 0x3f, 0x3c, 0x3d, 0x3a, 0x3b, 0x38, 0x39,
                0x06, 0x07, 0x04, 0x05, 0x02, 0x03, 0x00, 0x01, 0x0e, 0x0f, 0x0c, 0x0d, 0x0a, 0x0b, 0x08, 0x09,
                0x16, 0x17, 0x14, 0x15, 0x12, 0x13, 0x10, 0x11, 0x1e, 0x1f, 0x1c, 0x1d, 0x1a, 0x1b, 0x18, 0x19,
            ],
            [ // 0x03
                0x39, 0x38, 0x3b, 0x3a, 0x3d, 0x3c, 0x3f, 0x3e, 0x31, 0x30, 0x33, 0x32, 0x35, 0x34, 0x37, 0x36,
                0x29, 0x28, 0x2b, 0x2a, 0x2d, 0x2c, 0x2f, 0x2e, 0x21, 0x20, 0x23, 0x22, 0x25, 0x24, 0x27, 0x26,
                0x19, 0x18, 0x1b, 0x1a, 0x1d, 0x1c, 0x1f, 0x1e, 0x11, 0x10, 0x13, 0x12, 0x15, 0x14, 0x17, 0x16,
                0x09, 0x08, 0x0b, 0x0a, 0x0d, 0x0c, 0x0f, 0x0e, 0x01, 0x00, 0x03, 0x02, 0x05, 0x04, 0x07, 0x06,
            ],
            [ // 0x04
                0x0c, 0x0d, 0x0e, 0x0f, 0x08, 0x09, 0x0a, 0x0b, 0x04, 0x05, 0x06, 0x07, 0x00, 0x01, 0x02, 0x03,
                0x1c, 0x1d, 0x1e, 0x1f, 0x18, 0x19, 0x1a, 0x1b, 0x14, 0x15, 0x16, 0x17, 0x10, 0x11, 0x12, 0x13,
                0x2c, 0x2d, 0x2e, 0x2f, 0x28, 0x29, 0x2a, 0x2b, 0x24, 0x25, 0x26, 0x27, 0x20, 0x21, 0x22, 0x23,
                0x3c, 0x3d, 0x3e, 0x3f, 0x38, 0x39, 0x3a, 0x3b, 0x34, 0x35, 0x36, 0x37, 0x30, 0x31, 0x32, 0x33,
            ],
            [ // 0x05
                0x1f, 0x1e, 0x1d, 0x1c, 0x1b, 0x1a, 0x19, 0x18, 0x17, 0x16, 0x15, 0x14, 0x13, 0x12, 0x11, 0x10,
                0x0f, 0x0e, 0x0d, 0x0c, 0x0b, 0x0a, 0x09, 0x08, 0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00,
                0x3f, 0x3e, 0x3d, 0x3c, 0x3b, 0x3a, 0x39, 0x38, 0x37, 0x36, 0x35, 0x34, 0x33, 0x32, 0x31, 0x30,
                0x2f, 0x2e, 0x2d, 0x2c, 0x2b, 0x2a, 0x29, 0x28, 0x27, 0x26, 0x25, 0x24, 0x23, 0x22, 0x21, 0x20,
            ],
            [ // 0x06
                0x32, 0x33, 0x30, 0x31, 0x36, 0x37, 0x34, 0x35, 0x3a, 0x3b, 0x38, 0x39, 0x3e, 0x3f, 0x3c, 0x3d,
                0x22, 0x23, 0x20, 0x21, 0x26, 0x27, 0x24, 0x25, 0x2a, 0x2b, 0x28, 0x29, 0x2e, 0x2f, 0x2c, 0x2d,
                0x12, 0x13, 0x10, 0x11, 0x16, 0x17, 0x14, 0x15, 0x1a, 0x1b, 0x18, 0x19, 0x1e, 0x1f, 0x1c, 0x1d,
                0x02, 0x03, 0x00, 0x01, 0x06, 0x07, 0x04, 0x05, 0x0a, 0x0b, 0x08, 0x09, 0x0e, 0x0f, 0x0c, 0x0d,
            ],
            [ // 0x07
                0x05, 0x04, 0x07, 0x06, 0x01, 0x00, 0x03, 0x02, 0x0d, 0x0c, 0x0f, 0x0e, 0x09, 0x08, 0x0b, 0x0a,
                0x15, 0x14, 0x17, 0x16, 0x11, 0x10, 0x13, 0x12, 0x1d, 0x1c, 0x1f, 0x1e, 0x19, 0x18, 0x1b, 0x1a,
                0x25, 0x24, 0x27, 0x26, 0x21, 0x20, 0x23, 0x22, 0x2d, 0x2c, 0x2f, 0x2e, 0x29, 0x28, 0x2b, 0x2a,
                0x35, 0x34, 0x37, 0x36, 0x31, 0x30, 0x33, 0x32, 0x3d, 0x3c, 0x3f, 0x3e, 0x39, 0x38, 0x3b, 0x3a,
            ],
            [ // 0x08
                0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
                0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27,
            ],
            [ // 0x09
                0x2b, 0x2a, 0x29, 0x28, 0x2f, 0x2e, 0x2d, 0x2c, 0x23, 0x22, 0x21, 0x20, 0x27, 0x26, 0x25, 0x24,
                0x3b, 0x3a, 0x39, 0x38, 0x3f, 0x3e, 0x3d, 0x3c, 0x33, 0x32, 0x31, 0x30, 0x37, 0x36, 0x35, 0x34,
                0x0b, 0x0a, 0x09, 0x08, 0x0f, 0x0e, 0x0d, 0x0c, 0x03, 0x02, 0x01, 0x00, 0x07, 0x06, 0x05, 0x04,
                0x1b, 0x1a, 0x19, 0x18, 0x1f, 0x1e, 0x1d, 0x1c, 0x13, 0x12, 0x11, 0x10, 0x17, 0x16, 0x15, 0x14,
            ],
            [ // 0x0a
                0x3e, 0x3f, 0x3c, 0x3d, 0x3a, 0x3b, 0x38, 0x39, 0x36, 0x37, 0x34, 0x35, 0x32, 0x33, 0x30, 0x31,
                0x2e, 0x2f, 0x2c, 0x2d, 0x2a, 0x2b, 0x28, 0x29, 0x26, 0x27, 0x24, 0x25, 0x22, 0x23, 0x20, 0x21,
                0x1e, 0x1f, 0x1c, 0x1d, 0x1a, 0x1b, 0x18, 0x19, 0x16, 0x17, 0x14, 0x15, 0x12, 0x13, 0x10, 0x11,
                0x0e, 0x0f, 0x0c, 0x0d, 0x0a, 0x0b, 0x08, 0x09, 0x06, 0x07, 0x04, 0x05, 0x02, 0x03, 0x00, 0x01,
            ],
            [ // 0x0b
                0x11, 0x10, 0x13, 0x12, 0x15, 0x14, 0x17, 0x16, 0x19, 0x18, 0x1b, 0x1a, 0x1d, 0x1c, 0x1f, 0x1e,
                0x01, 0x00, 0x03, 0x02, 0x05, 0x04, 0x07, 0x06, 0x09, 0x08, 0x0b, 0x0a, 0x0d, 0x0c, 0x0f, 0x0e,
                0x31, 0x30, 0x33, 0x32, 0x35, 0x34, 0x37, 0x36, 0x39, 0x38, 0x3b, 0x3a, 0x3d, 0x3c, 0x3f, 0x3e,
                0x21, 0x20, 0x23, 0x22, 0x25, 0x24, 0x27, 0x26, 0x29, 0x28, 0x2b, 0x2a, 0x2d, 0x2c, 0x2f, 0x2e,
            ],
            [ // 0x0c
                0x24, 0x25, 0x26, 0x27, 0x20, 0x21, 0x22, 0x23, 0x2c, 0x2d, 0x2e, 0x2f, 0x28, 0x29, 0x2a, 0x2b,
                0x34, 0x35, 0x36, 0x37, 0x30, 0x31, 0x32, 0x33, 0x3c, 0x3d, 0x3e, 0x3f, 0x38, 0x39, 0x3a, 0x3b,
                0x04, 0x05, 0x06, 0x07, 0x00, 0x01, 0x02, 0x03, 0x0c, 0x0d, 0x0e, 0x0f, 0x08, 0x09, 0x0a, 0x0b,
                0x14, 0x15, 0x16, 0x17, 0x10, 0x11, 0x12, 0x13, 0x1c, 0x1d, 0x1e, 0x1f, 0x18, 0x19, 0x1a, 0x1b,
            ],
            [ // 0x0d
                0x37, 0x36, 0x35, 0x34, 0x33, 0x32, 0x31, 0x30, 0x3f, 0x3e, 0x3d, 0x3c, 0x3b, 0x3a, 0x39, 0x38,
                0x27, 0x26, 0x25, 0x24, 0x23, 0x22, 0x21, 0x20, 0x2f, 0x2e, 0x2d, 0x2c, 0x2b, 0x2a, 0x29, 0x28,
                0x17, 0x16, 0x15, 0x14, 0x13, 0x12, 0x11, 0x10, 0x1f, 0x1e, 0x1d, 0x1c, 0x1b, 0x1a, 0x19, 0x18,
                0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00, 0x0f, 0x0e, 0x0d, 0x0c, 0x0b, 0x0a, 0x09, 0x08,
            ],
            [ // 0x0e
                0x0a, 0x0b, 0x08, 0x09, 0x0e, 0x0f, 0x0c, 0x0d, 0x02, 0x03, 0x00, 0x01, 0x06, 0x07, 0x04, 0x05,
                0x1a, 0x1b, 0x18, 0x19, 0x1e, 0x1f, 0x1c, 0x1d, 0x12, 0x13, 0x10, 0x11, 0x16, 0x17, 0x14, 0x15,
                0x2a, 0x2b, 0x28, 0x29, 0x2e, 0x2f, 0x2c, 0x2d, 0x22, 0x23, 0x20, 0x21, 0x26, 0x27, 0x24, 0x25,
                0x3a, 0x3b, 0x38, 0x39, 0x3e, 0x3f, 0x3c, 0x3d, 0x32, 0x33, 0x30, 0x31, 0x36, 0x37, 0x34, 0x35,
            ],
            [ // 0x0f
                0x1d, 0x1c, 0x1f, 0x1e, 0x19, 0x18, 0x1b, 0x1a, 0x15, 0x14, 0x17, 0x16, 0x11, 0x10, 0x13, 0x12,
                0x0d, 0x0c, 0x0f, 0x0e, 0x09, 0x08, 0x0b, 0x0a, 0x05, 0x04, 0x07, 0x06, 0x01, 0x00, 0x03, 0x02,
                0x3d, 0x3c, 0x3f, 0x3e, 0x39, 0x38, 0x3b, 0x3a, 0x35, 0x34, 0x37, 0x36, 0x31, 0x30, 0x33, 0x32,
                0x2d, 0x2c, 0x2f, 0x2e, 0x29, 0x28, 0x2b, 0x2a, 0x25, 0x24, 0x27, 0x26, 0x21, 0x20, 0x23, 0x22,
            ],
            [ // 0x10
                0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f,
                0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f,
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f,
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f,
            ],
            [ // 0x11
                0x03, 0x02, 0x01, 0x00, 0x07, 0x06, 0x05, 0x04, 0x0b, 0x0a, 0x09, 0x08, 0x0f, 0x0e, 0x0d, 0x0c,
                0x13, 0x12, 0x11, 0x10, 0x17, 0x16, 0x15, 0x14, 0x1b, 0x1a, 0x19, 0x18, 0x1f, 0x1e, 0x1d, 0x1c,
                0x23, 0x22, 0x21, 0x20, 0x27, 0x26, 0x25, 0x24, 0x2b, 0x2a, 0x29, 0x28, 0x2f, 0x2e, 0x2d, 0x2c,
                0x33, 0x32, 0x31, 0x30, 0x37, 0x36, 0x35, 0x34, 0x3b, 0x3a, 0x39, 0x38, 0x3f, 0x3e, 0x3d, 0x3c,
            ],
            [ // 0x12
                0x16, 0x17, 0x14, 0x15, 0x12, 0x13, 0x10, 0x11, 0x1e, 0x1f, 0x1c, 0x1d, 0x1a, 0x1b, 0x18, 0x19,
                0x06, 0x07, 0x04, 0x05, 0x02, 0x03, 0x00, 0x01, 0x0e, 0x0f, 0x0c, 0x0d, 0x0a, 0x0b, 0x08, 0x09,
                0x36, 0x37, 0x34, 0x35, 0x32, 0x33, 0x30, 0x31, 0x3e, 0x3f, 0x3c, 0x3d, 0x3a, 0x3b, 0x38, 0x39,
                0x26, 0x27, 0x24, 0x25, 0x22, 0x23, 0x20, 0x21, 0x2e, 0x2f, 0x2c, 0x2d, 0x2a, 0x2b, 0x28, 0x29,
            ],
            [ // 0x13
                0x29, 0x28, 0x2b, 0x2a, 0x2d, 0x2c, 0x2f, 0x2e, 0x21, 0x20, 0x23, 0x22, 0x25, 0x24, 0x27, 0x26,
                0x39, 0x38, 0x3b, 0x3a, 0x3d, 0x3c, 0x3f, 0x3e, 0x31, 0x30, 0x33, 0x32, 0x35, 0x34, 0x37, 0x36,
                0x09, 0x08, 0x0b, 0x0a, 0x0d, 0x0c, 0x0f, 0x0e, 0x01, 0x00, 0x03, 0x02, 0x05, 0x04, 0x07, 0x06,
                0x19, 0x18, 0x1b, 0x1a, 0x1d, 0x1c, 0x1f, 0x1e, 0x11, 0x10, 0x13, 0x12, 0x15, 0x14, 0x17, 0x16,
            ],
            [ // 0x14
                0x3c, 0x3d, 0x3e, 0x3f, 0x38, 0x39, 0x3a, 0x3b, 0x34, 0x35, 0x36, 0x37, 0x30, 0x31, 0x32, 0x33,
                0x2c, 0x2d, 0x2e, 0x2f, 0x28, 0x29, 0x2a, 0x2b, 0x24, 0x25, 0x26, 0x27, 0x20, 0x21, 0x22, 0x23,
                0x1c, 0x1d, 0x1e, 0x1f, 0x18, 0x19, 0x1a, 0x1b, 0x14, 0x15, 0x16, 0x17, 0x10, 0x11, 0x12, 0x13,
                0x0c, 0x0d, 0x0e, 0x0f, 0x08, 0x09, 0x0a, 0x0b, 0x04, 0x05, 0x06, 0x07, 0x00, 0x01, 0x02, 0x03,
            ],
            [ // 0x15
                0x0f, 0x0e, 0x0d, 0x0c, 0x0b, 0x0a, 0x09, 0x08, 0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00,
                0x1f, 0x1e, 0x1d, 0x1c, 0x1b, 0x1a, 0x19, 0x18, 0x17, 0x16, 0x15, 0x14, 0x13, 0x12, 0x11, 0x10,
                0x2f, 0x2e, 0x2d, 0x2c, 0x2b, 0x2a, 0x29, 0x28, 0x27, 0x26, 0x25, 0x24, 0x23, 0x22, 0x21, 0x20,
                0x3f, 0x3e, 0x3d, 0x3c, 0x3b, 0x3a, 0x39, 0x38, 0x37, 0x36, 0x35, 0x34, 0x33, 0x32, 0x31, 0x30,
            ],
            [ // 0x16
                0x22, 0x23, 0x20, 0x21, 0x26, 0x27, 0x24, 0x25, 0x2a, 0x2b, 0x28, 0x29, 0x2e, 0x2f, 0x2c, 0x2d,
                0x32, 0x33, 0x30, 0x31, 0x36, 0x37, 0x34, 0x35, 0x3a, 0x3b, 0x38, 0x39, 0x3e, 0x3f, 0x3c, 0x3d,
                0x02, 0x03, 0x00, 0x01, 0x06, 0x07, 0x04, 0x05, 0x0a, 0x0b, 0x08, 0x09, 0x0e, 0x0f, 0x0c, 0x0d,
                0x12, 0x13, 0x10, 0x11, 0x16, 0x17, 0x14, 0x15, 0x1a, 0x1b, 0x18, 0x19, 0x1e, 0x1f, 0x1c, 0x1d,
            ],
            [ // 0x17
                0x35, 0x34, 0x37, 0x36, 0x31, 0x30, 0x33, 0x32, 0x3d, 0x3c, 0x3f, 0x3e, 0x39, 0x38, 0x3b, 0x3a,
                0x25, 0x24, 0x27, 0x26, 0x21, 0x20, 0x23, 0x22, 0x2d, 0x2c, 0x2f, 0x2e, 0x29, 0x28, 0x2b, 0x2a,
                0x15, 0x14, 0x17, 0x16, 0x11, 0x10, 0x13, 0x12, 0x1d, 0x1c, 0x1f, 0x1e, 0x19, 0x18, 0x1b, 0x1a,
                0x05, 0x04, 0x07, 0x06, 0x01, 0x00, 0x03, 0x02, 0x0d, 0x0c, 0x0f, 0x0e, 0x09, 0x08, 0x0b, 0x0a,
            ],
            [ // 0x18
                0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27,
                0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
            ],
            [ // 0x19
                0x1b, 0x1a, 0x19, 0x18, 0x1f, 0x1e, 0x1d, 0x1c, 0x13, 0x12, 0x11, 0x10, 0x17, 0x16, 0x15, 0x14,
                0x0b, 0x0a, 0x09, 0x08, 0x0f, 0x0e, 0x0d, 0x0c, 0x03, 0x02, 0x01, 0x00, 0x07, 0x06, 0x05, 0x04,
                0x3b, 0x3a, 0x39, 0x38, 0x3f, 0x3e, 0x3d, 0x3c, 0x33, 0x32, 0x31, 0x30, 0x37, 0x36, 0x35, 0x34,
                0x2b, 0x2a, 0x29, 0x28, 0x2f, 0x2e, 0x2d, 0x2c, 0x23, 0x22, 0x21, 0x20, 0x27, 0x26, 0x25, 0x24,
            ],
            [ // 0x1a
                0x2e, 0x2f, 0x2c, 0x2d, 0x2a, 0x2b, 0x28, 0x29, 0x26, 0x27, 0x24, 0x25, 0x22, 0x23, 0x20, 0x21,
                0x3e, 0x3f, 0x3c, 0x3d, 0x3a, 0x3b, 0x38, 0x39, 0x36, 0x37, 0x34, 0x35, 0x32, 0x33, 0x30, 0x31,
                0x0e, 0x0f, 0x0c, 0x0d, 0x0a, 0x0b, 0x08, 0x09, 0x06, 0x07, 0x04, 0x05, 0x02, 0x03, 0x00, 0x01,
                0x1e, 0x1f, 0x1c, 0x1d, 0x1a, 0x1b, 0x18, 0x19, 0x16, 0x17, 0x14, 0x15, 0x12, 0x13, 0x10, 0x11,
            ],
            [ // 0x1b
                0x01, 0x00, 0x03, 0x02, 0x05, 0x04, 0x07, 0x06, 0x09, 0x08, 0x0b, 0x0a, 0x0d, 0x0c, 0x0f, 0x0e,
                0x11, 0x10, 0x13, 0x12, 0x15, 0x14, 0x17, 0x16, 0x19, 0x18, 0x1b, 0x1a, 0x1d, 0x1c, 0x1f, 0x1e,
                0x21, 0x20, 0x23, 0x22, 0x25, 0x24, 0x27, 0x26, 0x29, 0x28, 0x2b, 0x2a, 0x2d, 0x2c, 0x2f, 0x2e,
                0x31, 0x30, 0x33, 0x32, 0x35, 0x34, 0x37, 0x36, 0x39, 0x38, 0x3b, 0x3a, 0x3d, 0x3c, 0x3f, 0x3e,
            ],
            [ // 0x1c
                0x14, 0x15, 0x16, 0x17, 0x10, 0x11, 0x12, 0x13, 0x1c, 0x1d, 0x1e, 0x1f, 0x18, 0x19, 0x1a, 0x1b,
                0x04, 0x05, 0x06, 0x07, 0x00, 0x01, 0x02, 0x03, 0x0c, 0x0d, 0x0e, 0x0f, 0x08, 0x09, 0x0a, 0x0b,
                0x34, 0x35, 0x36, 0x37, 0x30, 0x31, 0x32, 0x33, 0x3c, 0x3d, 0x3e, 0x3f, 0x38, 0x39, 0x3a, 0x3b,
                0x24, 0x25, 0x26, 0x27, 0x20, 0x21, 0x22, 0x23, 0x2c, 0x2d, 0x2e, 0x2f, 0x28, 0x29, 0x2a, 0x2b,
            ],
            [ // 0x1d
                0x27, 0x26, 0x25, 0x24, 0x23, 0x22, 0x21, 0x20, 0x2f, 0x2e, 0x2d, 0x2c, 0x2b, 0x2a, 0x29, 0x28,
                0x37, 0x36, 0x35, 0x34, 0x33, 0x32, 0x31, 0x30, 0x3f, 0x3e, 0x3d, 0x3c, 0x3b, 0x3a, 0x39, 0x38,
                0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00, 0x0f, 0x0e, 0x0d, 0x0c, 0x0b, 0x0a, 0x09, 0x08,
                0x17, 0x16, 0x15, 0x14, 0x13, 0x12, 0x11, 0x10, 0x1f, 0x1e, 0x1d, 0x1c, 0x1b, 0x1a, 0x19, 0x18,
            ],
            [ // 0x1e
                0x3a, 0x3b, 0x38, 0x39, 0x3e, 0x3f, 0x3c, 0x3d, 0x32, 0x33, 0x30, 0x31, 0x36, 0x37, 0x34, 0x35,
                0x2a, 0x2b, 0x28, 0x29, 0x2e, 0x2f, 0x2c, 0x2d, 0x22, 0x23, 0x20, 0x21, 0x26, 0x27, 0x24, 0x25,
                0x1a, 0x1b, 0x18, 0x19, 0x1e, 0x1f, 0x1c, 0x1d, 0x12, 0x13, 0x10, 0x11, 0x16, 0x17, 0x14, 0x15,
                0x0a, 0x0b, 0x08, 0x09, 0x0e, 0x0f, 0x0c, 0x0d, 0x02, 0x03, 0x00, 0x01, 0x06, 0x07, 0x04, 0x05,
            ],
            [ // 0x1f
                0x0d, 0x0c, 0x0f, 0x0e, 0x09, 0x08, 0x0b, 0x0a, 0x05, 0x04, 0x07, 0x06, 0x01, 0x00, 0x03, 0x02,
                0x1d, 0x1c, 0x1f, 0x1e, 0x19, 0x18, 0x1b, 0x1a, 0x15, 0x14, 0x17, 0x16, 0x11, 0x10, 0x13, 0x12,
                0x2d, 0x2c, 0x2f, 0x2e, 0x29, 0x28, 0x2b, 0x2a, 0x25, 0x24, 0x27, 0x26, 0x21, 0x20, 0x23, 0x22,
                0x3d, 0x3c, 0x3f, 0x3e, 0x39, 0x38, 0x3b, 0x3a, 0x35, 0x34, 0x37, 0x36, 0x31, 0x30, 0x33, 0x32,
            ],
            [ // 0x20
                0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f,
                0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f,
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f,
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f,
            ],
            [ // 0x21
                0x33, 0x32, 0x31, 0x30, 0x37, 0x36, 0x35, 0x34, 0x3b, 0x3a, 0x39, 0x38, 0x3f, 0x3e, 0x3d, 0x3c,
                0x23, 0x22, 0x21, 0x20, 0x27, 0x26, 0x25, 0x24, 0x2b, 0x2a, 0x29, 0x28, 0x2f, 0x2e, 0x2d, 0x2c,
                0x13, 0x12, 0x11, 0x10, 0x17, 0x16, 0x15, 0x14, 0x1b, 0x1a, 0x19, 0x18, 0x1f, 0x1e, 0x1d, 0x1c,
                0x03, 0x02, 0x01, 0x00, 0x07, 0x06, 0x05, 0x04, 0x0b, 0x0a, 0x09, 0x08, 0x0f, 0x0e, 0x0d, 0x0c,
            ],
            [ // 0x22
                0x06, 0x07, 0x04, 0x05, 0x02, 0x03, 0x00, 0x01, 0x0e, 0x0f, 0x0c, 0x0d, 0x0a, 0x0b, 0x08, 0x09,
                0x16, 0x17, 0x14, 0x15, 0x12, 0x13, 0x10, 0x11, 0x1e, 0x1f, 0x1c, 0x1d, 0x1a, 0x1b, 0x18, 0x19,
                0x26, 0x27, 0x24, 0x25, 0x22, 0x23, 0x20, 0x21, 0x2e, 0x2f, 0x2c, 0x2d, 0x2a, 0x2b, 0x28, 0x29,
                0x36, 0x37, 0x34, 0x35, 0x32, 0x33, 0x30, 0x31, 0x3e, 0x3f, 0x3c, 0x3d, 0x3a, 0x3b, 0x38, 0x39,
            ],
            [ // 0x23
                0x19, 0x18, 0x1b, 0x1a, 0x1d, 0x1c, 0x1f, 0x1e, 0x11, 0x10, 0x13, 0x12, 0x15, 0x14, 0x17, 0x16,
                0x09, 0x08, 0x0b, 0x0a, 0x0d, 0x0c, 0x0f, 0x0e, 0x01, 0x00, 0x03, 0x02, 0x05, 0x04, 0x07, 0x06,
                0x39, 0x38, 0x3b, 0x3a, 0x3d, 0x3c, 0x3f, 0x3e, 0x31, 0x30, 0x33, 0x32, 0x35, 0x34, 0x37, 0x36,
                0x29, 0x28, 0x2b, 0x2a, 0x2d, 0x2c, 0x2f, 0x2e, 0x21, 0x20, 0x23, 0x22, 0x25, 0x24, 0x27, 0x26,
            ],
            [ // 0x24
                0x2c, 0x2d, 0x2e, 0x2f, 0x28, 0x29, 0x2a, 0x2b, 0x24, 0x25, 0x26, 0x27, 0x20, 0x21, 0x22, 0x23,
                0x3c, 0x3d, 0x3e, 0x3f, 0x38, 0x39, 0x3a, 0x3b, 0x34, 0x35, 0x36, 0x37, 0x30, 0x31, 0x32, 0x33,
                0x0c, 0x0d, 0x0e, 0x0f, 0x08, 0x09, 0x0a, 0x0b, 0x04, 0x05, 0x06, 0x07, 0x00, 0x01, 0x02, 0x03,
                0x1c, 0x1d, 0x1e, 0x1f, 0x18, 0x19, 0x1a, 0x1b, 0x14, 0x15, 0x16, 0x17, 0x10, 0x11, 0x12, 0x13,
            ],
            [ // 0x25
                0x3f, 0x3e, 0x3d, 0x3c, 0x3b, 0x3a, 0x39, 0x38, 0x37, 0x36, 0x35, 0x34, 0x33, 0x32, 0x31, 0x30,
                0x2f, 0x2e, 0x2d, 0x2c, 0x2b, 0x2a, 0x29, 0x28, 0x27, 0x26, 0x25, 0x24, 0x23, 0x22, 0x21, 0x20,
                0x1f, 0x1e, 0x1d, 0x1c, 0x1b, 0x1a, 0x19, 0x18, 0x17, 0x16, 0x15, 0x14, 0x13, 0x12, 0x11, 0x10,
                0x0f, 0x0e, 0x0d, 0x0c, 0x0b, 0x0a, 0x09, 0x08, 0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00,
            ],
            [ // 0x26
                0x12, 0x13, 0x10, 0x11, 0x16, 0x17, 0x14, 0x15, 0x1a, 0x1b, 0x18, 0x19, 0x1e, 0x1f, 0x1c, 0x1d,
                0x02, 0x03, 0x00, 0x01, 0x06, 0x07, 0x04, 0x05, 0x0a, 0x0b, 0x08, 0x09, 0x0e, 0x0f, 0x0c, 0x0d,
                0x32, 0x33, 0x30, 0x31, 0x36, 0x37, 0x34, 0x35, 0x3a, 0x3b, 0x38, 0x39, 0x3e, 0x3f, 0x3c, 0x3d,
                0x22, 0x23, 0x20, 0x21, 0x26, 0x27, 0x24, 0x25, 0x2a, 0x2b, 0x28, 0x29, 0x2e, 0x2f, 0x2c, 0x2d,
            ],
            [ // 0x27
                0x25, 0x24, 0x27, 0x26, 0x21, 0x20, 0x23, 0x22, 0x2d, 0x2c, 0x2f, 0x2e, 0x29, 0x28, 0x2b, 0x2a,
                0x35, 0x34, 0x37, 0x36, 0x31, 0x30, 0x33, 0x32, 0x3d, 0x3c, 0x3f, 0x3e, 0x39, 0x38, 0x3b, 0x3a,
                0x05, 0x04, 0x07, 0x06, 0x01, 0x00, 0x03, 0x02, 0x0d, 0x0c, 0x0f, 0x0e, 0x09, 0x08, 0x0b, 0x0a,
                0x15, 0x14, 0x17, 0x16, 0x11, 0x10, 0x13, 0x12, 0x1d, 0x1c, 0x1f, 0x1e, 0x19, 0x18, 0x1b, 0x1a,
            ],
            [ // 0x28
                0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
                0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27,
                0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
                0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
            ],
            [ // 0x29
                0x0b, 0x0a, 0x09, 0x08, 0x0f, 0x0e, 0x0d, 0x0c, 0x03, 0x02, 0x01, 0x00, 0x07, 0x06, 0x05, 0x04,
                0x1b, 0x1a, 0x19, 0x18, 0x1f, 0x1e, 0x1d, 0x1c, 0x13, 0x12, 0x11, 0x10, 0x17, 0x16, 0x15, 0x14,
                0x2b, 0x2a, 0x29, 0x28, 0x2f, 0x2e, 0x2d, 0x2c, 0x23, 0x22, 0x21, 0x20, 0x27, 0x26, 0x25, 0x24,
                0x3b, 0x3a, 0x39, 0x38, 0x3f, 0x3e, 0x3d, 0x3c, 0x33, 0x32, 0x31, 0x30, 0x37, 0x36, 0x35, 0x34,
            ],
            [ // 0x2a
                0x1e, 0x1f, 0x1c, 0x1d, 0x1a, 0x1b, 0x18, 0x19, 0x16, 0x17, 0x14, 0x15, 0x12, 0x13, 0x10, 0x11,
                0x0e, 0x0f, 0x0c, 0x0d, 0x0a, 0x0b, 0x08, 0x09, 0x06, 0x07, 0x04, 0x05, 0x02, 0x03, 0x00, 0x01,
                0x3e, 0x3f, 0x3c, 0x3d, 0x3a, 0x3b, 0x38, 0x39, 0x36, 0x37, 0x34, 0x35, 0x32, 0x33, 0x30, 0x31,
                0x2e, 0x2f, 0x2c, 0x2d, 0x2a, 0x2b, 0x28, 0x29, 0x26, 0x27, 0x24, 0x25, 0x22, 0x23, 0x20, 0x21,
            ],
            [ // 0x2b
                0x31, 0x30, 0x33, 0x32, 0x35, 0x34, 0x37, 0x36, 0x39, 0x38, 0x3b, 0x3a, 0x3d, 0x3c, 0x3f, 0x3e,
                0x21, 0x20, 0x23, 0x22, 0x25, 0x24, 0x27, 0x26, 0x29, 0x28, 0x2b, 0x2a, 0x2d, 0x2c, 0x2f, 0x2e,
                0x11, 0x10, 0x13, 0x12, 0x15, 0x14, 0x17, 0x16, 0x19, 0x18, 0x1b, 0x1a, 0x1d, 0x1c, 0x1f, 0x1e,
                0x01, 0x00, 0x03, 0x02, 0x05, 0x04, 0x07, 0x06, 0x09, 0x08, 0x0b, 0x0a, 0x0d, 0x0c, 0x0f, 0x0e,
            ],
            [ // 0x2c
                0x04, 0x05, 0x06, 0x07, 0x00, 0x01, 0x02, 0x03, 0x0c, 0x0d, 0x0e, 0x0f, 0x08, 0x09, 0x0a, 0x0b,
                0x14, 0x15, 0x16, 0x17, 0x10, 0x11, 0x12, 0x13, 0x1c, 0x1d, 0x1e, 0x1f, 0x18, 0x19, 0x1a, 0x1b,
                0x24, 0x25, 0x26, 0x27, 0x20, 0x21, 0x22, 0x23, 0x2c, 0x2d, 0x2e, 0x2f, 0x28, 0x29, 0x2a, 0x2b,
                0x34, 0x35, 0x36, 0x37, 0x30, 0x31, 0x32, 0x33, 0x3c, 0x3d, 0x3e, 0x3f, 0x38, 0x39, 0x3a, 0x3b,
            ],
            [ // 0x2d
                0x17, 0x16, 0x15, 0x14, 0x13, 0x12, 0x11, 0x10, 0x1f, 0x1e, 0x1d, 0x1c, 0x1b, 0x1a, 0x19, 0x18,
                0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00, 0x0f, 0x0e, 0x0d, 0x0c, 0x0b, 0x0a, 0x09, 0x08,
                0x37, 0x36, 0x35, 0x34, 0x33, 0x32, 0x31, 0x30, 0x3f, 0x3e, 0x3d, 0x3c, 0x3b, 0x3a, 0x39, 0x38,
                0x27, 0x26, 0x25, 0x24, 0x23, 0x22, 0x21, 0x20, 0x2f, 0x2e, 0x2d, 0x2c, 0x2b, 0x2a, 0x29, 0x28,
            ],
            [ // 0x2e
                0x2a, 0x2b, 0x28, 0x29, 0x2e, 0x2f, 0x2c, 0x2d, 0x22, 0x23, 0x20, 0x21, 0x26, 0x27, 0x24, 0x25,
                0x3a, 0x3b, 0x38, 0x39, 0x3e, 0x3f, 0x3c, 0x3d, 0x32, 0x33, 0x30, 0x31, 0x36, 0x37, 0x34, 0x35,
                0x0a, 0x0b, 0x08, 0x09, 0x0e, 0x0f, 0x0c, 0x0d, 0x02, 0x03, 0x00, 0x01, 0x06, 0x07, 0x04, 0x05,
                0x1a, 0x1b, 0x18, 0x19, 0x1e, 0x1f, 0x1c, 0x1d, 0x12, 0x13, 0x10, 0x11, 0x16, 0x17, 0x14, 0x15,
            ],
            [ // 0x2f
                0x3d, 0x3c, 0x3f, 0x3e, 0x39, 0x38, 0x3b, 0x3a, 0x35, 0x34, 0x37, 0x36, 0x31, 0x30, 0x33, 0x32,
                0x2d, 0x2c, 0x2f, 0x2e, 0x29, 0x28, 0x2b, 0x2a, 0x25, 0x24, 0x27, 0x26, 0x21, 0x20, 0x23, 0x22,
                0x1d, 0x1c, 0x1f, 0x1e, 0x19, 0x18, 0x1b, 0x1a, 0x15, 0x14, 0x17, 0x16, 0x11, 0x10, 0x13, 0x12,
                0x0d, 0x0c, 0x0f, 0x0e, 0x09, 0x08, 0x0b, 0x0a, 0x05, 0x04, 0x07, 0x06, 0x01, 0x00, 0x03, 0x02,
            ],
            [ // 0x30
                0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f,
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f,
                0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f,
                0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f,
            ],
            [ // 0x31
                0x23, 0x22, 0x21, 0x20, 0x27, 0x26, 0x25, 0x24, 0x2b, 0x2a, 0x29, 0x28, 0x2f, 0x2e, 0x2d, 0x2c,
                0x33, 0x32, 0x31, 0x30, 0x37, 0x36, 0x35, 0x34, 0x3b, 0x3a, 0x39, 0x38, 0x3f, 0x3e, 0x3d, 0x3c,
                0x03, 0x02, 0x01, 0x00, 0x07, 0x06, 0x05, 0x04, 0x0b, 0x0a, 0x09, 0x08, 0x0f, 0x0e, 0x0d, 0x0c,
                0x13, 0x12, 0x11, 0x10, 0x17, 0x16, 0x15, 0x14, 0x1b, 0x1a, 0x19, 0x18, 0x1f, 0x1e, 0x1d, 0x1c,
            ],
            [ // 0x32
                0x36, 0x37, 0x34, 0x35, 0x32, 0x33, 0x30, 0x31, 0x3e, 0x3f, 0x3c, 0x3d, 0x3a, 0x3b, 0x38, 0x39,
                0x26, 0x27, 0x24, 0x25, 0x22, 0x23, 0x20, 0x21, 0x2e, 0x2f, 0x2c, 0x2d, 0x2a, 0x2b, 0x28, 0x29,
                0x16, 0x17, 0x14, 0x15, 0x12, 0x13, 0x10, 0x11, 0x1e, 0x1f, 0x1c, 0x1d, 0x1a, 0x1b, 0x18, 0x19,
                0x06, 0x07, 0x04, 0x05, 0x02, 0x03, 0x00, 0x01, 0x0e, 0x0f, 0x0c, 0x0d, 0x0a, 0x0b, 0x08, 0x09,
            ],
            [ // 0x33
                0x09, 0x08, 0x0b, 0x0a, 0x0d, 0x0c, 0x0f, 0x0e, 0x01, 0x00, 0x03, 0x02, 0x05, 0x04, 0x07, 0x06,
                0x19, 0x18, 0x1b, 0x1a, 0x1d, 0x1c, 0x1f, 0x1e, 0x11, 0x10, 0x13, 0x12, 0x15, 0x14, 0x17, 0x16,
                0x29, 0x28, 0x2b, 0x2a, 0x2d, 0x2c, 0x2f, 0x2e, 0x21, 0x20, 0x23, 0x22, 0x25, 0x24, 0x27, 0x26,
                0x39, 0x38, 0x3b, 0x3a, 0x3d, 0x3c, 0x3f, 0x3e, 0x31, 0x30, 0x33, 0x32, 0x35, 0x34, 0x37, 0x36,
            ],
            [ // 0x34
                0x1c, 0x1d, 0x1e, 0x1f, 0x18, 0x19, 0x1a, 0x1b, 0x14, 0x15, 0x16, 0x17, 0x10, 0x11, 0x12, 0x13,
                0x0c, 0x0d, 0x0e, 0x0f, 0x08, 0x09, 0x0a, 0x0b, 0x04, 0x05, 0x06, 0x07, 0x00, 0x01, 0x02, 0x03,
                0x3c, 0x3d, 0x3e, 0x3f, 0x38, 0x39, 0x3a, 0x3b, 0x34, 0x35, 0x36, 0x37, 0x30, 0x31, 0x32, 0x33,
                0x2c, 0x2d, 0x2e, 0x2f, 0x28, 0x29, 0x2a, 0x2b, 0x24, 0x25, 0x26, 0x27, 0x20, 0x21, 0x22, 0x23,
            ],
            [ // 0x35
                0x2f, 0x2e, 0x2d, 0x2c, 0x2b, 0x2a, 0x29, 0x28, 0x27, 0x26, 0x25, 0x24, 0x23, 0x22, 0x21, 0x20,
                0x3f, 0x3e, 0x3d, 0x3c, 0x3b, 0x3a, 0x39, 0x38, 0x37, 0x36, 0x35, 0x34, 0x33, 0x32, 0x31, 0x30,
                0x0f, 0x0e, 0x0d, 0x0c, 0x0b, 0x0a, 0x09, 0x08, 0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00,
                0x1f, 0x1e, 0x1d, 0x1c, 0x1b, 0x1a, 0x19, 0x18, 0x17, 0x16, 0x15, 0x14, 0x13, 0x12, 0x11, 0x10,
            ],
            [ // 0x36
                0x02, 0x03, 0x00, 0x01, 0x06, 0x07, 0x04, 0x05, 0x0a, 0x0b, 0x08, 0x09, 0x0e, 0x0f, 0x0c, 0x0d,
                0x12, 0x13, 0x10, 0x11, 0x16, 0x17, 0x14, 0x15, 0x1a, 0x1b, 0x18, 0x19, 0x1e, 0x1f, 0x1c, 0x1d,
                0x22, 0x23, 0x20, 0x21, 0x26, 0x27, 0x24, 0x25, 0x2a, 0x2b, 0x28, 0x29, 0x2e, 0x2f, 0x2c, 0x2d,
                0x32, 0x33, 0x30, 0x31, 0x36, 0x37, 0x34, 0x35, 0x3a, 0x3b, 0x38, 0x39, 0x3e, 0x3f, 0x3c, 0x3d,
            ],
            [ // 0x37
                0x15, 0x14, 0x17, 0x16, 0x11, 0x10, 0x13, 0x12, 0x1d, 0x1c, 0x1f, 0x1e, 0x19, 0x18, 0x1b, 0x1a,
                0x05, 0x04, 0x07, 0x06, 0x01, 0x00, 0x03, 0x02, 0x0d, 0x0c, 0x0f, 0x0e, 0x09, 0x08, 0x0b, 0x0a,
                0x35, 0x34, 0x37, 0x36, 0x31, 0x30, 0x33, 0x32, 0x3d, 0x3c, 0x3f, 0x3e, 0x39, 0x38, 0x3b, 0x3a,
                0x25, 0x24, 0x27, 0x26, 0x21, 0x20, 0x23, 0x22, 0x2d, 0x2c, 0x2f, 0x2e, 0x29, 0x28, 0x2b, 0x2a,
            ],
            [ // 0x38
                0x28, 0x29, 0x2a, 0x2b, 0x2c, 0x2d, 0x2e, 0x2f, 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27,
                0x38, 0x39, 0x3a, 0x3b, 0x3c, 0x3d, 0x3e, 0x3f, 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37,
                0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x18, 0x19, 0x1a, 0x1b, 0x1c, 0x1d, 0x1e, 0x1f, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17,
            ],
            [ // 0x39
                0x3b, 0x3a, 0x39, 0x38, 0x3f, 0x3e, 0x3d, 0x3c, 0x33, 0x32, 0x31, 0x30, 0x37, 0x36, 0x35, 0x34,
                0x2b, 0x2a, 0x29, 0x28, 0x2f, 0x2e, 0x2d, 0x2c, 0x23, 0x22, 0x21, 0x20, 0x27, 0x26, 0x25, 0x24,
                0x1b, 0x1a, 0x19, 0x18, 0x1f, 0x1e, 0x1d, 0x1c, 0x13, 0x12, 0x11, 0x10, 0x17, 0x16, 0x15, 0x14,
                0x0b, 0x0a, 0x09, 0x08, 0x0f, 0x0e, 0x0d, 0x0c, 0x03, 0x02, 0x01, 0x00, 0x07, 0x06, 0x05, 0x04,
            ],
            [ // 0x3a
                0x0e, 0x0f, 0x0c, 0x0d, 0x0a, 0x0b, 0x08, 0x09, 0x06, 0x07, 0x04, 0x05, 0x02, 0x03, 0x00, 0x01,
                0x1e, 0x1f, 0x1c, 0x1d, 0x1a, 0x1b, 0x18, 0x19, 0x16, 0x17, 0x14, 0x15, 0x12, 0x13, 0x10, 0x11,
                0x2e, 0x2f, 0x2c, 0x2d, 0x2a, 0x2b, 0x28, 0x29, 0x26, 0x27, 0x24, 0x25, 0x22, 0x23, 0x20, 0x21,
                0x3e, 0x3f, 0x3c, 0x3d, 0x3a, 0x3b, 0x38, 0x39, 0x36, 0x37, 0x34, 0x35, 0x32, 0x33, 0x30, 0x31,
            ],
            [ // 0x3b
                0x21, 0x20, 0x23, 0x22, 0x25, 0x24, 0x27, 0x26, 0x29, 0x28, 0x2b, 0x2a, 0x2d, 0x2c, 0x2f, 0x2e,
                0x31, 0x30, 0x33, 0x32, 0x35, 0x34, 0x37, 0x36, 0x39, 0x38, 0x3b, 0x3a, 0x3d, 0x3c, 0x3f, 0x3e,
                0x01, 0x00, 0x03, 0x02, 0x05, 0x04, 0x07, 0x06, 0x09, 0x08, 0x0b, 0x0a, 0x0d, 0x0c, 0x0f, 0x0e,
                0x11, 0x10, 0x13, 0x12, 0x15, 0x14, 0x17, 0x16, 0x19, 0x18, 0x1b, 0x1a, 0x1d, 0x1c, 0x1f, 0x1e,
            ],
            [ // 0x3c
                0x34, 0x35, 0x36, 0x37, 0x30, 0x31, 0x32, 0x33, 0x3c, 0x3d, 0x3e, 0x3f, 0x38, 0x39, 0x3a, 0x3b,
                0x24, 0x25, 0x26, 0x27, 0x20, 0x21, 0x22, 0x23, 0x2c, 0x2d, 0x2e, 0x2f, 0x28, 0x29, 0x2a, 0x2b,
                0x14, 0x15, 0x16, 0x17, 0x10, 0x11, 0x12, 0x13, 0x1c, 0x1d, 0x1e, 0x1f, 0x18, 0x19, 0x1a, 0x1b,
                0x04, 0x05, 0x06, 0x07, 0x00, 0x01, 0x02, 0x03, 0x0c, 0x0d, 0x0e, 0x0f, 0x08, 0x09, 0x0a, 0x0b,
            ],
            [ // 0x3d
                0x07, 0x06, 0x05, 0x04, 0x03, 0x02, 0x01, 0x00, 0x0f, 0x0e, 0x0d, 0x0c, 0x0b, 0x0a, 0x09, 0x08,
                0x17, 0x16, 0x15, 0x14, 0x13, 0x12, 0x11, 0x10, 0x1f, 0x1e, 0x1d, 0x1c, 0x1b, 0x1a, 0x19, 0x18,
                0x27, 0x26, 0x25, 0x24, 0x23, 0x22, 0x21, 0x20, 0x2f, 0x2e, 0x2d, 0x2c, 0x2b, 0x2a, 0x29, 0x28,
                0x37, 0x36, 0x35, 0x34, 0x33, 0x32, 0x31, 0x30, 0x3f, 0x3e, 0x3d, 0x3c, 0x3b, 0x3a, 0x39, 0x38,
            ],
            [ // 0x3e
                0x1a, 0x1b, 0x18, 0x19, 0x1e, 0x1f, 0x1c, 0x1d, 0x12, 0x13, 0x10, 0x11, 0x16, 0x17, 0x14, 0x15,
                0x0a, 0x0b, 0x08, 0x09, 0x0e, 0x0f, 0x0c, 0x0d, 0x02, 0x03, 0x00, 0x01, 0x06, 0x07, 0x04, 0x05,
                0x3a, 0x3b, 0x38, 0x39, 0x3e, 0x3f, 0x3c, 0x3d, 0x32, 0x33, 0x30, 0x31, 0x36, 0x37, 0x34, 0x35,
                0x2a, 0x2b, 0x28, 0x29, 0x2e, 0x2f, 0x2c, 0x2d, 0x22, 0x23, 0x20, 0x21, 0x26, 0x27, 0x24, 0x25,
            ],
            [ // 0x3f
                0x2d, 0x2c, 0x2f, 0x2e, 0x29, 0x28, 0x2b, 0x2a, 0x25, 0x24, 0x27, 0x26, 0x21, 0x20, 0x23, 0x22,
                0x3d, 0x3c, 0x3f, 0x3e, 0x39, 0x38, 0x3b, 0x3a, 0x35, 0x34, 0x37, 0x36, 0x31, 0x30, 0x33, 0x32,
                0x0d, 0x0c, 0x0f, 0x0e, 0x09, 0x08, 0x0b, 0x0a, 0x05, 0x04, 0x07, 0x06, 0x01, 0x00, 0x03, 0x02,
                0x1d, 0x1c, 0x1f, 0x1e, 0x19, 0x18, 0x1b, 0x1a, 0x15, 0x14, 0x17, 0x16, 0x11, 0x10, 0x13, 0x12,
            ],
        ];

        #endregion
    }
}
