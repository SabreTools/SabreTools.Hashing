using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Hasher.Features;
using SabreTools.CommandLine;
using SabreTools.CommandLine.Features;
using SabreTools.CommandLine.Inputs;
using SabreTools.Hashing;

namespace Hasher
{
    public static class Program
    {
        #region Constants

        private const string _debugName = "debug";
        private const string _typeName = "type";

        #endregion

        public static void Main(string[] args)
        {
            // Create the command set
            var commandSet = CreateCommands();

            // If we have no args, show the help and quit
            if (args == null || args.Length == 0)
            {
                commandSet.OutputAllHelp();
                return;
            }

            // Cache the first argument and starting index
            string featureName = args[0];

            // Try processing the standalone arguments
            var topLevel = commandSet.GetTopLevel(featureName);
            switch (topLevel)
            {
                case Help help: help.ProcessArgs(args, 0, commandSet); return;
                case ListFeature lf: lf.Execute(); return;
            }

            // Loop through and process the options
            int firstFileIndex = 0;
            for (; firstFileIndex < args.Length; firstFileIndex++)
            {
                string arg = args[firstFileIndex];

                var input = commandSet.GetTopLevel(arg);
                if (input == null)
                    break;

                input.ProcessInput(args, ref firstFileIndex);
            }

            // Get the required variables
            bool debug = commandSet.GetBoolean(_debugName);
            List<HashType> hashTypes = GetHashTypes(commandSet.GetStringList(_typeName));

            // Loop through all of the input files
            for (int i = firstFileIndex; i < args.Length; i++)
            {
                string arg = args[i];
                PrintPathHashes(arg, hashTypes, debug);
            }
        }

        /// <summary>
        /// Create the command set for the program
        /// </summary>
        private static CommandSet CreateCommands()
        {
            List<string> header = [
                "File Hashing Program",
                string.Empty,
                "Hasher <options> file|directory ...",
                string.Empty,
            ];

            List<string> footer = [
                string.Empty,
                "If no hash types are provided, this tool will default to",
                "outputting CRC-32, MD5, SHA-1, and SHA-256.",
                "Optionally, all supported hashes can be output by",
                "specifying a value of 'all'.",
            ];

            var commandSet = new CommandSet(header, footer);

            // Standalone Options
            commandSet.Add(new Help());
            commandSet.Add(new ListFeature());

            // Hasher Options
            commandSet.Add(new FlagInput(_debugName, ["-d", "--debug"], "Enable debug mode"));
            commandSet.Add(new StringListInput(_typeName, ["-t", "--type"], "Select included hashes"));

            return commandSet;
        }
    
        /// <inheritdoc/>
        private static List<HashType> GetHashTypes(List<string> types)
        {
            List<HashType> hashTypes = [];
            if (types.Count == 0)
            {
                hashTypes.Add(HashType.CRC32);
                hashTypes.Add(HashType.MD5);
                hashTypes.Add(HashType.SHA1);
                hashTypes.Add(HashType.SHA256);
            }
            else if (types.Contains("all"))
            {
                hashTypes = [.. (HashType[])Enum.GetValues(typeof(HashType))];
            }
            else
            {
                foreach (string typeString in types)
                {
                    HashType? hashType = typeString.GetHashType();
                    if (hashType != null && !hashTypes.Contains(hashType.Value))
                        hashTypes.Add(item: hashType.Value);
                }
            }

            return hashTypes;
        }

        /// <summary>
        /// Wrapper to print hashes for a single path
        /// </summary>
        /// <param name="path">File or directory path</param>
        /// <param name="hashTypes">Set of hashes to retrieve</param>
        /// <param name="debug">Enable debug output</param>
        private static void PrintPathHashes(string path, List<HashType> hashTypes, bool debug)
        {
            Console.WriteLine($"Checking possible path: {path}");

            // Check if the file or directory exists
            if (File.Exists(path))
            {
                PrintFileHashes(path, hashTypes, debug);
            }
            else if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
                {
                    PrintFileHashes(file, hashTypes, debug);
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
        /// <param name="hashTypes">Set of hashes to retrieve</param>
        /// <param name="debug">Enable debug output</param>
        private static void PrintFileHashes(string file, List<HashType> hashTypes, bool debug)
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
                    if (debug) Console.WriteLine($"Hashes for {file} could not be retrieved");
                    return;
                }

                // Output subset of available hashes
                var builder = new StringBuilder();
                foreach (HashType hashType in hashTypes)
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
                Console.WriteLine(debug ? ex : "[Exception opening file, please try again]");
                return;
            }
        }
    }
}
