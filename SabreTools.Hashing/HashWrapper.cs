using System;
#if NET462_OR_GREATER || NETCOREAPP
using System.IO.Hashing;
#endif
using System.Security.Cryptography;
using Aaru.Checksums;
using Aaru.CommonTypes.Interfaces;
#if NET7_0_OR_GREATER
using Blake3;
#endif
using SabreTools.Hashing.Crc;

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
        public byte[]? CurrentHashBytes
        {
            get
            {
                switch (_hasher)
                {
                    case CrcRunner cr:
                        var crArr = cr.Finalize();
                        Array.Reverse(crArr);
                        return crArr;

                    case HashAlgorithm ha:
                        return ha.Hash;

                    case IChecksum ic:
                        return ic.Final();

#if NET462_OR_GREATER || NETCOREAPP
                    case XxHash64 xxh64:
                        return xxh64.GetCurrentHash();
                    case XxHash3 xxh3:
                        return xxh3.GetCurrentHash();
                    case XxHash128 xxh128:
                        return xxh128.GetCurrentHash();
                    case NonCryptographicHashAlgorithm ncha:
                        var nchaArr = ncha.GetCurrentHash();
                        Array.Reverse(nchaArr);
                        return nchaArr;
#endif

#if NET8_0_OR_GREATER
                    case Shake128 s128:
                        return s128.GetCurrentHash(32);
                    case Shake256 s256:
                        return s256.GetCurrentHash(64);
#endif

                    case XxHash.XxHash32 xxh32:
                        var xxh32Arr = xxh32.Finalize();
                        Array.Reverse(xxh32Arr);
                        return xxh32Arr;
                    case XxHash.XxHash64 xxh64:
                        var xxh64Arr = xxh64.Finalize();
                        Array.Reverse(xxh64Arr);
                        return xxh64Arr;

                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// Current hash as a string
        /// </summary>
        public string? CurrentHashString
        {
            get
            {
                switch (_hasher)
                {
                    case CrcRunner cr:
                        var crArr = cr.Finalize();
                        ulong crHash = BytesToUInt64(crArr);
                        int length = cr.Def.Width / 4 + (cr.Def.Width % 4 > 0 ? 1 : 0);
                        return crHash.ToString($"x{length}");
                    case IChecksum ic:
                        return ic.End();

                    default:
                        return ByteArrayToString(CurrentHashBytes);
                }
            }
        }

        #endregion

        #region Private Fields

        /// <summary>
        /// Internal hasher being used for processing
        /// </summary>
        /// <remarks>May be either a HashAlgorithm or NonCryptographicHashAlgorithm</remarks>
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
                HashType.Adler32 => new Adler32Context(),

#if NET7_0_OR_GREATER
                HashType.BLAKE3 => new Blake3HashAlgorithm(),
#endif

                HashType.CRC1_ZERO => new CrcRunner(StandardDefinitions.CRC1_ZERO),
                HashType.CRC1_ONE => new CrcRunner(StandardDefinitions.CRC1_ONE),

                HashType.CRC3_GSM => new CrcRunner(StandardDefinitions.CRC3_GSM),
                HashType.CRC3_ROHC => new CrcRunner(StandardDefinitions.CRC3_ROHC),

                HashType.CRC4_G704 => new CrcRunner(StandardDefinitions.CRC4_G704),
                HashType.CRC4_INTERLAKEN => new CrcRunner(StandardDefinitions.CRC4_INTERLAKEN),

                HashType.CRC5_EPCC1G2 => new CrcRunner(StandardDefinitions.CRC5_EPCC1G2),
                HashType.CRC5_G704 => new CrcRunner(StandardDefinitions.CRC5_G704),
                HashType.CRC5_USB => new CrcRunner(StandardDefinitions.CRC5_USB),

                HashType.CRC6_CDMA2000A => new CrcRunner(StandardDefinitions.CRC6_CDMA2000A),
                HashType.CRC6_CDMA2000B => new CrcRunner(StandardDefinitions.CRC6_CDMA2000B),
                HashType.CRC6_DARC => new CrcRunner(StandardDefinitions.CRC6_DARC),
                HashType.CRC6_G704 => new CrcRunner(StandardDefinitions.CRC6_G704),
                HashType.CRC6_GSM => new CrcRunner(StandardDefinitions.CRC6_GSM),

                HashType.CRC7_MMC => new CrcRunner(StandardDefinitions.CRC7_MMC),
                HashType.CRC7_ROHC => new CrcRunner(StandardDefinitions.CRC7_ROHC),
                HashType.CRC7_UMTS => new CrcRunner(StandardDefinitions.CRC7_UMTS),

                HashType.CRC8 => new CrcRunner(StandardDefinitions.CRC8_SMBUS),
                HashType.CRC8_AUTOSAR => new CrcRunner(StandardDefinitions.CRC8_AUTOSAR),
                HashType.CRC8_BLUETOOTH => new CrcRunner(StandardDefinitions.CRC8_BLUETOOTH),
                HashType.CRC8_CDMA2000 => new CrcRunner(StandardDefinitions.CRC8_CDMA2000),
                HashType.CRC8_DARC => new CrcRunner(StandardDefinitions.CRC8_DARC),
                HashType.CRC8_DVBS2 => new CrcRunner(StandardDefinitions.CRC8_DVBS2),
                HashType.CRC8_GSMA => new CrcRunner(StandardDefinitions.CRC8_GSMA),
                HashType.CRC8_GSMB => new CrcRunner(StandardDefinitions.CRC8_GSMB),
                HashType.CRC8_HITAG => new CrcRunner(StandardDefinitions.CRC8_HITAG),
                HashType.CRC8_I4321 => new CrcRunner(StandardDefinitions.CRC8_I4321),
                HashType.CRC8_ICODE => new CrcRunner(StandardDefinitions.CRC8_ICODE),
                HashType.CRC8_LTE => new CrcRunner(StandardDefinitions.CRC8_LTE),
                HashType.CRC8_MAXIMDOW => new CrcRunner(StandardDefinitions.CRC8_MAXIMDOW),
                HashType.CRC8_MIFAREMAD => new CrcRunner(StandardDefinitions.CRC8_MIFAREMAD),
                HashType.CRC8_NRSC5 => new CrcRunner(StandardDefinitions.CRC8_NRSC5),
                HashType.CRC8_OPENSAFETY => new CrcRunner(StandardDefinitions.CRC8_OPENSAFETY),
                HashType.CRC8_ROHC => new CrcRunner(StandardDefinitions.CRC8_ROHC),
                HashType.CRC8_SAEJ1850 => new CrcRunner(StandardDefinitions.CRC8_SAEJ1850),
                HashType.CRC8_SMBUS => new CrcRunner(StandardDefinitions.CRC8_SMBUS),
                HashType.CRC8_TECH3250 => new CrcRunner(StandardDefinitions.CRC8_TECH3250),
                HashType.CRC8_WCDMA => new CrcRunner(StandardDefinitions.CRC8_WCDMA),

                HashType.CRC10_ATM => new CrcRunner(StandardDefinitions.CRC10_ATM),
                HashType.CRC10_CDMA2000 => new CrcRunner(StandardDefinitions.CRC10_CDMA2000),
                HashType.CRC10_GSM => new CrcRunner(StandardDefinitions.CRC10_GSM),

                HashType.CRC11_FLEXRAY => new CrcRunner(StandardDefinitions.CRC11_FLEXRAY),
                HashType.CRC11_UMTS => new CrcRunner(StandardDefinitions.CRC11_UMTS),

                HashType.CRC12_CDMA2000 => new CrcRunner(StandardDefinitions.CRC12_CDMA2000),
                HashType.CRC12_DECT => new CrcRunner(StandardDefinitions.CRC12_DECT),
                HashType.CRC12_GSM => new CrcRunner(StandardDefinitions.CRC12_GSM),
                HashType.CRC12_UMTS => new CrcRunner(StandardDefinitions.CRC12_UMTS),

                HashType.CRC13_BBC => new CrcRunner(StandardDefinitions.CRC13_BBC),

                HashType.CRC14_DARC => new CrcRunner(StandardDefinitions.CRC14_DARC),
                HashType.CRC14_GSM => new CrcRunner(StandardDefinitions.CRC14_GSM),

                HashType.CRC15_CAN => new CrcRunner(StandardDefinitions.CRC15_CAN),
                HashType.CRC15_MPT1327 => new CrcRunner(StandardDefinitions.CRC15_MPT1327),

                HashType.CRC16 => new CrcRunner(StandardDefinitions.CRC16_ARC),
                HashType.CRC16_ARC => new CrcRunner(StandardDefinitions.CRC16_ARC),
                HashType.CRC16_CDMA2000 => new CrcRunner(StandardDefinitions.CRC16_CDMA2000),
                HashType.CRC16_CMS => new CrcRunner(StandardDefinitions.CRC16_CMS),
                HashType.CRC16_DDS110 => new CrcRunner(StandardDefinitions.CRC16_DDS110),
                HashType.CRC16_DECTR => new CrcRunner(StandardDefinitions.CRC16_DECTR),
                HashType.CRC16_DECTX => new CrcRunner(StandardDefinitions.CRC16_DECTX),
                HashType.CRC16_DNP => new CrcRunner(StandardDefinitions.CRC16_DNP),
                HashType.CRC16_EN13757 => new CrcRunner(StandardDefinitions.CRC16_EN13757),
                HashType.CRC16_GENIBUS => new CrcRunner(StandardDefinitions.CRC16_GENIBUS),
                HashType.CRC16_GSM => new CrcRunner(StandardDefinitions.CRC16_GSM),
                HashType.CRC16_IBM3740 => new CrcRunner(StandardDefinitions.CRC16_IBM3740),
                HashType.CRC16_IBMSDLC => new CrcRunner(StandardDefinitions.CRC16_IBMSDLC),
                HashType.CRC16_ISOIEC144433A => new CrcRunner(StandardDefinitions.CRC16_ISOIEC144433A),
                HashType.CRC16_KERMIT => new CrcRunner(StandardDefinitions.CRC16_KERMIT),
                HashType.CRC16_LJ1200 => new CrcRunner(StandardDefinitions.CRC16_LJ1200),
                HashType.CRC16_M17 => new CrcRunner(StandardDefinitions.CRC16_M17),
                HashType.CRC16_MAXIMDOW => new CrcRunner(StandardDefinitions.CRC16_MAXIMDOW),
                HashType.CRC16_MCRF4XX => new CrcRunner(StandardDefinitions.CRC16_MCRF4XX),
                HashType.CRC16_MODBUS => new CrcRunner(StandardDefinitions.CRC16_MODBUS),
                HashType.CRC16_NRSC5 => new CrcRunner(StandardDefinitions.CRC16_NRSC5),
                HashType.CRC16_OPENSAFETYA => new CrcRunner(StandardDefinitions.CRC16_OPENSAFETYA),
                HashType.CRC16_OPENSAFETYB => new CrcRunner(StandardDefinitions.CRC16_OPENSAFETYB),
                HashType.CRC16_PROFIBUS => new CrcRunner(StandardDefinitions.CRC16_PROFIBUS),
                HashType.CRC16_RIELLO => new CrcRunner(StandardDefinitions.CRC16_RIELLO),
                HashType.CRC16_SPIFUJITSU => new CrcRunner(StandardDefinitions.CRC16_SPIFUJITSU),
                HashType.CRC16_T10DIF => new CrcRunner(StandardDefinitions.CRC16_T10DIF),
                HashType.CRC16_TELEDISK => new CrcRunner(StandardDefinitions.CRC16_TELEDISK),
                HashType.CRC16_TMS37157 => new CrcRunner(StandardDefinitions.CRC16_TMS37157),
                HashType.CRC16_UMTS => new CrcRunner(StandardDefinitions.CRC16_UMTS),
                HashType.CRC16_USB => new CrcRunner(StandardDefinitions.CRC16_USB),
                HashType.CRC16_XMODEM => new CrcRunner(StandardDefinitions.CRC16_XMODEM),

                HashType.CRC17_CANFD => new CrcRunner(StandardDefinitions.CRC17_CANFD),

                HashType.CRC21_CANFD => new CrcRunner(StandardDefinitions.CRC21_CANFD),

                HashType.CRC24_BLE => new CrcRunner(StandardDefinitions.CRC24_BLE),
                HashType.CRC24_FLEXRAYA => new CrcRunner(StandardDefinitions.CRC24_FLEXRAYA),
                HashType.CRC24_FLEXRAYB => new CrcRunner(StandardDefinitions.CRC24_FLEXRAYB),
                HashType.CRC24_INTERLAKEN => new CrcRunner(StandardDefinitions.CRC24_INTERLAKEN),
                HashType.CRC24_LTEA => new CrcRunner(StandardDefinitions.CRC24_LTEA),
                HashType.CRC24_LTEB => new CrcRunner(StandardDefinitions.CRC24_LTEB),
                HashType.CRC24_OPENPGP => new CrcRunner(StandardDefinitions.CRC24_OPENPGP),
                HashType.CRC24_OS9 => new CrcRunner(StandardDefinitions.CRC24_OS9),

                HashType.CRC30_CDMA => new CrcRunner(StandardDefinitions.CRC30_CDMA),

                HashType.CRC31_PHILIPS => new CrcRunner(StandardDefinitions.CRC31_PHILIPS),

                HashType.CRC32 => new CrcRunner(StandardDefinitions.CRC32_ISOHDLC),
                HashType.CRC32_AIXM => new CrcRunner(StandardDefinitions.CRC32_AIXM),
                HashType.CRC32_AUTOSAR => new CrcRunner(StandardDefinitions.CRC32_AUTOSAR),
                HashType.CRC32_BASE91D => new CrcRunner(StandardDefinitions.CRC32_BASE91D),
                HashType.CRC32_BZIP2 => new CrcRunner(StandardDefinitions.CRC32_BZIP2),
                HashType.CRC32_CDROMEDC => new CrcRunner(StandardDefinitions.CRC32_CDROMEDC),
                HashType.CRC32_CKSUM => new CrcRunner(StandardDefinitions.CRC32_CKSUM),
                HashType.CRC32_ISCSI => new CrcRunner(StandardDefinitions.CRC32_ISCSI),
                HashType.CRC32_ISOHDLC => new CrcRunner(StandardDefinitions.CRC32_ISOHDLC),
                HashType.CRC32_JAMCRC => new CrcRunner(StandardDefinitions.CRC32_JAMCRC),
                HashType.CRC32_MEF => new CrcRunner(StandardDefinitions.CRC32_MEF),
                HashType.CRC32_MPEG2 => new CrcRunner(StandardDefinitions.CRC32_MPEG2),
                HashType.CRC32_XFER => new CrcRunner(StandardDefinitions.CRC32_XFER),

                HashType.CRC40_GSM => new CrcRunner(StandardDefinitions.CRC40_GSM),

                HashType.CRC64 => new CrcRunner(StandardDefinitions.CRC64_ECMA182),
                HashType.CRC64_ECMA182 => new CrcRunner(StandardDefinitions.CRC64_ECMA182),
                HashType.CRC64_GOISO => new CrcRunner(StandardDefinitions.CRC64_GOISO),
                HashType.CRC64_MS => new CrcRunner(StandardDefinitions.CRC64_MS),
                HashType.CRC64_NVME => new CrcRunner(StandardDefinitions.CRC64_NVME),
                HashType.CRC64_REDIS => new CrcRunner(StandardDefinitions.CRC64_REDIS),
                HashType.CRC64_WE => new CrcRunner(StandardDefinitions.CRC64_WE),
                HashType.CRC64_XZ => new CrcRunner(StandardDefinitions.CRC64_XZ),

                HashType.Fletcher16 => new Fletcher16Context(),
                HashType.Fletcher32 => new Fletcher32Context(),

                HashType.MD5 => MD5.Create(),

#if NETFRAMEWORK
                HashType.RIPEMD160 => RIPEMD160.Create(),
#endif

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

                HashType.SpamSum => new SpamSumContext(),

                HashType.XxHash32 => new XxHash.XxHash32(),
#if NET462_OR_GREATER || NETCOREAPP
                HashType.XxHash64 => new XxHash64(),
                HashType.XxHash3 => new XxHash3(),
                HashType.XxHash128 => new XxHash128(),
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
                case CrcRunner cr:
                    cr.TransformBlock(buffer, offset, size);
                    break;

                case HashAlgorithm ha:
                    ha.TransformBlock(buffer, offset, size, null, 0);
                    break;

                case IChecksum ic:
                    byte[] icBlock = new byte[size];
                    Array.Copy(buffer, offset, icBlock, 0, size);
                    ic.Update(icBlock);
                    break;

#if NET462_OR_GREATER || NETCOREAPP
                case NonCryptographicHashAlgorithm ncha:
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

                case XxHash.XxHash32 xxh32:
                    xxh32.TransformBlock(buffer, offset, size);
                    break;
                case XxHash.XxHash64 xxh64:
                    xxh64.TransformBlock(buffer, offset, size);
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
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Convert a byte array to a hex string
        /// </summary>
        /// <param name="bytes">Byte array to convert</param>
        /// <returns>Hex string representing the byte array</returns>
        /// <link>http://stackoverflow.com/questions/311165/how-do-you-convert-byte-array-to-hexadecimal-string-and-vice-versa</link>
        private static string? ByteArrayToString(byte[]? bytes)
        {
            // If we get null in, we send null out
            if (bytes == null)
                return null;

            try
            {
                string hex = BitConverter.ToString(bytes);
                return hex.Replace("-", string.Empty).ToLowerInvariant();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert a byte array to a UInt64
        /// </summary>
        /// <param name="bytes">Byte array to convert</param>
        /// <returns>UInt64 representing the byte array</returns>
        /// <link>https://stackoverflow.com/questions/66750224/how-to-convert-a-byte-array-of-any-size-to-ulong-in-c</link>
        private static ulong BytesToUInt64(byte[]? bytes)
        {
            // If we get null in, we send 0 out
            if (bytes == null)
                return default;

            ulong result = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                result |= (ulong)bytes[i] << (i * 8);
            }

            return result;
        }

        #endregion
    }
}