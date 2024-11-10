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
        /// Internal UInt32 buffer for processing
        /// </summary>
        protected readonly uint[] _block = new uint[16];

        public MessageDigestBase()
        {
            Reset();
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
        {
            // Reset the seed values
            ResetSeed();

            // Reset the byte count
            _totalBytes = 0;

            // Reset the buffers
            Array.Clear(_buffer, 0, _buffer.Length);
            Array.Clear(_block, 0, _block.Length);
        }

        /// <summary>
        /// Reset the seed value(s)
        /// </summary>
        protected abstract void ResetSeed();

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
}