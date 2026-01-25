using System;
using System.Security.Cryptography;
using SabreTools.Hashing.Checksum;
using SabreTools.Hashing.CryptographicHash;
using SabreTools.Hashing.NonCryptographicHash;
using static SabreTools.Hashing.HashOperations;

namespace SabreTools.Hashing
{
    /// <summary>
    /// Wrapper for a single hashing algorithm
    /// </summary>
    public class HashWrapper : IDisposable
    {
        #region Properties

        /// <summary>
        /// Hash type associated with the current state
        /// </summary>
        public readonly HashType HashType;

        /// <summary>
        /// Current hash in bytes
        /// </summary>
        public byte[]? CurrentHashBytes => _hasher switch
        {
            HashAlgorithm ha => ha.Hash,
#if NET462_OR_GREATER || NETCOREAPP
            System.IO.Hashing.NonCryptographicHashAlgorithm ncha => ncha.GetCurrentHash(),
#endif
#if NET8_0_OR_GREATER
            Shake128 s128 => s128.GetCurrentHash(32),
            Shake256 s256 => s256.GetCurrentHash(64),
#endif
            _ => null,
        };

        /// <summary>
        /// Current hash as a string
        /// </summary>
        public string? CurrentHashString => _hasher switch
        {
            // Needed due to variable bit widths
            Crc cr => GetCRCVariableLengthString(cr),

            // Needed due to Base64 text output
            SpamSum.SpamSum ss => GetSpamSumBase64String(ss),

            // Everything else are direct conversions
            _ => ByteArrayToString(CurrentHashBytes),
        };

        #endregion

        #region Private Fields

        /// <summary>
        /// Internal hasher being used for processing
        /// </summary>
        private object? _hasher;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hashType">Hash type to instantiate</param>
        public HashWrapper(HashType hashType)
        {
            HashType = hashType;
            GetHasher();
        }

        /// <summary>
        /// Generate the correct hashing class based on the hash type
        /// </summary>
        private void GetHasher()
        {
            _hasher = HashType switch
            {
                HashType.Adler32 => new Adler32(),

#if NET7_0_OR_GREATER
                HashType.BLAKE3 => new Blake3.Blake3HashAlgorithm(),
#endif

                HashType.CRC1_ZERO => new Crc(StandardDefinitions.CRC1_ZERO),
                HashType.CRC1_ONE => new Crc(StandardDefinitions.CRC1_ONE),

                HashType.CRC3_GSM => new Crc(StandardDefinitions.CRC3_GSM),
                HashType.CRC3_ROHC => new Crc(StandardDefinitions.CRC3_ROHC),

                HashType.CRC4_G704 => new Crc(StandardDefinitions.CRC4_G704),
                HashType.CRC4_INTERLAKEN => new Crc(StandardDefinitions.CRC4_INTERLAKEN),

                HashType.CRC5_EPCC1G2 => new Crc(StandardDefinitions.CRC5_EPCC1G2),
                HashType.CRC5_G704 => new Crc(StandardDefinitions.CRC5_G704),
                HashType.CRC5_USB => new Crc(StandardDefinitions.CRC5_USB),

                HashType.CRC6_CDMA2000A => new Crc(StandardDefinitions.CRC6_CDMA2000A),
                HashType.CRC6_CDMA2000B => new Crc(StandardDefinitions.CRC6_CDMA2000B),
                HashType.CRC6_DARC => new Crc(StandardDefinitions.CRC6_DARC),
                HashType.CRC6_G704 => new Crc(StandardDefinitions.CRC6_G704),
                HashType.CRC6_GSM => new Crc(StandardDefinitions.CRC6_GSM),

                HashType.CRC7_MMC => new Crc(StandardDefinitions.CRC7_MMC),
                HashType.CRC7_ROHC => new Crc(StandardDefinitions.CRC7_ROHC),
                HashType.CRC7_UMTS => new Crc(StandardDefinitions.CRC7_UMTS),

                HashType.CRC8 => new Crc(StandardDefinitions.CRC8_SMBUS),
                HashType.CRC8_AUTOSAR => new Crc(StandardDefinitions.CRC8_AUTOSAR),
                HashType.CRC8_BLUETOOTH => new Crc(StandardDefinitions.CRC8_BLUETOOTH),
                HashType.CRC8_CDMA2000 => new Crc(StandardDefinitions.CRC8_CDMA2000),
                HashType.CRC8_DARC => new Crc(StandardDefinitions.CRC8_DARC),
                HashType.CRC8_DVBS2 => new Crc(StandardDefinitions.CRC8_DVBS2),
                HashType.CRC8_GSMA => new Crc(StandardDefinitions.CRC8_GSMA),
                HashType.CRC8_GSMB => new Crc(StandardDefinitions.CRC8_GSMB),
                HashType.CRC8_HITAG => new Crc(StandardDefinitions.CRC8_HITAG),
                HashType.CRC8_I4321 => new Crc(StandardDefinitions.CRC8_I4321),
                HashType.CRC8_ICODE => new Crc(StandardDefinitions.CRC8_ICODE),
                HashType.CRC8_LTE => new Crc(StandardDefinitions.CRC8_LTE),
                HashType.CRC8_MAXIMDOW => new Crc(StandardDefinitions.CRC8_MAXIMDOW),
                HashType.CRC8_MIFAREMAD => new Crc(StandardDefinitions.CRC8_MIFAREMAD),
                HashType.CRC8_NRSC5 => new Crc(StandardDefinitions.CRC8_NRSC5),
                HashType.CRC8_OPENSAFETY => new Crc(StandardDefinitions.CRC8_OPENSAFETY),
                HashType.CRC8_ROHC => new Crc(StandardDefinitions.CRC8_ROHC),
                HashType.CRC8_SAEJ1850 => new Crc(StandardDefinitions.CRC8_SAEJ1850),
                HashType.CRC8_SMBUS => new Crc(StandardDefinitions.CRC8_SMBUS),
                HashType.CRC8_TECH3250 => new Crc(StandardDefinitions.CRC8_TECH3250),
                HashType.CRC8_WCDMA => new Crc(StandardDefinitions.CRC8_WCDMA),

                HashType.CRC10_ATM => new Crc(StandardDefinitions.CRC10_ATM),
                HashType.CRC10_CDMA2000 => new Crc(StandardDefinitions.CRC10_CDMA2000),
                HashType.CRC10_GSM => new Crc(StandardDefinitions.CRC10_GSM),

                HashType.CRC11_FLEXRAY => new Crc(StandardDefinitions.CRC11_FLEXRAY),
                HashType.CRC11_UMTS => new Crc(StandardDefinitions.CRC11_UMTS),

                HashType.CRC12_CDMA2000 => new Crc(StandardDefinitions.CRC12_CDMA2000),
                HashType.CRC12_DECT => new Crc(StandardDefinitions.CRC12_DECT),
                HashType.CRC12_GSM => new Crc(StandardDefinitions.CRC12_GSM),
                HashType.CRC12_UMTS => new Crc(StandardDefinitions.CRC12_UMTS),

                HashType.CRC13_BBC => new Crc(StandardDefinitions.CRC13_BBC),

                HashType.CRC14_DARC => new Crc(StandardDefinitions.CRC14_DARC),
                HashType.CRC14_GSM => new Crc(StandardDefinitions.CRC14_GSM),

                HashType.CRC15_CAN => new Crc(StandardDefinitions.CRC15_CAN),
                HashType.CRC15_MPT1327 => new Crc(StandardDefinitions.CRC15_MPT1327),

                HashType.CRC16 => new Crc(StandardDefinitions.CRC16_ARC),
                HashType.CRC16_ARC => new Crc(StandardDefinitions.CRC16_ARC),
                HashType.CRC16_CDMA2000 => new Crc(StandardDefinitions.CRC16_CDMA2000),
                HashType.CRC16_CMS => new Crc(StandardDefinitions.CRC16_CMS),
                HashType.CRC16_DDS110 => new Crc(StandardDefinitions.CRC16_DDS110),
                HashType.CRC16_DECTR => new Crc(StandardDefinitions.CRC16_DECTR),
                HashType.CRC16_DECTX => new Crc(StandardDefinitions.CRC16_DECTX),
                HashType.CRC16_DNP => new Crc(StandardDefinitions.CRC16_DNP),
                HashType.CRC16_EN13757 => new Crc(StandardDefinitions.CRC16_EN13757),
                HashType.CRC16_GENIBUS => new Crc(StandardDefinitions.CRC16_GENIBUS),
                HashType.CRC16_GSM => new Crc(StandardDefinitions.CRC16_GSM),
                HashType.CRC16_IBM3740 => new Crc(StandardDefinitions.CRC16_IBM3740),
                HashType.CRC16_IBMSDLC => new Crc(StandardDefinitions.CRC16_IBMSDLC),
                HashType.CRC16_ISOIEC144433A => new Crc(StandardDefinitions.CRC16_ISOIEC144433A),
                HashType.CRC16_KERMIT => new Crc(StandardDefinitions.CRC16_KERMIT),
                HashType.CRC16_LJ1200 => new Crc(StandardDefinitions.CRC16_LJ1200),
                HashType.CRC16_M17 => new Crc(StandardDefinitions.CRC16_M17),
                HashType.CRC16_MAXIMDOW => new Crc(StandardDefinitions.CRC16_MAXIMDOW),
                HashType.CRC16_MCRF4XX => new Crc(StandardDefinitions.CRC16_MCRF4XX),
                HashType.CRC16_MODBUS => new Crc(StandardDefinitions.CRC16_MODBUS),
                HashType.CRC16_NRSC5 => new Crc(StandardDefinitions.CRC16_NRSC5),
                HashType.CRC16_OPENSAFETYA => new Crc(StandardDefinitions.CRC16_OPENSAFETYA),
                HashType.CRC16_OPENSAFETYB => new Crc(StandardDefinitions.CRC16_OPENSAFETYB),
                HashType.CRC16_PROFIBUS => new Crc(StandardDefinitions.CRC16_PROFIBUS),
                HashType.CRC16_RIELLO => new Crc(StandardDefinitions.CRC16_RIELLO),
                HashType.CRC16_SPIFUJITSU => new Crc(StandardDefinitions.CRC16_SPIFUJITSU),
                HashType.CRC16_T10DIF => new Crc(StandardDefinitions.CRC16_T10DIF),
                HashType.CRC16_TELEDISK => new Crc(StandardDefinitions.CRC16_TELEDISK),
                HashType.CRC16_TMS37157 => new Crc(StandardDefinitions.CRC16_TMS37157),
                HashType.CRC16_UMTS => new Crc(StandardDefinitions.CRC16_UMTS),
                HashType.CRC16_USB => new Crc(StandardDefinitions.CRC16_USB),
                HashType.CRC16_XMODEM => new Crc(StandardDefinitions.CRC16_XMODEM),

                HashType.CRC17_CANFD => new Crc(StandardDefinitions.CRC17_CANFD),

                HashType.CRC21_CANFD => new Crc(StandardDefinitions.CRC21_CANFD),

                HashType.CRC24_BLE => new Crc(StandardDefinitions.CRC24_BLE),
                HashType.CRC24_FLEXRAYA => new Crc(StandardDefinitions.CRC24_FLEXRAYA),
                HashType.CRC24_FLEXRAYB => new Crc(StandardDefinitions.CRC24_FLEXRAYB),
                HashType.CRC24_INTERLAKEN => new Crc(StandardDefinitions.CRC24_INTERLAKEN),
                HashType.CRC24_LTEA => new Crc(StandardDefinitions.CRC24_LTEA),
                HashType.CRC24_LTEB => new Crc(StandardDefinitions.CRC24_LTEB),
                HashType.CRC24_OPENPGP => new Crc(StandardDefinitions.CRC24_OPENPGP),
                HashType.CRC24_OS9 => new Crc(StandardDefinitions.CRC24_OS9),

                HashType.CRC30_CDMA => new Crc(StandardDefinitions.CRC30_CDMA),

                HashType.CRC31_PHILIPS => new Crc(StandardDefinitions.CRC31_PHILIPS),

                HashType.CRC32 => new Crc(StandardDefinitions.CRC32_ISOHDLC),
                HashType.CRC32_AIXM => new Crc(StandardDefinitions.CRC32_AIXM),
                HashType.CRC32_AUTOSAR => new Crc(StandardDefinitions.CRC32_AUTOSAR),
                HashType.CRC32_BASE91D => new Crc(StandardDefinitions.CRC32_BASE91D),
                HashType.CRC32_BZIP2 => new Crc(StandardDefinitions.CRC32_BZIP2),
                HashType.CRC32_CDROMEDC => new Crc(StandardDefinitions.CRC32_CDROMEDC),
                HashType.CRC32_CKSUM => new Crc(StandardDefinitions.CRC32_CKSUM),
                HashType.CRC32_DVDROMEDC => new Crc(StandardDefinitions.CRC32_DVDROMEDC),
                HashType.CRC32_ISCSI => new Crc(StandardDefinitions.CRC32_ISCSI),
                HashType.CRC32_ISOHDLC => new Crc(StandardDefinitions.CRC32_ISOHDLC),
                HashType.CRC32_JAMCRC => new Crc(StandardDefinitions.CRC32_JAMCRC),
                HashType.CRC32_MEF => new Crc(StandardDefinitions.CRC32_MEF),
                HashType.CRC32_MPEG2 => new Crc(StandardDefinitions.CRC32_MPEG2),
                HashType.CRC32_XFER => new Crc(StandardDefinitions.CRC32_XFER),

                HashType.CRC40_GSM => new Crc(StandardDefinitions.CRC40_GSM),

                HashType.CRC64 => new Crc(StandardDefinitions.CRC64_ECMA182),
                HashType.CRC64_ECMA182 => new Crc(StandardDefinitions.CRC64_ECMA182),
                HashType.CRC64_GOISO => new Crc(StandardDefinitions.CRC64_GOISO),
                HashType.CRC64_MS => new Crc(StandardDefinitions.CRC64_MS),
                HashType.CRC64_NVME => new Crc(StandardDefinitions.CRC64_NVME),
                HashType.CRC64_REDIS => new Crc(StandardDefinitions.CRC64_REDIS),
                HashType.CRC64_WE => new Crc(StandardDefinitions.CRC64_WE),
                HashType.CRC64_XZ => new Crc(StandardDefinitions.CRC64_XZ),

                HashType.Fletcher16 => new Fletcher16(),
                HashType.Fletcher32 => new Fletcher32(),
                HashType.Fletcher64 => new Fletcher64(),

                HashType.FNV0_32 => new FNV0_32(),
                HashType.FNV0_64 => new FNV0_64(),
                HashType.FNV1_32 => new FNV1_32(),
                HashType.FNV1_64 => new FNV1_64(),
                HashType.FNV1a_32 => new FNV1a_32(),
                HashType.FNV1a_64 => new FNV1a_64(),

                HashType.MekaCrc => new MekaCrc(),

                HashType.MD2 => new MD2(),
                HashType.MD4 => new MD4(),
                HashType.MD5 => MD5.Create(),

                HashType.RIPEMD128 => new RipeMD128(),
                HashType.RIPEMD160 => new RipeMD160(),
                HashType.RIPEMD256 => new RipeMD256(),
                HashType.RIPEMD320 => new RipeMD320(),

                HashType.SHA1 => SHA1.Create(),
                HashType.SHA256 => SHA256.Create(),
                HashType.SHA384 => SHA384.Create(),
                HashType.SHA512 => SHA512.Create(),
#if NET8_0_OR_GREATER
                HashType.SHA3_256 => SHA3_256.IsSupported ? SHA3_256.Create() : null,
                HashType.SHA3_384 => SHA3_384.IsSupported ? SHA3_384.Create() : null,
                HashType.SHA3_512 => SHA3_512.IsSupported ? SHA3_512.Create() : null,
                HashType.SHAKE128 => Shake128.IsSupported ? new Shake128() : null,
                HashType.SHAKE256 => Shake256.IsSupported ? new Shake256() : null,
#endif

                HashType.SpamSum => new SpamSum.SpamSum(),

                HashType.Tiger128_3 => new Tiger128_3(),
                HashType.Tiger128_4 => new Tiger128_4(),
                HashType.Tiger160_3 => new Tiger160_3(),
                HashType.Tiger160_4 => new Tiger160_4(),
                HashType.Tiger192_3 => new Tiger192_3(),
                HashType.Tiger192_4 => new Tiger192_4(),
                HashType.Tiger2_128_3 => new Tiger2_128_3(),
                HashType.Tiger2_128_4 => new Tiger2_128_4(),
                HashType.Tiger2_160_3 => new Tiger2_160_3(),
                HashType.Tiger2_160_4 => new Tiger2_160_4(),
                HashType.Tiger2_192_3 => new Tiger2_192_3(),
                HashType.Tiger2_192_4 => new Tiger2_192_4(),

                HashType.XxHash32 => new XxHash32(),
                HashType.XxHash64 => new XxHash64(),
#if NET462_OR_GREATER || NETCOREAPP
                HashType.XxHash3 => new System.IO.Hashing.XxHash3(),
                HashType.XxHash128 => new System.IO.Hashing.XxHash128(),
#endif
                _ => null,
            };
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_hasher is IDisposable disposable)
                disposable.Dispose();
        }

        #endregion

        #region Hashing

        /// <summary>
        /// Process a buffer of some length with the internal hash algorithm
        /// </summary>
        public void Process(byte[] buffer, int offset, int size)
        {
            switch (_hasher)
            {
                case HashAlgorithm ha:
                    ha.TransformBlock(buffer, offset, size, null, 0);
                    break;

#if NET462_OR_GREATER || NETCOREAPP
                case System.IO.Hashing.NonCryptographicHashAlgorithm ncha:
                    var nchaBufferSpan = new ReadOnlySpan<byte>(buffer, offset, size);
                    ncha.Append(nchaBufferSpan);
                    break;
#endif

#if NET8_0_OR_GREATER
                case Shake128 s128:
                    var s128BufferSpan = new ReadOnlySpan<byte>(buffer, offset, size);
                    s128.AppendData(s128BufferSpan);
                    break;
                case Shake256 s256:
                    var s256BufferSpan = new ReadOnlySpan<byte>(buffer, offset, size);
                    s256.AppendData(s256BufferSpan);
                    break;
#endif

                default:
                    // No-op
                    break;
            }
        }

        /// <summary>
        /// Finalize the internal hash algorigthm
        /// </summary>
        /// <remarks>NonCryptographicHashAlgorithm, SHAKE128, and SHAKE256 implementations do not need finalization</remarks>
        public void Terminate()
        {
            byte[] emptyBuffer = [];
            switch (_hasher)
            {
                case HashAlgorithm ha:
                    ha.TransformFinalBlock(emptyBuffer, 0, 0);
                    break;
                default:
                    // No-op
                    break;
            }
        }

        /// <summary>
        /// Get the variable-length string representing a CRC value
        /// </summary>
        /// <param name="cr">Crc to get the value from</param>
        /// <returns>String representing the CRC, null on error</returns>
        private static string? GetCRCVariableLengthString(Crc cr)
        {
            // Ignore null values
            if (cr.Hash is null)
                return null;

            // Get the total number of characters needed
            ulong hash = BytesToUInt64(cr.Hash);
            int length = (cr.Def.Width / 4) + (cr.Def.Width % 4 > 0 ? 1 : 0);
            return hash.ToString($"x{length}");
        }

        /// <summary>
        /// Get the Base64 representation of a SpamSum value
        /// </summary>
        /// <param name="ss">SpamSum to get the value from</param>
        /// <returns>String representing the SpamSum, null on error</returns>
        private static string? GetSpamSumBase64String(SpamSum.SpamSum ss)
        {
            // Ignore null values
            if (ss.Hash is null)
                return null;

            return System.Text.Encoding.ASCII.GetString(ss.Hash);
        }

        #endregion
    }
}
