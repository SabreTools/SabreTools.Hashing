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
                        if (HashType == HashType.CRC32)
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
            this.HashType = hashType;
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
                HashType.CRC16_CCITT => new CRC16CcittContext(),
                HashType.CRC16_IBM => new CRC16IbmContext(),
                HashType.CRC32 => new CrcRunner(StandardDefinitions.CRC32_ISOHDLC),
                HashType.CRC32_ISOHDLC => new CrcRunner(StandardDefinitions.CRC32_ISOHDLC),
                HashType.CRC64_ECMA182 => new CrcRunner(StandardDefinitions.CRC64_ECMA182),
                HashType.CRC64_GOISO => new CrcRunner(StandardDefinitions.CRC64_GOISO),
                HashType.CRC64_XZ => new Crc64Context(),
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