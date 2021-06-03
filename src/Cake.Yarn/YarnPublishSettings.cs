using Cake.Core;
using Cake.Core.IO;

namespace Cake.Yarn
{
    /// <summary>
    /// Yarn publish options
    /// </summary>
    public class YarnPublishSettings : YarnRunnerSettings
    {
        private string _newVersion;
        private string _tag;

        /// <summary>
        /// Yarn "publish" settings
        /// </summary>
        public YarnPublishSettings() : base("publish")
        {
        }

        /// <summary>
        /// Evaluate options
        /// </summary>
        /// <param name="args"></param>
        protected override void EvaluateCore(ProcessArgumentBuilder args)
        {
            if (!string.IsNullOrEmpty(_newVersion))
            {
                args.Append($"--new-version {_newVersion}");
            }

            if (!string.IsNullOrEmpty(_tag))
            {
                args.Append($"--tag {_tag}");
            }
        }

        /// <summary>
        /// Applies the --new-version parameter.
        /// </summary>
        /// <param name="major">Major version</param>
        /// <param name="minor">Minor version</param>
        /// <param name="patch">Patch version</param>
        /// <returns></returns>
        public YarnPublishSettings NewVersion(int major, int minor, int patch)
        {
            _newVersion = $"{major}.{minor}.{patch}";
            return this;
        }

        /// <summary>Applies the --new-version parameter.</summary>
        /// <param name="version">The version</param>
        /// <returns></returns>
        public YarnPublishSettings NewVersion(string version)
        {
            _newVersion = version;
            return this;
        }

        /// <summary>
        /// Applies the --tag parameter
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public YarnPublishSettings Tag(string tag)
        {
            _tag = tag;
            return this;
        }
    }
}
