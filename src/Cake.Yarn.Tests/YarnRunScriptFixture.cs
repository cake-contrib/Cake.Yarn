using System;
using Cake.Testing.Fixtures;

namespace Cake.Yarn.Tests
{
    public class YarnRunScriptFixture : ToolFixture<YarnRunSettings>
    {
        internal string ScriptName { get; set; }

        public YarnRunScriptFixture(string scriptName = "build") : base("yarn")
        {
            ScriptName = scriptName;
        }

        public Action<YarnRunSettings> RunScriptSettings { get; set; }

        protected override void RunTool()
        {
            var tool = new YarnRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.RunScript(ScriptName, RunScriptSettings);
        }
    }
}
