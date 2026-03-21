using System;
using SabreTools.Hashing.Checksum;
using SabreTools.Hashing.CryptographicHash;
using SabreTools.Hashing.NonCryptographicHash;

namespace SabreTools.Hashing
{
    /// <summary>
    /// Represents a single hashing or checksumming type definition
    /// </summary>
    public sealed class HashType
    {
        #region Properties

        /// <summary>
        /// Name of hash or checksum type
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Long description of checksum type
        /// </summary>
        public readonly string Description;

        /// <summary>
        /// Hash of a 0-byte file as a byte array
        /// </summary>
        /// <remarks>Reference only, does not impact operation</remarks>
        public readonly byte[] ZeroBytes;

        /// <summary>
        /// Hash of a 0-byte file as a string
        /// </summary>
        /// <remarks>Reference only, does not impact operation</remarks>
        public readonly string ZeroString;

        /// <summary>
        /// Function used to generate the correct hashing class for the hash type
        /// </summary>
        public readonly Func<object?> CreateHasher;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        private HashType(string name, string description, byte[] zeroBytes, string zeroString, Func<object?> createHasher)
        {
            Name = name;
            Description = description;
            ZeroBytes = zeroBytes;
            ZeroString = zeroString;
            CreateHasher = createHasher;
        }

        #endregion

        #region Static Instances

        /// <summary>
        /// Mark Adler's 32-bit checksum
        /// </summary>
        public static readonly HashType Adler32 = new("Adler-32",
            "Mark Adler's 32-bit checksum",
            [0x00, 0x00, 0x00, 0x01],
            "00000001",
            static () => new Adler32());

#if NET7_0_OR_GREATER
        /// <summary>
        /// BLAKE3 512-bit digest
        /// </summary>
        public static readonly HashType BLAKE3 = new("BLAKE3",
            "BLAKE3 512-bit digest",
            [0xaf, 0x13, 0x49, 0xb9, 0xf5, 0xf9, 0xa1, 0xa6, 0xa0, 0x40, 0x4d, 0xea, 0x36, 0xdc, 0xc9, 0x49, 0x9b, 0xcb, 0x25, 0xc9, 0xad, 0xc1, 0x12, 0xb7, 0xcc, 0x9a, 0x93, 0xca, 0xe4, 0x1f, 0x32, 0x62],
            "af1349b9f5f9a1a6a0404dea36dcc9499bcb25c9adc112b7cc9a93cae41f3262",
            static () => new Blake3.Blake3HashAlgorithm());
#endif

        #region CRC

        #region CRC-1

        /// <summary>
        /// CRC 1-bit checksum (CRC-1/ZERO [Parity bit with 0 start])
        /// </summary>
        public static readonly HashType CRC1_ZERO = new("CRC-1/ZERO",
            "CRC 1-bit checksum (CRC-1/ZERO [Parity bit with 0 start])",
            [0x00],
            "0",
            static () => new Crc(StandardDefinitions.CRC1_ZERO));

        /// <summary>
        /// CRC 1-bit checksum (CRC-1/ONE [Parity bit with 1 start])
        /// </summary>
        public static readonly HashType CRC1_ONE = new("CRC-1/ONE",
            "CRC 1-bit checksum (CRC-1/ONE [Parity bit with 1 start])",
            [0x01],
            "1",
            static () => new Crc(StandardDefinitions.CRC1_ONE));

        #endregion

        #region CRC-3

        /// <summary>
        /// CRC 3-bit checksum (CRC-3/GSM)
        /// </summary>
        public static readonly HashType CRC3_GSM = new("CRC-3/GSM",
            "CRC 3-bit checksum (CRC-3/GSM)",
            [0x07],
            "7",
            static () => new Crc(StandardDefinitions.CRC3_GSM));

        /// <summary>
        /// CRC 3-bit checksum (CRC-3/ROHC)
        /// </summary>
        public static readonly HashType CRC3_ROHC = new("CRC-3/ROHC",
            "CRC 3-bit checksum (CRC-3/ROHC)",
            [0x07],
            "7",
            static () => new Crc(StandardDefinitions.CRC3_ROHC));

        #endregion

        #region CRC-4

        /// <summary>
        /// CRC 4-bit checksum (CRC-4/G-704 [CRC-4/ITU])
        /// </summary>
        public static readonly HashType CRC4_G704 = new("CRC-4/G-704",
            "CRC 4-bit checksum (CRC-4/G-704 [CRC-4/ITU])",
            [0x00],
            "0",
            static () => new Crc(StandardDefinitions.CRC4_G704));

        /// <summary>
        /// CRC 4-bit checksum (CRC-4/INTERLAKEN)
        /// </summary>
        public static readonly HashType CRC4_INTERLAKEN = new("CRC-4/INTERLAKEN",
            "CRC 4-bit checksum (CRC-4/INTERLAKEN)",
            [0x00],
            "0",
            static () => new Crc(StandardDefinitions.CRC4_INTERLAKEN));

        #endregion

        #region CRC-5

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/EPC-C1G2 [CRC-5/EPC])
        /// </summary>
        public static readonly HashType CRC5_EPCC1G2 = new("CRC-5/EPC-C1G2",
            "CRC 5-bit checksum (CRC-5/EPC-C1G2 [CRC-5/EPC])",
            [0x09],
            "09",
            static () => new Crc(StandardDefinitions.CRC5_EPCC1G2));

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/G-704 [CRC-5/ITU])
        /// </summary>
        public static readonly HashType CRC5_G704 = new("CRC-5/G-704",
            "CRC 5-bit checksum (CRC-5/G-704 [CRC-5/ITU])",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC5_G704));

        /// <summary>
        /// CRC 5-bit checksum (CRC-5/USB)
        /// </summary>
        public static readonly HashType CRC5_USB = new("CRC-5/USB",
            "CRC 5-bit checksum (CRC-5/USB)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC5_USB));

        #endregion

        #region CRC-6

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/CDMA2000-A)
        /// </summary>
        public static readonly HashType CRC6_CDMA2000A = new("CRC-6/CDMA2000-A",
            "CRC 6-bit checksum (CRC-6/CDMA2000-A)",
            [0x3f],
            "3f",
            static () => new Crc(StandardDefinitions.CRC6_CDMA2000A));

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/CDMA2000-B)
        /// </summary>
        public static readonly HashType CRC6_CDMA2000B = new("CRC-6/CDMA2000-B",
            "CRC 6-bit checksum (CRC-6/CDMA2000-B)",
            [0x3f],
            "3f",
            static () => new Crc(StandardDefinitions.CRC6_CDMA2000B));

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/DARC)
        /// </summary>
        public static readonly HashType CRC6_DARC = new("CRC-6/DARC",
            "CRC 6-bit checksum (CRC-6/DARC)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC6_DARC));

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/G-704 [CRC-6/ITU])
        /// </summary>
        public static readonly HashType CRC6_G704 = new("CRC-6/G-704",
            "CRC 6-bit checksum (CRC-6/G-704 [CRC-6/ITU])",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC6_G704));

        /// <summary>
        /// CRC 6-bit checksum (CRC-6/GSM)
        /// </summary>
        public static readonly HashType CRC6_GSM = new("CRC-6/GSM",
            "CRC 6-bit checksum (CRC-6/GSM)",
            [0x3f],
            "3f",
            static () => new Crc(StandardDefinitions.CRC6_GSM));

        #endregion

        #region CRC-7

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/MMC [CRC-7])
        /// </summary>
        public static readonly HashType CRC7_MMC = new("CRC-7/MMC",
            "CRC 7-bit checksum (CRC-7/MMC [CRC-7])",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC7_MMC));

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/ROHC)
        /// </summary>
        public static readonly HashType CRC7_ROHC = new("CRC-7/ROHC",
            "CRC 7-bit checksum (CRC-7/ROHC)",
            [0x7f],
            "7f",
            static () => new Crc(StandardDefinitions.CRC7_ROHC));

        /// <summary>
        /// CRC 7-bit checksum (CRC-7/UMTS)
        /// </summary>
        public static readonly HashType CRC7_UMTS = new("CRC-7/UMTS",
            "CRC 7-bit checksum (CRC-7/UMTS)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC7_UMTS));

        #endregion

        #region CRC-8

        /// <summary>
        /// CRC 8-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC8_SMBUS"/>
        public static readonly HashType CRC8 = new("CRC8",
            "CRC-8",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_SMBUS));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/AUTOSAR)
        /// </summary>
        public static readonly HashType CRC8_AUTOSAR = new("CRC-8/AUTOSAR",
            "CRC 8-bit checksum (CRC-8/AUTOSAR)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_AUTOSAR));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/BLUETOOTH)
        /// </summary>
        public static readonly HashType CRC8_BLUETOOTH = new("CRC-8/BLUETOOTH",
            "CRC 8-bit checksum (CRC-8/BLUETOOTH)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_BLUETOOTH));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/CDMA2000)
        /// </summary>
        public static readonly HashType CRC8_CDMA2000 = new("CRC-8/CDMA2000",
            "CRC 8-bit checksum (CRC-8/CDMA2000)",
            [0xff],
            "ff",
            static () => new Crc(StandardDefinitions.CRC8_CDMA2000));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/DARC)
        /// </summary>
        public static readonly HashType CRC8_DARC = new("CRC-8/DARC",
            "CRC 8-bit checksum (CRC-8/DARC)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_DARC));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/DVB-S2)
        /// </summary>
        public static readonly HashType CRC8_DVBS2 = new("CRC-8/DVB-S2",
            "CRC 8-bit checksum (CRC-8/DVB-S2)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_DVBS2));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/GSM-A)
        /// </summary>
        public static readonly HashType CRC8_GSMA = new("CRC-8/GSM-A",
            "CRC 8-bit checksum (CRC-8/GSM-A)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_GSMA));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/GSM-B)
        /// </summary>
        public static readonly HashType CRC8_GSMB = new("CRC-8/GSM-B",
            "CRC 8-bit checksum (CRC-8/GSM-B)",
            [0xff],
            "ff",
            static () => new Crc(StandardDefinitions.CRC8_GSMB));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/HITAG)
        /// </summary>
        public static readonly HashType CRC8_HITAG = new("CRC-8/HITAG",
            "CRC 8-bit checksum (CRC-8/HITAG)",
            [0xff],
            "ff",
            static () => new Crc(StandardDefinitions.CRC8_HITAG));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/I-432-1 [CRC-8/ITU])
        /// </summary>
        public static readonly HashType CRC8_I4321 = new("CRC-8/I-432-1",
            "CRC 8-bit checksum (CRC-8/I-432-1 [CRC-8/ITU])",
            [0x55],
            "55",
            static () => new Crc(StandardDefinitions.CRC8_I4321));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/I-CODE)
        /// </summary>
        public static readonly HashType CRC8_ICODE = new("CRC-8/I-CODE",
            "CRC 8-bit checksum (CRC-8/I-CODE)",
            [0xfd],
            "fd",
            static () => new Crc(StandardDefinitions.CRC8_ICODE));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/LTE)
        /// </summary>
        public static readonly HashType CRC8_LTE = new("CRC-8/LTE",
            "CRC 8-bit checksum (CRC-8/LTE)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_LTE));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/MAXIM-DOW [CRC-8/MAXIM, DOW-CRC])
        /// </summary>
        public static readonly HashType CRC8_MAXIMDOW = new("CRC-8/MAXIM-DOW",
            "CRC 8-bit checksum (CRC-8/MAXIM-DOW [CRC-8/MAXIM, DOW-CRC])",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_MAXIMDOW));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/MIFARE-MAD)
        /// </summary>
        public static readonly HashType CRC8_MIFAREMAD = new("CRC-8/MIFARE-MAD",
            "CRC 8-bit checksum (CRC-8/MIFARE-MAD)",
            [0xc7],
            "c7",
            static () => new Crc(StandardDefinitions.CRC8_MIFAREMAD));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/NRSC-5)
        /// </summary>
        public static readonly HashType CRC8_NRSC5 = new("CRC-8/NRSC-5",
            "CRC 8-bit checksum (CRC-8/NRSC-5)",
            [0xff],
            "ff",
            static () => new Crc(StandardDefinitions.CRC8_NRSC5));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/OPENSAFETY)
        /// </summary>
        public static readonly HashType CRC8_OPENSAFETY = new("CRC-8/OPENSAFETY",
            "CRC 8-bit checksum (CRC-8/OPENSAFETY)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_OPENSAFETY));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/ROHC)
        /// </summary>
        public static readonly HashType CRC8_ROHC = new("CRC-8/ROHC",
            "CRC 8-bit checksum (CRC-8/ROHC)",
            [0xff],
            "ff",
            static () => new Crc(StandardDefinitions.CRC8_ROHC));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/SAE-J1850)
        /// </summary>
        public static readonly HashType CRC8_SAEJ1850 = new("CRC-8/SAE-J1850",
            "CRC 8-bit checksum (CRC-8/SAE-J1850)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_SAEJ1850));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/SMBUS [CRC-8])
        /// </summary>
        public static readonly HashType CRC8_SMBUS = new("CRC-8/SMBUS",
            "CRC 8-bit checksum (CRC-8/SMBUS [CRC-8])",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_SMBUS));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/TECH-3250 [CRC-8/AES, CRC-8/EBU])
        /// </summary>
        public static readonly HashType CRC8_TECH3250 = new("CRC-8/TECH-3250",
            "CRC 8-bit checksum (CRC-8/TECH-3250 [CRC-8/AES, CRC-8/EBU])",
            [0xff],
            "ff",
            static () => new Crc(StandardDefinitions.CRC8_TECH3250));

        /// <summary>
        /// CRC 8-bit checksum (CRC-8/WCDMA)
        /// </summary>
        public static readonly HashType CRC8_WCDMA = new("CRC-8/WCDMA",
            "CRC 8-bit checksum (CRC-8/WCDMA)",
            [0x00],
            "00",
            static () => new Crc(StandardDefinitions.CRC8_WCDMA));

        #endregion

        #region CRC-10

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/ATM [CRC-10, CRC-10/I-610])
        /// </summary>
        public static readonly HashType CRC10_ATM = new("CRC-10/ATM",
            "CRC 10-bit checksum (CRC-10/ATM [CRC-10, CRC-10/I-610])",
            [0x00, 0x00],
            "000",
            static () => new Crc(StandardDefinitions.CRC10_ATM));

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/CDMA2000)
        /// </summary>
        public static readonly HashType CRC10_CDMA2000 = new("CRC-10/CDMA2000",
            "CRC 10-bit checksum (CRC-10/CDMA2000)",
            [0x03, 0xff],
            "3ff",
            static () => new Crc(StandardDefinitions.CRC10_CDMA2000));

        /// <summary>
        /// CRC 10-bit checksum (CRC-10/GSM)
        /// </summary>
        public static readonly HashType CRC10_GSM = new("CRC-10/GSM",
            "CRC 10-bit checksum (CRC-10/GSM)",
            [0x03, 0xff],
            "3ff",
            static () => new Crc(StandardDefinitions.CRC10_GSM));

        #endregion

        #region CRC-11

        /// <summary>
        /// CRC 11-bit checksum (CRC-11/FLEXRAY [CRC-11])
        /// </summary>
        public static readonly HashType CRC11_FLEXRAY = new("CRC-11/FLEXRAY",
            "CRC 11-bit checksum (CRC-11/FLEXRAY [CRC-11])",
            [0x00, 0x1a],
            "01a",
            static () => new Crc(StandardDefinitions.CRC11_FLEXRAY));

        /// <summary>
        /// CRC 11-bit checksum (CRC-11/UMTS)
        /// </summary>
        public static readonly HashType CRC11_UMTS = new("CRC-11/UMTS",
            "CRC 11-bit checksum (CRC-11/UMTS)",
            [0x00, 0x00],
            "000",
            static () => new Crc(StandardDefinitions.CRC11_UMTS));

        #endregion

        #region CRC-12

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/CDMA2000)
        /// </summary>
        public static readonly HashType CRC12_CDMA2000 = new("CRC-12/CDMA2000",
            "CRC 12-bit checksum (CRC-12/CDMA2000)",
            [0x0f, 0xff],
            "fff",
            static () => new Crc(StandardDefinitions.CRC12_CDMA2000));

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/DECT [X-CRC-12])
        /// </summary>
        public static readonly HashType CRC12_DECT = new("CRC-12/DECT",
            "CRC 12-bit checksum (CRC-12/DECT [X-CRC-12])",
            [0x00, 0x00],
            "000",
            static () => new Crc(StandardDefinitions.CRC12_DECT));

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/GSM)
        /// </summary>
        public static readonly HashType CRC12_GSM = new("CRC-12/GSM",
            "CRC 12-bit checksum (CRC-12/GSM)",
            [0x0f, 0xff],
            "fff",
            static () => new Crc(StandardDefinitions.CRC12_GSM));

        /// <summary>
        /// CRC 12-bit checksum (CRC-12/UMTS [CRC-12/3GPP])
        /// </summary>
        public static readonly HashType CRC12_UMTS = new("CRC-12/UMTS",
            "CRC 12-bit checksum (CRC-12/UMTS [CRC-12/3GPP])",
            [0x00, 0x00],
            "000",
            static () => new Crc(StandardDefinitions.CRC12_UMTS));

        #endregion

        #region CRC-13

        /// <summary>
        /// CRC 13-bit checksum (CRC-13/BBC)
        /// </summary>
        public static readonly HashType CRC13_BBC = new("CRC-13/BBC",
            "CRC 13-bit checksum (CRC-13/BBC)",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC13_BBC));

        #endregion

        #region CRC-14

        /// <summary>
        /// CRC 14-bit checksum (CRC-14/DARC)
        /// </summary>
        public static readonly HashType CRC14_DARC = new("CRC-14/DARC",
            "CRC 14-bit checksum (CRC-14/DARC)",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC14_DARC));

        /// <summary>
        /// CRC 14-bit checksum (CRC-14/GSM)
        /// </summary>
        public static readonly HashType CRC14_GSM = new("CRC-14/GSM",
            "CRC 14-bit checksum (CRC-14/GSM)",
            [0x3f, 0xff],
            "3fff",
            static () => new Crc(StandardDefinitions.CRC14_GSM));

        #endregion

        #region CRC-15

        /// <summary>
        /// CRC 15-bit checksum (CRC-15/CAN [CRC-15])
        /// </summary>
        public static readonly HashType CRC15_CAN = new("CRC-15/CAN",
            "CRC 15-bit checksum (CRC-15/CAN [CRC-15])",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC15_CAN));

        /// <summary>
        /// CRC 15-bit checksum (CRC-15/MPT1327)
        /// </summary>
        public static readonly HashType CRC15_MPT1327 = new("CRC-15/MPT1327",
            "CRC 15-bit checksum (CRC-15/MPT1327)",
            [0x00, 0x01],
            "0001",
            static () => new Crc(StandardDefinitions.CRC15_MPT1327));

        #endregion

        #region CRC-16

        /// <summary>
        /// CRC 16-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC16_ARC"/>
        public static readonly HashType CRC16 = new("CRC16",
            "CRC-16",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_ARC));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/ARC [ARC, CRC-16, CRC-16/LHA, CRC-IBM])
        /// </summary>
        public static readonly HashType CRC16_ARC = new("CRC-16/ARC",
            "CRC 16-bit checksum (CRC-16/ARC [ARC, CRC-16, CRC-16/LHA, CRC-IBM])",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_ARC));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/CDMA2000)
        /// </summary>
        public static readonly HashType CRC16_CDMA2000 = new("CRC-16/CDMA2000",
            "CRC 16-bit checksum (CRC-16/CDMA2000)",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_CDMA2000));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/CMS)
        /// </summary>
        public static readonly HashType CRC16_CMS = new("CRC-16/CMS",
            "CRC 16-bit checksum (CRC-16/CMS)",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_CMS));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DDS-110)
        /// </summary>
        public static readonly HashType CRC16_DDS110 = new("CRC-16/DDS-110",
            "CRC 16-bit checksum (CRC-16/DDS-110)",
            [0x80, 0x0d],
            "800d",
            static () => new Crc(StandardDefinitions.CRC16_DDS110));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DECT-R [R-CRC-16])
        /// </summary>
        public static readonly HashType CRC16_DECTR = new("CRC-16/DECT-R",
            "CRC 16-bit checksum (CRC-16/DECT-R [R-CRC-16])",
            [0x00, 0x01],
            "0001",
            static () => new Crc(StandardDefinitions.CRC16_DECTR));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DECT-X [X-CRC-16])
        /// </summary>
        public static readonly HashType CRC16_DECTX = new("CRC-16/DECT-X",
            "CRC 16-bit checksum (CRC-16/DECT-X [X-CRC-16])",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_DECTX));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/DNP)
        /// </summary>
        public static readonly HashType CRC16_DNP = new("CRC-16/DNP",
            "CRC 16-bit checksum (CRC-16/DNP)",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_DNP));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/EN-13757)
        /// </summary>
        public static readonly HashType CRC16_EN13757 = new("CRC-16/EN-13757",
            "CRC 16-bit checksum (CRC-16/EN-13757)",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_EN13757));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/GENIBUS [CRC-16/DARC, CRC-16/EPC, CRC-16/EPC-C1G2, CRC-16/I-CODE])
        /// </summary>
        public static readonly HashType CRC16_GENIBUS = new("CRC-16/GENIBUS",
            "CRC 16-bit checksum (CRC-16/GENIBUS [CRC-16/DARC, CRC-16/EPC, CRC-16/EPC-C1G2, CRC-16/I-CODE])",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_GENIBUS));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/GSM)
        /// </summary>
        public static readonly HashType CRC16_GSM = new("CRC-16/GSM",
            "CRC 16-bit checksum (CRC-16/GSM)",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_GSM));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/IBM-3740 [CRC-16/AUTOSAR, CRC-16/CCITT-FALSE])
        /// </summary>
        public static readonly HashType CRC16_IBM3740 = new("CRC-16/IBM-3740",
            "CRC 16-bit checksum (CRC-16/IBM-3740 [CRC-16/AUTOSAR, CRC-16/CCITT-FALSE])",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_IBM3740));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/IBM-SDLC [CRC-16/ISO-HDLC, CRC-16/ISO-IEC-14443-3-B, CRC-16/X-25, CRC-B, X-25])
        /// </summary>
        public static readonly HashType CRC16_IBMSDLC = new("CRC-16/IBM-SDLC",
            "CRC 16-bit checksum (CRC-16/IBM-SDLC [CRC-16/ISO-HDLC, CRC-16/ISO-IEC-14443-3-B, CRC-16/X-25, CRC-B, X-25])",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_IBMSDLC));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/ISO-IEC-14443-3-A [CRC-A])
        /// </summary>
        public static readonly HashType CRC16_ISOIEC144433A = new("CRC-16/ISO-IEC-14443-3-A",
            "CRC 16-bit checksum (CRC-16/ISO-IEC-14443-3-A [CRC-A])",
            [0x63, 0x63],
            "6363",
            static () => new Crc(StandardDefinitions.CRC16_ISOIEC144433A));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/KERMIT [CRC-16/BLUETOOTH, CRC-16/CCITT, CRC-16/CCITT-TRUE, CRC-16/V-41-LSB, CRC-CCITT, KERMIT])
        /// </summary>
        public static readonly HashType CRC16_KERMIT = new("CRC-16/KERMIT",
            "CRC 16-bit checksum (CRC-16/KERMIT [CRC-16/BLUETOOTH, CRC-16/CCITT, CRC-16/CCITT-TRUE, CRC-16/V-41-LSB, CRC-CCITT, KERMIT])",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_KERMIT));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/LJ1200)
        /// </summary>
        public static readonly HashType CRC16_LJ1200 = new("CRC-16/LJ1200",
            "CRC 16-bit checksum (CRC-16/LJ1200)",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_LJ1200));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/M17)
        /// </summary>
        public static readonly HashType CRC16_M17 = new("CRC-16/M17",
            "CRC 16-bit checksum (CRC-16/M17)",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_M17));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MAXIM-DOW [CRC-16/MAXIM])
        /// </summary>
        public static readonly HashType CRC16_MAXIMDOW = new("CRC-16/MAXIM-DOW",
            "CRC 16-bit checksum (CRC-16/MAXIM-DOW [CRC-16/MAXIM])",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_MAXIMDOW));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MCRF4XX)
        /// </summary>
        public static readonly HashType CRC16_MCRF4XX = new("CRC-16/MCRF4XX",
            "CRC 16-bit checksum (CRC-16/MCRF4XX)",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_MCRF4XX));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/MODBUS [MODBUS])
        /// </summary>
        public static readonly HashType CRC16_MODBUS = new("CRC-16/MODBUS",
            "CRC 16-bit checksum (CRC-16/MODBUS [MODBUS])",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_MODBUS));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/NRSC-5)
        /// </summary>
        public static readonly HashType CRC16_NRSC5 = new("CRC-16/NRSC-5",
            "CRC 16-bit checksum (CRC-16/NRSC-5)",
            [0xff, 0xff],
            "ffff",
            static () => new Crc(StandardDefinitions.CRC16_NRSC5));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/OPENSAFETY-A)
        /// </summary>
        public static readonly HashType CRC16_OPENSAFETYA = new("CRC-16/OPENSAFETY-A",
            "CRC 16-bit checksum (CRC-16/OPENSAFETY-A)",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_OPENSAFETYA));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/OPENSAFETY-B)
        /// </summary>
        public static readonly HashType CRC16_OPENSAFETYB = new("CRC-16/OPENSAFETY-B",
            "CRC 16-bit checksum (CRC-16/OPENSAFETY-B)",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_OPENSAFETYB));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/PROFIBUS [CRC-16/IEC-61158-2])
        /// </summary>
        public static readonly HashType CRC16_PROFIBUS = new("CRC-16/PROFIBUS",
            "CRC 16-bit checksum (CRC-16/PROFIBUS [CRC-16/IEC-61158-2])",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_PROFIBUS));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/RIELLO)
        /// </summary>
        public static readonly HashType CRC16_RIELLO = new("CRC-16/RIELLO",
            "CRC 16-bit checksum (CRC-16/RIELLO)",
            [0x55, 0x4d],
            "554d",
            static () => new Crc(StandardDefinitions.CRC16_RIELLO));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/SPI-FUJITSU [CRC-16/AUG-CCITT])
        /// </summary>
        public static readonly HashType CRC16_SPIFUJITSU = new("CRC-16/SPI-FUJITSU",
            "CRC 16-bit checksum (CRC-16/SPI-FUJITSU [CRC-16/AUG-CCITT])",
            [0x1d, 0x0f],
            "1d0f",
            static () => new Crc(StandardDefinitions.CRC16_SPIFUJITSU));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/T10-DIF)
        /// </summary>
        public static readonly HashType CRC16_T10DIF = new("CRC-16/T10-DIF",
            "CRC 16-bit checksum (CRC-16/T10-DIF)",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_T10DIF));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/TELEDISK)
        /// </summary>
        public static readonly HashType CRC16_TELEDISK = new("CRC-16/TELEDISK",
            "CRC 16-bit checksum (CRC-16/TELEDISK)",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_TELEDISK));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/TMS37157)
        /// </summary>
        public static readonly HashType CRC16_TMS37157 = new("CRC-16/TMS37157",
            "CRC 16-bit checksum (CRC-16/TMS37157)",
            [0x37, 0x91],
            "3791",
            static () => new Crc(StandardDefinitions.CRC16_TMS37157));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/UMTS [CRC-16/BUYPASS, CRC-16/VERIFONE])
        /// </summary>
        public static readonly HashType CRC16_UMTS = new("CRC-16/UMTS",
            "CRC 16-bit checksum (CRC-16/UMTS [CRC-16/BUYPASS, CRC-16/VERIFONE])",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_UMTS));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/USB)
        /// </summary>
        public static readonly HashType CRC16_USB = new("CRC-16/USB",
            "CRC 16-bit checksum (CRC-16/USB)",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_USB));

        /// <summary>
        /// CRC 16-bit checksum (CRC-16/XMODEM [CRC-16/ACORN, CRC-16/LTE, CRC-16/V-41-MSB, XMODEM, ZMODEM])
        /// </summary>
        public static readonly HashType CRC16_XMODEM = new("CRC-16/XMODEM",
            "CRC 16-bit checksum (CRC-16/XMODEM [CRC-16/ACORN, CRC-16/LTE, CRC-16/V-41-MSB, XMODEM, ZMODEM])",
            [0x00, 0x00],
            "0000",
            static () => new Crc(StandardDefinitions.CRC16_XMODEM));

        #endregion

        #region CRC-17

        /// <summary>
        /// CRC 17-bit checksum (CRC-17/CAN-FD)
        /// </summary>
        public static readonly HashType CRC17_CANFD = new("CRC-17/CAN-FD",
            "CRC 17-bit checksum (CRC-17/CAN-FD)",
            [0x00, 0x00, 0x00],
            "00000",
            static () => new Crc(StandardDefinitions.CRC17_CANFD));

        #endregion

        #region CRC-21

        /// <summary>
        /// CRC 21-bit checksum (CRC-21/CAN-FD)
        /// </summary>
        public static readonly HashType CRC21_CANFD = new("CRC-21/CAN-FD",
            "CRC 21-bit checksum (CRC-21/CAN-FD)",
            [0x00, 0x00, 0x00],
            "000000",
            static () => new Crc(StandardDefinitions.CRC21_CANFD));

        #endregion

        #region CRC-24

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/BLE)
        /// </summary>
        public static readonly HashType CRC24_BLE = new("CRC-24/BLE",
            "CRC 24-bit checksum (CRC-24/BLE)",
            [0xaa, 0xaa, 0xaa],
            "aaaaaa",
            static () => new Crc(StandardDefinitions.CRC24_BLE));

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/FLEXRAY-A)
        /// </summary>
        public static readonly HashType CRC24_FLEXRAYA = new("CRC-24/FLEXRAY-A",
            "CRC 24-bit checksum (CRC-24/FLEXRAY-A)",
            [0xfe, 0xdc, 0xba],
            "fedcba",
            static () => new Crc(StandardDefinitions.CRC24_FLEXRAYA));

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/FLEXRAY-B)
        /// </summary>
        public static readonly HashType CRC24_FLEXRAYB = new("CRC-24/FLEXRAY-B",
            "CRC 24-bit checksum (CRC-24/FLEXRAY-B)",
            [0xab, 0xcd, 0xef],
            "abcdef",
            static () => new Crc(StandardDefinitions.CRC24_FLEXRAYB));

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/INTERLAKEN)
        /// </summary>
        public static readonly HashType CRC24_INTERLAKEN = new("CRC-24/INTERLAKEN",
            "CRC 24-bit checksum (CRC-24/INTERLAKEN)",
            [0x00, 0x00, 0x00],
            "000000",
            static () => new Crc(StandardDefinitions.CRC24_INTERLAKEN));

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/LTE-A)
        /// </summary>
        public static readonly HashType CRC24_LTEA = new("CRC-24/LTE-A",
            "CRC 24-bit checksum (CRC-24/LTE-A)",
            [0x00, 0x00, 0x00],
            "000000",
            static () => new Crc(StandardDefinitions.CRC24_LTEA));

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/LTE-B)
        /// </summary>
        public static readonly HashType CRC24_LTEB = new("CRC-24/LTE-B",
            "CRC 24-bit checksum (CRC-24/LTE-B)",
            [0x00, 0x00, 0x00],
            "000000",
            static () => new Crc(StandardDefinitions.CRC24_LTEB));

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/OPENPGP)
        /// </summary>
        public static readonly HashType CRC24_OPENPGP = new("CRC-24/OPENPGP",
            "CRC 24-bit checksum (CRC-24/OPENPGP)",
            [0xb7, 0x04, 0xce],
            "b704ce",
            static () => new Crc(StandardDefinitions.CRC24_OPENPGP));

        /// <summary>
        /// CRC 24-bit checksum (CRC-24/OS-9)
        /// </summary>
        public static readonly HashType CRC24_OS9 = new("CRC-24/OS-9",
            "CRC 24-bit checksum (CRC-24/OS-9)",
            [0x00, 0x00, 0x00],
            "000000",
            static () => new Crc(StandardDefinitions.CRC24_OS9));

        #endregion

        #region CRC-30

        /// <summary>
        /// CRC 30-bit checksum (CRC-30/CDMA)
        /// </summary>
        public static readonly HashType CRC30_CDMA = new("CRC-30/CDMA",
            "CRC 30-bit checksum (CRC-30/CDMA)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC30_CDMA));

        #endregion

        #region CRC-31

        /// <summary>
        /// CRC 31-bit checksum (CRC-31/PHILIPS)
        /// </summary>
        public static readonly HashType CRC31_PHILIPS = new("CRC-31/PHILIPS",
            "CRC 31-bit checksum (CRC-31/PHILIPS)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC31_PHILIPS));

        #endregion

        #region CRC-32

        /// <summary>
        /// CRC 32-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC32_ISOHDLC"/>
        public static readonly HashType CRC32 = new("CRC32",
            "CRC-32",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC32_ISOHDLC));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/AIXM)
        /// </summary>
        public static readonly HashType CRC32_AIXM = new("CRC-32/AIXM",
            "CRC 32-bit checksum (CRC-32/AIXM)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC32_AIXM));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/AUTOSAR)
        /// </summary>
        public static readonly HashType CRC32_AUTOSAR = new("CRC-32/AUTOSAR",
            "CRC 32-bit checksum (CRC-32/AUTOSAR)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC32_AUTOSAR));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/BASE91-D)
        /// </summary>
        public static readonly HashType CRC32_BASE91D = new("CRC-32/BASE91-D",
            "CRC 32-bit checksum (CRC-32/BASE91-D)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC32_BASE91D));

        /// <summary>
        /// CRC 32-bit checksum (BZIP2)
        /// </summary>
        public static readonly HashType CRC32_BZIP2 = new("BZIP2",
            "CRC 32-bit checksum (BZIP2)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC32_BZIP2));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/CD-ROM-EDC)
        /// </summary>
        public static readonly HashType CRC32_CDROMEDC = new("CRC-32/CD-ROM-EDC",
            "CRC 32-bit checksum (CRC-32/CD-ROM-EDC)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC32_CDROMEDC));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/CKSUM)
        /// </summary>
        public static readonly HashType CRC32_CKSUM = new("CRC-32/CKSUM",
            "CRC 32-bit checksum (CRC-32/CKSUM)",
            [0xff, 0xff, 0xff, 0xff],
            "ffffffff",
            static () => new Crc(StandardDefinitions.CRC32_CKSUM));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/DVD-ROM-EDC)
        /// </summary>
        public static readonly HashType CRC32_DVDROMEDC = new("CRC-32/DVD-ROM-EDC",
            "CRC 32-bit checksum (CRC-32/DVD-ROM-EDC)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC32_DVDROMEDC));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/ISCSI)
        /// </summary>
        public static readonly HashType CRC32_ISCSI = new("CRC-32/ISCSI",
            "CRC 32-bit checksum (CRC-32/ISCSI)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC32_ISCSI));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/ISO-HDLC)
        /// </summary>
        public static readonly HashType CRC32_ISOHDLC = new("CRC-32/ISO-HDLC",
            "CRC 32-bit checksum (CRC-32/ISO-HDLC)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC32_ISOHDLC));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/JAMCRC)
        /// </summary>
        public static readonly HashType CRC32_JAMCRC = new("CRC-32/JAMCRC",
            "CRC 32-bit checksum (CRC-32/JAMCRC)",
            [0xff, 0xff, 0xff, 0xff],
            "ffffffff",
            static () => new Crc(StandardDefinitions.CRC32_JAMCRC));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/MEF)
        /// </summary>
        public static readonly HashType CRC32_MEF = new("CRC-32/MEF",
            "CRC 32-bit checksum (CRC-32/MEF)",
            [0xff, 0xff, 0xff, 0xff],
            "ffffffff",
            static () => new Crc(StandardDefinitions.CRC32_MEF));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/MPEG-2)
        /// </summary>
        public static readonly HashType CRC32_MPEG2 = new("CRC-32/MPEG-2",
            "CRC 32-bit checksum (CRC-32/MPEG-2)",
            [0xff, 0xff, 0xff, 0xff],
            "ffffffff",
            static () => new Crc(StandardDefinitions.CRC32_MPEG2));

        /// <summary>
        /// CRC 32-bit checksum (CRC-32/XFER)
        /// </summary>
        public static readonly HashType CRC32_XFER = new("CRC-32/XFER",
            "CRC 32-bit checksum (CRC-32/XFER)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Crc(StandardDefinitions.CRC32_XFER));

        #endregion

        #region CRC-40

        /// <summary>
        /// CRC 40-bit checksum (CRC-40/GSM)
        /// </summary>
        public static readonly HashType CRC40_GSM = new("CRC-40/GSM",
            "CRC 40-bit checksum (CRC-40/GSM)",
            [0xff, 0xff, 0xff, 0xff, 0xff],
            "ffffffffff",
            static () => new Crc(StandardDefinitions.CRC40_GSM));

        #endregion

        #region CRC-64

        /// <summary>
        /// CRC 64-bit checksum
        /// </summary>
        /// <remarks>Identical to <see cref="CRC64_ECMA182"/>
        public static readonly HashType CRC64 = new("CRC64",
            "CRC-64",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "0000000000000000",
            static () => new Crc(StandardDefinitions.CRC64_ECMA182));

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/ECMA-182, Microsoft implementation)
        /// </summary>
        public static readonly HashType CRC64_ECMA182 = new("CRC-64/ECMA-182",
            "CRC 64-bit checksum (CRC-64/ECMA-182, Microsoft implementation)",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "0000000000000000",
            static () => new Crc(StandardDefinitions.CRC64_ECMA182));

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/GO-ISO)
        /// </summary>
        public static readonly HashType CRC64_GOISO = new("CRC-64/GO-ISO",
            "CRC 64-bit checksum (CRC-64/GO-ISO)",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "0000000000000000",
            static () => new Crc(StandardDefinitions.CRC64_GOISO));

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/MS)
        /// </summary>
        public static readonly HashType CRC64_MS = new("CRC-64/MS",
            "CRC 64-bit checksum (CRC-64/MS)",
            [0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff],
            "ffffffffffffffff",
            static () => new Crc(StandardDefinitions.CRC64_MS));

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/NVME)
        /// </summary>
        public static readonly HashType CRC64_NVME = new("CRC-64/NVME",
            "CRC 64-bit checksum (CRC-64/NVME)",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "0000000000000000",
            static () => new Crc(StandardDefinitions.CRC64_NVME));

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/REDIS)
        /// </summary>
        public static readonly HashType CRC64_REDIS = new("CRC-64/REDIS",
            "CRC 64-bit checksum (CRC-64/REDIS)",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "0000000000000000",
            static () => new Crc(StandardDefinitions.CRC64_REDIS));

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/WE)
        /// </summary>
        public static readonly HashType CRC64_WE = new("CRC-64/WE",
            "CRC 64-bit checksum (CRC-64/WE)",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "0000000000000000",
            static () => new Crc(StandardDefinitions.CRC64_WE));

        /// <summary>
        /// CRC 64-bit checksum (CRC-64/XZ)
        /// </summary>
        public static readonly HashType CRC64_XZ = new("CRC-64/XZ",
            "CRC 64-bit checksum (CRC-64/XZ)",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "0000000000000000",
            static () => new Crc(StandardDefinitions.CRC64_XZ));

        #endregion

        #region CRC-82

#if NET7_0_OR_GREATER
        /// <summary>
        /// CRC 82-bit checksum (CRC-82/DARC)
        /// </summary>
        public static readonly HashType CRC82_DARC = new("CRC-82/DARC",
            "CRC 82-bit checksum (CRC-82/DARC)",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "000000000000000000000",
            static () => new Crc(StandardDefinitions.CRC82_DARC));
#endif

        #endregion

        #endregion

        #region Fletcher

        /// <summary>
        /// John G. Fletcher's 16-bit checksum
        /// </summary>
        public static readonly HashType Fletcher16 = new("Fletcher-16",
            "John G. Fletcher's 16-bit checksum",
            [0x00, 0x00],
            "0000",
            static () => new Fletcher16());

        /// <summary>
        /// John G. Fletcher's 32-bit checksum
        /// </summary>
        public static readonly HashType Fletcher32 = new("Fletcher-32",
            "John G. Fletcher's 32-bit checksum",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new Fletcher32());

        /// <summary>
        /// John G. Fletcher's 64-bit checksum
        /// </summary>
        public static readonly HashType Fletcher64 = new("Fletcher-64",
            "John G. Fletcher's 64-bit checksum",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "0000000000000000",
            static () => new Fletcher64());

        #endregion

        #region FNV

        /// <summary>
        /// FNV hash (Variant 0, 32-bit)
        /// </summary>
        public static readonly HashType FNV0_32 = new("FNV0-32",
            "FNV hash (Variant 0, 32-bit)",
            [0x00, 0x00, 0x00, 0x00],
            "00000000",
            static () => new FNV0_32());

        /// <summary>
        /// FNV hash (Variant 0, 64-bit)
        /// </summary>
        public static readonly HashType FNV0_64 = new("FNV0-64",
            "FNV hash (Variant 0, 64-bit)",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "0000000000000000",
            static () => new FNV0_64());

        /// <summary>
        /// FNV hash (Variant 1, 32-bit)
        /// </summary>
        public static readonly HashType FNV1_32 = new("FNV1-32",
            "FNV hash (Variant 1, 32-bit)",
            [0x81, 0x1c, 0x9d, 0xc5],
            "811c9dc5",
            static () => new FNV1_32());

        /// <summary>
        /// FNV hash (Variant 1, 64-bit)
        /// </summary>
        public static readonly HashType FNV1_64 = new("FNV1-64",
            "FNV hash (Variant 1, 64-bit)",
            [0xcb, 0xf2, 0x9c, 0xe4, 0x84, 0x22, 0x23, 0x25],
            "cbf29ce484222325",
            static () => new FNV1_64());

        /// <summary>
        /// FNV hash (Variant 1a, 32-bit)
        /// </summary>
        public static readonly HashType FNV1a_32 = new("FNV1a-32",
            "FNV hash (Variant 1a, 32-bit)",
            [0x81, 0x1c, 0x9d, 0xc5],
            "811c9dc5",
            static () => new FNV1a_32());

        /// <summary>
        /// FNV hash (Variant 1a, 64-bit)
        /// </summary>
        public static readonly HashType FNV1a_64 = new("FNV1a-64",
            "FNV hash (Variant 1a, 64-bit)",
            [0xcb, 0xf2, 0x9c, 0xe4, 0x84, 0x22, 0x23, 0x25],
            "cbf29ce484222325",
            static () => new FNV1a_64());

        #endregion

        /// <summary>
        /// Custom checksum used by MEKA
        /// </summary>
        public static readonly HashType MekaCrc = new("MEKA-CRC",
            "Custom checksum used by MEKA",
            [0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "0000000000000000",
            static () => new MekaCrc());

        #region Message Digest

        /// <summary>
        /// MD2 message-digest algorithm
        /// </summary>
        public static readonly HashType MD2 = new("MD2",
            "MD2 message-digest algorithm",
            [0x83, 0x50, 0xe5, 0xa3, 0xe2, 0x4c, 0x15, 0x3d, 0xf2, 0x27, 0x5c, 0x9f, 0x80, 0x69, 0x27, 0x73],
            "8350e5a3e24c153df2275c9f80692773",
            static () => new MD2());

        /// <summary>
        /// MD4 message-digest algorithm
        /// </summary>
        public static readonly HashType MD4 = new("MD4",
            "MD4 message-digest algorithm",
            [0x31, 0xd6, 0xcf, 0xe0, 0xd1, 0x6a, 0xe9, 0x31, 0xb7, 0x3c, 0x59, 0xd7, 0xe0, 0xc0, 0x89, 0xc0],
            "31d6cfe0d16ae931b73c59d7e0c089c0",
            static () => new MD4());

        /// <summary>
        /// MD5 message-digest algorithm
        /// </summary>
        public static readonly HashType MD5 = new("MD5",
            "MD5 message-digest algorithm",
            [0xd4, 0x1d, 0x8c, 0xd9, 0x8f, 0x00, 0xb2, 0x04, 0xe9, 0x80, 0x09, 0x98, 0xec, 0xf8, 0x42, 0x7e],
            "d41d8cd98f00b204e9800998ecf8427e",
            static () => System.Security.Cryptography.MD5.Create());

        #endregion

        #region RIPE Message Digest

        /// <summary>
        /// RIPE 128-bit message digest
        /// </summary>
        public static readonly HashType RIPEMD128 = new("RIPEMD-128",
            "RIPE 128-bit message digest",
            [0xcd, 0xf2, 0x62, 0x13, 0xa1, 0x50, 0xdc, 0x3e, 0xcb, 0x61, 0x0f, 0x18, 0xf6, 0xb3, 0x8b, 0x46],
            "cdf26213a150dc3ecb610f18f6b38b46",
            static () => new RipeMD128());

        /// <summary>
        /// RIPE 160-bit message digest
        /// </summary>
        public static readonly HashType RIPEMD160 = new("RIPEMD-160",
            "RIPE 160-bit message digest",
            [0x9c, 0x11, 0x85, 0xa5, 0xc5, 0xe9, 0xfc, 0x54, 0x61, 0x28, 0x08, 0x97, 0x7e, 0xe8, 0xf5, 0x48, 0xb2, 0x25, 0x8d, 0x31],
            "9c1185a5c5e9fc54612808977ee8f548b2258d31",
            static () => new RipeMD160());

        /// <summary>
        /// RIPE 256-bit message digest
        /// </summary>
        public static readonly HashType RIPEMD256 = new("RIPEMD-256",
            "RIPE 256-bit message digest",
            [0x02, 0xba, 0x4c, 0x4e, 0x5f, 0x8e, 0xcd, 0x18, 0x77, 0xfc, 0x52, 0xd6, 0x4d, 0x30, 0xe3, 0x7a, 0x2d, 0x97, 0x74, 0xfb, 0x1e, 0x5d, 0x02, 0x63, 0x80, 0xae, 0x01, 0x68, 0xe3, 0xc5, 0x52, 0x2d],
            "02ba4c4e5f8ecd1877fc52d64d30e37a2d9774fb1e5d026380ae0168e3c5522d",
            static () => new RipeMD256());

        /// <summary>
        /// RIPE 320-bit message digest
        /// </summary>
        public static readonly HashType RIPEMD320 = new("RIPEMD-320",
            "RIPE 320-bit message digest",
            [0x22, 0xd6, 0x5d, 0x56, 0x61, 0x53, 0x6c, 0xdc, 0x75, 0xc1, 0xfd, 0xf5, 0xc6, 0xde, 0x7b, 0x41, 0xb9, 0xf2, 0x73, 0x25, 0xeb, 0xc6, 0x1e, 0x85, 0x57, 0x17, 0x7d, 0x70, 0x5a, 0x0e, 0xc8, 0x80, 0x15, 0x1c, 0x3a, 0x32, 0xa0, 0x08, 0x99, 0xb8],
            "22d65d5661536cdc75c1fdf5c6de7b41b9f27325ebc61e8557177d705a0ec880151c3a32a00899b8",
            static () => new RipeMD320());

        #endregion

        #region SHA

        /// <summary>
        /// SHA-1 hash
        /// </summary>
        public static readonly HashType SHA1 = new("SHA-1",
            "SHA-1 hash",
            [0xda, 0x39, 0xa3, 0xee, 0x5e, 0x6b, 0x4b, 0x0d, 0x32, 0x55, 0xbf, 0xef, 0x95, 0x60, 0x18, 0x90, 0xaf, 0xd8, 0x07, 0x09],
            "da39a3ee5e6b4b0d3255bfef95601890afd80709",
            static () => System.Security.Cryptography.SHA1.Create());

        /// <summary>
        /// SHA-256 hash
        /// </summary>
        public static readonly HashType SHA256 = new("SHA-256",
            "SHA-256 hash",
            [0xe3, 0xb0, 0xc4, 0x42, 0x98, 0xfc, 0x1c, 0x14, 0x9a, 0xfb, 0xf4, 0xc8, 0x99, 0x6f, 0xb9, 0x24, 0x27, 0xae, 0x41, 0xe4, 0x64, 0x9b, 0x93, 0x4c, 0xa4, 0x95, 0x99, 0x1b, 0x78, 0x52, 0xb8, 0x55],
            "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
            static () => System.Security.Cryptography.SHA256.Create());

        /// <summary>
        /// SHA-384 hash
        /// </summary>
        public static readonly HashType SHA384 = new("SHA-384",
            "SHA-384 hash",
            [0x38, 0xb0, 0x60, 0xa7, 0x51, 0xac, 0x96, 0x38, 0x4c, 0xd9, 0x32, 0x7e, 0xb1, 0xb1, 0xe3, 0x6a, 0x21, 0xfd, 0xb7, 0x11, 0x14, 0xbe, 0x07, 0x43, 0x4c, 0x0c, 0xc7, 0xbf, 0x63, 0xf6, 0xe1, 0xda, 0x27, 0x4e, 0xde, 0xbf, 0xe7, 0x6f, 0x65, 0xfb, 0xd5, 0x1a, 0xd2, 0xf1, 0x48, 0x98, 0xb9, 0x5b],
            "38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b",
            static () => System.Security.Cryptography.SHA384.Create());

        /// <summary>
        /// SHA-512 hash
        /// </summary>
        public static readonly HashType SHA512 = new("SHA-512",
            "SHA-512 hash",
            [0xcf, 0x83, 0xe1, 0x35, 0x7e, 0xef, 0xb8, 0xbd, 0xf1, 0x54, 0x28, 0x50, 0xd6, 0x6d, 0x80, 0x07, 0xd6, 0x20, 0xe4, 0x05, 0x0b, 0x57, 0x15, 0xdc, 0x83, 0xf4, 0xa9, 0x21, 0xd3, 0x6c, 0xe9, 0xce, 0x47, 0xd0, 0xd1, 0x3c, 0x5d, 0x85, 0xf2, 0xb0, 0xff, 0x83, 0x18, 0xd2, 0x87, 0x7e, 0xec, 0x2f, 0x63, 0xb9, 0x31, 0xbd, 0x47, 0x41, 0x7a, 0x81, 0xa5, 0x38, 0x32, 0x7a, 0xf9, 0x27, 0xda, 0x3e],
            "cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e",
            static () => System.Security.Cryptography.SHA512.Create());

#if NET8_0_OR_GREATER
        /// <summary>
        /// SHA3-256 hash
        /// </summary>
        public static readonly HashType SHA3_256 = new("SHA3-256",
            "SHA3-256 hash",
            [0xa7, 0xff, 0xc6, 0xf8, 0xbf, 0x1e, 0xd7, 0x66, 0x51, 0xc1, 0x47, 0x56, 0xa0, 0x61, 0xd6, 0x62, 0xf5, 0x80, 0xff, 0x4d, 0xe4, 0x3b, 0x49, 0xfa, 0x82, 0xd8, 0x0a, 0x4b, 0x80, 0xf8, 0x43, 0x4a],
            "a7ffc6f8bf1ed76651c14756a061d662f580ff4de43b49fa82d80a4b80f8434a",
            static () => System.Security.Cryptography.SHA3_256.IsSupported ? System.Security.Cryptography.SHA3_256.Create() : null);

        /// <summary>
        /// SHA3-384 hash
        /// </summary>
        public static readonly HashType SHA3_384 = new("SHA3-384",
            "SHA3-384 hash",
            [0x0c, 0x63, 0xa7, 0x5b, 0x84, 0x5e, 0x4f, 0x7d, 0x01, 0x10, 0x7d, 0x85, 0x2e, 0x4c, 0x24, 0x85, 0xc5, 0x1a, 0x50, 0xaa, 0xaa, 0x94, 0xfc, 0x61, 0x99, 0x5e, 0x71, 0xbb, 0xee, 0x98, 0x3a, 0x2a, 0xc3, 0x71, 0x38, 0x31, 0x26, 0x4a, 0xdb, 0x47, 0xfb, 0x6b, 0xd1, 0xe0, 0x58, 0xd5, 0xf0, 0x04],
            "0c63a75b845e4f7d01107d852e4c2485c51a50aaaa94fc61995e71bbee983a2ac3713831264adb47fb6bd1e058d5f004",
            static () => System.Security.Cryptography.SHA3_384.IsSupported ? System.Security.Cryptography.SHA3_384.Create() : null);

        /// <summary>
        /// SHA3-512 hash
        /// </summary>
        public static readonly HashType SHA3_512 = new("SHA3-512",
            "SHA3-512 hash",
            [0xa6, 0x9f, 0x73, 0xcc, 0xa2, 0x3a, 0x9a, 0xc5, 0xc8, 0xb5, 0x67, 0xdc, 0x18, 0x5a, 0x75, 0x6e, 0x97, 0xc9, 0x82, 0x16, 0x4f, 0xe2, 0x58, 0x59, 0xe0, 0xd1, 0xdc, 0xc1, 0x47, 0x5c, 0x80, 0xa6, 0x15, 0xb2, 0x12, 0x3a, 0xf1, 0xf5, 0xf9, 0x4c, 0x11, 0xe3, 0xe9, 0x40, 0x2c, 0x3a, 0xc5, 0x58, 0xf5, 0x00, 0x19, 0x9d, 0x95, 0xb6, 0xd3, 0xe3, 0x01, 0x75, 0x85, 0x86, 0x28, 0x1d, 0xcd, 0x26],
            "a69f73cca23a9ac5c8b567dc185a756e97c982164fe25859e0d1dcc1475c80a615b2123af1f5f94c11e3e9402c3ac558f500199d95b6d3e301758586281dcd26",
            static () => System.Security.Cryptography.SHA3_512.IsSupported ? System.Security.Cryptography.SHA3_512.Create() : null);

        /// <summary>
        /// SHAKE128 SHA-3 family hash
        /// </summary>
        /// <remarks>Outputs a 256-bit (32-byte) hash</remarks>
        public static readonly HashType SHAKE128 = new("SHAKE128",
            "SHAKE128 SHA-3 family hash (256-bit)",
            [0x7f, 0x9c, 0x2b, 0xa4, 0xe8, 0x8f, 0x82, 0x7d, 0x61, 0x60, 0x45, 0x50, 0x76, 0x05, 0x85, 0x3e, 0xd7, 0x3b, 0x80, 0x93, 0xf6, 0xef, 0xbc, 0x88, 0xeb, 0x1a, 0x6e, 0xac, 0xfa, 0x66, 0xef, 0x26],
            "7f9c2ba4e88f827d616045507605853ed73b8093f6efbc88eb1a6eacfa66ef26",
            static () => System.Security.Cryptography.Shake128.IsSupported ? new System.Security.Cryptography.Shake128() : null);

        /// <summary>
        /// SHAKE256 SHA-3 family hash
        /// </summary>
        /// <remarks>Outputs a 512-bit (64-byte) hash</remarks>
        public static readonly HashType SHAKE256 = new("SHAKE256",
            "SHAKE256 SHA-3 family hash (512-bit)",
            [0x46, 0xb9, 0xdd, 0x2b, 0x0b, 0xa8, 0x8d, 0x13, 0x23, 0x3b, 0x3f, 0xeb, 0x74, 0x3e, 0xeb, 0x24, 0x3f, 0xcd, 0x52, 0xea, 0x62, 0xb8, 0x1b, 0x82, 0xb5, 0x0c, 0x27, 0x64, 0x6e, 0xd5, 0x76, 0x2f, 0xd7, 0x5d, 0xc4, 0xdd, 0xd8, 0xc0, 0xf2, 0x00, 0xcb, 0x05, 0x01, 0x9d, 0x67, 0xb5, 0x92, 0xf6, 0xfc, 0x82, 0x1c, 0x49, 0x47, 0x9a, 0xb4, 0x86, 0x40, 0x29, 0x2e, 0xac, 0xb3, 0xb7, 0xc4, 0xbe],
            "46b9dd2b0ba88d13233b3feb743eeb243fcd52ea62b81b82b50c27646ed5762fd75dc4ddd8c0f200cb05019d67b592f6fc821c49479ab48640292eacb3b7c4be",
            static () => System.Security.Cryptography.Shake256.IsSupported ? new System.Security.Cryptography.Shake256() : null);
#endif

        #endregion

        /// <summary>
        /// spamsum fuzzy hash
        /// </summary>
        public static readonly HashType SpamSum = new("spamsum",
            "spamsum fuzzy hash",
            [0x33, 0x3a, 0x3a, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00],
            "3::",
            static () => new SpamSum.SpamSum());

        #region Tiger

        /// <summary>
        /// Tiger 128-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger128_3 = new("Tiger-128-3",
            "Tiger 128-bit hash, 3 passes",
            [0x32, 0x93, 0xac, 0x63, 0x0c, 0x13, 0xf0, 0x24, 0x5f, 0x92, 0xbb, 0xb1, 0x76, 0x6e, 0x16, 0x16],
            "3293ac630c13f0245f92bbb1766e1616",
            static () => new Tiger128_3());

        /// <summary>
        /// Tiger 128-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger128_4 = new("Tiger-128-4",
            "Tiger 128-bit hash, 4 passes",
            [0x24, 0xcc, 0x78, 0xa7, 0xf6, 0xff, 0x35, 0x46, 0xe7, 0x98, 0x4e, 0x59, 0x69, 0x5c, 0xa1, 0x3d],
            "24cc78a7f6ff3546e7984e59695ca13d",
            static () => new Tiger128_4());

        /// <summary>
        /// Tiger 160-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger160_3 = new("Tiger-160-3",
            "Tiger 160-bit hash, 3 passes",
            [0x32, 0x93, 0xac, 0x63, 0x0c, 0x13, 0xf0, 0x24, 0x5f, 0x92, 0xbb, 0xb1, 0x76, 0x6e, 0x16, 0x16, 0x7a, 0x4e, 0x58, 0x49],
            "3293ac630c13f0245f92bbb1766e16167a4e5849",
            static () => new Tiger160_3());

        /// <summary>
        /// Tiger 160-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger160_4 = new("Tiger-160-4",
            "Tiger 160-bit hash, 4 passes",
            [0x24, 0xcc, 0x78, 0xa7, 0xf6, 0xff, 0x35, 0x46, 0xe7, 0x98, 0x4e, 0x59, 0x69, 0x5c, 0xa1, 0x3d, 0x80, 0x4e, 0x0b, 0x68],
            "24cc78a7f6ff3546e7984e59695ca13d804e0b68",
            static () => new Tiger160_4());

        /// <summary>
        /// Tiger 192-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger192_3 = new("Tiger-192-3",
            "Tiger 192-bit hash, 3 passes",
            [0x32, 0x93, 0xac, 0x63, 0x0c, 0x13, 0xf0, 0x24, 0x5f, 0x92, 0xbb, 0xb1, 0x76, 0x6e, 0x16, 0x16, 0x7a, 0x4e, 0x58, 0x49, 0x2d, 0xde, 0x73, 0xf3],
            "3293ac630c13f0245f92bbb1766e16167a4e58492dde73f3",
            static () => new Tiger192_3());

        /// <summary>
        /// Tiger 192-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger192_4 = new("Tiger-192-4",
            "Tiger 192-bit hash, 4 passes",
            [0x24, 0xcc, 0x78, 0xa7, 0xf6, 0xff, 0x35, 0x46, 0xe7, 0x98, 0x4e, 0x59, 0x69, 0x5c, 0xa1, 0x3d, 0x80, 0x4e, 0x0b, 0x68, 0x6e, 0x25, 0x51, 0x94],
            "24cc78a7f6ff3546e7984e59695ca13d804e0b686e255194",
            static () => new Tiger192_4());

        /// <summary>
        /// Tiger2 128-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger2_128_3 = new("Tiger2-128-3",
            "Tiger2 128-bit hash, 3 passes",
            [0x44, 0x41, 0xbe, 0x75, 0xf6, 0x01, 0x87, 0x73, 0xc2, 0x06, 0xc2, 0x27, 0x45, 0x37, 0x4b, 0x92],
            "4441be75f6018773c206c22745374b92",
            static () => new Tiger2_128_3());

        /// <summary>
        /// Tiger2 128-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger2_128_4 = new("Tiger2-128-4",
            "Tiger2 128-bit hash, 4 passes",
            [0x6a, 0x72, 0x01, 0xa4, 0x7a, 0xac, 0x20, 0x65, 0x91, 0x38, 0x11, 0x17, 0x55, 0x53, 0x48, 0x9a],
            "6a7201a47aac2065913811175553489a",
            static () => new Tiger2_128_4());

        /// <summary>
        /// Tiger2 160-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger2_160_3 = new("Tiger2-160-3",
            "Tiger2 160-bit hash, 3 passes",
            [0x44, 0x41, 0xbe, 0x75, 0xf6, 0x01, 0x87, 0x73, 0xc2, 0x06, 0xc2, 0x27, 0x45, 0x37, 0x4b, 0x92, 0x4a, 0xa8, 0x31, 0x3f],
            "4441be75f6018773c206c22745374b924aa8313f",
            static () => new Tiger2_160_3());

        /// <summary>
        /// Tiger2 160-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger2_160_4 = new("Tiger2-160-4",
            "Tiger2 160-bit hash, 4 passes",
            [0x6a, 0x72, 0x01, 0xa4, 0x7a, 0xac, 0x20, 0x65, 0x91, 0x38, 0x11, 0x17, 0x55, 0x53, 0x48, 0x9a, 0xdd, 0x0f, 0x8b, 0x99],
            "6a7201a47aac2065913811175553489add0f8b99",
            static () => new Tiger2_160_4());

        /// <summary>
        /// Tiger2 192-bit hash, 3 passes
        /// </summary>
        public static readonly HashType Tiger2_192_3 = new("Tiger2-192-3",
            "Tiger2 192-bit hash, 3 passes",
            [0x44, 0x41, 0xbe, 0x75, 0xf6, 0x01, 0x87, 0x73, 0xc2, 0x06, 0xc2, 0x27, 0x45, 0x37, 0x4b, 0x92, 0x4a, 0xa8, 0x31, 0x3f, 0xef, 0x91, 0x9f, 0x41],
            "4441be75f6018773c206c22745374b924aa8313fef919f41",
            static () => new Tiger2_192_3());

        /// <summary>
        /// Tiger2 192-bit hash, 4 passes
        /// </summary>
        public static readonly HashType Tiger2_192_4 = new("Tiger2-192-4",
            "Tiger2 192-bit hash, 4 passes",
            [0x6a, 0x72, 0x01, 0xa4, 0x7a, 0xac, 0x20, 0x65, 0x91, 0x38, 0x11, 0x17, 0x55, 0x53, 0x48, 0x9a, 0xdd, 0x0f, 0x8b, 0x99, 0xe6, 0x5a, 0x09, 0x55],
            "6a7201a47aac2065913811175553489add0f8b99e65a0955",
            static () => new Tiger2_192_4());

        #endregion

        #region xxHash

        /// <summary>
        /// xxHash32 hash
        /// </summary>
        public static readonly HashType XxHash32 = new("xxHash32",
            "xxHash32 hash",
            [0x02, 0xcc, 0x5d, 0x05],
            "02cc5d05",
            static () => new XxHash32());

        /// <summary>
        /// xxHash64 hash
        /// </summary>
        public static readonly HashType XxHash64 = new("xxHash64",
            "xxHash64 hash",
            [0xef, 0x46, 0xdb, 0x37, 0x51, 0xd8, 0xe9, 0x99],
            "ef46db3751d8e999",
            static () => new XxHash64());

#if NET462_OR_GREATER || NETCOREAPP
        /// <summary>
        /// XXH3 64-bit hash
        /// </summary>
        public static readonly HashType XxHash3 = new("XXH3-64",
            "XXH3 64-bit hash",
            [0x2d, 0x06, 0x80, 0x05, 0x38, 0xd3, 0x94, 0xc2],
            "2d06800538d394c2",
            static () => new System.IO.Hashing.XxHash3());

        /// <summary>
        /// XXH128 128-bit hash
        /// </summary>
        public static readonly HashType XxHash128 = new("XXH128",
            "XXH128 128-bit hash",
            [0x99, 0xaa, 0x06, 0xd3, 0x01, 0x47, 0x98, 0xd8, 0x60, 0x01, 0xc3, 0x24, 0x46, 0x8d, 0x49, 0x7f],
            "99aa06d3014798d86001c324468d497f",
            static () => new System.IO.Hashing.XxHash128());
#endif

        #endregion

        #endregion

        #region Static Collections

        /// <summary>
        /// All supported hashes
        /// </summary>
        public static readonly HashType[] AllHashes =
        [
            Adler32,

    #if NET7_0_OR_GREATER
            BLAKE3,
    #endif

            CRC1_ZERO,
            CRC1_ONE,

            CRC3_GSM,
            CRC3_ROHC,

            CRC4_G704,
            CRC4_INTERLAKEN,

            CRC5_EPCC1G2,
            CRC5_G704,
            CRC5_USB,

            CRC6_CDMA2000A,
            CRC6_CDMA2000B,
            CRC6_DARC,
            CRC6_G704,
            CRC6_GSM,

            CRC7_MMC,
            CRC7_ROHC,
            CRC7_UMTS,

            CRC8,
            CRC8_AUTOSAR,
            CRC8_BLUETOOTH,
            CRC8_CDMA2000,
            CRC8_DARC,
            CRC8_DVBS2,
            CRC8_GSMA,
            CRC8_GSMB,
            CRC8_HITAG,
            CRC8_I4321,
            CRC8_ICODE,
            CRC8_LTE,
            CRC8_MAXIMDOW,
            CRC8_MIFAREMAD,
            CRC8_NRSC5,
            CRC8_OPENSAFETY,
            CRC8_ROHC,
            CRC8_SAEJ1850,
            CRC8_SMBUS,
            CRC8_TECH3250,
            CRC8_WCDMA,

            CRC10_ATM,
            CRC10_CDMA2000,
            CRC10_GSM,

            CRC11_FLEXRAY,
            CRC11_UMTS,

            CRC12_CDMA2000,
            CRC12_DECT,
            CRC12_GSM,
            CRC12_UMTS,

            CRC13_BBC,

            CRC14_DARC,
            CRC14_GSM,

            CRC15_CAN,
            CRC15_MPT1327,

            CRC16,
            CRC16_ARC,
            CRC16_CDMA2000,
            CRC16_CMS,
            CRC16_DDS110,
            CRC16_DECTR,
            CRC16_DECTX,
            CRC16_DNP,
            CRC16_EN13757,
            CRC16_GENIBUS,
            CRC16_GSM,
            CRC16_IBM3740,
            CRC16_IBMSDLC,
            CRC16_ISOIEC144433A,
            CRC16_KERMIT,
            CRC16_LJ1200,
            CRC16_M17,
            CRC16_MAXIMDOW,
            CRC16_MCRF4XX,
            CRC16_MODBUS,
            CRC16_NRSC5,
            CRC16_OPENSAFETYA,
            CRC16_OPENSAFETYB,
            CRC16_PROFIBUS,
            CRC16_RIELLO,
            CRC16_SPIFUJITSU,
            CRC16_T10DIF,
            CRC16_TELEDISK,
            CRC16_TMS37157,
            CRC16_UMTS,
            CRC16_USB,
            CRC16_XMODEM,

            CRC17_CANFD,

            CRC21_CANFD,

            CRC24_BLE,
            CRC24_FLEXRAYA,
            CRC24_FLEXRAYB,
            CRC24_INTERLAKEN,
            CRC24_LTEA,
            CRC24_LTEB,
            CRC24_OPENPGP,
            CRC24_OS9,

            CRC30_CDMA,

            CRC31_PHILIPS,

            CRC32,
            CRC32_AIXM,
            CRC32_AUTOSAR,
            CRC32_BASE91D,
            CRC32_BZIP2,
            CRC32_CDROMEDC,
            CRC32_CKSUM,
            CRC32_DVDROMEDC,
            CRC32_ISCSI,
            CRC32_ISOHDLC,
            CRC32_JAMCRC,
            CRC32_MEF,
            CRC32_MPEG2,
            CRC32_XFER,

            CRC40_GSM,

            CRC64,
            CRC64_ECMA182,
            CRC64_GOISO,
            CRC64_MS,
            CRC64_NVME,
            CRC64_REDIS,
            CRC64_WE,
            CRC64_XZ,

#if NET7_0_OR_GREATER
            CRC82_DARC,
#endif

            Fletcher16,
            Fletcher32,
            Fletcher64,

            FNV0_32,
            FNV0_64,
            FNV1_32,
            FNV1_64,
            FNV1a_32,
            FNV1a_64,

            MekaCrc,

            MD2,
            MD4,
            MD5,

            RIPEMD128,
            RIPEMD160,
            RIPEMD256,
            RIPEMD320,

            SHA1,
            SHA256,
            SHA384,
            SHA512,
#if NET8_0_OR_GREATER
            SHA3_256,
            SHA3_384,
            SHA3_512,
            SHAKE128,
            SHAKE256,
#endif

            SpamSum,

            Tiger128_3,
            Tiger128_4,
            Tiger160_3,
            Tiger160_4,
            Tiger192_3,
            Tiger192_4,
            Tiger2_128_3,
            Tiger2_128_4,
            Tiger2_160_3,
            Tiger2_160_4,
            Tiger2_192_3,
            Tiger2_192_4,

            XxHash32,
            XxHash64,
#if NET462_OR_GREATER || NETCOREAPP
            XxHash3,
            XxHash128,
#endif
        ];

        /// <summary>
        /// Common collection of hash types
        /// </summary>
        /// <remarks><see cref="CRC32"/>, <see cref="MD5"/>, <see cref="SHA1"/>, <see cref="SHA256"/></remarks>
        public static readonly HashType[] StandardHashes =
        [
            CRC32,
            MD5,
            SHA1,
            SHA256,
        ];

        #endregion
    }
}
