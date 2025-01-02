using System;

namespace SabreTools.Hashing.Checksum
{
    /// <summary>
    /// Common base class for Fletcher checksums
    /// </summary>
    public abstract class ChecksumBase
    {
        /// <inheritdoc cref="System.Security.Cryptography.HashAlgorithm.HashCore(byte[], int, int)"/>
        public abstract void HashCore(byte[] data, int offset, int length);

        /// <inheritdoc cref="System.Security.Cryptography.HashAlgorithm.HashFinal"/>
        public abstract byte[] HashFinal();
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

        /// <inheritdoc cref="System.Security.Cryptography.HashAlgorithm.Initialize"/>
        public virtual void Initialize()
        {
            _hash = default;
        }

        /// <inheritdoc/>
        public override byte[] HashFinal()
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