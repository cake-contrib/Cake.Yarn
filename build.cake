/*
 * Load additional cake addins
 */
#addin Cake.Git&version=0.17.0

/*
 * Load additional tools
 */
#tool xunit.runner.console&version=2.3.1
#tool GitVersion.CommandLine&version=3.6.5

/*
 * Commandline argument handling
 */
string target = Argument("target", "Default");
string configuration = Argument("configuration", "Release");

/*
 * Constants, initial variables
 */
string projectName = "Cake.Yarn";
string testProjectName = "Cake.Yarn.Tests";
FilePath project = $"./src/{projectName}/{projectName}.csproj";
FilePath testProject = $"./src/{testProjectName}/{testProjectName}.csproj";
DirectoryPath artifacts = "./artifacts";
GitVersion version = GitVersion();

/*
 * Helper: GetReleaseNotes
 *
 * Parses the git commits since the last tag to render some release notes
 * that will be taken into account when publishing the repository.
 */
string[] GetReleaseNotes()
{
	IEnumerable<string> changes;
	string tag;

	try
	{
		tag = GitDescribe(".", "HEAD~1", false, GitDescribeStrategy.Tags, 0);
	}
	catch(Exception)
	{
		return new string[0];
	}

	string commitRange = tag + "..HEAD";

	StartProcess("git", new ProcessSettings
	{
		RedirectStandardOutput = true,
		Silent = true,
		Arguments = $"log {commitRange} --no-merges --format=\"[%h] %s\""
	}, out changes);

	return changes.ToArray();
}

/*
 * Task: Clean
 */
Task("Clean")
	.Does(() =>
{	
	CleanDirectories($"./src/**/bin/{configuration}");
	CleanDirectories("./src/**/obj");
	CleanDirectory(artifacts);
});

/*
 * Task: Restore
 */
Task("Restore")
	.Does(() =>
{
	DotNetCoreRestore(testProject.FullPath);
});

/*
 * Task: Build
 */
Task("Build")
	.IsDependentOn("Clean")
	.Does(() =>
{
	DotNetCoreBuild(project.FullPath, new DotNetCoreBuildSettings {
		Configuration = configuration
	});
});


/*
 * Task: Test
 */
Task("Test")
	.IsDependentOn("Clean")
	.IsDependentOn("Restore")
	.Does(() =>
{
	DotNetCoreTest(testProject.FullPath);
});

/*
 * Task: Pack
 */
Task("Pack")
	.IsDependentOn("Clean")
	.IsDependentOn("Build")
	.IsDependentOn("Test")
	.Does (() =>
{
	var nugetDirectory = artifacts.Combine("nuget");

	EnsureDirectoryExists(nugetDirectory);

	DotNetCorePack(project.FullPath, new DotNetCorePackSettings {
		Configuration = configuration,
		IncludeSymbols = true,
		NoBuild = true,
		OutputDirectory = nugetDirectory,
		ArgumentCustomization = args =>
		{
			return args
				.Append($"/p:PackageVersion={version.NuGetVersion}")
				.AppendQuoted("/p:PackageReleaseNotes=" + string.Join("\n", GetReleaseNotes()));
		}
	});
});

/*
 * Task: Default
 */
Task("Default").IsDependentOn("Pack");

/*
 * Script Execution
 */
RunTarget(target);
