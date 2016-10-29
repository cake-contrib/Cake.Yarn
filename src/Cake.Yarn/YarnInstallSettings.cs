using Cake.Core;
using Cake.Core.IO;

namespace Cake.Yarn
{
    /// <summary>
    /// Yarn install options
    /// </summary>
    public class YarnInstallSettings : YarnRunnerSettings
    {
        /// <summary>
        /// Yarn "install" settings
        /// </summary>
        public YarnInstallSettings() : base("install")
        {
        }

        /// <summary>
        /// Evaluate options
        /// </summary>
        /// <param name="args"></param>
        protected override void EvaluateCore(ProcessArgumentBuilder args)
        {
            if (Production)
            {
                args.Append("--production");
            }
        }

        /// <summary>
        /// Applies the --production parameter (can not be set when installing individual packages
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public YarnInstallSettings ForProduction(bool enabled = true)
        {
            Production = enabled;
            return this;
        }

        /// <summary>
        /// --production
        /// </summary>
        public bool Production { get; internal set; }
    }
}
