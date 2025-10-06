using System;
using System.Collections.Generic;
using Hasher.Features;
using SabreTools.CommandLine;
using SabreTools.CommandLine.Features;

namespace Hasher
{
    public static class Program
    {
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
            int index = 1;

            // Get the associated feature
            var topLevel = commandSet.GetTopLevel(featureName);
            if (topLevel == null || topLevel is not Feature feature)
            {
                index = 0;
                feature = commandSet.GetFeature(HashFeature.DisplayName)!;
            }

            // Handle default help functionality
            if (topLevel is Help helpFeature)
            {
                helpFeature.ProcessArgs(args, 0, commandSet);
                return;
            }

            // Now verify that all other flags are valid
            if (!feature.ProcessArgs(args, index))
            {
                commandSet.OutputAllHelp();
                return;
            }

            // If there are no valid inputs
            if (feature.RequiresInputs && !feature.VerifyInputs())
            {
                Console.WriteLine("At least one path is required!");
                commandSet.OutputAllHelp();
                return;
            }

            // Now execute the current feature
            if (!feature.Execute())
            {
                Console.Error.WriteLine("An error occurred during processing!");
                commandSet.OutputFeatureHelp(feature.Name);
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

            commandSet.Add(new Help());
            commandSet.Add(new ListFeature());
            commandSet.Add(new HashFeature());

            return commandSet;
        }
    
    }
}
