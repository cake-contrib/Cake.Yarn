using Cake.Core;
using Cake.Core.IO;

namespace Cake.Yarn
{
    /// <summary>
    /// Yarn pack options
    /// </summary>
    public class YarnPackSettings : YarnRunnerSettings
    {
        /// <summary>
        /// Yarn "pack" settings
        /// </summary>
        public YarnPackSettings() : base("pack")
        {
        }

        /// <summary>
        /// Evaluate options
        /// </summary>
        /// <param name="args"></param>
        protected override void EvaluateCore(ProcessArgumentBuilder args)
        {
            if (!string.IsNullOrEmpty(Filename))
            {
                args.Append($"--filename \"{Filename}\"");
            }
        }

        /// <summary>
        /// Applies the --filename parameter
        /// </summary>
        /// <returns></returns>
        public YarnPackSettings Named(string filename)
        {
            Filename = filename;
            return this;
        }

        /// <summary>
        /// --filename
        /// </summary>
        public string Filename { get; internal set; }
    }
}
