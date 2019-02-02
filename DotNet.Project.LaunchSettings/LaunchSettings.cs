namespace DotNet.Project.LaunchSettings
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
}