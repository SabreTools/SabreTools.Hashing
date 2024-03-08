/*
 * Copyright (c) 2012-2015 Eugene Larchenko (el6345@gmail.com)
 * This code is licensed under the MIT License.
 * See the file LICENSE_MIT for the license details.
 */

using System;

namespace CRC32
{
    public class OptimizedCRC
    {
        private const uint kPoly = 0xEDB88320;
        private const uint kInit = 0xFFFFFFFF;
        private const int NUM_TABLES = 8;
        private static readonly uint[] Table;

        static OptimizedCRC()
        {
            unchecked
            {
                Table = new uint[256 * NUM_TABLES];
                int i;
                for (i = 0; i < 256; i++)
                {
                    uint r = (uint)i;
                    for (int j = 0; j < 8; j++)
                        r = (r >> 1) ^ (kPoly & ~((r & 1) - 1));
                    Table[i] = r;
                }
                for (; i < 256 * NUM_TABLES; i++)
                {
                    uint r = Table[i - 256];
                    Table[i] = Table[r & 0xFF] ^ (r >> 8);
                }
            }
        }

        private uint value;

        public OptimizedCRC()
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

        public void Update(byte[] data, int offset, int count)
        {
            new ArraySegment<byte>(data, offset, count); // check arguments
            if (count <= 0) return;

            var table = OptimizedCRC.Table;

            uint crc = value;

            for (; (offset & 7) != 0 && count != 0; count--)
                crc = (crc >> 8) ^ table[(byte)crc ^ data[offset++]];

            if (count >= 8)
            {
                int end = (count - 8) & ~7;
                count -= end;
                end += offset;

                while (offset != end)
                {
                    crc ^= (uint)(data[offset] + (data[offset + 1] << 8) + (data[offset + 2] << 16) + (data[offset + 3] << 24));
                    uint high = (uint)(data[offset + 4] + (data[offset + 5] << 8) + (data[offset + 6] << 16) + (data[offset + 7] << 24));
                    offset += 8;

                    crc = table[(byte)crc + 0x700]
                        ^ table[(byte)(crc >>= 8) + 0x600]
                        ^ table[(byte)(crc >>= 8) + 0x500]
                        ^ table[/*(byte)*/(crc >> 8) + 0x400]
                        ^ table[(byte)(high) + 0x300]
                        ^ table[(byte)(high >>= 8) + 0x200]
                        ^ table[(byte)(high >>= 8) + 0x100]
                        ^ table[/*(byte)*/(high >> 8) + 0x000];
                }
            }

            while (count-- != 0)
                crc = (crc >> 8) ^ table[(byte)crc ^ data[offset++]];

            value = crc;
        }

        static public int Compute(byte[] data, int offset, int count)
        {
            var crc = new OptimizedCRC();
            crc.Update(data, offset, count);
            return crc.Value;
        }

        static public int Compute(byte[] data)
        {
            return Compute(data, 0, data.Length);
        }

    }
}
