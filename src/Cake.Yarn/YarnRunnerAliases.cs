using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Yarn
{
    /// <summary>
    /// Provides a wrapper around Yarn functionality within a Cake build script
    /// </summary>
    [CakeAliasCategory("Yarn")]
    public static class YarnRunnerAliases
    {
        /// <summary>
        /// Get an Yarn runner
        /// </summary>
        /// <param name="context">The context</param>
        /// <returns></returns>
        /// <example>
        /// <para>Run 'yarn install'</para>
        /// <para>Cake task:</para>
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
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Install")
        ///     .Does(() =>
        /// {
        ///     Yarn.Install();
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'yarn pack'</para>
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Pack")
        ///     .Does(() =>
        /// {
        ///     Yarn.Pack();
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'yarn add gulp'</para>
        /// <para>Cake task:</para>
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
        /// <para>Cake task:</para>
        /// <code>
        /// <![CDATA[
        /// Task("Yarn-Add-Gulp")
        ///     .Does(() =>
        /// {
        ///     Yarn.Add(settings => settings.Package("gulp").Globally());
        /// });
        /// ]]>
        /// </code>
        /// <para>Run 'yarn run hello'</para>
        /// <para>Cake task:</para>
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
        [CakePropertyAlias]
        public static YarnRunner Yarn(this ICakeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return new YarnRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        }
    }
}
