using System;

namespace SabreTools.Hashing.XxHash
{
    internal class XxHash32
    {
        /// <summary>
        /// The 32-bit seed to alter the hash result predictably.
        /// </summary>
        private readonly uint _seed;

        /// <summary>
        /// Internal xxHash32 state
        /// </summary>
        private readonly XXH32State _state;

        public XxHash32(uint seed = 0)
        {
            _seed = seed;
            _state = new XXH32State();
            _state.Reset(seed);
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
        {
            _state.Reset(_seed);
        }

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
            => _state.Update(data, offset, length);

        /// <summary>
        /// Finalize the hash and return as a byte array
        /// </summary>
        public byte[] Finalize()
        {
            uint hash = _state.Digest();
            return BitConverter.GetBytes(hash);
        }
    }
}