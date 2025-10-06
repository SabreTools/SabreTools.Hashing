using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SabreTools.CommandLine;
using SabreTools.CommandLine.Inputs;
using SabreTools.Hashing;

namespace Hasher.Features
{
    /// <summary>
    /// Set of options for the test executable
    /// </summary>
    /// TODO: Add file output
    internal sealed class HashFeature : Feature
    {
        #region Feature Definition

        public const string DisplayName = "options";

        private static readonly string[] _flags = ["hash"];

        private const string _description = "Hash all input paths (default)";

        #endregion

        #region Cached Inputs

        /// <summary>
        /// Enable debug output for relevant operations
        /// </summary>
        private bool _debug = false;

        /// <summary>
        /// List of all hash types to process
        /// </summary>
        private List<HashType> _hashTypes = [];

        #endregion

        #region Constructors

        public HashFeature()
            : base(DisplayName, _flags, _description)
        {
            RequiresInputs = true;

            Add(new FlagInput("debug", ["-d", "--debug"], "Enable debug mode"));
            Add(new StringListInput("type", ["-t", "--type"], "Select included hashes"));
        }

        #endregion

        /// <inheritdoc/>
        public override bool ProcessArgs(string[] args, int index)
        {
            // Process the arguments normally first
            if (!base.ProcessArgs(args, index))
                return false;

            // Set the debug flag for easier access
            _debug = GetBoolean("debug");

            // Get hash types list
            var hashTypes = GetStringList("type");
            if (hashTypes.Count == 0)
            {
                _hashTypes.Add(HashType.CRC32);
                _hashTypes.Add(HashType.MD5);
                _hashTypes.Add(HashType.SHA1);
                _hashTypes.Add(HashType.SHA256);
            }
            else if (hashTypes.Contains("all"))
            {
                _hashTypes = [.. (HashType[])Enum.GetValues(typeof(HashType))];
            }
            else
            {
                foreach (string typeString in hashTypes)
                {
                    HashType? hashType = typeString.GetHashType();
                    if (hashType != null && !_hashTypes.Contains(hashType.Value))
                        _hashTypes.Add(item: hashType.Value);
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => Inputs.Count > 0;

        /// <inheritdoc/>
        public override bool Execute()
        {
            foreach (string inputPath in Inputs)
            {
                PrintPathHashes(inputPath);
            }

            return true;
        }

        /// <summary>
        /// Wrapper to print hashes for a single path
        /// </summary>
        /// <param name="path">File or directory path</param>
        private void PrintPathHashes(string path)
        {
            Console.WriteLine($"Checking possible path: {path}");

            // Check if the file or directory exists
            if (File.Exists(path))
            {
                PrintFileHashes(path);
            }
            else if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path, "*", SearchOption.AllDirectories))
                {
                    PrintFileHashes(file);
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
        private void PrintFileHashes(string file)
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
                    if (_debug) Console.WriteLine($"Hashes for {file} could not be retrieved");
                    return;
                }

                // Output subset of available hashes
                var builder = new StringBuilder();
                foreach (HashType hashType in _hashTypes)
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
                Console.WriteLine(_debug ? ex : "[Exception opening file, please try again]");
                return;
            }
        }
    }
}
