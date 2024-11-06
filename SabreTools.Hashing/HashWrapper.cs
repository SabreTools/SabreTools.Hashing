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
                return _hasher switch
                {
                    IChecksum ic => ic.End(),
                    _ => ByteArrayToString(CurrentHashBytes),
                };
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
                //HashType.CRC16_ISOIEC144433A => new CrcRunner(StandardDefinitions.CRC16_ISOIEC144433A),
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
                //HashType.CRC16_RIELLO => new CrcRunner(StandardDefinitions.CRC16_RIELLO),
                HashType.CRC16_SPIFUJITSU => new CrcRunner(StandardDefinitions.CRC16_SPIFUJITSU),
                HashType.CRC16_T10DIF => new CrcRunner(StandardDefinitions.CRC16_T10DIF),
                HashType.CRC16_TELEDISK => new CrcRunner(StandardDefinitions.CRC16_TELEDISK),
                //HashType.CRC16_TMS37157 => new CrcRunner(StandardDefinitions.CRC16_TMS37157),
                HashType.CRC16_UMTS => new CrcRunner(StandardDefinitions.CRC16_UMTS),
                HashType.CRC16_USB => new CrcRunner(StandardDefinitions.CRC16_USB),
                HashType.CRC16_XMODEM => new CrcRunner(StandardDefinitions.CRC16_XMODEM),

                //HashType.CRC24_BLE => new CrcRunner(StandardDefinitions.CRC24_BLE),
                HashType.CRC24_FLEXRAYA => new CrcRunner(StandardDefinitions.CRC24_FLEXRAYA),
                HashType.CRC24_FLEXRAYB => new CrcRunner(StandardDefinitions.CRC24_FLEXRAYB),
                HashType.CRC24_INTERLAKEN => new CrcRunner(StandardDefinitions.CRC24_INTERLAKEN),
                HashType.CRC24_LTEA => new CrcRunner(StandardDefinitions.CRC24_LTEA),
                HashType.CRC24_LTEB => new CrcRunner(StandardDefinitions.CRC24_LTEB),
                HashType.CRC24_OPENPGP => new CrcRunner(StandardDefinitions.CRC24_OPENPGP),
                HashType.CRC24_OS9 => new CrcRunner(StandardDefinitions.CRC24_OS9),

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
#if NET462_OR_GREATER || NETCOREAPP
                HashType.XxHash32 => new XxHash32(),
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

        #endregion
    }
}