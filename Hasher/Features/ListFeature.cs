using System;
using SabreTools.CommandLine;
using SabreTools.Hashing;

namespace Hasher.Features
{
    internal sealed class ListFeature : Feature
    {
        #region Feature Definition

        public const string DisplayName = "list";

        private static readonly string[] _flags = ["--list"];

        private const string _description = "List all available hashes and quit";

        #endregion

        public ListFeature()
           : base(DisplayName, _flags, _description)
        {
            RequiresInputs = false;
        }

        /// <inheritdoc/>
        /// TODO: Print all supported variants of names?
        public override bool Execute()
        {
            Console.WriteLine("Hash Name                               Parameter Name        ");
            Console.WriteLine("--------------------------------------------------------------");

            foreach (var hashType in HashType.AllHashes)
            {
                // Derive the parameter name
                string paramName = hashType.Name;
                paramName = paramName.Replace("-", string.Empty);
                paramName = paramName.Replace(" ", string.Empty);
                paramName = paramName.Replace("/", "_");
                paramName = paramName.Replace("\\", "_");
                paramName = paramName.ToLowerInvariant();

                Console.WriteLine($"{hashType.Description,-39} {paramName}");
            }

            return true;
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;
    }
}
