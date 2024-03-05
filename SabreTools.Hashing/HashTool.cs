using System;
using System.Collections.Generic;
using System.IO;
#if NET40_OR_GREATER || NETCOREAPP
using System.Threading.Tasks;
#endif
using Compress.ThreadReaders;

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

        #region File Hashes Without Size

        /// <summary>
        /// Get hashes from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetFileHashes(string filename)
            => GetFileHashesAndSize(filename, out _);

        /// <summary>
        /// Get a hash from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static string? GetFileHash(string filename, HashType hashType)
        {
            var hashes = GetFileHashes(filename, [hashType]);
            return hashes?[hashType];
        }

        /// <summary>
        /// Get hashes from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetFileHashes(string filename, HashType[] hashTypes)
            => GetFileHashesAndSize(filename, hashTypes, out _);

        #endregion

        #region File Hashes With Size

        /// <summary>
        /// Get hashes and size from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetFileHashesAndSize(string filename, out long size)
        {
            // Create a hash array for all entries
            HashType[] hashTypes = (HashType[])Enum.GetValues(typeof(HashType));

            // Return the hashes from the stream
            return GetFileHashesAndSize(filename, hashTypes, out size);
        }

        /// <summary>
        /// Get a hash and size from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static string? GetFileHashAndSize(string filename, HashType hashType, out long size)
        {
            var hashes = GetFileHashesAndSize(filename, [hashType], out size);
            return hashes?[hashType];
        }

        /// <summary>
        /// Get hashes and size from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetFileHashesAndSize(string filename, HashType[] hashTypes, out long size)
        {
            // If the file doesn't exist, we can't do anything
            if (!File.Exists(filename))
            {
                size = -1;
                return null;
            }

            // Set the file size
            size = new FileInfo(filename).Length;

            // Open the input file
            var input = File.OpenRead(filename);

            // Return the hashes from the stream
            return GetStreamHashes(input, hashTypes);
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
        /// Get a hash from an input byte array
        /// </summary>
        /// <param name="input">Byte array to hash</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static string? GetByteArrayHash(byte[] input, HashType hashType)
        {
            var hashes = GetStreamHashes(new MemoryStream(input), [hashType]);
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

        #endregion

        #region Stream Hashes

        /// <summary>
        /// Get hashes from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetStreamHashes(Stream input, bool leaveOpen = false)
        {
            // Create a hash array for all entries
            HashType[] hashTypes = (HashType[])Enum.GetValues(typeof(HashType));

            // Get the output hashes
            return GetStreamHashes(input, hashTypes, leaveOpen);
        }

        /// <summary>
        /// Get a hash and size from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashType">Hash type to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static string? GetStreamHash(Stream input, HashType hashType, bool leaveOpen = false)
        {
            var hashes = GetStreamHashes(input, [hashType], leaveOpen);
            return hashes?[hashType];
        }

        /// <summary>
        /// Get hashes from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <param name="hashTypes">Array of hash types to get from the file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetStreamHashes(Stream input, HashType[] hashTypes, bool leaveOpen = false)
        {
            // Create the output dictionary
            var hashDict = new Dictionary<HashType, string?>();

            try
            {
                // Get a list of hashers to run over the buffer
                var hashers = new Dictionary<HashType, HashWrapper>();

                // Add hashers based on requested types
                foreach (HashType hashType in hashTypes)
                {
                    hashers[hashType] = new HashWrapper(hashType);
                }

                // Initialize the hashing helpers
                var loadBuffer = new ThreadLoadBuffer(input);
                int buffersize = 3 * 1024 * 1024;
                byte[] buffer0 = new byte[buffersize];
                byte[] buffer1 = new byte[buffersize];

                /*
                Please note that some of the following code is adapted from
                RomVault. This is a modified version of how RomVault does
                threaded hashing. As such, some of the terminology and code
                is the same, though variable names and comments may have
                been tweaked to better fit this code base.
                */

                // Pre load the first buffer
                long refsize = input.Length;
                int next = refsize > buffersize ? buffersize : (int)refsize;
                input.Read(buffer0, 0, next);
                int current = next;
                refsize -= next;
                bool bufferSelect = true;

                while (current > 0)
                {
                    // Trigger the buffer load on the second buffer
                    next = refsize > buffersize ? buffersize : (int)refsize;
                    if (next > 0)
                        loadBuffer.Trigger(bufferSelect ? buffer1 : buffer0, next);

                    byte[] buffer = bufferSelect ? buffer0 : buffer1;

#if NET20 || NET35
                    // Run hashers sequentially on each chunk
                    foreach (var h in hashers)
                    {
                        h.Value.Process(buffer, 0, current);
                    }
#else
                    // Run hashers in parallel on each chunk
                    Parallel.ForEach(hashers, h => h.Value.Process(buffer, 0, current));
#endif

                    // Wait for the load buffer worker, if needed
                    if (next > 0)
                        loadBuffer.Wait();

                    // Setup for the next hashing step
                    current = next;
                    refsize -= next;
                    bufferSelect = !bufferSelect;
                }

                // Finalize all hashing helpers
                loadBuffer.Finish();
#if NET20 || NET35
                foreach (var h in hashers)
                {
                    h.Value.Terminate();
                }
#else
                Parallel.ForEach(hashers, h => h.Value.Terminate());
#endif

                // Get the results
                foreach (var hasher in hashers)
                {
                    hashDict[hasher.Key] = hasher.Value.CurrentHashString;
                }

                // Dispose of the hashers
                loadBuffer.Dispose();
                foreach (var hasher in hashers.Values)
                {
                    hasher.Dispose();
                }

                return hashDict;
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