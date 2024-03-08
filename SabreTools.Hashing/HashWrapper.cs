using System;
#if NET462_OR_GREATER || NETCOREAPP
using System.IO.Hashing;
#endif
using System.Linq;
using System.Security.Cryptography;
using Aaru.Checksums;
using Aaru.CommonTypes.Interfaces;

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
#if NETFRAMEWORK || NETCOREAPP3_1
        public HashType HashType { get; private set; }
#else
        public HashType HashType { get; init; }
#endif

        /// <summary>
        /// Current hash in bytes
        /// </summary>
        public byte[]? CurrentHashBytes
        {
            get
            {
                return _hasher switch
                {
                    HashAlgorithm ha => ha.Hash,
                    IChecksum ic => ic.Final(),
#if NET462_OR_GREATER || NETCOREAPP
                    NonCryptographicHashAlgorithm ncha => ncha.GetCurrentHash().Reverse().ToArray(),
#endif
                    OptimizedCRC.OptimizedCRC ocrc => BitConverter.GetBytes(ocrc.Value).Reverse().ToArray(),
                    _ => null,
                };
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
#if NET462_OR_GREATER || NETCOREAPP
                HashType.CRC32 => new Crc32(),
                HashType.CRC32_Optimized => new OptimizedCRC.OptimizedCRC(),
                HashType.CRC64 => new Crc64(),
#else
                HashType.CRC32 => new OptimizedCRC.OptimizedCRC(),
#endif
                HashType.MD5 => MD5.Create(),
#if NETFRAMEWORK
                HashType.RIPEMD160 => RIPEMD160.Create(),
#endif
                HashType.SHA1 => SHA1.Create(),
                HashType.SHA256 => SHA256.Create(),
                HashType.SHA384 => SHA384.Create(),
                HashType.SHA512 => SHA512.Create(),
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
                case HashAlgorithm ha:
                    ha.TransformBlock(buffer, offset, size, null, 0);
                    break;

                case IChecksum ic:
                    byte[] icBuffer = GetArraySegment(buffer, offset, size);
                    ic.Update(icBuffer);
                    break;

#if NET462_OR_GREATER || NETCOREAPP
                case NonCryptographicHashAlgorithm ncha:
                    var nchaBufferSpan = new ReadOnlySpan<byte>(buffer, offset, size);
                    ncha.Append(nchaBufferSpan);
                    break;
#endif

                case OptimizedCRC.OptimizedCRC oc:
                    oc.Update(buffer, offset, size);
                    break;
            }
        }

        /// <summary>
        /// Finalize the internal hash algorigthm
        /// </summary>
        /// <remarks>NonCryptographicHashAlgorithm implementations do not need finalization</remarks>
        public void Terminate()
        {
            byte[] emptyBuffer = [];
            switch (_hasher)
            {
                case HashAlgorithm ha:
                    ha.TransformFinalBlock(emptyBuffer, 0, 0);
                    break;

                case OptimizedCRC.OptimizedCRC oc:
                    oc.Update([], 0, 0);
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
        /// Get a segment from the array based on an offset and size
        /// </summary>
        private static byte[] GetArraySegment(byte[] buffer, int offset, int size)
        {
#if NET452_OR_GREATER || NETCOREAPP
            var icBufferSpan = new ReadOnlySpan<byte>(buffer, offset, size);
            byte[] trimmedBuffer = icBufferSpan.ToArray();
#else
            byte[] trimmedBuffer = new byte[size];
            Array.Copy(buffer, offset, trimmedBuffer, 0, size);
#endif
            return trimmedBuffer;
        }

        #endregion
    }
}