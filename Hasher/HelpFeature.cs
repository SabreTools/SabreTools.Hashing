

using SabreTools.CommandLine;

namespace Hasher
{
    /// <remarks>Copied implementation from CommandLine until printing is fixed</remarks>
    internal sealed class HelpFeature : Feature
    {
        public const string DisplayName = "Help";

        private static readonly string[] _flags = ["-?", "-h", "--help"];

        private const string _description = "Show this help";

        public HelpFeature()
            : base(DisplayName, _flags, _description)
        {
            RequiresInputs = false;
        }

        /// <inheritdoc/>
        public override bool VerifyInputs() => true;

        /// <inheritdoc/>
        public override bool Execute() => true;
    }
}
