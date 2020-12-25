using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using System;
using System.Collections.Generic;
using System.IO;

namespace Cake.Yarn
{
    /// <summary>
    /// Yarn Runner configuration
    /// </summary>
    public interface IYarnRunnerConfiguration
    {
        /// <summary>
        /// Sets the working directory for yarn commands
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IYarnRunnerCommands FromPath(DirectoryPath path);
    }

    /// <summary>
    /// A wrapper around the Yarn package manager
    /// </summary>
    public class YarnRunner : Tool<YarnRunnerSettings>, IYarnRunnerCommands, IYarnRunnerConfiguration
    {
        private readonly IFileSystem _fileSystem;
        private DirectoryPath _workingDirectoryPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="YarnRunner" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system</param>
        /// <param name="environment">The environment</param>
        /// <param name="processRunner">The process runner</param>
        /// <param name="toolLocator">The tool locator</param>
        public YarnRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator toolLocator)
            : base(fileSystem, environment, processRunner, toolLocator)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Sets the working directory for yarn commands
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public IYarnRunnerCommands FromPath(DirectoryPath path)
        {
            _workingDirectoryPath = path;
            return this;
        }

        #region yarn install

        /// <summary>
        /// execute 'yarn install' with options
        /// </summary>
        /// <param name="configure">options when running 'yarn install'</param>
        /// <example>
        /// <para>Run 'yarn install'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-FromPath")
        ///     .Does(() =>
        /// {
        ///     Yarn.FromPath("./dir-with-packagejson").Install();
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'yarn install'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Install")
        ///     .Does(() =>
        /// {
        ///     Yarn.Install();
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public IYarnRunnerCommands Install(Action<YarnInstallSettings> configure = null)
        {
            var settings = new YarnInstallSettings();
            configure?.Invoke(settings);

            var args = GetYarnInstallArguments(settings);

            Run(settings, args);
            return this;
        }

        private static ProcessArgumentBuilder GetYarnInstallArguments(YarnInstallSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            settings?.Evaluate(args);
            return args;
        }

        #endregion

        #region yarn add

        /// <summary>
        /// execute 'yarn add' with options
        /// </summary>
        /// <param name="configure">options when running 'yarn install'</param>
        /// <example>
        /// <para>Run 'yarn add gulp'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Add-Gulp")
        ///     .Does(() =>
        /// {
        ///     Yarn.Add(settings => settings.Package("gulp"));
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'yarn global add gulp'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Add-Gulp")
        ///     .Does(() =>
        /// {
        ///     Yarn.Add(settings => settings.Package("gulp").Globally());
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public IYarnRunnerCommands Add(Action<YarnAddSettings> configure = null)
        {
            var settings = new YarnAddSettings();
            configure?.Invoke(settings);

            var args = GetYarnAddArguments(settings);

            Run(settings, args);
            return this;
        }

        private static ProcessArgumentBuilder GetYarnAddArguments(YarnAddSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            if (settings != null && settings.Global)
            {
                args.Append("global");
            }
            settings?.Evaluate(args);
            return args;
        }

        #endregion

        #region yarn pack

        /// <summary>
        /// execute 'yarn pack' with options
        /// </summary>
        /// <param name="packSettings">options when running 'yarn pack'</param>
        /// <example>
        /// <para>Run 'yarn pack'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Pack")
        ///     .Does(() =>
        /// {
        ///     Yarn.Pack();
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <example>
        /// <para>Run 'yarn pack --filename filename'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Pack")
        ///     .Does(() =>
        /// {
        ///     Yarn.Pack(settings => settings.Named("Filename"));
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public IYarnRunnerCommands Pack(Action<YarnPackSettings> packSettings = null)
        {
            var settings = new YarnPackSettings();
            packSettings?.Invoke(settings);
            var args = GetYarnPackArguments(settings);

            Run(settings, args);

            return this;
        }

        private static ProcessArgumentBuilder GetYarnPackArguments(YarnPackSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            settings?.Evaluate(args);
            return args;
        }
        #endregion

        #region yarn remove

        /// <summary>
        /// execute 'yarn remove' with options
        /// </summary>
        /// <param name="configure">options when running 'yarn remove'</param>
        /// <example>
        /// <para>Run 'yarn remove gulp'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Remove-Gulp")
        ///     .Does(() =>
        /// {
        ///     Yarn.Remove(settings => settings.Package("gulp"));
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'yarn global remove gulp'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-remove-Gulp")
        ///     .Does(() =>
        /// {
        ///     Yarn.Remove(settings => settings.Package("gulp").Globally());
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public IYarnRunnerCommands Remove(Action<YarnRemoveSettings> configure = null)
        {
            var settings = new YarnRemoveSettings();
            configure?.Invoke(settings);

            var args = GetYarnRemoveArguments(settings);

            Run(settings, args);
            return this;
        }

        private static ProcessArgumentBuilder GetYarnRemoveArguments(YarnRemoveSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            if (settings != null && settings.Global)
            {
                args.Append("global");
            }
            settings?.Evaluate(args);
            return args;
        }

        #endregion

        #region yarn run

        /// <summary>
        /// execute 'yarn run' with arguments
        /// </summary>
        /// <param name="scriptName">name of the script to run</param>
        /// <param name="configure"></param>
        /// <example>
        /// <para>Run 'yarn run hello'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Run")
        ///     .Does(() =>
        /// {
        ///     Yarn.RunScript("hello");
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public IYarnRunnerCommands RunScript(string scriptName, Action<YarnRunSettings> configure = null)
        {
            var settings = new YarnRunSettings(scriptName);
            configure?.Invoke(settings);
            var args = GetYarnRunArguments(settings);

            Run(settings, args);
            return this;
        }

        private static ProcessArgumentBuilder GetYarnRunArguments(YarnRunSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            settings?.Evaluate(args);
            return args;
        }

        #endregion

        #region yarn cache

        /// <summary>
        /// execute 'yarn cache' with arguments
        /// </summary>
        /// <param name="subCommand">subcommand of cache to run </param>
        /// <param name="configure"></param>
        /// <example>
        /// <para>Run 'yarn cache clean'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Clean")
        ///     .Does(() =>
        /// {
        ///     Yarn.Cache("clean");
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public IYarnRunnerCommands Cache(string subCommand, Action<YarnCacheSettings> configure = null)
        {
            var settings = new YarnCacheSettings(subCommand);
            configure?.Invoke(settings);
            var args = GetYarnCacheArguments(settings);

            Run(settings, args);
            return this;
        }

        private static ProcessArgumentBuilder GetYarnCacheArguments(YarnCacheSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            settings?.Evaluate(args);
            return args;
        }

        #endregion

        #region yarn version

        /// <summary>
        /// execute 'yarn version' with options
        /// </summary>
        /// <param name="versionSettings">options when running 'yarn version'</param>
        /// <example>
        /// <para>Run 'yarn version'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Version")
        ///     .Does(() =>
        /// {
        ///     Yarn.Version();
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <example>
        /// <para>Run 'yarn version --new-version 0.1.0'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Set-Version")
        ///     .Does(() =>
        /// {
        ///     Yarn.Version(settings => settings.SetVersion("0.1.0"));
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <example>
        /// <para>Run 'yarn version --no-git-tag-version'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Version")
        ///     .Does(() =>
        /// {
        ///     Yarn.Version(settings => settings.DisableGitTagCreation());
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public IYarnRunnerCommands Version(Action<YarnVersionSettings> versionSettings = null)
        {
            var settings = new YarnVersionSettings();
            versionSettings?.Invoke(settings);
            var args = GetYarnVersionArguments(settings);

            Run(settings, args);

            return this;
        }

        private static ProcessArgumentBuilder GetYarnVersionArguments(YarnVersionSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            settings?.Evaluate(args);
            return args;
        }

        #endregion

        #region yarn audit

        /// <summary>
        /// execute 'yarn audit' with options
        /// </summary>
        /// <param name="auditSettings">options when running 'yarn audit'</param>
        /// <example>
        /// <para>Run 'yarn audit'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Audit")
        ///     .Does(() =>
        /// {
        ///     Yarn.Audit();
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <example>
        /// <para>Run 'yarn audit --verbose'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Set-Verbose")
        ///     .Does(() =>
        /// {
        ///     Yarn.Audit(settings => settings.SetVerbose());
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <example>
        /// <para>Run 'yarn audit --json'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Set-Json")
        ///     .Does(() =>
        /// {
        ///     Yarn.Audit(settings => settings.SetJson());
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public IYarnRunnerCommands Audit(Action<YarnAuditSettings> auditSettings = null)
        {
            var settings = new YarnAuditSettings();
            auditSettings?.Invoke(settings);
            var args = GetYarnAuditArguments(settings);

            Run(settings, args);
            return this;

        }

        private static ProcessArgumentBuilder GetYarnAuditArguments(YarnAuditSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            settings?.Evaluate(args);
            return args;
        }

        #endregion

        #region yarn publish

        /// <summary>
        /// execute 'yarn publish' with options
        /// </summary>
        /// <param name="publishSettings">options when running 'yarn publish'</param>
        /// <example>
        /// <para>Run 'yarn publish'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Publish")
        ///     .Does(() =>
        /// {
        ///     Yarn.Publish();
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <example>
        /// <para>Run 'yarn publish --new-version 1.2.3'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Publish-With-New-Version")
        ///     .Does(() =>
        /// {
        ///     Yarn.Publish(settings => settings.NewVersion(1, 2, 3));
        /// });
        /// ]]>
        /// </code>
        /// </example>
        /// <example>
        /// <para>Run 'yarn publish --tag beta'</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Publish-With-Tag")
        ///     .Does(() =>
        /// {
        ///     Yarn.Publish(settings => settings.Tag("beta"));
        /// });
        /// ]]>
        /// </code>
        /// </example>
        public IYarnRunnerCommands Publish(Action<YarnPublishSettings> publishSettings = null)
        {
            var settings = new YarnPublishSettings();
            publishSettings?.Invoke(settings);
            var args = GetYarnPublishArguments(settings);

            Run(settings, args);
            return this;
        }

        private static ProcessArgumentBuilder GetYarnPublishArguments(YarnPublishSettings settings)
        {
            var args = new ProcessArgumentBuilder();
            settings?.Evaluate(args);
            return args;
        }

        #endregion

        /// <summary>
        /// Gets the name of the tool
        /// </summary>
        /// <returns>the name of the tool</returns>
        protected override string GetToolName()
        {
            return "Yarn Runner";
        }

        /// <summary>
        /// Gets the name of the tool executable
        /// </summary>
        /// <returns>The tool executable name</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "yarn.cmd";
            yield return "yarn";
        }

        /// <summary>
        /// Gets the working directory from the YarnRunnerSettings
        ///             Defaults to the currently set working directory.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>
        /// The working directory for the tool.
        /// </returns>
        protected override DirectoryPath GetWorkingDirectory(YarnRunnerSettings settings)
        {
            if (_workingDirectoryPath == null)
            {
                return base.GetWorkingDirectory(settings);
            }

            if (!_fileSystem.Exist(_workingDirectoryPath))
            {
                throw new DirectoryNotFoundException(
                    $"Working directory path not found [{_workingDirectoryPath.FullPath}]");
            }

            return _workingDirectoryPath;
        }
    }
}
