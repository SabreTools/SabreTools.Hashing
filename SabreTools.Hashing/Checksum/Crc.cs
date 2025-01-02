using System;
using static SabreTools.Hashing.HashOperations;

namespace SabreTools.Hashing.Checksum
{
    public class Crc : ChecksumBase<ulong>
    {
        /// <summary>
        /// Definition used to create the runner
        /// </summary>
        public readonly CrcDefinition Def;

        /// <summary>
        /// Table used for calculation steps
        /// </summary>
        private readonly CrcTable _table;

        public Crc(CrcDefinition def)
        {
            // Check for a valid bit width
            if (def.Width < 0 || def.Width > 64)
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
            ulong localHash = _hash;

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