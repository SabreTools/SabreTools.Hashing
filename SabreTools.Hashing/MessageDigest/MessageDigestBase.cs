using System;

namespace SabreTools.Hashing.MessageDigest
{
    public abstract class MessageDigestBase
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

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public abstract void TransformBlock(byte[] data, int offset, int length);

        /// <summary>
        /// End the hashing process
        /// </summary>
        /// TODO: Combine this when the padding byte can be set by implementing classes
        public abstract void Terminate();

        /// <summary>
        /// Get the current value of the hash
        /// </summary>
        /// <remarks>
        /// If <see cref="Terminate"/> has not been run, this value
        /// will not be accurate for the processed bytes so far.
        /// </remarks>
        /// TODO: Combine this when there's an easier way of passing the state
        public abstract byte[] GetHash();
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

            Reset();
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
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