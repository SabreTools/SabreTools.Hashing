namespace SabreTools.Hashing
{
    public static class Extensions
    {
        /// <summary>
        /// Get the hash type associated to a string, if possible
        /// </summary>
        /// TODO: This should be automated instead of hardcoded
        public static HashType? ToHashType(this string? str)
        {
            // Ignore invalid strings
            if (string.IsNullOrEmpty(str))
                return null;

            // Normalize the string before matching
            str = str!.Replace("-", string.Empty);
            str = str.Replace(" ", string.Empty);
            str = str.Replace("/", "_");
            str = str.Replace("\\", "_");
            str = str.ToLowerInvariant();

            // Match based on potential names
            return str switch
            {
                "adler" or "adler32" => HashType.Adler32,

#if NET7_0_OR_GREATER
                "blake3" => HashType.BLAKE3,
#endif

                "crc1_0" or "crc1_zero" => HashType.CRC1_ZERO,
                "crc1_1" or "crc1_one" => HashType.CRC1_ONE,

                "crc3_gsm" => HashType.CRC3_GSM,
                "crc3_rohc" => HashType.CRC3_ROHC,

                "crc4_g704" or "crc4_itu" => HashType.CRC4_G704,
                "crc4_interlaken" => HashType.CRC4_INTERLAKEN,

                "crc5_epc" or "crc5_epcc1g2" => HashType.CRC5_EPCC1G2,
                "crc5_g704" or "crc5_itu" => HashType.CRC5_G704,
                "crc5_usb" => HashType.CRC5_USB,

                "crc6_cdma2000a" => HashType.CRC6_CDMA2000A,
                "crc6_cdma2000b" => HashType.CRC6_CDMA2000B,
                "crc6_darc" => HashType.CRC6_DARC,
                "crc6_g704" or "crc6_itu" => HashType.CRC6_G704,
                "crc6_gsm" => HashType.CRC6_GSM,

                "crc7" or "crc7_mmc" => HashType.CRC7_MMC,
                "crc7_rohc" => HashType.CRC7_ROHC,
                "crc7_umts" => HashType.CRC7_UMTS,

                "crc8" => HashType.CRC8,
                "crc8_autosar" => HashType.CRC8_AUTOSAR,
                "crc8_bluetooth" => HashType.CRC8_BLUETOOTH,
                "crc8_cdma2000" => HashType.CRC8_CDMA2000,
                "crc8_darc" => HashType.CRC8_DARC,
                "crc8_dvbs2" => HashType.CRC8_DVBS2,
                "crc8_gsma" => HashType.CRC8_GSMA,
                "crc8_gsmb" => HashType.CRC8_GSMB,
                "crc8_hitag" => HashType.CRC8_HITAG,
                "crc8_i4321" or "crc8_itu" => HashType.CRC8_I4321,
                "crc8_icode" => HashType.CRC8_ICODE,
                "crc8_lte" => HashType.CRC8_LTE,
                "crc8_maximdow" or "crc8_maxim" or "dowcrc" => HashType.CRC8_MAXIMDOW,
                "crc8_mifaremad" => HashType.CRC8_MIFAREMAD,
                "crc8_nrsc5" => HashType.CRC8_NRSC5,
                "crc8_opensafety" => HashType.CRC8_OPENSAFETY,
                "crc8_rohc" => HashType.CRC8_ROHC,
                "crc8_saej1850" => HashType.CRC8_SAEJ1850,
                "crc8_smbus" => HashType.CRC8_SMBUS,
                "crc8_tech3250" or "crc8_aes" or "crc8_ebu" => HashType.CRC8_TECH3250,
                "crc8_wcdma" => HashType.CRC8_WCDMA,

                "crc10_atm" or "crc10" or "crc10_i610" => HashType.CRC10_ATM,
                "crc10_cdma2000" => HashType.CRC10_CDMA2000,
                "crc10_gsm" => HashType.CRC10_GSM,

                "crc11_flexray" or "crc11" => HashType.CRC11_FLEXRAY,
                "crc11_umts" => HashType.CRC11_UMTS,

                "crc12_cdma2000" => HashType.CRC12_CDMA2000,
                "crc12_dect" or "xcrc12" => HashType.CRC12_DECT,
                "crc12_gsm" => HashType.CRC12_GSM,
                "crc12_umts" or "crc12_3gpp" => HashType.CRC12_UMTS,

                "crc13_bbc" => HashType.CRC13_BBC,

                "crc14_darc" => HashType.CRC14_DARC,
                "crc14_gsm" => HashType.CRC14_GSM,

                "crc15_can" or "crc15" => HashType.CRC15_CAN,
                "crc15_mpt1327" => HashType.CRC15_MPT1327,

                "crc16" => HashType.CRC16,
                "crc16_arc" or "arc" or "crc16_lha" or "crcibm" => HashType.CRC16_ARC,
                "crc16_cdma2000" => HashType.CRC16_CDMA2000,
                "crc16_cms" => HashType.CRC16_CMS,
                "crc16_dds110" => HashType.CRC16_DDS110,
                "crc16_dectr" or "rcrc16" => HashType.CRC16_DECTR,
                "crc16_dectx" or "xcrc16" => HashType.CRC16_DECTX,
                "crc16_dnp" => HashType.CRC16_DNP,
                "crc16_en13757" => HashType.CRC16_EN13757,
                "crc16_genibus" or "crc16_darc" or "crc16_epc" or "crc16_epcc1g2" or "crc16_icode" => HashType.CRC16_GENIBUS,
                "crc16_gsm" => HashType.CRC16_GSM,
                "crc16_ibm3740" or "crc16_autosar" or "crc16_cittfalse" => HashType.CRC16_IBM3740,
                "crc16_ibmsdlc" or "crc16_isohdlc" or "crc16_isoiec144433b" or "crc16_x25" or "crcb" or "x25" => HashType.CRC16_IBMSDLC,
                "crc16_isoiec144433a" or "crca" => HashType.CRC16_ISOIEC144433A,
                "crc16_kermit" or "crc16_bluetooth" or "crc16_ccitt" or "crc16_ccitttrue" or "crc16_v41lsb" or "crcccitt" or "kermit" => HashType.CRC16_KERMIT,
                "crc16_lj1200" => HashType.CRC16_LJ1200,
                "crc16_m17" => HashType.CRC16_M17,
                "crc16_maximdow" or "crc16_maxim" => HashType.CRC16_MAXIMDOW,
                "crc16_mcrf4xx" => HashType.CRC16_MCRF4XX,
                "crc16_modbus" or "modbus" => HashType.CRC16_MODBUS,
                "crc16_nrsc5" => HashType.CRC16_NRSC5,
                "crc16_opensafetya" => HashType.CRC16_OPENSAFETYA,
                "crc16_opensafetyb" => HashType.CRC16_OPENSAFETYB,
                "crc16_profibus" or "crc16_iec611582" => HashType.CRC16_PROFIBUS,
                "crc16_riello" => HashType.CRC16_RIELLO,
                "crc16_spifujitsu" or "crc16_augccitt" => HashType.CRC16_SPIFUJITSU,
                "crc16_t10dif" => HashType.CRC16_T10DIF,
                "crc16_teledisk" => HashType.CRC16_TELEDISK,
                "crc16_tms37157" => HashType.CRC16_TMS37157,
                "crc16_umts" or "crc16_buypass" or "crc16_verifone" => HashType.CRC16_UMTS,
                "crc16_usb" => HashType.CRC16_USB,
                "crc16_xmodem" or "crc16_acorn" or "crc16_lte" or "crc16_v41msb" or "xmodem" or "zmodem" => HashType.CRC16_XMODEM,

                "crc17_canfd" => HashType.CRC17_CANFD,

                "crc21_canfd" => HashType.CRC21_CANFD,

                "crc24_ble" => HashType.CRC24_BLE,
                "crc24_flexraya" => HashType.CRC24_FLEXRAYA,
                "crc24_flexrayb" => HashType.CRC24_FLEXRAYB,
                "crc24_interlaken" => HashType.CRC24_INTERLAKEN,
                "crc24_ltea" => HashType.CRC24_LTEA,
                "crc24_lteb" => HashType.CRC24_LTEB,
                "crc24_openpgp" => HashType.CRC24_OPENPGP,
                "crc24_os9" => HashType.CRC24_OS9,

                "crc30_cdma" => HashType.CRC30_CDMA,

                "crc31_philips" => HashType.CRC31_PHILIPS,

                "crc32" => HashType.CRC32,
                "crc32_aixm" => HashType.CRC32_AIXM,
                "crc32_autosar" => HashType.CRC32_AUTOSAR,
                "crc32_base91d" => HashType.CRC32_BASE91D,
                "crc32_bzip2" => HashType.CRC32_BZIP2,
                "crc32_cdromedc" => HashType.CRC32_CDROMEDC,
                "crc32_cksum" => HashType.CRC32_CKSUM,
                "crc32_dvdromedc" => HashType.CRC32_DVDROMEDC,
                "crc32_iscsi" => HashType.CRC32_ISCSI,
                "crc32_isohdlc" => HashType.CRC32_ISOHDLC,
                "crc32_jamcrc" => HashType.CRC32_JAMCRC,
                "crc32_mef" => HashType.CRC32_MEF,
                "crc32_mpeg2" => HashType.CRC32_MPEG2,
                "crc32_xfer" => HashType.CRC32_XFER,

                "crc40_gsm" => HashType.CRC40_GSM,

                "crc64" => HashType.CRC64,
                "crc64_ecma182" => HashType.CRC64_ECMA182,
                "crc64_goiso" => HashType.CRC64_GOISO,
                "crc64_ms" => HashType.CRC64_MS,
                "crc64_nvme" => HashType.CRC64_NVME,
                "crc64_redis" => HashType.CRC64_REDIS,
                "crc64_we" => HashType.CRC64_WE,
                "crc64_xz" => HashType.CRC64_XZ,

                "fletcher16" => HashType.Fletcher16,
                "fletcher32" => HashType.Fletcher32,
                "fletcher64" => HashType.Fletcher64,

                "fnv0_32" => HashType.FNV0_32,
                "fnv0_64" => HashType.FNV0_64,
                "fnv1_32" => HashType.FNV1_32,
                "fnv1_64" => HashType.FNV1_64,
                "fnv1a_32" => HashType.FNV1a_32,
                "fnv1a_64" => HashType.FNV1a_64,

                "meka" or "mekacrc" or "meka_crc" => HashType.MekaCrc,

                "md2" => HashType.MD2,
                "md4" => HashType.MD4,
                "md5" => HashType.MD5,

                "ripemd128" => HashType.RIPEMD128,
                "ripemd160" => HashType.RIPEMD160,
                "ripemd256" => HashType.RIPEMD256,
                "ripemd320" => HashType.RIPEMD320,

                "sha1" => HashType.SHA1,
                "sha256" => HashType.SHA256,
                "sha384" => HashType.SHA384,
                "sha512" => HashType.SHA512,
#if NET8_0_OR_GREATER
                "sha3_256" => HashType.SHA3_256,
                "sha3_384" => HashType.SHA3_384,
                "sha3_512" => HashType.SHA3_512,
                "shake128" => HashType.SHAKE128,
                "shake256" => HashType.SHAKE256,
#endif

                "spamsum" => HashType.SpamSum,

                "tiger128_3" => HashType.Tiger128_3,
                "tiger128_4" => HashType.Tiger128_4,
                "tiger160_3" => HashType.Tiger160_3,
                "tiger160_4" => HashType.Tiger160_4,
                "tiger192_3" => HashType.Tiger192_3,
                "tiger192_4" => HashType.Tiger192_4,
                "tiger2_128_3" => HashType.Tiger2_128_3,
                "tiger2_128_4" => HashType.Tiger2_128_4,
                "tiger2_160_3" => HashType.Tiger2_160_3,
                "tiger2_160_4" => HashType.Tiger2_160_4,
                "tiger2_192_3" => HashType.Tiger2_192_3,
                "tiger2_192_4" => HashType.Tiger2_192_4,

                "xxh" or "xxh32" or "xxh_32" or "xxhash" or "xxhash32" or "xxhash_32" => HashType.XxHash32,
                "xxh64" or "xxh_64" or "xxhash64" or "xxhash_64" => HashType.XxHash64,
#if NET462_OR_GREATER || NETCOREAPP
                "xxh3" or "xxh3_64" or "xxhash3" or "xxhash_3" => HashType.XxHash3,
                "xxh128" or "xxh_128" or "xxhash128" or "xxhash_128" => HashType.XxHash128,
#endif

                _ => null,
            };
        }
    }
}
