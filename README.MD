# SabreTools.Hashing

This library comprises of methods and helpers to simplify the process of getting checksums and hashes from both files and streams. See the following table for information about where each of the various components comes from:

| Source | Hash / Checksum Types | Notes |
| --- | --- | --- |
| [Aaru.Checksums](https://github.com/aaru-dps/Aaru.Checksums) | SpamSum | Some code tweaks made to support older .NET versions |
| [Blake3.NET](https://github.com/xoofx/Blake3.NET) | BLAKE3 | Used in `net7.0` and above |
| [Compress](https://github.com/RomVault/RVWorld/tree/master/Compress) | N/A | Used for threaded hashing |
| [CRC32](https://gitlab.com/eugene77/CRC32) | CRC-32 (Multiple implementations) | Used in `net20`, `net35`, `net40`, and `net452` as primary CRC-32 calculation |
| [System.IO.Hashing](https://www.nuget.org/packages/System.IO.Hashing) | CRC-32, CRC-64, xxHash32, xxHash64, XXH3, XXH128 | Used in `net462` and above |
| [System.Security.Cryptography](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography) | MD5, RIPEMD160 (.NET Framework only), SHA-1, SHA-256, SHA-384, SHA-512 | Built-in library |

Find the link to the Nuget package [here](https://www.nuget.org/packages/SabreTools.Hashing).
