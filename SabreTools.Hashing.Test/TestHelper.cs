using System.Collections.Generic;
using Xunit;

namespace SabreTools.Hashing.Test
{
    /// <summary>
    /// Helper class for tests
    /// </summary>
    /// CRC values confirmed with <see href="https://emn178.github.io/online-tools/crc/"/> 
    internal static class TestHelper
    {
        #region Known File Information

        private const long _hashFileSize = 125;
        private const string _adler32 = "08562d95";
#if NET7_0_OR_GREATER
        private const string _blake3 = "d4bd7ca6f1ebea9580d9381106b248eb5b6069170d0bfd00b17d659fcd10dcdc";
#endif
        private const string _crc16 = "7573";
        private const string _crc16_arc = "7573";
        private const string _crc16_cdma2000 = "8b5f";
        private const string _crc16_cms = "1a37";
        private const string _crc16_dds110 = "241d";
        private const string _crc16_dectr = "7390";
        private const string _crc16_dectx = "7391";
        private const string _crc16_dnp = "4bbb";
        private const string _crc16_en13757 = "e28b";
        private const string _crc16_genibus = "b65d";
        private const string _crc16_gsm = "482d";
        private const string _crc16_ibm3740 = "49a2";
        private const string _crc16_ibmsdlc = "4f52";
        private const string _crc16_isoiec144433a = "85cd";
        private const string _crc16_kermit = "bed2";
        private const string _crc16_lj1200 = "3533";
        private const string _crc16_m17 = "5223";
        private const string _crc16_maximdow = "8a8c";
        private const string _crc16_mcrf4xx = "b0ad";
        private const string _crc16_modbus = "9e54";
        private const string _crc16_nrsc5 = "4857";
        private const string _crc16_opensafetya = "abcd";
        private const string _crc16_opensafetyb = "76f4";
        private const string _crc16_profibus = "3099";
        private const string _crc16_riello = "23e0";
        private const string _crc16_spifujitsu = "f98b";
        private const string _crc16_t10dif = "2642";
        private const string _crc16_teledisk = "7e05";
        private const string _crc16_tms37157 = "dba0";
        private const string _crc16_umts = "fee0";
        private const string _crc16_usb = "61ab";
        private const string _crc16_xmodem = "b7d2";
        private const string _crc17_canfd = "0706d";
        private const string _crc21_canfd = "117d4b";
        private const string _crc24_ble = "2969f2";
        private const string _crc24_flexraya = "ce9dc7";
        private const string _crc24_flexrayb = "0f49d7";
        private const string _crc24_interlaken = "fb4725";
        private const string _crc24_ltea = "675e55";
        private const string _crc24_lteb = "c91203";
        private const string _crc24_openpgp = "0c6012";
        private const string _crc24_os9 = "610e21";
        private const string _crc30_cdma = "2ce682b2";
        private const string _crc31_philips = "247c3cbe";
        private const string _crc32 = "ba02a660";
        private const string _crc32_aixm = "6174a75a";
        private const string _crc32_autosar = "c050428e";
        private const string _crc32_base91d = "e741ba25";
        private const string _crc32_bzip2 = "18aa4603";
        private const string _crc32_cdromedc = "b8ced467";
        private const string _crc32_cksum = "f27b3c27";
        private const string _crc32_iscsi = "544d37db";
        private const string _crc32_isohdlc = "ba02a660";
        private const string _crc32_jamcrc = "45fd599f";
        private const string _crc32_mef = "d9d98444";
        private const string _crc32_mpeg2 = "e755b9fc";
        private const string _crc32_xfer = "55bdf222";
        private const string _crc40_gsm = "c9843306eb";
        private const string _crc64 = "8d33b5189c00e0a0";
        private const string _crc64_ecma182 = "8d33b5189c00e0a0";
        private const string _crc64_goiso = "6c3bf747ccfa1e3b";
        private const string _crc64_ms = "799edc0db430d7be";
        private const string _crc64_nvme = "9242023bbcf6bbf9";
        private const string _crc64_redis = "408dab12b9f45dad";
        private const string _crc64_we = "91812be748f941c4";
        private const string _crc64_xz = "fb49044e8331f6e5";
        private const string _fletcher16 = "46c1";
        private const string _fletcher32 = "073f2d94";
        private const string _md5 = "b722871eaa950016296184d026c5dec9";
#if NETFRAMEWORK
        private const string _ripemd160 = "346361e1d7fdb836650cecdb842b0dbe660eed66";
#endif
        private const string _sha1 = "eea1ee2d801d830c4bdad4df3c8da6f9f52d1a9f";
        private const string _sha256 = "fdb02dee8c319c52087382c45f099c90d0b6cc824850aff28c1bfb2884b7b855";
        private const string _sha384 = "e276c49618fff25bc1fe2e0659cd0ef0e7c1186563b063e07c52323b9899f3ce9b091be04d6208444b3ef1265e879074";
        private const string _sha512 = "15d69514eb628c2403e945a7cafd1d27e557f6e336c69b63ea17e7ed9d256cc374ee662f09305836d6de37fdae59d83883b982aa8446e4ff26346b6b6b50b240";
#if NET8_0_OR_GREATER
        private const string _sha3_256 = "1d76459e68c865b5911ada5104067cc604c5c60b345c4e81b3905e916a43c868";
        private const string _sha3_384 = "1bcbed87b73f25c0adf486c3afbf0ea3105763c387af3f8b2bd79b0a1964d42832b1d7c6a2225f9153ead26f442e8b67";
        private const string _sha3_512 = "89852144df37c58d01f5912124f1942dd00bac0346eb3971943416699c3094cff087fb42c356019c3d91f8e8f55b9254c8caec48e9414af6817297d06725ffeb";
        private const string _shake128 = "e5f88d0db79a71c39490beb9ebac21eaf4a5d6368438fca20f5e4ce77cfee9aa";
        private const string _shake256 = "24d9e83198bbc7baf4dcd293bfc35ae3fff05399786c37318f1b1ef85f41970c66926f8a2a1f912d96e2d8e45535af88a301a1c200697437c1a65d7e980344bc";
#endif
        private const string _spamsum = "3:hMCPQCE6AFQxWyENFACBE+rW6Tj7SMQmKozr9MVERkL:hZRdxZENFs+rPSromekL";
#if NET462_OR_GREATER || NETCOREAPP
        private const string _xxhash32 = "8e331daa";
        private const string _xxhash64 = "082bf6f0a49e1e18";
        private const string _xxhash3 = "040474eb0eda9ff2";
        private const string _xxhash128 = "d934b4b4a5e1e11baeef8012fbcd11e8";
#endif

        #endregion

        /// <summary>
        /// Validate the hashes in a hash dictionary
        /// </summary>
        public static void ValidateHashes(Dictionary<HashType, string?>? hashDict)
        {
            Assert.NotNull(hashDict);
            Assert.Equal(_adler32, hashDict![HashType.Adler32]);

#if NET7_0_OR_GREATER
            Assert.Equal(_blake3, hashDict[HashType.BLAKE3]);
#endif

            Assert.Equal(_crc16, hashDict[HashType.CRC16]);
            Assert.Equal(_crc16_arc, hashDict[HashType.CRC16_ARC]);
            Assert.Equal(_crc16_cdma2000, hashDict[HashType.CRC16_CDMA2000]);
            Assert.Equal(_crc16_cms, hashDict[HashType.CRC16_CMS]);
            Assert.Equal(_crc16_dds110, hashDict[HashType.CRC16_DDS110]);
            Assert.Equal(_crc16_dectr, hashDict[HashType.CRC16_DECTR]);
            Assert.Equal(_crc16_dectx, hashDict[HashType.CRC16_DECTX]);
            Assert.Equal(_crc16_dnp, hashDict[HashType.CRC16_DNP]);
            Assert.Equal(_crc16_en13757, hashDict[HashType.CRC16_EN13757]);
            Assert.Equal(_crc16_genibus, hashDict[HashType.CRC16_GENIBUS]);
            Assert.Equal(_crc16_gsm, hashDict[HashType.CRC16_GSM]);
            Assert.Equal(_crc16_ibm3740, hashDict[HashType.CRC16_IBM3740]);
            Assert.Equal(_crc16_ibmsdlc, hashDict[HashType.CRC16_IBMSDLC]);
            //Assert.Equal(_crc16_isoiec144433a, hashDict[HashType.CRC16_ISOIEC144433A]);
            Assert.Equal(_crc16_kermit, hashDict[HashType.CRC16_KERMIT]);
            Assert.Equal(_crc16_lj1200, hashDict[HashType.CRC16_LJ1200]);
            Assert.Equal(_crc16_m17, hashDict[HashType.CRC16_M17]);
            Assert.Equal(_crc16_maximdow, hashDict[HashType.CRC16_MAXIMDOW]);
            Assert.Equal(_crc16_mcrf4xx, hashDict[HashType.CRC16_MCRF4XX]);
            Assert.Equal(_crc16_modbus, hashDict[HashType.CRC16_MODBUS]);
            Assert.Equal(_crc16_nrsc5, hashDict[HashType.CRC16_NRSC5]);
            Assert.Equal(_crc16_opensafetya, hashDict[HashType.CRC16_OPENSAFETYA]);
            Assert.Equal(_crc16_opensafetyb, hashDict[HashType.CRC16_OPENSAFETYB]);
            Assert.Equal(_crc16_profibus, hashDict[HashType.CRC16_PROFIBUS]);
            //Assert.Equal(_crc16_riello, hashDict[HashType.CRC16_RIELLO]);
            Assert.Equal(_crc16_spifujitsu, hashDict[HashType.CRC16_SPIFUJITSU]);
            Assert.Equal(_crc16_t10dif, hashDict[HashType.CRC16_T10DIF]);
            Assert.Equal(_crc16_teledisk, hashDict[HashType.CRC16_TELEDISK]);
            //Assert.Equal(_crc16_tms37157, hashDict[HashType.CRC16_TMS37157]);
            Assert.Equal(_crc16_umts, hashDict[HashType.CRC16_UMTS]);
            Assert.Equal(_crc16_usb, hashDict[HashType.CRC16_USB]);
            Assert.Equal(_crc16_xmodem, hashDict[HashType.CRC16_XMODEM]);

            //Assert.Equal(_crc17_canfd, hashDict[HashType.CRC17_CANFD]);

            Assert.Equal(_crc21_canfd, hashDict[HashType.CRC21_CANFD]);

            //Assert.Equal(_crc24_ble, hashDict[HashType.CRC24_BLE]);
            Assert.Equal(_crc24_flexraya, hashDict[HashType.CRC24_FLEXRAYA]);
            Assert.Equal(_crc24_flexrayb, hashDict[HashType.CRC24_FLEXRAYB]);
            Assert.Equal(_crc24_interlaken, hashDict[HashType.CRC24_INTERLAKEN]);
            Assert.Equal(_crc24_ltea, hashDict[HashType.CRC24_LTEA]);
            Assert.Equal(_crc24_lteb, hashDict[HashType.CRC24_LTEB]);
            Assert.Equal(_crc24_openpgp, hashDict[HashType.CRC24_OPENPGP]);
            Assert.Equal(_crc24_os9, hashDict[HashType.CRC24_OS9]);

            Assert.Equal(_crc30_cdma, hashDict[HashType.CRC30_CDMA]);

            Assert.Equal(_crc31_philips, hashDict[HashType.CRC31_PHILIPS]);

            Assert.Equal(_crc32, hashDict[HashType.CRC32]);
            Assert.Equal(_crc32_aixm, hashDict[HashType.CRC32_AIXM]);
            Assert.Equal(_crc32_autosar, hashDict[HashType.CRC32_AUTOSAR]);
            Assert.Equal(_crc32_base91d, hashDict[HashType.CRC32_BASE91D]);
            Assert.Equal(_crc32_bzip2, hashDict[HashType.CRC32_BZIP2]);
            Assert.Equal(_crc32_cdromedc, hashDict[HashType.CRC32_CDROMEDC]);
            Assert.Equal(_crc32_cksum, hashDict[HashType.CRC32_CKSUM]);
            Assert.Equal(_crc32_iscsi, hashDict[HashType.CRC32_ISCSI]);
            Assert.Equal(_crc32_isohdlc, hashDict[HashType.CRC32_ISOHDLC]);
            Assert.Equal(_crc32_jamcrc, hashDict[HashType.CRC32_JAMCRC]);
            Assert.Equal(_crc32_mef, hashDict[HashType.CRC32_MEF]);
            Assert.Equal(_crc32_mpeg2, hashDict[HashType.CRC32_MPEG2]);
            Assert.Equal(_crc32_xfer, hashDict[HashType.CRC32_XFER]);

            Assert.Equal(_crc40_gsm, hashDict[HashType.CRC40_GSM]);

            Assert.Equal(_crc64, hashDict[HashType.CRC64]);
            Assert.Equal(_crc64_ecma182, hashDict[HashType.CRC64_ECMA182]);
            Assert.Equal(_crc64_goiso, hashDict[HashType.CRC64_GOISO]);
            Assert.Equal(_crc64_ms, hashDict[HashType.CRC64_MS]);
            Assert.Equal(_crc64_nvme, hashDict[HashType.CRC64_NVME]);
            Assert.Equal(_crc64_redis, hashDict[HashType.CRC64_REDIS]);
            Assert.Equal(_crc64_we, hashDict[HashType.CRC64_WE]);
            Assert.Equal(_crc64_xz, hashDict[HashType.CRC64_XZ]);

            Assert.Equal(_fletcher16, hashDict[HashType.Fletcher16]);
            Assert.Equal(_fletcher32, hashDict[HashType.Fletcher32]);

            Assert.Equal(_md5, hashDict[HashType.MD5]);

#if NETFRAMEWORK
            Assert.Equal(_ripemd160, hashDict[HashType.RIPEMD160]);
#endif

            Assert.Equal(_sha1, hashDict[HashType.SHA1]);
            Assert.Equal(_sha256, hashDict[HashType.SHA256]);
            Assert.Equal(_sha384, hashDict[HashType.SHA384]);
            Assert.Equal(_sha512, hashDict[HashType.SHA512]);
#if NET8_0_OR_GREATER
            if (System.Security.Cryptography.SHA3_256.IsSupported)
                Assert.Equal(_sha3_256, hashDict[HashType.SHA3_256]);
            if (System.Security.Cryptography.SHA3_384.IsSupported)
                Assert.Equal(_sha3_384, hashDict[HashType.SHA3_384]);
            if (System.Security.Cryptography.SHA3_512.IsSupported)
                Assert.Equal(_sha3_512, hashDict[HashType.SHA3_512]);
            if (System.Security.Cryptography.Shake128.IsSupported)
                Assert.Equal(_shake128, hashDict[HashType.SHAKE128]);
            if (System.Security.Cryptography.Shake256.IsSupported)
                Assert.Equal(_shake256, hashDict[HashType.SHAKE256]);
#endif

            Assert.Equal(_spamsum, hashDict[HashType.SpamSum]);

#if NET462_OR_GREATER || NETCOREAPP
            Assert.Equal(_xxhash32, hashDict[HashType.XxHash32]);
            Assert.Equal(_xxhash64, hashDict[HashType.XxHash64]);
            Assert.Equal(_xxhash3, hashDict[HashType.XxHash3]);
            Assert.Equal(_xxhash128, hashDict[HashType.XxHash128]);
#endif
        }
    }
}