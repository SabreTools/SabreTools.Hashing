using System;

namespace SabreTools.Hashing.CryptographicHash
{
    public abstract class MessageDigestBase : System.Security.Cryptography.HashAlgorithm
    {
        /// <summary>
        /// Total number of bytes processed
        /// </summary>
        protected long _totalBytes;

        /// <summary>
        /// Internal byte buffer to accumulate before <see cref="_block"/>
        /// </summary>
        protected readonly byte[] _buffer = new byte[64];

        /// <summary>
        /// Reset additional values
        /// </summary>
        protected abstract void ResetImpl();

        /// <inheritdoc/>
        protected abstract override void HashCore(byte[] array, int ibStart, int cbSize);
    }

    public abstract class MessageDigestBase<T> : MessageDigestBase where T : struct
    {
        /// <summary>
        /// Internal buffer for processing
        /// </summary>
        protected readonly T[] _block;

        public MessageDigestBase()
        {
            if (typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
                _block = new T[32];
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(uint))
                _block = new T[16];
            else if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong))
                _block = new T[8];

            else
                throw new InvalidOperationException();

            Initialize();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            // Reset the seed values
            ResetImpl();

            // Reset the byte count
            _totalBytes = 0;

            // Reset the buffers
            Array.Clear(_buffer, 0, _buffer.Length);
            Array.Clear(_block, 0, _block.Length);
        }
    }
}
