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
        } while (LookingForCsproj(directory));
        
        return new VisualStudioLaunchSettings(directory is {} ? Path.Combine(directory, "Properties", "launchSettings.json") : "");
    }

    private static bool LookingForCsproj(string? directory)
    {
        return directory is not null && Directory.Exists(directory) && HasCsproj(directory);
    }

    private static bool HasCsproj(string directory)
    {
        return Directory.GetFiles(directory, "*.csproj", SearchOption.TopDirectoryOnly).FirstOrDefault() is null;
    }

    protected override Stream GetReader()
    {
        try
        {
            if (File.Exists(filePath))
            {
                return new FileStream(filePath, FileMode.Open, FileAccess.Read);
            }
        }
        catch (IOException)
        {
        }

        return Stream.Null;
    }
}