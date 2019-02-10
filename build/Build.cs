using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[CheckBuildProjectConfigurations]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Pack);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution("DotNet.Project.LaunchSettings.sln")] readonly Solution Solution;

    AbsolutePath OutputDirectory => RootDirectory / "output";

    AbsolutePath TestResultsDirectory => RootDirectory / "test_results";

    AbsolutePath PackageDirectory => RootDirectory / "package";

    AbsolutePath NuspecFile => RootDirectory / "DotNet.Project.LaunchSettings.nuspec";

    readonly string Project = "DotNet.Project.LaunchSettings";

    Target Clean => _ => _
        .Executes(() =>
        {
            EnsureCleanDirectory(OutputDirectory);
            EnsureCleanDirectory(TestResultsDirectory);
            EnsureCleanDirectory(PackageDirectory);
        });

    Target Restore => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetLogger($"xunit;LogFilePath={TestResultsDirectory}\\results.xml")
                .EnableNoBuild());
        });

    Target Publish => _ => _
        .DependsOn(Test)
        .Executes(() =>
        {
            DotNetPublish(s => s
                .SetProject(Project)
                .SetConfiguration(Configuration)
                .SetOutput(OutputDirectory)
                .EnableNoBuild());
        });

    Target Pack => _ => _
        .DependsOn(Publish)
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetProject(Project)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(PackageDirectory)
                .SetProperty("NuspecFile", NuspecFile.ToString())
                .EnableNoBuild());
        });
}
