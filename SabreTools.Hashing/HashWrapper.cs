using System;
using System.Security.Cryptography;
using SabreTools.Hashing.Checksum;
using static SabreTools.Hashing.HashOperations;

namespace SabreTools.Hashing
{
    /// <summary>
    /// Wrapper for a single hashing algorithm
    /// </summary>
    public sealed class HashWrapper : IDisposable
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
        private readonly object? _hasher;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hashType">Hash type to instantiate</param>
        public HashWrapper(HashType hashType)
        {
            HashType = hashType;
            _hasher = HashType.CreateHasher(hashType);
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
