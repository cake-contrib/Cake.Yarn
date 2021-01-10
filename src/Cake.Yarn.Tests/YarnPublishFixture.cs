using Cake.Testing.Fixtures;
using System;

namespace Cake.Yarn.Tests
{
    public class YarnPublishFixture : ToolFixture<YarnPublishSettings>
    {
        public YarnPublishFixture() : base("yarn")
        {
        }

        public Action<YarnPublishSettings> PublishSettings { get; set; }

        protected override void RunTool()
        {
            var tool = new YarnRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Publish(PublishSettings);
        }
    }
}
