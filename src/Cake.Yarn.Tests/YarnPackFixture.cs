using System;
using Cake.Testing.Fixtures;

namespace Cake.Yarn.Tests
{
    public class YarnPackFixture : ToolFixture<YarnPackSettings>
    {
        public YarnPackFixture() : base("yarn")
        {
        }

        public Action<YarnPackSettings> PackSettings { get; set; }

        protected override void RunTool()
        {
            var tool = new YarnRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Pack(PackSettings);
        }
    }
}
