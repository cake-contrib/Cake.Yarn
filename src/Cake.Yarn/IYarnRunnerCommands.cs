using System;

namespace Cake.Yarn
{
    /// <summary>
    /// Yarn Runner command interface
    /// </summary>
    public interface IYarnRunnerCommands
    {
        /// <summary>
        /// execute 'yarn install' with options
        /// </summary>
        /// <param name="configure">options when running 'yarn install'</param>
        IYarnRunnerCommands Install(Action<YarnInstallSettings> configure = null);

        /// <summary>
        /// execute 'yarn add' with options
        /// </summary>
        /// <param name="configure">options when running 'yarn add'</param>
        IYarnRunnerCommands Add(Action<YarnAddSettings> configure = null);

        /// <summary>
        /// execute 'yarn run' with arguments
        /// </summary>
        /// <param name="scriptName">name of the </param>
        /// <param name="configure">options when running 'yarn run'</param>
        IYarnRunnerCommands RunScript(string scriptName, Action<YarnRunSettings> configure = null);
    }
}
