using System;
using static SabreTools.Hashing.MessageDigest.Constants;

namespace SabreTools.Hashing.MessageDigest
{
    /// <see href="https://datatracker.ietf.org/doc/html/rfc1115"/>
    public class MD2 : MessageDigestBase<uint>
    {
        /// <summary>
        /// Buffer for forming digest in
        /// </summary>
        /// <remarks>At the end, D[0...15] form the message digest</remarks>
        private readonly byte[] _digest = new byte[48];

        /// <summary>
        /// Checksum register
        /// </summary>
        private readonly byte[] _checksum = new byte[16];

        /// <summary>
        /// Number of bytes handled, modulo 16
        /// </summary>
        private byte _byteCount;

        /// <summary>
        /// Last checksum char saved
        /// </summary>
        private byte _lastByte;

        public MD2() : base()
        {
        }

        /// <inheritdoc/>
        protected override void ResetImpl()
        {
            Array.Clear(_digest, 0, _digest.Length);
            Array.Clear(_checksum, 0, _checksum.Length);
            _byteCount = 0;
            _lastByte = 0;
        }

        /// <inheritdoc/>
        protected override void HashCore(byte[] data, int offset, int length)
        {
            // Figure out how much buffer is needed
            int bufferLen = 16 - _byteCount;

            // If there is buffer to fill and it will meet the limit
            if (_byteCount > 0 && _byteCount + length >= 16)
            {
                // Fill the buffer from the input
                for (int i = 0; i < bufferLen; i++)
                {
                    // Add new character to buffer
                    _digest[16 + _byteCount] = data[offset + i];
                    _digest[32 + _byteCount] = (byte)(data[offset + i] ^ _digest[_byteCount]);

                    // Update checksum register C and value L
                    _lastByte = _checksum[_byteCount] ^= MD2SBox[0xff & (data[offset + i] ^ _lastByte)];

                    // Increment i by one modulo 16
                    _byteCount = (byte)((_byteCount + 1) & 15);
                }

                // Set the new values
                offset += bufferLen;
                length -= bufferLen;

                // Run the update
                Update();
            }

            /// Process any standalone blocks
            while (length >= 16)
            {
                // Fill the buffer from the input
                for (int i = 0; i < 16; i++)
                {
                    // Add new character to buffer
                    _digest[16 + _byteCount] = data[offset + i];
                    _digest[32 + _byteCount] = (byte)(data[offset + i] ^ _digest[_byteCount]);

                    // Update checksum register C and value L
                    _lastByte = _checksum[_byteCount] ^= MD2SBox[0xff & (data[offset + i] ^ _lastByte)];

                    // Increment i by one modulo 16
                    _byteCount = (byte)((_byteCount + 1) & 15);
                }

                // Set the new values
                offset += 16;
                length -= 16;

                // Run the update
                Update();
            }

            // Save the remainder in the buffer
            if (length > 0)
            {
                // Fill the buffer from the input
                for (int i = 0; i < length; i++)
                {
                    // Add new character to buffer
                    _digest[16 + _byteCount] = data[offset + i];
                    _digest[32 + _byteCount] = (byte)(data[offset + i] ^ _digest[_byteCount]);

                    // Update checksum register C and value L
                    _lastByte = _checksum[_byteCount] ^= MD2SBox[0xff & (data[offset + i] ^ _lastByte)];

                    // Increment i by one modulo 16
                    _byteCount = (byte)((_byteCount + 1) & 15);
                }
            }
        }

        /// <inheritdoc/>
        protected override byte[] HashFinal()
        {
            // Determine the pad length
            byte padLength = (byte)(16 - _byteCount);

            // Pad the block
            byte[] padding = new byte[padLength];
#if NETFRAMEWORK
            for (int i = 0; i < padLength; i++)
            {
                padding[i] = padLength;
            }
#else
            Array.Fill(padding, padLength);
#endif

            // Pad the block
            HashCore(padding, 0, padLength);
            HashCore(_checksum, 0, _checksum.Length);

            // Get the hash
            var hash = new byte[16];
            Array.Copy(_digest, hash, 16);
            return hash;
        }

        /// <summary>
        /// The routine MDUPDATE updates the message digest context buffer to
        /// account for the presence of the character c in the message whose
        /// digest is being computed.  This routine will be called for each
        /// message byte in turn.
        /// </summary>
        /// <remarks>The following is a more efficient version of the loop</remarks>
        private void Update()
        {
            byte t = 0;
            for (byte j = 0; j < 18; j++)
            {
                for (byte i = 0; i < 48; i++)
                {
                    t = _digest[i] = (byte)(_digest[i] ^ MD2SBox[t]);
                }

                t += j;
            }
        }
    }
}