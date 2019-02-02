namespace DotNet.Project.LaunchSettings
{
    using System.IO;
    using Newtonsoft.Json;

    public abstract class LaunchSettings
    {
        private readonly JsonSerializer _jsonSerializer = JsonSerializer.Create();

        public Profiles GetProfiles()
        {
            var jsonTextReader = GetJsonTextReader();
            var profiles = _jsonSerializer.Deserialize<Profiles>(jsonTextReader);
            return profiles;
        }

        private JsonTextReader GetJsonTextReader()
        {
            var jsonTextReader = new JsonTextReader(GetReader());
            return jsonTextReader;
        }

        protected abstract TextReader GetReader();
    }
}