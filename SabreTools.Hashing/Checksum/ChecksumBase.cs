using System;

namespace SabreTools.Hashing.Checksum
{
    /// <summary>
    /// Common base class for Fletcher checksums
    /// </summary>
    public abstract class ChecksumBase : System.Security.Cryptography.HashAlgorithm
    {
        #if NET7_0_OR_GREATER
        /// <summary>
        /// Convert an Int64 to a byte array
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        internal static byte[] GetBytes(Int128 value)
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
        internal static byte[] GetBytes(UInt128 value)
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
        /// Convert a byte array at an offset to a UInt128
        /// </summary>
        /// <remarks>Reads in little-endian format</remarks>
        internal static UInt128 ToUInt128(byte[] value)
        {
            return value[0]
                | ((UInt128)value[1] << 8)
                | ((UInt128)value[2] << 16)
                | ((UInt128)value[3] << 24)
                | ((UInt128)value[4] << 32)
                | ((UInt128)value[5] << 40)
                | ((UInt128)value[6] << 48)
                | ((UInt128)value[7] << 56)
                | ((UInt128)value[8] << 64)
                | ((UInt128)value[9] << 72)
                | ((UInt128)value[10] << 80)
                | ((UInt128)value[11] << 88)
                | ((UInt128)value[12] << 96)
                | ((UInt128)value[13] << 104)
                | ((UInt128)value[14] << 112)
                | ((UInt128)value[15] << 120);
        }
#endif
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
    }
}
