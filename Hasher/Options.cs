using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SabreTools.CommandLine;
using SabreTools.CommandLine.Inputs;
using SabreTools.Hashing;

namespace Hasher
{
    /// <summary>
    /// Set of options for the test executable
    /// </summary>
    /// TODO: Add file output
    internal sealed class Options : Feature
    {
        #region Feature Definition

        public const string DisplayName = "options";

        /// <remarks>Default feature does not use flags</remarks>
        private static readonly string[] _flags = [];

        /// <remarks>Default feature does not use description</remarks>
        private const string _description = "";

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

        public Options()
            : base(DisplayName, _flags, _description)
        {
            RequiresInputs = true;

            Add(new FlagInput("debug", ["-d", "--debug"], "Enable debug mode"));
            Add(new StringListInput("type", ["-t", "--type"], "Output file hashes"));
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

            // Validate we have any input paths to work on
            if (!VerifyInputs())
            {
                Console.WriteLine("At least one path is required!");
                return false;
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
        /// Display help text
        /// </summary>
        /// TODO: Replace this with standard help output
        public static void DisplayHelp()
        {
            Console.WriteLine("File Hashing Program");
            Console.WriteLine();
            Console.WriteLine("Hasher <options> file|directory ...");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("-?, -h, --help           Display this help text and quit");
            Console.WriteLine("-d, --debug              Enable debug mode");
            Console.WriteLine("-l, --list               List all available hashes and quit");
            Console.WriteLine("-t, --type [TYPE]        Output file hashes");
            Console.WriteLine();
            Console.WriteLine("If no hash types are provided, this tool will default");
            Console.WriteLine("to outputting CRC-32, MD5, SHA-1, and SHA-256.");
            Console.WriteLine("Optionally, all supported hashes can be output");
            Console.WriteLine("by specifying a value of 'all'.");
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
