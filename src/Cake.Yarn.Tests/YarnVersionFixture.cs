using System;
using Cake.Testing.Fixtures;

namespace Cake.Yarn.Tests
{
    public class YarnVersionFixture : ToolFixture<YarnVersionSettings>
    {
        public YarnVersionFixture() : base("yarn")
        {
        }

        public Action<YarnVersionSettings> VersionSettings { get; set; }

        protected override void RunTool()
        {
            var tool = new YarnRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Version(VersionSettings);
        }
    }
}