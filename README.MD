# SabreTools.Hashing

This library comprises of methods and helpers to simplify the process of getting checksums and hashes from both files and streams. See the following table for information about where each of the various components comes from:

| Source | Hash / Checksum Types | Notes |
| --- | --- | --- |
| \<**INTERNAL**\> | Standardized CRC implementations, RIPEMD-160, xxHash-32, xxHash-64 | Built for compatability, written from original specifications where possible |
| [Aaru.Checksums](https://github.com/aaru-dps/Aaru.Checksums) | Adler-32, Fletcher-16, Fletcher-32, SpamSum | Some code tweaks made to support older .NET versions |
| [Blake3.NET](https://github.com/xoofx/Blake3.NET) | BLAKE3 | Used in `net7.0` and above |
| [System.IO.Hashing](https://www.nuget.org/packages/System.IO.Hashing) | XXH3, XXH128 | Used in `net462` and above |
| [System.Security.Cryptography](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography) | MD5, SHA-1, SHA-256, SHA-384, SHA-512, SHA3-256, SHA3-384, SHA3-512, SHAKE128, SHAKE256 | Built-in library; SHA3-256, SHA3-384, SHA3-512, SHAKE128, and SHAKE256 are `net8.0` and above only for [supported platforms](https://learn.microsoft.com/en-us/dotnet/standard/security/cross-platform-cryptography) |

Find the link to the Nuget package [here](https://www.nuget.org/packages/SabreTools.Hashing).
