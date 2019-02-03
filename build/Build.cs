using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
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

    public static int Main ()
    {
        string path = System.IO.Path.Combine(RootDirectory, ".nuke");
        Console.Write("Path to .nuke file: ");
        Console.WriteLine(path);

        Console.Write("Contents of .nuke file: ");
        try
        {
            string str = File.ReadAllLines(path).First();
            Console.WriteLine(str);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to read content of .nuke file:");
            Console.WriteLine(ex.ToString());
        }
        
        return Execute<Build>(x => x.Publish);
    }

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;

    AbsolutePath OutputDirectory => RootDirectory / "output";
    
    AbsolutePath TestResultsDirectory => RootDirectory / "test_results";

    Target Clean => _ => _
        .Executes(() =>
        {
            EnsureCleanDirectory(OutputDirectory);
            EnsureCleanDirectory(TestResultsDirectory);
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
                .SetProject(Solution.GetProject("DotNet.Project.LaunchSettings"))
                .SetConfiguration(Configuration)
                .SetOutput(OutputDirectory)
                .EnableNoBuild());
        });
}
