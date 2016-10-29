using System;
using Cake.Testing.Fixtures;

namespace Cake.Yarn.Tests
{
    public class YarnAddFixture : ToolFixture<YarnAddSettings>
    {
        public YarnAddFixture() : base("yarn")
        {
        }

        public Action<YarnAddSettings> AddSettings { get; set; }

        protected override void RunTool()
        {
            var tool = new YarnRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Add(AddSettings);
        }
    }
}
