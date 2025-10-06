using System;
using System.Collections.Generic;
using SabreTools.CommandLine;

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
            string.Empty,
            "If no hash types are provided, this tool will default to",
            "outputting CRC-32, MD5, SHA-1, and SHA-256.",
            "Optionally, all supported hashes can be output by",
            "specifying a value of 'all'.",
        ];

        #endregion

        public static void Main(string[] args)
        {
            // Build the command set
            var commandSet = new CommandSet(_header, _footer);
            commandSet.Add(new HelpFeature());
            commandSet.Add(new ListFeature());
            var hashFeature = new HashFeature();
            commandSet.Add(hashFeature);

            // If there are no arguments
            if (args.Length == 0)
            {
                commandSet.OutputAllHelp();
                return;
            }

            // Cache the first argument
            string featureName = args[0];

            // Check if the feature is recognized
            var feature = commandSet.GetTopLevel(featureName);
            switch (feature)
            {
                case HelpFeature: commandSet.OutputAllHelp(); return;
                case ListFeature lf: lf.Execute(); return;
            }

            // Otherwise, process the arguments normally
            if (!hashFeature.ProcessArgs(args, 0))
            {
                commandSet.OutputAllHelp();
                return;
            }

            // If there are no valid inputs
            if (hashFeature.RequiresInputs && !hashFeature.VerifyInputs())
            {
                Console.WriteLine("At least one path is required!");
                commandSet.OutputAllHelp();
                return;
            }

            // Execute based on the options set
            hashFeature.Execute();
        }
    }
}
