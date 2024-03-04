# SabreTools.Hashing

This library comprises of methods and helpers to simplify the process of getting checksums and checksums from both files and streams. See the following table for information about where each of the various components comes from:

| Source | Hash / Checksum Types | Notes |
| --- | --- | --- |
| [Aaru.Checksums](https://github.com/aaru-dps/Aaru.Checksums) | SpamSum | Some code tweaks made to support older .NET versions |
| [Compress](https://github.com/RomVault/RVWorld/tree/master/Compress) | N/A | Used for threaded hashing |
| [System.IO.Hashing](https://www.nuget.org/packages/System.IO.Hashing) | CRC-32, CRC-64, xxHash32, xxHash64 | Used in `net462` and above |
| [System.Security.Cryptography](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography) | MD5, SHA-1, SHA-256, SHA-384, SHA-512 | Built-in library |

Find the link to the Nuget package [here](https://www.nuget.org/packages/SabreTools.Hashing).
