using System;
using System.IO;
using System.Text;
using SabreTools.Hashing;
using SabreTools.Hashing.SpamSum;

namespace Hasher
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Get the options from the arguments
            var options = Options.ParseOptions(args);

            // If we have an invalid state
            if (options == null)
            {
                Options.DisplayHelp();
                return;
            }

            // If a printing option was defined
            if (options.PrintAvailableHashes)
            {
                PrintAvailableHashes();
                return;
            }
            
            // If a printing option was defined
            if (options.PrintCompareSpamSum)
            {
                PrintCompareSpamSum(options.InputSpamSumHashes[0], options.InputSpamSumHashes[1]);
                return;
            }

            // Loop through the input paths
            foreach (string inputPath in options.InputPaths)
            {
                PrintPathHashes(inputPath, options);
            }
        }

        /// <summary>
        /// Print all available hashes along with their short names
        /// </summary>
        /// TODO: Print all supported variants of names?
        private static void PrintAvailableHashes()
        {
            Console.WriteLine("Hash Name                               Parameter Name        ");
            Console.WriteLine("--------------------------------------------------------------");

            var hashTypes = (HashType[])Enum.GetValues(typeof(HashType));
            foreach (var hashType in hashTypes)
            {
                // Derive the parameter name
                string paramName = $"{hashType}";
                paramName = paramName.Replace("-", string.Empty);
                paramName = paramName.Replace(" ", string.Empty);
                paramName = paramName.Replace("/", "_");
                paramName = paramName.Replace("\\", "_");
                paramName = paramName.ToLowerInvariant();

                Console.WriteLine($"{hashType.GetHashName()?.PadRight(39, ' ')} {paramName}");
            }
        }
        
        /// <summary>
        /// Print all available hashes along with their short names
        /// </summary>
        /// TODO: Print all supported variants of names?
        private static void PrintCompareSpamSum(string stringOne, string stringTwo)
        {
            Console.WriteLine("Hash Entered                                                  ");
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine($"{stringOne}");
            Console.WriteLine($"{stringTwo}");
            Console.WriteLine("Similarity score:                                                  ");
            Console.WriteLine("--------------------------------------------------------------");
            var score = SpamSum.FuzzyCompare(stringOne, stringTwo);
            Console.WriteLine($"{score}");
        }

        /// <summary>
        /// Wrapper to print hashes for a single path
        /// </summary>
        /// <param name="path">File or directory path</param>
        /// <param name="options">User-defined options</param>
        private static void PrintPathHashes(string path, Options options)
        {
            Console.WriteLine($"Checking possible path: {path}");

            // Check if the file or directory exists
            if (File.Exists(path))
            {
                PrintFileHashes(path, options);
            }
            else if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
                {
                    PrintFileHashes(file, options);
                }
            }
            else
            {
                Console.WriteLine($"{path} does not exist, skipping...");
            }
        }

        /// <summary>
        /// Print information for a single file, if possible
        /// </summary>
        /// <param name="file">File path</param>
        /// <param name="options">User-defined options</param>
        private static void PrintFileHashes(string file, Options options)
        {
            Console.WriteLine($"Attempting to hash {file}, this may take a while...");
            Console.WriteLine();

            // If the file doesn't exist
            if (!File.Exists(file))
            {
                Console.WriteLine($"{file} does not exist, skipping...");
                return;
            }

            try
            {
                // Get all file hashes for flexibility
                var hashes = HashTool.GetFileHashes(file);
                if (hashes == null)
                {
                    if (options.Debug) Console.WriteLine($"Hashes for {file} could not be retrieved");
                    return;
                }

                // Output subset of available hashes
                var builder = new StringBuilder();
                foreach (HashType hashType in options.HashTypes)
                {
                    // TODO: Make helper to pretty-print hash type names
                    if (hashes.TryGetValue(hashType, out string? hash) && hash != null)
                        builder.AppendLine($"{hashType}: {hash}");
                }

                // Create and print the output data
                string hashData = builder.ToString();
                Console.WriteLine(hashData);
            }
            catch (Exception ex)
            {
                Console.WriteLine(options.Debug ? ex : "[Exception opening file, please try again]");
                return;
            }
        }
    }
}
