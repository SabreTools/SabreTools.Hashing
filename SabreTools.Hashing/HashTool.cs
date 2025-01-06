using System;
using System.Collections.Generic;
using System.IO;
#if NET40_OR_GREATER || NETCOREAPP
using System.Threading.Tasks;
#endif

namespace SabreTools.Hashing
{
    /// <summary>
    /// Provides static hashing methods
    /// </summary>
    public static class HashTool
    {
        /// <summary>
        /// Get CRC-32, MD5, and SHA-1 hashes from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="size">Calculated file size on success, -1 on error</param>
        /// <param name="crc32">CRC-32 for the input file</param>
        /// <param name="md5">MD5 for the input file</param>
        /// <param name="sha1">SHA-1 for the input file</param>
        /// <returns>True if hashing was successful, false otherwise</returns>
        public static bool GetStandardHashes(string filename, out long size, out string? crc32, out string? md5, out string? sha1)
        {
            // Set all initial values
            crc32 = null; md5 = null; sha1 = null;

            // Get all file hashes
            HashType[] standardHashTypes = [HashType.CRC32, HashType.MD5, HashType.SHA1];
            var fileHashes = GetFileHashesAndSize(filename, standardHashTypes, out size);
            if (fileHashes == null)
                return false;

            // Assign the file hashes and return
            crc32 = fileHashes[HashType.CRC32];
            md5 = fileHashes[HashType.MD5];
            sha1 = fileHashes[HashType.SHA1];
            return true;
        }

        /// <summary>
        /// Get CRC-32, MD5, and SHA-1 hashes from an input stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        /// <param name="size">Calculated file size on success, -1 on error</param>
        /// <param name="crc32">CRC-32 for the input file</param>
        /// <param name="md5">MD5 for the input file</param>
        /// <param name="sha1">SHA-1 for the input file</param>
        /// <returns>True if hashing was successful, false otherwise</returns>
        public static bool GetStandardHashes(Stream stream, out long size, out string? crc32, out string? md5, out string? sha1)
        {
            // Set all initial values
            crc32 = null; md5 = null; sha1 = null;

            // Get all file hashes
            HashType[] standardHashTypes = [HashType.CRC32, HashType.MD5, HashType.SHA1];
            var fileHashes = GetStreamHashesAndSize(stream, standardHashTypes, out size);
            if (fileHashes == null)
                return false;

            // Assign the file hashes and return
            crc32 = fileHashes[HashType.CRC32];
            md5 = fileHashes[HashType.MD5];
            sha1 = fileHashes[HashType.SHA1];
            return true;
        }

        #region File Hashes Without Size

        /// <summary>
        /// Get hashes from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetFileHashes(string filename)
            => GetFileHashesAndSize(filename, out _);

        /// <summary>
        /// Get hashes from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetFileHashArrays(string filename)
            => GetFileHashArraysAndSize(filename, out _);

        /// <summary>
        /// Get a hash from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <returns>Hash on success, null on error</returns>
        public static string? GetFileHash(string filename, HashType hashType)
            => GetFileHashAndSize(filename, hashType, out _);

        /// <summary>
        /// Get a hash from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <returns>Hash on success, null on error</returns>
        public static byte[]? GetFileHashArray(string filename, HashType hashType)
            => GetFileHashArrayAndSize(filename, hashType, out _);

        /// <summary>
        /// Get hashes from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetFileHashes(string filename, HashType[] hashTypes)
            => GetFileHashesAndSize(filename, hashTypes, out _);

        /// <summary>
        /// Get hashes from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetFileHashArrays(string filename, HashType[] hashTypes)
            => GetFileHashArraysAndSize(filename, hashTypes, out _);

        #endregion

        #region File Hashes With Size

        /// <summary>
        /// Get hashes and size from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetFileHashesAndSize(string filename, out long size)
        {
            // Create a hash array for all entries
            HashType[] hashTypes = (HashType[])Enum.GetValues(typeof(HashType));

            // Return the hashes from the stream
            return GetFileHashesAndSize(filename, hashTypes, out size);
        }

        /// <summary>
        /// Get hashes and size from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetFileHashArraysAndSize(string filename, out long size)
        {
            // Create a hash array for all entries
            HashType[] hashTypes = (HashType[])Enum.GetValues(typeof(HashType));

            // Return the hashes from the stream
            return GetFileHashArraysAndSize(filename, hashTypes, out size);
        }

        /// <summary>
        /// Get a hash and size from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Hash and size on success, null on error</returns>
        public static string? GetFileHashAndSize(string filename, HashType hashType, out long size)
        {
            var hashes = GetFileHashesAndSize(filename, [hashType], out size);
            return hashes?[hashType];
        }

        /// <summary>
        /// Get a hash and size from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Hash and size on success, null on error</returns>
        public static byte[]? GetFileHashArrayAndSize(string filename, HashType hashType, out long size)
        {
            var hashes = GetFileHashArraysAndSize(filename, [hashType], out size);
            return hashes?[hashType];
        }

        /// <summary>
        /// Get hashes and size from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetFileHashesAndSize(string filename, HashType[] hashTypes, out long size)
        {
            // If the file doesn't exist, we can't do anything
            if (!File.Exists(filename))
            {
                size = -1;
                return null;
            }

            // Open the input file
            var input = File.OpenRead(filename);

            // Return the hashes from the stream
            return GetStreamHashesAndSize(input, hashTypes, out size);
        }

        /// <summary>
        /// Get hashes and size from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetFileHashArraysAndSize(string filename, HashType[] hashTypes, out long size)
        {
            // If the file doesn't exist, we can't do anything
            if (!File.Exists(filename))
            {
                size = -1;
                return null;
            }

            // Open the input file
            var input = File.OpenRead(filename);

            // Return the hashes from the stream
            return GetStreamHashArraysAndSize(input, hashTypes, out size);
        }

        #endregion

        #region Byte Array Hashes

        /// <summary>
        /// Get hashes from an input byte array
        /// </summary>
        /// <param name="input">Byte array to hash</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetByteArrayHashes(byte[] input)
        {
            // Create a hash array for all entries
            HashType[] hashTypes = (HashType[])Enum.GetValues(typeof(HashType));

            // Return the hashes from the stream
            return GetStreamHashes(new MemoryStream(input), hashTypes);
        }

        /// <summary>
        /// Get hashes from an input byte array
        /// </summary>
        /// <param name="input">Byte array to hash</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetByteArrayHashArrays(byte[] input)
        {
            // Create a hash array for all entries
            HashType[] hashTypes = (HashType[])Enum.GetValues(typeof(HashType));

            // Return the hashes from the stream
            return GetStreamHashArrays(new MemoryStream(input), hashTypes);
        }

        /// <summary>
        /// Get a hash from an input byte array
        /// </summary>
        /// <param name="input">Byte array to hash</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <returns>Hash on success, null on error</returns>
        public static string? GetByteArrayHash(byte[] input, HashType hashType)
        {
            var hashes = GetStreamHashes(new MemoryStream(input), [hashType]);
            return hashes?[hashType];
        }

        /// <summary>
        /// Get a hash from an input byte array
        /// </summary>
        /// <param name="input">Byte array to hash</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <returns>Hash on success, null on error</returns>
        public static byte[]? GetByteArrayHashArray(byte[] input, HashType hashType)
        {
            var hashes = GetStreamHashArrays(new MemoryStream(input), [hashType]);
            return hashes?[hashType];
        }

        /// <summary>
        /// Get hashes from an input byte array
        /// </summary>
        /// <param name="input">Byte array to hash</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetByteArrayHashes(byte[] input, HashType[] hashTypes)
            => GetStreamHashes(new MemoryStream(input), hashTypes);

        /// <summary>
        /// Get hashes from an input byte array
        /// </summary>
        /// <param name="input">Byte array to hash</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetByteArrayHashArrays(byte[] input, HashType[] hashTypes)
            => GetStreamHashArrays(new MemoryStream(input), hashTypes);

        #endregion

        #region Stream Hashes Without Size

        /// <summary>
        /// Get hashes from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetStreamHashes(Stream input, bool leaveOpen = false)
            => GetStreamHashesAndSize(input, leaveOpen, out _);

        /// <summary>
        /// Get hashes from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetStreamHashArrays(Stream input, bool leaveOpen = false)
            => GetStreamHashArraysAndSize(input, leaveOpen, out _);

        /// <summary>
        /// Get a hash from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <returns>Hash on success, null on error</returns>
        public static string? GetStreamHash(Stream input, HashType hashType, bool leaveOpen = false)
            => GetStreamHashAndSize(input, hashType, leaveOpen, out _);

        /// <summary>
        /// Get a hash from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <returns>Hash on success, null on error</returns>
        public static byte[]? GetStreamHashArray(Stream input, HashType hashType, bool leaveOpen = false)
            => GetStreamHashArrayAndSize(input, hashType, leaveOpen, out _);

        /// <summary>
        /// Get hashes from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetStreamHashes(Stream input, HashType[] hashTypes, bool leaveOpen = false)
            => GetStreamHashesAndSize(input, hashTypes, leaveOpen, out _);

        /// <summary>
        /// Get hashes from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetStreamHashArrays(Stream input, HashType[] hashTypes, bool leaveOpen = false)
            => GetStreamHashArraysAndSize(input, hashTypes, leaveOpen, out _);

        #endregion

        #region Stream Hashes With Size

        /// <summary>
        /// Get hashes and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetStreamHashesAndSize(Stream input, out long size)
            => GetStreamHashesAndSize(input, leaveOpen: false, out size);

        /// <summary>
        /// Get hashes and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetStreamHashesAndSize(Stream input, bool leaveOpen, out long size)
        {
            // Create a hash array for all entries
            HashType[] hashTypes = (HashType[])Enum.GetValues(typeof(HashType));

            // Get the output hashes
            return GetStreamHashesAndSize(input, hashTypes, leaveOpen, out size);
        }

        /// <summary>
        /// Get hashes and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetStreamHashArraysAndSize(Stream input, out long size)
            => GetStreamHashArraysAndSize(input, leaveOpen: false, out size);

        /// <summary>
        /// Get hashes and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetStreamHashArraysAndSize(Stream input, bool leaveOpen, out long size)
        {
            // Create a hash array for all entries
            HashType[] hashTypes = (HashType[])Enum.GetValues(typeof(HashType));

            // Get the output hashes
            return GetStreamHashArraysAndSize(input, hashTypes, leaveOpen, out size);
        }

        /// <summary>
        /// Get a hash and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Hash on success, null on error</returns>
        public static string? GetStreamHashAndSize(Stream input, HashType hashType, out long size)
            => GetStreamHashAndSize(input, hashType, leaveOpen: false, out size);

        /// <summary>
        /// Get a hash and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Hash on success, null on error</returns>
        public static string? GetStreamHashAndSize(Stream input, HashType hashType, bool leaveOpen, out long size)
        {
            var hashes = GetStreamHashesAndSize(input, [hashType], leaveOpen, out size);
            return hashes?[hashType];
        }

        /// <summary>
        /// Get a hash and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Hash on success, null on error</returns>
        public static byte[]? GetStreamHashArrayAndSize(Stream input, HashType hashType, out long size)
            => GetStreamHashArrayAndSize(input, hashType, leaveOpen: false, out size);

        /// <summary>
        /// Get a hash and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Hash on success, null on error</returns>
        public static byte[]? GetStreamHashArrayAndSize(Stream input, HashType hashType, bool leaveOpen, out long size)
        {
            var hashes = GetStreamHashArraysAndSize(input, [hashType], leaveOpen, out size);
            return hashes?[hashType];
        }

        /// <summary>
        /// Get hashes and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetStreamHashesAndSize(Stream input, HashType[] hashTypes, out long size)
            => GetStreamHashesAndSize(input, hashTypes, leaveOpen: false, out size);

        /// <summary>
        /// Get hashes and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetStreamHashesAndSize(Stream input, HashType[] hashTypes, bool leaveOpen, out long size)
        {
            // Create the output dictionary
            var hashDict = new Dictionary<HashType, string?>();

            try
            {
                // Shortcut if we have a 0-byte input
                if (input.Length == 0)
                {
                    foreach (var hashType in hashTypes)
                    {
                        hashDict[hashType] = ZeroHash.GetString(hashType);
                    }

                    size = 0;
                    return hashDict;
                }
            }
            catch { }

            // Run the hashing
            var hashers = GetStreamHashesInternal(input, hashTypes, leaveOpen, out size);
            if (hashers == null)
                return null;

            // Get the results
            foreach (var hasher in hashers)
            {
                hashDict[hasher.Key] = hasher.Value.CurrentHashString;
            }

            // Dispose of the hashers
            foreach (var hasher in hashers.Values)
            {
                hasher.Dispose();
            }

            return hashDict;
        }

        /// <summary>
        /// Get hashes and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetStreamHashArraysAndSize(Stream input, HashType[] hashTypes, out long size)
            => GetStreamHashArraysAndSize(input, hashTypes, leaveOpen: false, out size);

        /// <summary>
        /// Get hashes and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, byte[]?>? GetStreamHashArraysAndSize(Stream input, HashType[] hashTypes, bool leaveOpen, out long size)
        {
            // Create the output dictionary
            var hashDict = new Dictionary<HashType, byte[]?>();

            try
            {
                // Shortcut if we have a 0-byte input
                if (input.Length == 0)
                {
                    foreach (var hashType in hashTypes)
                    {
                        hashDict[hashType] = ZeroHash.GetBytes(hashType);
                    }

                    size = 0;
                    return hashDict;
                }
            }
            catch { }

            // Run the hashing
            var hashers = GetStreamHashesInternal(input, hashTypes, leaveOpen, out size);
            if (hashers == null)
                return null;

            // Get the results
            foreach (var hasher in hashers)
            {
                hashDict[hasher.Key] = hasher.Value.CurrentHashBytes;
            }

            // Dispose of the hashers
            foreach (var hasher in hashers.Values)
            {
                hasher.Dispose();
            }

            return hashDict;
        }

        /// <summary>
        /// Get hashes and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <param name="leaveOpen">Indicates if the source stream should be left open after hashing</param>
        /// <param name="size">Amount of bytes read during hashing</param>
        /// <returns></returns>
        private static Dictionary<HashType, HashWrapper>? GetStreamHashesInternal(Stream input, HashType[] hashTypes, bool leaveOpen, out long size)
        {
            // Create the output dictionary and size counter
            var hashDict = new Dictionary<HashType, string?>();
            size = 0;

            try
            {
                // Get a list of hashers to run over the buffer
                var hashers = new Dictionary<HashType, HashWrapper>();

                // Add hashers based on requested types
                foreach (HashType hashType in hashTypes)
                {
                    hashers[hashType] = new HashWrapper(hashType);
                }

                // Create the buffer for holding data
                int buffersize = 3 * 1024 * 1024;
                byte[] buffer = new byte[buffersize];
                int lastRead;

                // Hash the input data in blocks
                do
                {
                    // Load the buffer and hold the number of bytes read
                    lastRead = input.Read(buffer, 0, buffersize);
                    size += lastRead;
                    if (lastRead == 0)
                        break;

#if NET20 || NET35
                    // Run hashers sequentially on each chunk
                    foreach (var h in hashers)
                    {
                        h.Value.Process(buffer, 0, lastRead);
                    }
#else
                    // Run hashers in parallel on each chunk
                    Parallel.ForEach(hashers, h => h.Value.Process(buffer, 0, lastRead));
#endif
                }
                while (lastRead > 0);

#if NET20 || NET35
                // Finalize all hashing helpers sequentially
                foreach (var h in hashers)
                {
                    h.Value.Terminate();
                }
#else
                // Finalize all hashing helpers in parallel
                Parallel.ForEach(hashers, h => h.Value.Terminate());
#endif

                return hashers;
            }
            catch (IOException)
            {
                return null;
            }
            finally
            {
                if (!leaveOpen)
                    input.Dispose();
            }
        }

        #endregion
    }
}