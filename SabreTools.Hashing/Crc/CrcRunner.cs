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
        {
            // Empty data just returns
            if (data.Length == 0)
                return;

            // Check for valid offset and length
            if (offset > data.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
            else if (offset + length > data.Length)
                throw new ArgumentOutOfRangeException(nameof(length));

            // Get the bit shift for the MSB
            int msb = _definition.Width - (_definition.Width < 8 ? 1 : 8);

            // Process the data byte-wise
            for (int i = offset; i < offset + length; i++)
            {
                PerformChecksumStep(data, i, msb);
            }
        }

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

        /// <summary>
        /// Perform a single checksum step
        /// </summary>
        /// <param name="data">Byte array representing the data</param>
        /// <param name="offset">Offset in the data to process</param>
        /// <param name="msb">Bit shift for the MSB</param>
        private void PerformChecksumStep(byte[] data, int offset, int msb)
        {
            // Per-byte processing
            if (_definition.Width >= 8)
            {
                if (_definition.ReflectIn)
                    _hash = (_hash >> 8) ^ _table.StdTable[(byte)_hash ^ data[offset]];
                else
                    _hash = (_hash << 8) ^ _table.StdTable[((byte)(_hash >> msb)) ^ data[offset]];
            }

            // Per-bit processing
            else
            {
                for (int b = 0; b < 8; b++)
                {
                    if (_definition.ReflectIn)
                        _hash = (_hash >> 1) ^ _table.StdTable[(byte)(_hash & 1) ^ ((byte)(data[offset] >> b) & 1)];
                    else
                        _hash = (_hash << 1) ^ _table.StdTable[(byte)((_hash >> msb) & 1) ^ ((byte)(data[offset] >> (7 - b)) & 1)];
                }
            }
        }
    }
}