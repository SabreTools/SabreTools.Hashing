using System;

namespace SabreTools.Hashing.Checksum
{
    /// <summary>
    /// Common base class for Fletcher checksums
    /// </summary>
    public abstract class ChecksumBase : System.Security.Cryptography.HashAlgorithm
    {
        // No common, untyped functionality
    }

    /// <summary>
    /// Common base class for checksums
    /// </summary>
    public abstract class ChecksumBase<T> : ChecksumBase where T : struct
    {
        /// <summary>
        /// The current value of the hash
        /// </summary>
        protected T _hash;

        /// <inheritdoc/>
        public override void Initialize()
        {
            _hash = default;
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
