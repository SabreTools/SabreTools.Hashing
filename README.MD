# SabreTools.Hashing

This library comprises of methods and helpers to simplify the process of getting checksums and hashes from both files and streams.

## Internal Implementations

All hash and checksum types here have been written to ensure compatibility across all .NET versions. Some may have been adapted to ensure this compatibility. These can be treated as reference implementations, not always optimized.

| Hash / Checksum Type | Notes |
| --- | --- |
| CRC | All CRC values documented [here](https://reveng.sourceforge.io/crc-catalogue/all.htm#crc.legend) |
| RIPEMD | 128-, 160-, 256-, and 320-bit variants |
| Tiger | 128-, 160-, and 192-bit variants; 3- and 4-pass; `0x01` and `0x80` (Tiger2) pad-initialized |
| xxHash | xxHash-32 and xxHash-64 only |

## External Implementations

External implementations of hash and checksum types may not be compatible with all .NET versions. Please see the table below for more information about support.

| Source | Hash / Checksum Types | Notes |
| --- | --- | --- |
| [Aaru.Checksums](https://github.com/aaru-dps/Aaru.Checksums) | Adler-32, Fletcher-16, Fletcher-32, SpamSum | Some code tweaks made to support older .NET versions |
| [Blake3.NET](https://github.com/xoofx/Blake3.NET) | BLAKE3 | Used in `net7.0` and above |
| [System.IO.Hashing](https://www.nuget.org/packages/System.IO.Hashing) | XXH3, XXH128 | Used in `net462` and above |
| [System.Security.Cryptography](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography) | MD5, SHA-1, SHA-256, SHA-384, SHA-512, SHA3-256, SHA3-384, SHA3-512, SHAKE128, SHAKE256 | Built-in library; SHA3-256, SHA3-384, SHA3-512, SHAKE128, and SHAKE256 are `net8.0` and above only for [supported platforms](https://learn.microsoft.com/en-us/dotnet/standard/security/cross-platform-cryptography) |

Find the link to the Nuget package [here](https://www.nuget.org/packages/SabreTools.Hashing).
