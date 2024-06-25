using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SabreTools.Hashing.Tiger
{
    /// <summary>
    /// Reference implementation of the Tiger hash
    /// </summary>
    /// <see href="https://biham.cs.technion.ac.il/Reports/Tiger/"/> 
    public class TigerHash
    {
        /// <summary>
        /// The number of passes of the hash function.
        /// Three passes are recommended.
        /// Use four passes when you need extra security.
        /// Must be at least three.
        /// </summary>
        private const int PASSES = 3;

        public void tiger(ulong[] str, ulong length)
        {
            ulong i, j;
            byte[] temp = new byte[64];

            ulong[] res =
            [
                0x0123456789ABCDEF,
                0xFEDCBA9876543210,
                0xF096A5B4C3B2E187
            ];
            int strOffset = 0;
            for (i = length; i >= 64; i -= 64)
            {
                Compress(str, strOffset, res);
                strOffset += 8;
            }

            byte[] strBytes = new byte[str.Length * 8];
            Array.ConstrainedCopy(str, 0, strBytes, 0, str.Length);
            for (j = 0; j < i; j++)
            {
                temp[j] = strBytes[j];
            }

            temp[j++] = 0x01;
            for (; (j & 7) > 0; j++)
            {
                temp[j] = 0;
            }

            if (j > 56)
            {
                for (; j < 64; j++)
                {
                    temp[j] = 0;
                }

                Compress(ref temp, 0, res);
                j = 0;
            }

            for (; j < 56; j++)
            {
                temp[j] = 0;
            }

            Compress(ref temp, 0, res, true, length);
        }

        private static bool Compress(ref byte[] str, int strOffset, ulong[] state, bool shouldSetSeven = false, ulong length = 0)
        {
            // Create the temp array
            ulong[] temp = new ulong[str.Length / 8];
            Array.ConstrainedCopy(str, 0, temp, 0, str.Length);

            // Set index 7, if required
            if (shouldSetSeven)
                temp[7] = length << 3;

            // Run the compression
            bool success = Compress(temp, strOffset, state);

            // Copy the values back
            Array.ConstrainedCopy(temp, 0, str, 0, temp.Length);
            return success;
        }

        private static bool Compress(ulong[] str, int strOffset, ulong[] state)
        {
            // Bounds checking
            if (state.Length != 3)
                return false;
            if (str.Length < strOffset + 8)
                return false;

            ulong[] x = str.Skip(strOffset).Take(8).ToArray();

            // save_abc
            ulong aa = state[0];
            ulong bb = state[1];
            ulong cc = state[2];

            for (int pass_no = 0; pass_no < PASSES; pass_no++)
            {
                if (pass_no != 0)
                    KeySchedule(x);

                int mul = pass_no == 0 ? 5 : pass_no == 1 ? 7 : 9;
                Pass(state, x, mul);

                ulong temp = state[0]; state[0] = state[2]; state[2] = state[1]; state[1] = temp;
            }

            // feedforward
            state[0] ^= aa;
            state[1] -= bb;
            state[2] += cc;

            return true;
        }

        private static void KeySchedule(ulong[] x)
        {
            x[0] -= x[7] ^ 0xA5A5A5A5A5A5A5A5;
            x[1] ^= x[0];
            x[2] += x[1];
            x[3] -= x[2] ^ ((~x[1]) << 19);
            x[4] ^= x[3];
            x[5] += x[4];
            x[6] -= x[5] ^ ((~x[4]) >> 23);
            x[7] ^= x[6];
            x[0] += x[7];
            x[1] -= x[0] ^ ((~x[7]) << 19);
            x[2] ^= x[1];
            x[3] += x[2];
            x[4] -= x[3] ^ ((~x[2]) >> 23);
            x[5] ^= x[4];
            x[6] += x[5];
            x[7] -= x[6] ^ 0x0123456789ABCDEF;
        }

        private static bool Pass(ulong[] state, ulong[] x, int mul)
        {
            // Bounds checking
            if (state.Length != 3)
                return false;

            Round(state, 0, 1, 2, x[0], mul);
            Round(state, 1, 2, 0, x[1], mul);
            Round(state, 2, 0, 1, x[2], mul);
            Round(state, 0, 1, 2, x[3], mul);
            Round(state, 1, 2, 0, x[4], mul);
            Round(state, 2, 0, 1, x[5], mul);
            Round(state, 0, 1, 2, x[6], mul);
            Round(state, 1, 2, 0, x[7], mul);

            return true;
        }

        private static bool Round(ulong[] state, int a, int b, int c, ulong x, int mul)
        {
            // Bounds checking
            if (state.Length != 3)
                return false;

            state[c] ^= x;
            state[a] -= t1((byte)state[c])
                ^ t2((byte)(((uint)state[c]) >> (2 * 8)))
                ^ t3((byte)((state[c]) >> (4 * 8)))
                ^ t4((byte)(((uint)((state[c]) >> (4 * 8))) >> (2 * 8)));
            state[b] += t4((byte)(((uint)state[c]) >> (1 * 8)))
                ^ t3((byte)(((uint)state[c]) >> (3 * 8)))
                ^ t2((byte)(((uint)((state[c]) >> (4 * 8))) >> (1 * 8)))
                ^ t1((byte)(((uint)((state[c]) >> (4 * 8))) >> (3 * 8)));
            state[b] *= (ulong)mul;

            return true;
        }

        private static ulong t1(int offset) => SBoxes.Table[offset];
        private static ulong t2(int offset) => SBoxes.Table[offset + 256];
        private static ulong t3(int offset) => SBoxes.Table[offset + 256 * 2];
        private static ulong t4(int offset) => SBoxes.Table[offset + 256 * 3];
    }
}