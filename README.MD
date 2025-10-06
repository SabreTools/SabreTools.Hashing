# SabreTools.Hashing

[![Build and Test](https://github.com/SabreTools/SabreTools.Hashing/actions/workflows/build_and_test.yml/badge.svg)](https://github.com/SabreTools/SabreTools.Hashing/actions/workflows/build_and_test.yml)

This library comprises of methods and helpers to simplify the process of getting checksums and hashes from both files and streams.

Find the link to the Nuget package [here](https://www.nuget.org/packages/SabreTools.Hashing).

## Releases

For the most recent stable build, download the latest release here: [Releases Page](https://github.com/SabreTools/SabreTools.Hashing/releases)

For the latest WIP build here: [Rolling Release](https://github.com/SabreTools/SabreTools.Hashing/releases/rolling)

## Hasher

**Hasher** is a reference implementation for hashing and checksumming features of the library, packaged as a standalone executable for all supported platforms. It will attempt to select the correct hashing types based on input and then print the calculated values to standard output.

```text
Hasher <options> file|directory ...

Available options:
  -?, -h, --help              Show this help
  -l, --list                  List all available hashes and quit
  hash                        Hash all input paths (default)
      -d, --debug                 Enable debug mode
      -t=, --type=                Select included hashes

If no hash types are provided, this tool will default to
outputting CRC-32, MD5, SHA-1, and SHA-256.
Optionally, all supported hashes can be output by
specifying a value of 'all'.
```

## Internal Implementations

All hash and checksum types here have been written to ensure compatibility across all .NET versions. Some may have been adapted to ensure this compatibility. These can be treated as reference implementations, not always optimized.

| Hash / Checksum Type | Notes |
| --- | --- |
| Adler-32 | Based on the [zlib source code](https://github.com/madler/zlib/blob/v1.2.11/adler32.c) |
| CRC | All CRC values documented [here](https://reveng.sourceforge.io/crc-catalogue/all.htm) except for CRC-82 due to bit-length restrictions |
| Fletcher | 16-, 32-, and 64-bit variants |
| FNV | 32-, and 64-bit variants; 0, 1, and 1a algorithms |
| Message Digest | MD2 and MD4 only |
| RIPEMD | 128-, 160-, 256-, and 320-bit variants |
| SpamSum | Based on the [SSDEEP source code](https://github.com/ssdeep-project/ssdeep/blob/master/fuzzy.c) |
| Tiger | 128-, 160-, and 192-bit variants; 3- and 4-pass; `0x01` and `0x80` (Tiger2) pad-initialized |
| xxHash | xxHash-32 and xxHash-64 only |

## External Implementations

External implementations of hash and checksum types may not be compatible with all .NET versions. Please see the table below for more information about support.

| Source | Hash / Checksum Types | Notes |
| --- | --- | --- |
| [Blake3.NET](https://github.com/xoofx/Blake3.NET) | BLAKE3 | Used in `net7.0` and above |
| [System.IO.Hashing](https://www.nuget.org/packages/System.IO.Hashing) | XXH3, XXH128 | Used in `net462` and above |
| [System.Security.Cryptography](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography) | MD5, SHA-1, SHA-256, SHA-384, SHA-512, SHA3-256, SHA3-384, SHA3-512, SHAKE128, SHAKE256 | Built-in library; SHA3-256, SHA3-384, SHA3-512, SHAKE128, and SHAKE256 are `net8.0` and above only for [supported platforms](https://learn.microsoft.com/en-us/dotnet/standard/security/cross-platform-cryptography) |

**Note:** If all you care about is performance, I encourage you to forego this library and use the ones listed above directly instead.
