//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

var solutionPath            = MakeAbsolute(File(Argument("solutionPath", "./src/Cake.Yarn.sln")));
var projectName             = Argument("projectName", "Cake.Yarn");

var artifacts               = MakeAbsolute(Directory(Argument("artifactPath", "./artifacts")));
var testResultsPath         = MakeAbsolute(Directory(artifacts + "./test-results"));
var testAssemblies          = new List<FilePath> { 
                                MakeAbsolute(File("./src/Cake.Yarn.Tests/bin/" + configuration + "/netcoreapp2.0/Cake.Yarn.Tests.dll"))
                              };

SolutionParserResult solution        = null;
SolutionProject project              = null;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Setup(ctx => {
    CreateDirectory(artifacts);
    
    if(!FileExists(solutionPath)) throw new Exception(string.Format("Solution file not found - {0}", solutionPath.ToString()));
    solution = ParseSolution(solutionPath.ToString());
    project = solution.Projects.FirstOrDefault(x => x.Name == projectName);
    if(project == null || !FileExists(project.Path)) throw new Exception(string.Format("Project not found in solution - {0}", projectName));
});

Task("Clean")
    .Does(() =>
{
    CleanDirectory(artifacts);
    var binDirs = GetDirectories(solutionPath.GetDirectory() + @"\src\**\bin");
    var objDirs = GetDirectories(solutionPath.GetDirectory() + @"\src\**\obj");
    CleanDirectories(binDirs);
    CleanDirectories(objDirs);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solutionPath, new NuGetRestoreSettings());
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    MSBuild(solutionPath, settings => settings
        .WithProperty("TreatWarningsAsErrors","true")
        .WithProperty("UseSharedCompilation", "false")
        .WithProperty("AutoParameterizationWebConfigConnectionStrings", "false")
        .SetVerbosity(Verbosity.Quiet)
        .SetConfiguration(configuration)
        .WithTarget("Rebuild")
    );
});

Task("Copy-Files")
    .IsDependentOn("Build")
    .Does(() => 
{
    CreateDirectory(artifacts + "/build");
    var files = GetFiles(project.Path.GetDirectory() + "/bin/" + configuration + "/" + project.Name + ".*");
    CopyFiles(files, artifacts +"/build");
});

Task("Package")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Copy-Files")
    .Does(() =>
{
    Information("Packing...");
    CreateDirectory(Directory(artifacts + "/packages"));
    var settings = new DotNetCorePackSettings
    {
        Configuration = "Release",
        OutputDirectory = Directory(artifacts + "/packages")
    };

    DotNetCorePack("./src/Cake.Yarn/Cake.Yarn.csproj", settings);
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    var projectFiles = GetFiles("./src/**/*.Tests.csproj");
    foreach(var file in projectFiles)
    {
        DotNetCoreTest(file.FullPath);
    }
    
    // CreateDirectory(testResultsPath);

    // var settings = new XUnit2Settings {
    //     XmlReportV1 = true,
    //     NoAppDomain = true,
    //     OutputDirectory = testResultsPath,
    // };
    // settings.ExcludeTrait("Category", "Integration");
    
    // XUnit2(testAssemblies, settings);
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Build")
    .IsDependentOn("Copy-Files")
    .IsDependentOn("Run-Unit-Tests")
    .IsDependentOn("Package");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
