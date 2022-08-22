namespace DotNet.Project.LaunchSettings;

using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

public class VisualStudioLaunchSettings : LaunchSettings
{
    private readonly string filePath;

    private VisualStudioLaunchSettings(string filePath)
    {
        this.filePath = filePath;
    }

    public static VisualStudioLaunchSettings FromCaller([CallerFilePath] string filePath = "")
    {
        string? directory = null;
        do
        {
            directory = directory is null ? Path.GetDirectoryName(filePath) : Directory.GetParent(directory)?.FullName;
        } while (directory is not null && HasCsproj(directory));
        
        return new VisualStudioLaunchSettings(directory is {} ? Path.Combine(directory, "Properties", "launchSettings.json") : "");
    }

    private static bool HasCsproj(string? directory)
    {
        return string.IsNullOrWhiteSpace(directory) is false &&
               Directory.GetFiles(directory, "*.csproj", SearchOption.TopDirectoryOnly).FirstOrDefault() is null;
    }

    protected override Stream GetReader()
    {
        try
        {
            if (File.Exists(filePath))
            {
                return new FileStream(filePath, FileMode.Open);
            }
        }
        catch (IOException)
        {
        }

        return Stream.Null;
    }
}