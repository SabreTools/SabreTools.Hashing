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

#if NET7_0_OR_GREATER
                Int128 i => GetBytes(i),
                UInt128 i => GetBytes(i),
#endif

                _ => [],
            };

            Array.Reverse(hashArr);
            return hashArr;
        }

#if NET7_0_OR_GREATER
        /// <summary>
        /// Convert an Int64 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        private static byte[] GetBytes(Int128 value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 56) & 0xFF),
                (byte)((value >> 64) & 0xFF),
                (byte)((value >> 72) & 0xFF),
                (byte)((value >> 80) & 0xFF),
                (byte)((value >> 88) & 0xFF),
                (byte)((value >> 96) & 0xFF),
                (byte)((value >> 104) & 0xFF),
                (byte)((value >> 112) & 0xFF),
                (byte)((value >> 120) & 0xFF),
            ];

            return output;
        }

        /// <summary>
        /// Convert a UInt64 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        private static byte[] GetBytes(UInt128 value)
        {
            byte[] output =
            [
                (byte)(value & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 56) & 0xFF),
                (byte)((value >> 64) & 0xFF),
                (byte)((value >> 72) & 0xFF),
                (byte)((value >> 80) & 0xFF),
                (byte)((value >> 88) & 0xFF),
                (byte)((value >> 96) & 0xFF),
                (byte)((value >> 104) & 0xFF),
                (byte)((value >> 112) & 0xFF),
                (byte)((value >> 120) & 0xFF),
            ];

            return output;
        }
#endif
    }
}
