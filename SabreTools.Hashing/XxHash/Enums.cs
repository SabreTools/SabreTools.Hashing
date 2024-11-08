namespace SabreTools.Hashing.XxHash
{
    // https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h
    internal enum ErrorCode
    {
        /// <summary>
        /// OK
        /// </summary>
        XXH_OK = 0,

        /// <summary>
        /// Error
        /// </summary>
        XXH_ERROR,
    }

    // https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h
    internal enum Alignment
    {
        /// <summary>
        /// Aligned
        /// </summary>
        XXH_aligned,

        /// <summary>
        /// Possibly unaligned
        /// </summary>
        XXH_unaligned,
    }

    // https://github.com/Cyan4973/xxHash/blob/dev/xxhash.h
    internal enum VectorType
    {
        /// <summary>
        /// Portable scalar version
        /// </summary>
        XXH_SCALAR = 0,

        /// <summary>
        /// SSE2 for Pentium 4, Opteron, all x86_64.
        /// </summary>
        /// <remarks>
        /// SSE2 is also guaranteed on Windows 10, macOS, and Android x86.
        /// </remarks>
        XXH_SSE2 = 1,

        /// <summary>
        /// AVX2 for Haswell and Bulldozer
        /// </summary>
        XXH_AVX2 = 2,

        /// <summary>
        /// AVX512 for Skylake and Icelake
        /// </summary>
        XXH_AVX512 = 3,

        /// <summary>
        /// NEON for most ARMv7-A, all AArch64, and WASM SIMD128
        /// via the SIMDeverywhere polyfill provided with the
        /// Emscripten SDK.
        /// </summary>
        XXH_NEON = 4,

        /// <summary>
        /// VSX and ZVector for POWER8/z13 (64-bit)
        /// </summary>
        XXH_VSX = 5,

        /// <summary>
        /// SVE for some ARMv8-A and ARMv9-A
        /// </summary>
        XXH_SVE = 6,
    }
}