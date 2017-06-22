using System;
using Cake.Testing.Fixtures;

namespace Cake.Yarn.Tests
{
    public class YarnCacheFixture : ToolFixture<YarnCacheSettings>
    {
        internal string SubCommand { get; set; }

        public YarnCacheFixture(string subCommand = "clean") : base("yarn")
        {
            SubCommand = subCommand;
        }

        public Action<YarnCacheSettings> CacheSettings { get; set; }

        protected override void RunTool()
        {
            var tool = new YarnRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Cache(SubCommand, CacheSettings);
        }
    }
}