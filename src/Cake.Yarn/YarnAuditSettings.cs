using Cake.Core;
using Cake.Core.IO;

namespace Cake.Yarn
{
    /// <summary>
    /// Yarn audit options
    /// </summary>
    public class YarnAuditSettings : YarnRunnerSettings
    {
        /// <summary>
        /// Yarn "audit" settings
        /// </summary>
        public YarnAuditSettings() : base("audit")
        {
        }

        /// <summary>
        /// Evaluate options
        /// </summary>
        /// <param name="args"></param>
        protected override void EvaluateCore(ProcessArgumentBuilder args)
        {
            if (Json) {
                args.Append("--json");
            }

            if (Verbose)
            {
                args.Append("--verbose");
            }
        }

        /// <summary>
        /// Applies the --json parameter
        /// </summary>
        /// <returns></returns>
        public YarnAuditSettings SetJson()
        {
            Json = true;
            return this;
        }

        /// <summary>
        /// Applies the --verbose parameter
        /// </summary>
        /// <returns></returns>
        public YarnAuditSettings SetVerbose()
        {
            Verbose = true;
            return this;
        }

        /// <summary>
        /// --json
        /// </summary>
        public bool Json { get; internal set; }

        /// <summary>
        /// --verbose
        /// </summary>
        public bool Verbose { get; internal set; }
    }
}