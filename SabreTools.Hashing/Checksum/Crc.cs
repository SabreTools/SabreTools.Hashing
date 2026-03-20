using System;
using static SabreTools.Hashing.HashOperations;

namespace SabreTools.Hashing.Checksum
{
#if NET7_0_OR_GREATER
    public class Crc : ChecksumBase<UInt128>
#else
    public class Crc : ChecksumBase<ulong>
#endif
    {
        /// <inheritdoc/>
        public override int HashSize => Def.Width;

        /// <summary>
        /// Definition used to create the runner
        /// </summary>
        public readonly CrcDefinition Def;

        /// <summary>
        /// Table used for calculation steps
        /// </summary>
        private readonly CrcTable _table;

        /// <summary>
        /// Create a new Crc from a <see cref="CrcDefinition"/>
        /// </summary>
        /// <param name="def">CRC definition</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the bit width is not between 0 and 64.
        /// </exception>
        public Crc(CrcDefinition def)
        {
            // Check for a valid bit width
#if NET7_0_OR_GREATER
            if (def.Width < 0 || def.Width > 128)
#else
            if (def.Width < 0 || def.Width > 64)
#endif
                throw new ArgumentOutOfRangeException(nameof(def));

            Def = def;
            _table = new CrcTable(def);
            _hash = def.ReflectIn ? ReverseBits(def.Init, def.Width) : def.Init;
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            _hash = Def.Init;
        }

        /// <inheritdoc/>
        protected override void HashCore(byte[] data, int offset, int length)
            => _table.TransformBlock(ref _hash, data, offset, length);

        /// <inheritdoc/>
        protected override byte[] HashFinal()
        {
            // Create a copy of the hash
#if NET7_0_OR_GREATER
            UInt128 localHash = _hash;
#else
            ulong localHash = _hash;
#endif

            // Handle mutual reflection
            if (Def.ReflectIn ^ Def.ReflectOut)
                localHash = ReverseBits(localHash, Def.Width);

            // Handle XOR
            localHash ^= Def.XorOut;

            // Process the value and return
            return BitOperations.ClampValueToBytes(localHash, Def.Width);
        }
    }
}
