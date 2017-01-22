# cake-yarn

[![NuGet](https://img.shields.io/nuget/v/Cake.Yarn.svg)](https://www.nuget.org/packages/Cake.Yarn/) [![Build status](https://ci.appveyor.com/api/projects/status/dch44vu64cs7nb98?svg=true)](https://ci.appveyor.com/project/MilovanovM/cake-yarn)

## Usage

```c#
    #addin "Cake.Yarn"

    Task("Yarn")
        .Does(() =>
        {
            // yarn install
            Yarn.Install();

            // yarn global add gulp
            Yarn.Add(settings => settings.Package("gulp").Globally());

            // yarn add gulp
            Yarn.Add(settings => settings.Package("gulp"));
        });

    Task("Yarn-FromPath")
        .Does(() =>
        {
            // Yarn.FromPath(DirectoryPath).

            // yarn global add gulp (from path ./wwwroot)
            Yarn.FromPath("./wwwroot").Add(settings => settings.Package("gulp").Globally());

            // yarn add gulp (from parent path)
            Yarn.FromPath("../").Add(settings => settings.Package("gulp"));
        });

    Task("Yarn-RunScript")
        .Does(() =>
        {
            Yarn.RunScript("test");
        });
```

## Scope

Cake.Yarn currently supports the following yarn commands:

* ```yarn install```
* ```yarn add```
* ```yarn run```

My primary goal for the project is to support the build workflow I need as a .NET developer, additional features have been contributed

## Documentation

Thanks to the cakebuild.net site, documentation can be found [here](http://cakebuild.net/api/cake.yarn/)

## Tests

Cake.Yarn is covered by a set of unit tests

## I can't do _insert-command-here_

If you have feature requests please submit them as issues, or better yet as pull requests :)
