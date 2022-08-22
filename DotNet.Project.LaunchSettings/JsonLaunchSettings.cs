namespace DotNet.Project.LaunchSettings;

using System.IO;
using System.Text;

public class JsonLaunchSettings : LaunchSettings
{
    private readonly string _json;

    public JsonLaunchSettings(string json) => _json = json;

    protected override Stream GetReader()
    {
        var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, 1024, leaveOpen: true);
        streamWriter.Write(_json);
        streamWriter.Flush();
        memoryStream.Position = 0;
        streamWriter.Close();
        return memoryStream;
    }
}