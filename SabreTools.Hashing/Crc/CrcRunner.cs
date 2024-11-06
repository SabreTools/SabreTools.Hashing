using System;

namespace SabreTools.Hashing.Crc
{
    internal class CrcRunner
    {
        /// <summary>
        /// Definition used to create the runner
        /// </summary>
        private readonly CrcDefinition _definition;

        /// <summary>
        /// Table used for calculation steps
        /// </summary>
        private readonly CrcTable _table;

        /// <summary>
        /// The current value of the hash
        /// </summary>
        private ulong _hash;

        public CrcRunner(CrcDefinition def)
        {
            // Check for a valid bit width
            if (def.Width < 0 || def.Width > 64)
                throw new ArgumentOutOfRangeException(nameof(def));

            _definition = def;
            _table = new CrcTable(def);
            _hash = def.Init;
        }

        /// <summary>
        /// Reset the internal hashing state
        /// </summary>
        public void Reset()
        {
            _hash = _definition.Init;
        }

        /// <summary>
        /// Hash a block of data and append it to the existing hash
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the byte array to include</param>
        /// <param name="length">Length of the data to hash</param>
        public void TransformBlock(byte[] data, int offset, int length)
            => _table.TransformBlock(ref _hash, data, offset, length);

        /// <summary>
        /// Finalize the hash and return as a byte array
        /// </summary>
        public byte[] Finalize()
        {
            // Create a copy of the hash
            ulong localHash = _hash;

            // Handle mutual reflection
            if (_definition.ReflectIn ^ _definition.ReflectOut)
                localHash = BitOperations.ReverseBits(localHash, _definition.Width);

            // Handle XOR
            localHash ^= _definition.XorOut;

            // Process the value and return
            return BitOperations.ClampValueToBytes(localHash, _definition.Width);
        }
    }
}