using System;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Yarn
{
    /// <summary>
    /// Yarn cache options
    /// </summary>
    public class YarnCacheSettings : YarnRunnerSettings
    {
        /// <summary>
        /// Yarn "cache" settings
        /// </summary>
        public YarnCacheSettings() : base("cache")
        {
        }


        /// <summary>
        /// yarn 'run' settings for the named script
        /// </summary>
        /// <param name="command">script name to execute</param>
        public YarnCacheSettings(string command) : base("cache")
        {
            SubCommand = command;
        }


        /// <summary>
        /// Name of the script to execute as defined in package.json
        /// </summary>
        public string SubCommand { get; set; } = string.Empty;

        /// <summary>
        /// Evaluate options
        /// </summary>
        /// <param name="args"></param>
        protected override void EvaluateCore(ProcessArgumentBuilder args)
        {
            if (string.IsNullOrEmpty(SubCommand))
            {
                throw new ArgumentNullException(nameof(SubCommand), "You must provide a command for 'yarn cache`!");
            }

            args.Append(SubCommand);

            base.EvaluateCore(args);
        }
    }
}
