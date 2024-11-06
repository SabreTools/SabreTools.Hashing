namespace SabreTools.Hashing.Crc
{
    /// <see href="https://reveng.sourceforge.io/crc-catalogue/all.htm#crc.legend"/>
    internal class CrcDefinition
    {
        /// <summary>
        /// The number of bit cells in the linear feedback shift register;
        /// the degree of the generator polynomial, less one.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The generator polynomial that sets the feedback tap positions of
        /// the shift register. poly is written in the hexadecimal, direct
        /// notation found in MSB-first code. The least significant bit
        /// corresponds to the inward end of the shift register, and is always
        /// set. The highest-order term is omitted.
        /// </summary>
        public ulong Poly { get; set; }

        /// <summary>
        /// The settings of the bit cells at the start of each calculation,
        /// before reading the first message bit. init is written in the
        /// hexadecimal, direct notation found in MSB-first code. The least
        /// significant bit corresponds to the inward end of the shift register.
        /// </summary>
        public ulong Init { get; set; }

        /// <summary>
        /// If equal to false, specifies that the characters of the message
        /// are read bit-by-bit, most significant bit (MSB) first; if equal to
        /// true, the characters are read bit-by-bit, least significant bit (LSB)
        /// first. Each sampled message bit is then XORed with the bit being
        /// simultaneously shifted out of the register at the most significant
        /// end, and the result is passed to the feedback taps.
        /// </summary>
        public bool ReflectIn { get; set; }

        /// <summary>
        /// If equal to false, specifies that the contents of the register after
        /// reading the last message bit are unreflected before presentation;
        /// if equal to true, it specifies that they are reflected,
        /// character-by-character, before presentation. For the purpose of this
        /// definition, the reflection is performed by swapping the content of
        /// each cell with that of the cell an equal distance from the opposite
        /// end of the register; the characters of the CRC are then true images
        /// of parts of the reflected register, the character containing the
        /// original MSB always appearing first.
        /// </summary>
        public bool ReflectOut { get; set; }

        /// <summary>
        /// The XOR value applied to the contents of the register after the last
        /// message bit has been read and after the optional reflection. xorout
        /// is written in hexadecimal notation, having the same endianness as
        /// the CRC such that its true image appears in the characters of the CRC.
        /// </summary>
        public ulong XorOut { get; set; }
    }
}