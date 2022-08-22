namespace DotNet.Project.LaunchSettings;

using System.Collections.Generic;

public class Profile
{
    internal static Profile Empty { get; } = new();

    public string? CommandName { get; init; }
    public string? CommandLineArgs { get; init; }
    public string? WorkingDirectory { get; init; }
    public bool? LaunchBrowser { get; init; }
    public string? ApplicationUrl { get; init; }
    public Dictionary<string, string> EnvironmentVariables { get; init; } = new();
}