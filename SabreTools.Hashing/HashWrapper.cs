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
            if (HashType == HashType.Adler32)
                _hasher = new Adler32();

#if NET7_0_OR_GREATER
            else if (HashType == HashType.BLAKE3)
                _hasher = new Blake3.Blake3HashAlgorithm();
#endif

            else if (HashType == HashType.CRC1_ZERO)
                _hasher = new Crc(StandardDefinitions.CRC1_ZERO);
            else if (HashType == HashType.CRC1_ONE)
                _hasher = new Crc(StandardDefinitions.CRC1_ONE);

            else if (HashType == HashType.CRC3_GSM)
                _hasher = new Crc(StandardDefinitions.CRC3_GSM);
            else if (HashType == HashType.CRC3_ROHC)
                _hasher = new Crc(StandardDefinitions.CRC3_ROHC);

            else if (HashType == HashType.CRC4_G704)
                _hasher = new Crc(StandardDefinitions.CRC4_G704);
            else if (HashType == HashType.CRC4_INTERLAKEN)
                _hasher = new Crc(StandardDefinitions.CRC4_INTERLAKEN);

            else if (HashType == HashType.CRC5_EPCC1G2)
                _hasher = new Crc(StandardDefinitions.CRC5_EPCC1G2);
            else if (HashType == HashType.CRC5_G704)
                _hasher = new Crc(StandardDefinitions.CRC5_G704);
            else if (HashType == HashType.CRC5_USB)
                _hasher = new Crc(StandardDefinitions.CRC5_USB);

            else if (HashType == HashType.CRC6_CDMA2000A)
                _hasher = new Crc(StandardDefinitions.CRC6_CDMA2000A);
            else if (HashType == HashType.CRC6_CDMA2000B)
                _hasher = new Crc(StandardDefinitions.CRC6_CDMA2000B);
            else if (HashType == HashType.CRC6_DARC)
                _hasher = new Crc(StandardDefinitions.CRC6_DARC);
            else if (HashType == HashType.CRC6_G704)
                _hasher = new Crc(StandardDefinitions.CRC6_G704);
            else if (HashType == HashType.CRC6_GSM)
                _hasher = new Crc(StandardDefinitions.CRC6_GSM);

            else if (HashType == HashType.CRC7_MMC)
                _hasher = new Crc(StandardDefinitions.CRC7_MMC);
            else if (HashType == HashType.CRC7_ROHC)
                _hasher = new Crc(StandardDefinitions.CRC7_ROHC);
            else if (HashType == HashType.CRC7_UMTS)
                _hasher = new Crc(StandardDefinitions.CRC7_UMTS);

            else if (HashType == HashType.CRC8)
                _hasher = new Crc(StandardDefinitions.CRC8_SMBUS);
            else if (HashType == HashType.CRC8_AUTOSAR)
                _hasher = new Crc(StandardDefinitions.CRC8_AUTOSAR);
            else if (HashType == HashType.CRC8_BLUETOOTH)
                _hasher = new Crc(StandardDefinitions.CRC8_BLUETOOTH);
            else if (HashType == HashType.CRC8_CDMA2000)
                _hasher = new Crc(StandardDefinitions.CRC8_CDMA2000);
            else if (HashType == HashType.CRC8_DARC)
                _hasher = new Crc(StandardDefinitions.CRC8_DARC);
            else if (HashType == HashType.CRC8_DVBS2)
                _hasher = new Crc(StandardDefinitions.CRC8_DVBS2);
            else if (HashType == HashType.CRC8_GSMA)
                _hasher = new Crc(StandardDefinitions.CRC8_GSMA);
            else if (HashType == HashType.CRC8_GSMB)
                _hasher = new Crc(StandardDefinitions.CRC8_GSMB);
            else if (HashType == HashType.CRC8_HITAG)
                _hasher = new Crc(StandardDefinitions.CRC8_HITAG);
            else if (HashType == HashType.CRC8_I4321)
                _hasher = new Crc(StandardDefinitions.CRC8_I4321);
            else if (HashType == HashType.CRC8_ICODE)
                _hasher = new Crc(StandardDefinitions.CRC8_ICODE);
            else if (HashType == HashType.CRC8_LTE)
                _hasher = new Crc(StandardDefinitions.CRC8_LTE);
            else if (HashType == HashType.CRC8_MAXIMDOW)
                _hasher = new Crc(StandardDefinitions.CRC8_MAXIMDOW);
            else if (HashType == HashType.CRC8_MIFAREMAD)
                _hasher = new Crc(StandardDefinitions.CRC8_MIFAREMAD);
            else if (HashType == HashType.CRC8_NRSC5)
                _hasher = new Crc(StandardDefinitions.CRC8_NRSC5);
            else if (HashType == HashType.CRC8_OPENSAFETY)
                _hasher = new Crc(StandardDefinitions.CRC8_OPENSAFETY);
            else if (HashType == HashType.CRC8_ROHC)
                _hasher = new Crc(StandardDefinitions.CRC8_ROHC);
            else if (HashType == HashType.CRC8_SAEJ1850)
                _hasher = new Crc(StandardDefinitions.CRC8_SAEJ1850);
            else if (HashType == HashType.CRC8_SMBUS)
                _hasher = new Crc(StandardDefinitions.CRC8_SMBUS);
            else if (HashType == HashType.CRC8_TECH3250)
                _hasher = new Crc(StandardDefinitions.CRC8_TECH3250);
            else if (HashType == HashType.CRC8_WCDMA)
                _hasher = new Crc(StandardDefinitions.CRC8_WCDMA);

            else if (HashType == HashType.CRC10_ATM)
                _hasher = new Crc(StandardDefinitions.CRC10_ATM);
            else if (HashType == HashType.CRC10_CDMA2000)
                _hasher = new Crc(StandardDefinitions.CRC10_CDMA2000);
            else if (HashType == HashType.CRC10_GSM)
                _hasher = new Crc(StandardDefinitions.CRC10_GSM);

            else if (HashType == HashType.CRC11_FLEXRAY)
                _hasher = new Crc(StandardDefinitions.CRC11_FLEXRAY);
            else if (HashType == HashType.CRC11_UMTS)
                _hasher = new Crc(StandardDefinitions.CRC11_UMTS);

            else if (HashType == HashType.CRC12_CDMA2000)
                _hasher = new Crc(StandardDefinitions.CRC12_CDMA2000);
            else if (HashType == HashType.CRC12_DECT)
                _hasher = new Crc(StandardDefinitions.CRC12_DECT);
            else if (HashType == HashType.CRC12_GSM)
                _hasher = new Crc(StandardDefinitions.CRC12_GSM);
            else if (HashType == HashType.CRC12_UMTS)
                _hasher = new Crc(StandardDefinitions.CRC12_UMTS);

            else if (HashType == HashType.CRC13_BBC)
                _hasher = new Crc(StandardDefinitions.CRC13_BBC);

            else if (HashType == HashType.CRC14_DARC)
                _hasher = new Crc(StandardDefinitions.CRC14_DARC);
            else if (HashType == HashType.CRC14_GSM)
                _hasher = new Crc(StandardDefinitions.CRC14_GSM);

            else if (HashType == HashType.CRC15_CAN)
                _hasher = new Crc(StandardDefinitions.CRC15_CAN);
            else if (HashType == HashType.CRC15_MPT1327)
                _hasher = new Crc(StandardDefinitions.CRC15_MPT1327);

            else if (HashType == HashType.CRC16)
                _hasher = new Crc(StandardDefinitions.CRC16_ARC);
            else if (HashType == HashType.CRC16_ARC)
                _hasher = new Crc(StandardDefinitions.CRC16_ARC);
            else if (HashType == HashType.CRC16_CDMA2000)
                _hasher = new Crc(StandardDefinitions.CRC16_CDMA2000);
            else if (HashType == HashType.CRC16_CMS)
                _hasher = new Crc(StandardDefinitions.CRC16_CMS);
            else if (HashType == HashType.CRC16_DDS110)
                _hasher = new Crc(StandardDefinitions.CRC16_DDS110);
            else if (HashType == HashType.CRC16_DECTR)
                _hasher = new Crc(StandardDefinitions.CRC16_DECTR);
            else if (HashType == HashType.CRC16_DECTX)
                _hasher = new Crc(StandardDefinitions.CRC16_DECTX);
            else if (HashType == HashType.CRC16_DNP)
                _hasher = new Crc(StandardDefinitions.CRC16_DNP);
            else if (HashType == HashType.CRC16_EN13757)
                _hasher = new Crc(StandardDefinitions.CRC16_EN13757);
            else if (HashType == HashType.CRC16_GENIBUS)
                _hasher = new Crc(StandardDefinitions.CRC16_GENIBUS);
            else if (HashType == HashType.CRC16_GSM)
                _hasher = new Crc(StandardDefinitions.CRC16_GSM);
            else if (HashType == HashType.CRC16_IBM3740)
                _hasher = new Crc(StandardDefinitions.CRC16_IBM3740);
            else if (HashType == HashType.CRC16_IBMSDLC)
                _hasher = new Crc(StandardDefinitions.CRC16_IBMSDLC);
            else if (HashType == HashType.CRC16_ISOIEC144433A)
                _hasher = new Crc(StandardDefinitions.CRC16_ISOIEC144433A);
            else if (HashType == HashType.CRC16_KERMIT)
                _hasher = new Crc(StandardDefinitions.CRC16_KERMIT);
            else if (HashType == HashType.CRC16_LJ1200)
                _hasher = new Crc(StandardDefinitions.CRC16_LJ1200);
            else if (HashType == HashType.CRC16_M17)
                _hasher = new Crc(StandardDefinitions.CRC16_M17);
            else if (HashType == HashType.CRC16_MAXIMDOW)
                _hasher = new Crc(StandardDefinitions.CRC16_MAXIMDOW);
            else if (HashType == HashType.CRC16_MCRF4XX)
                _hasher = new Crc(StandardDefinitions.CRC16_MCRF4XX);
            else if (HashType == HashType.CRC16_MODBUS)
                _hasher = new Crc(StandardDefinitions.CRC16_MODBUS);
            else if (HashType == HashType.CRC16_NRSC5)
                _hasher = new Crc(StandardDefinitions.CRC16_NRSC5);
            else if (HashType == HashType.CRC16_OPENSAFETYA)
                _hasher = new Crc(StandardDefinitions.CRC16_OPENSAFETYA);
            else if (HashType == HashType.CRC16_OPENSAFETYB)
                _hasher = new Crc(StandardDefinitions.CRC16_OPENSAFETYB);
            else if (HashType == HashType.CRC16_PROFIBUS)
                _hasher = new Crc(StandardDefinitions.CRC16_PROFIBUS);
            else if (HashType == HashType.CRC16_RIELLO)
                _hasher = new Crc(StandardDefinitions.CRC16_RIELLO);
            else if (HashType == HashType.CRC16_SPIFUJITSU)
                _hasher = new Crc(StandardDefinitions.CRC16_SPIFUJITSU);
            else if (HashType == HashType.CRC16_T10DIF)
                _hasher = new Crc(StandardDefinitions.CRC16_T10DIF);
            else if (HashType == HashType.CRC16_TELEDISK)
                _hasher = new Crc(StandardDefinitions.CRC16_TELEDISK);
            else if (HashType == HashType.CRC16_TMS37157)
                _hasher = new Crc(StandardDefinitions.CRC16_TMS37157);
            else if (HashType == HashType.CRC16_UMTS)
                _hasher = new Crc(StandardDefinitions.CRC16_UMTS);
            else if (HashType == HashType.CRC16_USB)
                _hasher = new Crc(StandardDefinitions.CRC16_USB);
            else if (HashType == HashType.CRC16_XMODEM)
                _hasher = new Crc(StandardDefinitions.CRC16_XMODEM);

            else if (HashType == HashType.CRC17_CANFD)
                _hasher = new Crc(StandardDefinitions.CRC17_CANFD);

            else if (HashType == HashType.CRC21_CANFD)
                _hasher = new Crc(StandardDefinitions.CRC21_CANFD);

            else if (HashType == HashType.CRC24_BLE)
                _hasher = new Crc(StandardDefinitions.CRC24_BLE);
            else if (HashType == HashType.CRC24_FLEXRAYA)
                _hasher = new Crc(StandardDefinitions.CRC24_FLEXRAYA);
            else if (HashType == HashType.CRC24_FLEXRAYB)
                _hasher = new Crc(StandardDefinitions.CRC24_FLEXRAYB);
            else if (HashType == HashType.CRC24_INTERLAKEN)
                _hasher = new Crc(StandardDefinitions.CRC24_INTERLAKEN);
            else if (HashType == HashType.CRC24_LTEA)
                _hasher = new Crc(StandardDefinitions.CRC24_LTEA);
            else if (HashType == HashType.CRC24_LTEB)
                _hasher = new Crc(StandardDefinitions.CRC24_LTEB);
            else if (HashType == HashType.CRC24_OPENPGP)
                _hasher = new Crc(StandardDefinitions.CRC24_OPENPGP);
            else if (HashType == HashType.CRC24_OS9)
                _hasher = new Crc(StandardDefinitions.CRC24_OS9);

            else if (HashType == HashType.CRC30_CDMA)
                _hasher = new Crc(StandardDefinitions.CRC30_CDMA);

            else if (HashType == HashType.CRC31_PHILIPS)
                _hasher = new Crc(StandardDefinitions.CRC31_PHILIPS);

            else if (HashType == HashType.CRC32)
                _hasher = new Crc(StandardDefinitions.CRC32_ISOHDLC);
            else if (HashType == HashType.CRC32_AIXM)
                _hasher = new Crc(StandardDefinitions.CRC32_AIXM);
            else if (HashType == HashType.CRC32_AUTOSAR)
                _hasher = new Crc(StandardDefinitions.CRC32_AUTOSAR);
            else if (HashType == HashType.CRC32_BASE91D)
                _hasher = new Crc(StandardDefinitions.CRC32_BASE91D);
            else if (HashType == HashType.CRC32_BZIP2)
                _hasher = new Crc(StandardDefinitions.CRC32_BZIP2);
            else if (HashType == HashType.CRC32_CDROMEDC)
                _hasher = new Crc(StandardDefinitions.CRC32_CDROMEDC);
            else if (HashType == HashType.CRC32_CKSUM)
                _hasher = new Crc(StandardDefinitions.CRC32_CKSUM);
            else if (HashType == HashType.CRC32_DVDROMEDC)
                _hasher = new Crc(StandardDefinitions.CRC32_DVDROMEDC);
            else if (HashType == HashType.CRC32_ISCSI)
                _hasher = new Crc(StandardDefinitions.CRC32_ISCSI);
            else if (HashType == HashType.CRC32_ISOHDLC)
                _hasher = new Crc(StandardDefinitions.CRC32_ISOHDLC);
            else if (HashType == HashType.CRC32_JAMCRC)
                _hasher = new Crc(StandardDefinitions.CRC32_JAMCRC);
            else if (HashType == HashType.CRC32_MEF)
                _hasher = new Crc(StandardDefinitions.CRC32_MEF);
            else if (HashType == HashType.CRC32_MPEG2)
                _hasher = new Crc(StandardDefinitions.CRC32_MPEG2);
            else if (HashType == HashType.CRC32_XFER)
                _hasher = new Crc(StandardDefinitions.CRC32_XFER);

            else if (HashType == HashType.CRC40_GSM)
                _hasher = new Crc(StandardDefinitions.CRC40_GSM);

            else if (HashType == HashType.CRC64)
                _hasher = new Crc(StandardDefinitions.CRC64_ECMA182);
            else if (HashType == HashType.CRC64_ECMA182)
                _hasher = new Crc(StandardDefinitions.CRC64_ECMA182);
            else if (HashType == HashType.CRC64_GOISO)
                _hasher = new Crc(StandardDefinitions.CRC64_GOISO);
            else if (HashType == HashType.CRC64_MS)
                _hasher = new Crc(StandardDefinitions.CRC64_MS);
            else if (HashType == HashType.CRC64_NVME)
                _hasher = new Crc(StandardDefinitions.CRC64_NVME);
            else if (HashType == HashType.CRC64_REDIS)
                _hasher = new Crc(StandardDefinitions.CRC64_REDIS);
            else if (HashType == HashType.CRC64_WE)
                _hasher = new Crc(StandardDefinitions.CRC64_WE);
            else if (HashType == HashType.CRC64_XZ)
                _hasher = new Crc(StandardDefinitions.CRC64_XZ);

            else if (HashType == HashType.Fletcher16)
                _hasher = new Fletcher16();
            else if (HashType == HashType.Fletcher32)
                _hasher = new Fletcher32();
            else if (HashType == HashType.Fletcher64)
                _hasher = new Fletcher64();

            else if (HashType == HashType.FNV0_32)
                _hasher = new FNV0_32();
            else if (HashType == HashType.FNV0_64)
                _hasher = new FNV0_64();
            else if (HashType == HashType.FNV1_32)
                _hasher = new FNV1_32();
            else if (HashType == HashType.FNV1_64)
                _hasher = new FNV1_64();
            else if (HashType == HashType.FNV1a_32)
                _hasher = new FNV1a_32();
            else if (HashType == HashType.FNV1a_64)
                _hasher = new FNV1a_64();

            else if (HashType == HashType.MekaCrc)
                _hasher = new MekaCrc();

            else if (HashType == HashType.MD2)
                _hasher = new MD2();
            else if (HashType == HashType.MD4)
                _hasher = new MD4();
            else if (HashType == HashType.MD5)
                _hasher = MD5.Create();

            else if (HashType == HashType.RIPEMD128)
                _hasher = new RipeMD128();
            else if (HashType == HashType.RIPEMD160)
                _hasher = new RipeMD160();
            else if (HashType == HashType.RIPEMD256)
                _hasher = new RipeMD256();
            else if (HashType == HashType.RIPEMD320)
                _hasher = new RipeMD320();

            else if (HashType == HashType.SHA1)
                _hasher = SHA1.Create();
            else if (HashType == HashType.SHA256)
                _hasher = SHA256.Create();
            else if (HashType == HashType.SHA384)
                _hasher = SHA384.Create();
            else if (HashType == HashType.SHA512)
                _hasher = SHA512.Create();
#if NET8_0_OR_GREATER
            else if (HashType == HashType.SHA3_256)
                _hasher = SHA3_256.IsSupported ? SHA3_256.Create() : null;
            else if (HashType == HashType.SHA3_384)
                _hasher = SHA3_384.IsSupported ? SHA3_384.Create() : null;
            else if (HashType == HashType.SHA3_512)
                _hasher = SHA3_512.IsSupported ? SHA3_512.Create() : null;
            else if (HashType == HashType.SHAKE128)
                _hasher = Shake128.IsSupported ? new Shake128() : null;
            else if (HashType == HashType.SHAKE256)
                _hasher = Shake256.IsSupported ? new Shake256() : null;
#endif

            else if (HashType == HashType.SpamSum)
                _hasher = new SpamSum.SpamSum();

            else if (HashType == HashType.Tiger128_3)
                _hasher = new Tiger128_3();
            else if (HashType == HashType.Tiger128_4)
                _hasher = new Tiger128_4();
            else if (HashType == HashType.Tiger160_3)
                _hasher = new Tiger160_3();
            else if (HashType == HashType.Tiger160_4)
                _hasher = new Tiger160_4();
            else if (HashType == HashType.Tiger192_3)
                _hasher = new Tiger192_3();
            else if (HashType == HashType.Tiger192_4)
                _hasher = new Tiger192_4();
            else if (HashType == HashType.Tiger2_128_3)
                _hasher = new Tiger2_128_3();
            else if (HashType == HashType.Tiger2_128_4)
                _hasher = new Tiger2_128_4();
            else if (HashType == HashType.Tiger2_160_3)
                _hasher = new Tiger2_160_3();
            else if (HashType == HashType.Tiger2_160_4)
                _hasher = new Tiger2_160_4();
            else if (HashType == HashType.Tiger2_192_3)
                _hasher = new Tiger2_192_3();
            else if (HashType == HashType.Tiger2_192_4)
                _hasher = new Tiger2_192_4();

            else if (HashType == HashType.XxHash32)
                _hasher = new XxHash32();
            else if (HashType == HashType.XxHash64)
                _hasher = new XxHash64();
#if NET462_OR_GREATER || NETCOREAPP
            else if (HashType == HashType.XxHash3)
                _hasher = new System.IO.Hashing.XxHash3();
            else if (HashType == HashType.XxHash128)
                _hasher = new System.IO.Hashing.XxHash128();
#endif
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
