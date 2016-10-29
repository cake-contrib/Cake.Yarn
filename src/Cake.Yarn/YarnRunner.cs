using System;
using System.Collections.Generic;
using System.IO;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

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
        internal YarnRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
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

        #region yarn run

        /// <summary>
        /// execute 'yarn run' with arguments
        /// </summary>
        /// <param name="scriptName">name of the </param>
        /// <param name="configure"></param>
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
