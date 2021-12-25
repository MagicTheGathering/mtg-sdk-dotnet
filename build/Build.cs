using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.Coverlet;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
internal class Build : NukeBuild
{
    [PackageExecutable(
        packageId: "codecov.tool",
        packageExecutable: "codecov.dll",
        Framework = "net5.0")]
    private readonly Tool CodeCov;

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    private readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [GitRepository] private readonly GitRepository GitRepository;

    [GitVersion] private readonly GitVersion GitVersion;

    [Solution] private readonly Solution Solution;

    private AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    private Target Clean => _ => _
         .Executes(() =>
         {
             SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
             EnsureCleanDirectory(ArtifactsDirectory);
         });

    private Target Compile => _ => _
         .DependsOn(Restore)
         .Executes(() =>
         {
             DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .EnableNoRestore());
         });

    private Target Pack => _ => _
        .DependsOn(PublishCodeCoverage)
        .Requires(() => Configuration == Configuration.Release)
        .Executes(() =>
        {
            DotNetPack(s => s
                .EnableNoRestore()
                .EnableNoBuild()
                .SetProject(Solution.GetProject("MtgApiManager.Lib"))
                .SetConfiguration(Configuration)
                .SetOutputDirectory(ArtifactsDirectory)
                .SetVersion(GitVersion.NuGetVersionV2));
        });

    private Target PublishCodeCoverage => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            var filePath = ArtifactsDirectory / "coverage.cobertura.xml";
            CodeCov($"codecov -f {filePath} -t f7c9d882-99e0-4a44-a651-16ed7cee7bc4");
        });

    private Target Restore => _ => _
         .DependsOn(Clean)
         .Executes(() =>
         {
             DotNetRestore(s => s
                .SetProjectFile(Solution));
         });

    private AbsolutePath SourceDirectory => RootDirectory / "src";

    private Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var testSetting = new DotNetTestSettings()
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetResultsDirectory(ArtifactsDirectory)
                .SetNoBuild(true);

            var testProject = Solution.GetProject("MtgApiManager.Lib.Test");
            var assemblyPath = SourceDirectory / testProject.Name / $"bin\\{Configuration}\\net6.0\\MtgApiManager.Lib.Test.dll";

            CoverletTasks.Coverlet(s => s
                .SetTargetSettings(testSetting)
                .SetAssembly(assemblyPath)
                .SetOutput(ArtifactsDirectory / "coverage.cobertura.xml")
                .SetFormat(CoverletOutputFormat.cobertura)
                .SetExcludeByFile(@"**/Resources.Designer.cs"));
        });

    public static int Main() => Execute<Build>(x => x.Pack);
}