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
            var mainFeature = new MainFeature();
            var commandSet = CreateCommands(mainFeature);

            // If we have no args, show the help and quit
            if (args is null || args.Length == 0)
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
                // Standalone Options
                case Help help: help.ProcessArgs(args, 0, commandSet); return;
                case ListFeature lf: lf.Execute(); return;

                // Default Behavior
                default:
                    if (!mainFeature.ProcessArgs(args, 0))
                    {
                        commandSet.OutputAllHelp();
                        return;
                    }
                    else if (!mainFeature.VerifyInputs())
                    {
                        Console.Error.WriteLine("At least one input is required");
                        commandSet.OutputAllHelp();
                        return;
                    }

                    mainFeature.Execute();
                    break;
            }
        }

        /// <summary>
        /// Create the command set for the program
        /// </summary>
        private static CommandSet CreateCommands(MainFeature mainFeature)
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
            commandSet.Add(new Help(["-?", "-h", "--help"]));
            commandSet.Add(new ListFeature());

            // Hasher Options
            commandSet.Add(mainFeature.DebugInput);
            commandSet.Add(mainFeature.TypeInput);

            return commandSet;
        }
    }
}
