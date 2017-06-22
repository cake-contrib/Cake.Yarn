using System;
using Cake.Testing.Fixtures;

namespace Cake.Yarn.Tests
{
    public class YarnRemoveFixture : ToolFixture<YarnRemoveSettings>
    {
        public YarnRemoveFixture() : base("yarn")
        {
        }

        public Action<YarnRemoveSettings> AddSettings { get; set; }

        protected override void RunTool()
        {
            var tool = new YarnRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Remove(AddSettings);
        }
    }
}
