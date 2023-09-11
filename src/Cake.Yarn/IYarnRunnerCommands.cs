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

        /// <summary>
        /// execute 'yarn pack' with options
        /// </summary>
        /// <param name="packSettings">options when running 'yarn pack'</param>
        IYarnRunnerCommands Pack(Action<YarnPackSettings> packSettings = null);

        /// <summary>
        /// execute 'yarn version' with options
        /// </summary>
        /// <param name="versionSettings">options when running 'yarn version'</param>
        IYarnRunnerCommands Version(Action<YarnVersionSettings> versionSettings = null);

        /// <summary>
        /// execute 'yarn audit' with options
        /// </summary>
        /// <param name="auditSettings">options when running 'yarn audit'</param>
        /// <returns></returns>
        IYarnRunnerCommands Audit(Action<YarnAuditSettings> auditSettings = null);

        /// <summary>
        /// execute 'yarn publish' with options
        /// </summary>
        /// <param name="publishSettings">options when running 'yarn publish'</param>
        /// <returns></returns>
        IYarnRunnerCommands Publish(Action<YarnPublishSettings> publishSettings = null);
    }
}
