using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Yarn
{
    /// <summary>
    /// Yarn remove options
    /// </summary>
    public class YarnRemoveSettings : YarnRunnerSettings
    {
        private readonly ISet<string> _packages = new HashSet<string>();

        /// <summary>
        /// Yarn "remove" settings
        /// </summary>
        public YarnRemoveSettings() : base("remove")
        {
        }

        /// <summary>
        /// Evaluate options
        /// </summary>
        /// <param name="args"></param>
        protected override void EvaluateCore(ProcessArgumentBuilder args)
        {
            foreach (var package in Packages)
            {
                args.Append(package);
            }
        }

        /// <summary>
        /// Remove a package by the given url
        /// </summary>
        /// <param name="url">Url to directory containing package.json (see yarn docs)</param>
        /// <returns></returns>
        public YarnRemoveSettings Package(Uri url)
        {
            if (!url.IsAbsoluteUri)
            {
                throw new UriFormatException("You must provide an absolute url to a package");
            }
            _packages.Clear();
            _packages.Add(url.AbsoluteUri);
            return this;
        }

        /// <summary>
        /// remove a package by name, with optional version/tag and scope
        /// </summary>
        /// <param name="package"></param>
        /// <param name="versionOrTag"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        public YarnRemoveSettings Package(string package, string versionOrTag = null, string scope = null)
        {
            var packageName = package;
            if (!string.IsNullOrWhiteSpace(versionOrTag))
            {
                var versionOrTagValue = versionOrTag;
                if (versionOrTagValue.Contains(" "))
                {
                    versionOrTagValue = versionOrTag.Quote();
                }
                packageName = $"{package}@{versionOrTagValue}";
            }

            if (!string.IsNullOrWhiteSpace(scope))
            {
                if (!scope.StartsWith("@"))
                {
                    throw new ArgumentException("The scope should start with @");
                }
                packageName = !string.IsNullOrWhiteSpace(scope) ? $"{scope}/{packageName}" : packageName;
            }
            
            _packages.Add(packageName);
            return this;
        }

        /// <summary>
        /// Applies the --global parameter
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public YarnRemoveSettings Globally(bool enabled = true)
        {
            Global = enabled;
            return this;
        }

        /// <summary>
        /// List of packages to install
        /// </summary>
        public IEnumerable<string> Packages => _packages;
        
        /// <summary>
        /// 'global' option
        /// </summary>
        public bool Global { get; internal set; }
    }
}