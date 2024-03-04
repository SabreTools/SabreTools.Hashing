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
        /// <returns>True if hashing was successful, false otherwise</returns>
        public static bool GetFileHashes(string filename, out long size, out string? crc32, out string? md5, out string? sha1)
        {
            // Set all initial values
            crc32 = null; md5 = null; sha1 = null;

            // Get all file hashes
            var fileHashes = GetFileHashes(filename, out size);
            if (fileHashes == null)
                return false;

            // Assign the file hashes and return
            crc32 = fileHashes[HashType.CRC32];
            md5 = fileHashes[HashType.MD5];
            sha1 = fileHashes[HashType.SHA1];
            return true;
        }

        // TODO: Add variants of the two following that take either a list or array of HashType that should be used

        /// <summary>
        /// Get hashes from an input file path
        /// </summary>
        /// <param name="filename">Path to the input file</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetFileHashes(string filename, out long size)
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
            return GetStreamHashes(input);
        }

        /// <summary>
        /// Get hashes from an input Stream
        /// </summary>
        /// <param name="input">Stream to hash</param>
        /// <returns>Dictionary containing hashes on success, null on error</returns>
        public static Dictionary<HashType, string?>? GetStreamHashes(Stream input)
        {
            // Create the output dictionary
            var hashDict = new Dictionary<HashType, string?>();

            try
            {
                // Get a list of hashers to run over the buffer
                var hashers = new Dictionary<HashType, HashWrapper>
                {
                    { HashType.CRC32, new HashWrapper(HashType.CRC32) },
#if NET6_0_OR_GREATER
                    { HashType.CRC64, new HashWrapper(HashType.CRC64) },
#endif
                    { HashType.MD5, new HashWrapper(HashType.MD5) },
                    { HashType.SHA1, new HashWrapper(HashType.SHA1) },
                    { HashType.SHA256, new HashWrapper(HashType.SHA256) },
                    { HashType.SHA384, new HashWrapper(HashType.SHA384) },
                    { HashType.SHA512, new HashWrapper(HashType.SHA512) },
                    { HashType.SpamSum, new HashWrapper(HashType.SpamSum) },
#if NET6_0_OR_GREATER
                    { HashType.XxHash32, new HashWrapper(HashType.XxHash32) },
                    { HashType.XxHash64, new HashWrapper(HashType.XxHash64) },
#endif
                };

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
                        h.Value.Process(buffer, current);
                    }
#else
                    // Run hashers in parallel on each chunk
                    Parallel.ForEach(hashers, h => h.Value.Process(buffer, current));
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
                hashDict[HashType.CRC32] = hashers[HashType.CRC32].CurrentHashString;
#if NET6_0_OR_GREATER
                hashDict[HashType.CRC64] = hashers[HashType.CRC64].CurrentHashString;
#endif
                hashDict[HashType.MD5] = hashers[HashType.MD5].CurrentHashString;
                hashDict[HashType.SHA1] = hashers[HashType.SHA1].CurrentHashString;
                hashDict[HashType.SHA256] = hashers[HashType.SHA256].CurrentHashString;
                hashDict[HashType.SHA384] = hashers[HashType.SHA384].CurrentHashString;
                hashDict[HashType.SHA512] = hashers[HashType.SHA512].CurrentHashString;
                hashDict[HashType.SpamSum] = hashers[HashType.SpamSum].CurrentHashString;
#if NET6_0_OR_GREATER
                hashDict[HashType.XxHash32] = hashers[HashType.XxHash32].CurrentHashString;
                hashDict[HashType.XxHash64] = hashers[HashType.XxHash64].CurrentHashString;
#endif

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
                input.Dispose();
            }
        }
    }
}