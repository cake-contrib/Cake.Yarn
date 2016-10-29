using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Yarn
{
    /// <summary>
    /// Yarn runner settings
    /// </summary>
    public abstract class YarnRunnerSettings : ToolSettings
    {
        /// <summary>
        /// The command being ran
        /// </summary>
        protected string Command { get; set; }

        /// <summary>
        /// Creates Yarn Runner settings
        /// </summary>
        /// <param name="command">command to run</param>
        protected YarnRunnerSettings(string command)
        {
            Command = command;
        }

        internal void Evaluate(ProcessArgumentBuilder args)
        {
            args.Append(Command);
            EvaluateCore(args);
        }

        /// <summary>
        /// Evaluate options
        /// </summary>
        /// <param name="args"></param>
        protected virtual void EvaluateCore(ProcessArgumentBuilder args)
        {
        }
    }
}