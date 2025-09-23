using System;
using System.Collections.Generic;
using SabreTools.Hashing;

namespace Hasher
{
    /// <summary>
    /// Set of options for the test executable
    /// </summary>
    /// TODO: Add file output
    internal sealed class Options
    {
        #region Properties

        /// <summary>
        /// Enable debug output for relevant operations
        /// </summary>
        public bool Debug { get; private set; } = false;

        /// <summary>
        /// List of all hash types to process
        /// </summary>
        public List<HashType> HashTypes { get; private set; } = [];

        /// <summary>
        /// Set of input paths to use for operations
        /// </summary>
        public List<string> InputPaths { get; private set; } = [];

        /// <summary>
        /// Print all available hashes and then quit
        /// </summary>
        /// <remarks>Ignores all other flags if found</remarks>
        public bool PrintAvailableHashes { get; private set; } = false;

        #endregion

        #region Instance Variables

        /// <summary>
        /// Special flag to enable all hash types
        /// </summary>
        /// <remarks>Skips adding hash types specified otherwise</remarks>
        private bool _allHashTypesEnabled = false;

        #endregion

        /// <summary>
        /// Parse commandline arguments into an Options object
        /// </summary>
        public static Options? ParseOptions(string[] args)
        {
            // If we have invalid arguments
            if (args == null || args.Length == 0)
                return null;

            // Create an Options object
            var options = new Options();

            // Parse the features
            int index = 0;
            for (; index < args.Length; index++)
            {
                string arg = args[index];
                bool featureFound = false;
                switch (arg)
                {
                    case "-?":
                    case "-h":
                    case "--help":
                        return null;

                    default:
                        break;
                }

                // If the flag wasn't a feature
                if (!featureFound)
                    break;
            }

            // Parse the options and paths
            for (; index < args.Length; index++)
            {
                string arg = args[index];
                switch (arg)
                {
                    case "-d":
                    case "--debug":
                        options.Debug = true;
                        break;

                    case "-l":
                    case "--list":
                        options.PrintAvailableHashes = true;
                        break;

                    case "-t":
                    case "--type":
                        string value = index + 1 < args.Length ? args[++index] : string.Empty;
                        if (value.Equals("all", StringComparison.OrdinalIgnoreCase))
                        {
                            options._allHashTypesEnabled = true;
                            break;
                        }

                        if (!options._allHashTypesEnabled)
                        {
                            HashType? hashType = value.GetHashType();
                            if (hashType != null && !options.HashTypes.Contains(hashType.Value))
                                options.HashTypes.Add(item: hashType.Value);
                        }

                        break;

                    default:
                        options.InputPaths.Add(arg);
                        break;
                }
            }

            // Validate we have any input paths to work on
            if (options.InputPaths.Count == 0)
            {
                Console.WriteLine("At least one path is required!");
                return null;
            }

            // If the all hashes flag was enabled
            if (options._allHashTypesEnabled)
            {
                options.HashTypes = [.. (HashType[])Enum.GetValues(typeof(HashType))];
            }

            // If no hash types are provided, set defaults
            if (options.HashTypes.Count == 0)
            {
                options.HashTypes.Add(HashType.CRC32);
                options.HashTypes.Add(HashType.MD5);
                options.HashTypes.Add(HashType.SHA1);
                options.HashTypes.Add(HashType.SHA256);
            }

            return options;
        }

        /// <summary>
        /// Display help text
        /// </summary>
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
    }
}
