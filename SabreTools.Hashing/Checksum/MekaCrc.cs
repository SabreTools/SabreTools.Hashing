using System;

namespace SabreTools.Hashing.Checksum
{
    /// <see href="https://github.com/ocornut/meka/blob/master/meka/srcs/checksum.cpp"/>
    public class MekaCrc : ChecksumBase<ulong>
    {
        /// <inheritdoc/>
        public override int HashSize => 64;

        public MekaCrc()
        {
            Initialize();
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public override void Initialize()
        {
            _hash = 0;
        }

        /// <inheritdoc/>
        /// <remarks>The original code limits the maximum processed size to 8KiB</remarks>
        protected override void HashCore(byte[] data, int offset, int length)
        {
            // Read the current hash into a byte array
            byte[] temp = BitConverter.GetBytes(_hash);

            // Loop over the input and process
            for (int i = 0; i < length; i++)
            {
                byte v = data[offset + i];
                unchecked
                {
                    temp[v & 7]++;
                    temp[v >> 5]++;
                }
            }

            // Convert the hash back into a value
            _hash = BitConverter.ToUInt64(temp, 0);
        }
    }
}
