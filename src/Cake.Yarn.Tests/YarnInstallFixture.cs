using System;
using Cake.Testing.Fixtures;

namespace Cake.Yarn.Tests
{
    public class YarnInstallFixture : ToolFixture<YarnInstallSettings>
    {
        public YarnInstallFixture() : base("yarn")
        {
        }

        public Action<YarnInstallSettings> InstallSettings { get; set; }

        protected override void RunTool()
        {
            var tool = new YarnRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Install(InstallSettings);
        }
    }
}
