#r "artifacts/build/Cake.Yarn.dll"

var target = Argument("target", "Default");
    
Task("Default")
    .Does(() => 
    {
        // yarn install
        Yarn.Install();

        // yarn global add gulp
        Yarn.Add(settings => settings.Package("gulp").Globally());
    
        // yarn add gulp
        Yarn.Add(settings => settings.Package("gulp"));
        
        // yarn run hello
        Yarn.RunScript("hello");

        // run yarn in another directory
        Yarn.FromPath("./usage").Install().RunScript("hello");

        // run yarn pack
        Yarn.Pack();
    });
        
//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);    