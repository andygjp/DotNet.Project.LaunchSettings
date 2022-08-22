namespace DotNet.Project.LaunchSettings;

using System.IO;
using System.Text.Json;

public abstract class LaunchSettings
{
    public Profiles GetProfiles()
    {
        return Deserialize() ?? Profiles.Empty;
    }

    private Profiles? Deserialize()
    {
        try
        {
            return JsonSerializer.Deserialize<Profiles>(GetReader(), Options());
        }
        catch (JsonException ex) when(NotJson(ex))
        {
            // Newtonsoft was more forgivable about what it excepted as valid input
            return Profiles.Empty;
        }
    }

    private static bool NotJson(JsonException ex)
    {
        return ex.Path is "$" && ex.LineNumber is 0 && ex.BytePositionInLine is 0;
    }

    private static JsonSerializerOptions Options()
    {
        return new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    protected abstract Stream GetReader();
}