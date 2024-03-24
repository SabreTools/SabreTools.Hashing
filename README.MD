# SabreTools.Hashing

This library comprises of methods and helpers to simplify the process of getting checksums and hashes from both files and streams. See the following table for information about where each of the various components comes from:

| Source | Hash / Checksum Types | Notes |
| --- | --- | --- |
| [Aaru.Checksums](https://github.com/aaru-dps/Aaru.Checksums) | Adler-32, CRC-16 (CCITT and IBM polynomials), CRC-32, CRC-64 (Normal and Reversed Polynomials), Fletcher-16, Fletcher-32, SpamSum | Some code tweaks made to support older .NET versions, CRC-32 implementation used in `net20`, `net35`, `net40`, and `net452` |
| [Blake3.NET](https://github.com/xoofx/Blake3.NET) | BLAKE3 | Used in `net7.0` and above |
| [Compress](https://github.com/RomVault/RVWorld/tree/master/Compress) | N/A | Used for threaded hashing |
| [CRC32](https://gitlab.com/eugene77/CRC32) | CRC-32 (Multiple implementations) | |
| [System.IO.Hashing](https://www.nuget.org/packages/System.IO.Hashing) | CRC-32, CRC-64 (Normal Polynomial), xxHash32, xxHash64, XXH3, XXH128 | Used in `net462` and above |
| [System.Security.Cryptography](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography) | MD5, RIPEMD160 (.NET Framework only), SHA-1, SHA-256, SHA-384, SHA-512, SHAKE128, SHAKE256 | Built-in library; SHAKE128 and SHAKE256 are `net8.0` and above only for supported platforms |

Find the link to the Nuget package [here](https://www.nuget.org/packages/SabreTools.Hashing).
