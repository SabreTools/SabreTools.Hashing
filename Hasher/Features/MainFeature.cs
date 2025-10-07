using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SabreTools.CommandLine;
using SabreTools.CommandLine.Inputs;
using SabreTools.Hashing;

namespace Hasher.Features
{
    internal sealed class MainFeature : Feature
    {
        #region Feature Definition

        public const string DisplayName = "main";

        /// <remarks>Flags are unused</remarks>
        private static readonly string[] _flags = [];

        /// <remarks>Description is unused</remarks>
        private const string _description = "";

        #endregion

        #region Inputs

        private const string _debugName = "debug";
        internal readonly FlagInput DebugInput = new(_debugName, ["-d", "--debug"], "Enable debug mode");

        private const string _typeName = "type";
        internal readonly StringListInput TypeInput = new(_typeName, ["-t", "--type"], "Select included hashes");

        #endregion

        public MainFeature()
            : base(DisplayName, _flags, _description)
        {
            RequiresInputs = true;

            Add(DebugInput);
            Add(TypeInput);
        }

        /// <inheritdoc/>
        public override bool Execute()
        {
            // Get the required variables
            bool debug = GetBoolean(_debugName);
            List<HashType> hashTypes = GetHashTypes(GetStringList(_typeName));

            // Loop through all of the input files
            for (int i = 0; i < Inputs.Count; i++)
            {
                string arg = Inputs[i];
                PrintPathHashes(arg, hashTypes, debug);
            }

            return true;
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;

        /// <summary>
        /// Derive a list of hash types from a list of strings
        /// </summary>
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
