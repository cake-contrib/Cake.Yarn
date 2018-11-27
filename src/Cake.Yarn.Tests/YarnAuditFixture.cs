using System;
using Cake.Testing.Fixtures;

namespace Cake.Yarn.Tests
{
    public class YarnAuditFixture : ToolFixture<YarnAuditSettings>
    {
        public YarnAuditFixture() : base("yarn")
        {
        }

        public Action<YarnAuditSettings> AuditSettings { get; set; }

        protected override void RunTool()
        {
            var tool = new YarnRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Audit(AuditSettings);
        }
    }
}