using System.Collections.Generic;
using SabreTools.CommandLine;
using SabreTools.CommandLine.Features;

namespace Hasher
{
    public static class Program
    {
        #region Features

        /// <summary>
        /// Help header
        /// </summary>
        private static readonly List<string> _header = [
            "File Hashing Program",
            string.Empty,
            "Hasher <options> file|directory ...",
            string.Empty,
        ];

        /// <summary>
        /// Help footer
        /// </summary>
        private static readonly List<string> _footer = [
            "If no hash types are provided, this tool will default",
            "to outputting CRC-32, MD5, SHA-1, and SHA-256.",
            "Optionally, all supported hashes can be output",
            "by specifying a value of 'all'.",
        ];

        #endregion

        public static void Main(string[] args)
        {
            // Build the command set
            var commandSet = new CommandSet(_header, _footer);
            commandSet.Add(new Help(["-?", "-h", "--help"]));
            commandSet.Add(new ListFeature());
            var options = new Options();
            commandSet.Add(options);

            // If there are no arguments
            if (args.Length == 0)
            {
                Options.DisplayHelp();
                return;
            }

            // Cache the first argument
            string featureName = args[0];

            // Check if the feature is recognized
            var feature = commandSet.GetTopLevel(featureName);
            if (feature is Help)
            {
                Options.DisplayHelp();
                return;
            }
            else if (feature is ListFeature lf)
            {
                lf.Execute();
                return;
            }

            // Otherwise, process the arguments normally
            if (!options.ProcessArgs(args, 0))
            {
                commandSet.OutputGenericHelp();
                return;
            }

            // Execute based on the options set
            options.Execute();
        }
    }
}
