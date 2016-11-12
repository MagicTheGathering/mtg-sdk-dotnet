var target = Argument("target", "Default");

Task("Default")
    .IsDependentOn("RunMSUnitTests");

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
    CleanDirectories("./MtgApiManager.Lib/bin");
    CleanDirectories("./MtgApiManager.Lib/obj");
});

Task("BuildSolution")
    .IsDependentOn("Clean")
    .Does(() =>
{
    MSBuild("./MtgApiManager.sln", settings => settings.SetConfiguration("Release"));
});

Task("RunMSUnitTests")
    .IsDependentOn("BuildSolution")
    .Does(() =>
{
    MSTest("./MtgApiManager.Lib.Test/bin/Debug/MtgApiManager.Lib.Test.dll");
});

RunTarget(target);