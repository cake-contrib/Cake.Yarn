using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Yarn
{
    /// <summary>
    /// yarn run options
    /// </summary>
    public class YarnRunSettings : YarnRunnerSettings
    {
        /// <summary>
        /// Arguments to pass to the target script
        /// </summary>
        public IList<string> Arguments { get; set; } = new List<string>();

        /// <summary>
        /// yarn 'run' settings
        /// </summary>
        public YarnRunSettings() : base("run")
        {
        }

        /// <summary>
        /// yarn 'run' settings for the named script
        /// </summary>
        /// <param name="command">script name to execute</param>
        public YarnRunSettings(string command) : base("run")
        {
            ScriptName = command;
        }

        /// <summary>
        /// Name of the script to execute as defined in package.json
        /// </summary>
        public string ScriptName { get; set; } = string.Empty;

        /// <summary>
        /// Skip Yarn console logs, other types of logs (script output) will be printed
        /// </summary>
        public bool Silent { get; set; }

        /// <summary>
        /// Evaluate options
        /// </summary>
        /// <param name="args"></param>
        protected override void EvaluateCore(ProcessArgumentBuilder args)
        {
            if (string.IsNullOrEmpty(ScriptName))
            {
                throw new ArgumentNullException(nameof(ScriptName), "You must provide a script name!");
            }

            if (Silent)
            {
                args.Append(@"--silent");
            }

            args.Append(ScriptName);

            if (Arguments.Any())
            {
                foreach (var arg in Arguments)
                {
                    args.Append(arg);
                }
            }

            base.EvaluateCore(args);
        }

        /// <summary>
        /// Add an argument
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public YarnRunSettings WithArgument(string arg)
        {
            Arguments.Add(arg);
            return this;
        }

        /// <summary>
        /// Sets Silent property to true
        /// </summary>
        public YarnRunSettings WithSilent()
        {
            Silent = true;
            return this;
        }
    }
}
