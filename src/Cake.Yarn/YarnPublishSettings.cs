namespace Cake.Yarn
{
    public class YarnPublishSettings : YarnRunnerSettings
    {
        private string _newVersion;
        private string _tag;

        public YarnPublishSettings() : base("publish")
        {
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