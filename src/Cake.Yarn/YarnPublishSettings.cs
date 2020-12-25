using Cake.Core;
using Cake.Core.IO;

namespace Cake.Yarn
{
    public class YarnPublishSettings : YarnRunnerSettings
    {
        private string _newVersion;
        private string _tag;

        public YarnPublishSettings() : base("publish")
        {
        }

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

        public YarnPublishSettings NewVersion(int major, int minor, int patch)
        {
            _newVersion = $"{major}.{minor}.{patch}";
            return this;
        }

        public YarnPublishSettings Tag(string tag)
        {
            _tag = tag;
            return this;
        }
    }
}
