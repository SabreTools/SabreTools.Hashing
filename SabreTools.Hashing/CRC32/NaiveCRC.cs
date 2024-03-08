using System;

namespace CRC32
{
    public class NaiveCRC
    {
        private const uint kPoly = 0xEDB88320;
        private const uint kInit = 0xFFFFFFFF;
        private static readonly uint[] Table;

        static NaiveCRC()
        {
            unchecked
            {
                Table = new uint[256];
                for (uint i = 0; i < 256; i++)
                {
                    uint r = i;
                    for (int j = 0; j < 8; j++)
                        r = (r >> 1) ^ (kPoly & ~((r & 1) - 1));
                    Table[i] = r;
                }
            }
        }

        private uint value;

        public NaiveCRC()
        {
            Init();
        }

        public void Init()
        {
            value = kInit;
        }

        public int Value
        {
            get { return (int)~value; }
        }

        public void UpdateByte(byte b)
        {
            value = (value >> 8) ^ Table[(byte)value ^ b];
        }

        public void Update(byte[] data, int offset, int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException("count");
            while (count-- != 0)
                value = (value >> 8) ^ Table[(byte)value ^ data[offset++]];
        }

        static public int Compute(byte[] data, int offset, int count)
        {
            var crc = new NaiveCRC();
            crc.Update(data, offset, count);
            return crc.Value;
        }

        static public int Compute(byte[] data)
        {
            return Compute(data, 0, data.Length);
        }

    }
}