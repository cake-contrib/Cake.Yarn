using Cake.Core;
using Cake.Core.IO;

namespace Cake.Yarn
{
    /// <summary>
    /// Yarn version options
    /// </summary>
    public class YarnVersionSettings : YarnRunnerSettings
    {
        /// <summary>
        /// Yarn "version" settings
        /// </summary>
        public YarnVersionSettings() : base("version")
        {
        }

        /// <summary>
        /// Evaluate options
        /// </summary>
        /// <param name="args"></param>
        protected override void EvaluateCore(ProcessArgumentBuilder args)
        {
            if (!string.IsNullOrEmpty(NewVersion))
            {
                args.Append($"--new-version \"{NewVersion}\"");
            }
            if (DisableGitTagVersion) {
                args.Append("--no-git-tag-version");
            }
        }

        /// <summary>
        /// Applies the --new-version parameter
        /// </summary>
        /// <returns></returns>
        public YarnVersionSettings SetVersion(string newVersion)
        {
            NewVersion = newVersion;
            return this;
        }

        /// <summary>
        /// Applies the --no-git-tag-version parameter
        /// </summary>
        /// <returns></returns>
        public YarnVersionSettings DisableGitTagCreation()
        {
            DisableGitTagVersion = true;
            return this;
        }

        /// <summary>
        /// --new-version
        /// </summary>
        public string NewVersion { get; internal set; }

        /// <summary>
        /// --no-git-tag-version
        /// </summary>
        public bool DisableGitTagVersion { get; internal set; }
    }
}