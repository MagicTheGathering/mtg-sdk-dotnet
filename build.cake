#tool "nuget:?package=OpenCover"
#tool "nuget:?package=gitreleasemanager"

var target = Argument("target", "Default");

Task("Default")
    .IsDependentOn("RunCodeCoverage");

Task("NuGetRestorePackages")
    .Does(() =>
{
    Information("Restoring nuget packeges for solution");
    NuGetRestore("./MtgApiManager.sln");
});

Task("Clean")
    .IsDependentOn("NuGetRestorePackages")
    .Does(() =>
{
    CleanDirectories(new DirectoryPath[] 
        { 
            "./MtgApiManager.Lib/bin", 
            "./MtgApiManager.Lib/obj",
            "./MtgApiManager.Lib.Test/bin",
            "./MtgApiManager.Lib.Test/obj",
            "./MtgApiManager.Lib.TestApp/bin",
            "./MtgApiManager.Lib.TestApp/obj",            
        });
});

Task("BuildSolution")
    .IsDependentOn("Clean")
    .Does(() =>
{
    MSBuild("./MtgApiManager.sln", settings => settings.SetConfiguration("Release"));
});

Task("RunCodeCoverage")
    .IsDependentOn("BuildSolution")
    .Does(() =>
{
    Information("Creating the code coverage file with OpenCover");    
    OpenCover(tool =>
        {
            tool.MSTest(
                "./MtgApiManager.Lib.Test/bin/**/MtgApiManager.Lib.Test.dll", 
                new MSTestSettings() 
                    { 
                        NoIsolation = false 
                    });
        },
        new FilePath("./MtgApiManager.Lib_coverage.xml"),
        new OpenCoverSettings() 
        { 
            Register = "user",
            SkipAutoProps = true
        }
        .WithFilter("+[MtgApiManager.Lib]*")
        .WithFilter("-[MtgApiManager.Lib]MtgApiManager.Lib.Properties.*")
        .ExcludeByAttribute("*.ExcludeFromCodeCoverage*")
    );

    Information("Upload code coverage file to CodeCov.");

    using(var process = StartAndReturnProcess("pip", new ProcessSettings
        { 
            Arguments = "install codecov",
            WorkingDirectory = MakeAbsolute(Directory("./tools")).FullPath,
            EnvironmentVariables = new Dictionary<string, string>
            {
                { "PATH", "C:\\Python35\\Scripts" },
            }           
        }))
    {
        process.WaitForExit();
        Information("Install codecov returned with code: {0}", process.GetExitCode());
    }

    using(var process = StartAndReturnProcess("codecov", new ProcessSettings
        { 
            Arguments = "-f \"../MtgApiManager.Lib_coverage.xml\"",
            WorkingDirectory = MakeAbsolute(Directory("./tools")).FullPath,          
        }))
    {
        process.WaitForExit();
        Information("Upload coverage file returned with code: {0}", process.GetExitCode());
    }
});

RunTarget(target);