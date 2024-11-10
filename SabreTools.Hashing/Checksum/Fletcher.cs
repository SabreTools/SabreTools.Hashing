using System;

namespace SabreTools.Hashing.Checksum
{
    /// <summary>
    /// Common base class for Fletcher checksums
    /// </summary>
    public abstract class Fletcher
    {
        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public abstract void TransformBlock(byte[] data, int offset, int length);

        /// <summary>
        /// Finalize the hash and return as a byte array
        /// </summary>
        public abstract byte[] Finalize();
    }

    /// <summary>
    /// Common base class for Fletcher checksums
    /// </summary>
    public abstract class Fletcher<T> : Fletcher where T : struct
    {
        /// <summary>
        /// The current value of the hash
        /// </summary>
        protected T _hash;

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
        {
            _hash = default;
        }

        /// <inheritdoc/>
        public override byte[] Finalize()
        {
            return _hash switch
            {
                short s => BitConverter.GetBytes(s),
                ushort s => BitConverter.GetBytes(s),

                int i => BitConverter.GetBytes(i),
                uint i => BitConverter.GetBytes(i),

                long l => BitConverter.GetBytes(l),
                ulong l => BitConverter.GetBytes(l),

                _ => [],
            };
        }
    }
}