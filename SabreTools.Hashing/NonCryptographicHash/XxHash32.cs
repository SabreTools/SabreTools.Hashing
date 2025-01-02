using System;

namespace SabreTools.Hashing.NonCryptographicHash
{
    public class XxHash32 : System.Security.Cryptography.HashAlgorithm
    {
        /// <summary>
        /// The 32-bit seed to alter the hash result predictably.
        /// </summary>
        private readonly uint _seed;

        /// <summary>
        /// Internal xxHash-32 state
        /// </summary>
        private readonly XxHash32State _state;

        public XxHash32(uint seed = 0)
        {
            _seed = seed;
            _state = new XxHash32State();
            _state.Reset(seed);
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            _state.Reset(_seed);
        }

        /// <inheritdoc/>
        protected override void HashCore(byte[] data, int offset, int length)
            => _state.Update(data, offset, length);

        /// <inheritdoc/>
        protected override byte[] HashFinal()
        {
            uint hash = _state.Digest();
            byte[] hashArr = BitConverter.GetBytes(hash);
            Array.Reverse(hashArr);
            return hashArr;
        }
    }
}