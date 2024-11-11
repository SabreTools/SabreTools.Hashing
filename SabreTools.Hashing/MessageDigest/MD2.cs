using System;
using static SabreTools.Hashing.MessageDigest.Constants;

namespace SabreTools.Hashing.MessageDigest
{
    /// <see href="https://datatracker.ietf.org/doc/html/rfc1115"/>
    /// <remarks>Uses the inefficient reference implementation for now</remarks>
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
        public override void TransformBlock(byte[] data, int offset, int length)
        {
            // Loop and process all bytes sequentially
            while (length > 0)
            {
                Update(data[offset++]);
                length--;
            }
        }

        /// <inheritdoc/>
        public override void Terminate()
        {
            // Determine the pad length
            byte padLength = (byte)(16 - _byteCount);
            for (int i = 0; i < padLength; i++)
            {
                Update(padLength);
            }

            // Append the checksum
            for (int i = 0; i < 16; i++)
            {
                Update(_checksum[i]);
            }
        }

        /// <inheritdoc/>
        public override byte[] GetHash()
        {
            var hash = new byte[16];
            Array.Copy(_digest, hash, 16);

            // Reset the state and return
            Reset();
            return hash;
        }

        /// <summary>
        /// The routine MDUPDATE updates the message digest context buffer to
        /// account for the presence of the character c in the message whose
        /// digest is being computed.  This routine will be called for each
        /// message byte in turn.
        /// </summary>
        private void Update(byte c)
        {
            // TODO: The part from here until "Transform D if i = 0"
            // can be moved into the main TransformBlock. All it's doing
            // is ensuring that 16 bytes exist before it starts processing

            // Add new character to buffer
            _digest[16 + _byteCount] = c;
            _digest[32 + _byteCount] = (byte)(c ^ _digest[_byteCount]);

            // Update checksum register C and value L
            _lastByte = _checksum[_byteCount] ^= MD2SBox[0xff & (c ^ _lastByte)];

            // Increment i by one modulo 16
            _byteCount = (byte)((_byteCount + 1) & 15);

            // Transform D if i = 0
            if (_byteCount == 0)
            {
                byte t = 0;
                for (byte j = 0; j < 18; j++)
                {
                    // The following is a more efficient version of the loop
                    for (byte i = 0; i < 48; i++)
                    {
                        t = _digest[i] = (byte)(_digest[i] ^ MD2SBox[t]);
                    }

                    t += j;
                }
            }
        }
    }
}