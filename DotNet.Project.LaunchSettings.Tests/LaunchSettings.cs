namespace DotNet.Project.LaunchSettings.Tests
{
    using System.IO;
    using Newtonsoft.Json;

    public class LaunchSettings
    {
        private readonly TextReader _reader;

        public LaunchSettings(TextReader reader)
        {
            _reader = reader;
        }

        public Profiles GetProfiles()
        {
            var jsonSerializer = JsonSerializer.Create();
            var jsonTextReader = new JsonTextReader(_reader);
            var profiles = jsonSerializer.Deserialize<Profiles>(jsonTextReader);
            return profiles;
        }
    }
}