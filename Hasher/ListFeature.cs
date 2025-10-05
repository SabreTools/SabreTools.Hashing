using System;
using SabreTools.CommandLine;
using SabreTools.Hashing;

namespace Hasher
{
    internal class ListFeature : Feature
    {
        #region Feature Definition

        public const string DisplayName = "list";

        private static readonly string[] _flags = ["-l", "--list"];

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

            var hashTypes = (HashType[])Enum.GetValues(typeof(HashType));
            foreach (var hashType in hashTypes)
            {
                // Derive the parameter name
                string paramName = $"{hashType}";
                paramName = paramName.Replace("-", string.Empty);
                paramName = paramName.Replace(" ", string.Empty);
                paramName = paramName.Replace("/", "_");
                paramName = paramName.Replace("\\", "_");
                paramName = paramName.ToLowerInvariant();

                Console.WriteLine($"{hashType.GetHashName()?.PadRight(39, ' ')} {paramName}");
            }

            return true;
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;
    }
}