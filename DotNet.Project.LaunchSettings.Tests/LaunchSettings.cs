namespace DotNet.Project.LaunchSettings.Tests
{
    using System.IO;
    using Newtonsoft.Json;

    public abstract class LaunchSettings
    {
        public Profiles GetProfiles()
        {
            var jsonSerializer = JsonSerializer.Create();
            var jsonTextReader = new JsonTextReader(GetReader());
            var profiles = jsonSerializer.Deserialize<Profiles>(jsonTextReader);
            return profiles;
        }

        protected abstract TextReader GetReader();
    }

    public class JsonLaunchSettings : LaunchSettings
    {
        private readonly TextReader _reader;

        public JsonLaunchSettings(TextReader reader)
        {
            _reader = reader;
        }

        protected override TextReader GetReader()
        {
            return _reader;
        }
    }
}