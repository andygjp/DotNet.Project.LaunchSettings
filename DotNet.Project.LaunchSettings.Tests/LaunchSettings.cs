namespace DotNet.Project.LaunchSettings.Tests
{
    using System.IO;
    using Newtonsoft.Json;

    public class LaunchSettings
    {
        private readonly string _json;

        public LaunchSettings(string json)
        {
            _json = json;
        }

        public Profiles GetProfiles()
        {
            var jsonSerializer = JsonSerializer.Create();
            var stringReader = new StringReader(_json);
            var jsonTextReader = new JsonTextReader(stringReader);
            var profiles = jsonSerializer.Deserialize<Profiles>(jsonTextReader);
            return profiles;
        }
    }
}