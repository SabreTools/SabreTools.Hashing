using System;

namespace SabreTools.Hashing.NonCryptographicHash
{
    /// <summary>
    /// Common base class for FNV non-cryptographic hashes
    /// </summary>
    public abstract class FnvBase : System.Security.Cryptography.HashAlgorithm
    {
        // No common, untyped functionality
    }

    /// <summary>
    /// Common base class for FNV non-cryptographic hashes
    /// </summary>
    public abstract class FnvBase<T> : FnvBase where T : struct
    {
        /// <summary>
        /// Initial value to use
        /// </summary>
        protected T _basis;

        /// <summary>
        /// Round prime to use
        /// </summary>
        protected T _prime;

        /// <summary>
        /// The current value of the hash
        /// </summary>
        protected T _hash;

        /// <inheritdoc/>
        public override void Initialize()
        {
            _hash = _basis;
        }

        /// <inheritdoc/>
        protected override byte[] HashFinal()
        {
            byte[] hashArr = _hash switch
            {
                short s => BitConverter.GetBytes(s),
                ushort s => BitConverter.GetBytes(s),

                int i => BitConverter.GetBytes(i),
                uint i => BitConverter.GetBytes(i),

                long l => BitConverter.GetBytes(l),
                ulong l => BitConverter.GetBytes(l),

                _ => [],
            };

            Array.Reverse(hashArr);
            return hashArr;
        }
    }
}
