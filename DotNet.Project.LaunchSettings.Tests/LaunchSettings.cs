namespace DotNet.Project.LaunchSettings.Tests
{
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
            var profiles = JsonConvert.DeserializeObject<Profiles>(_json);
            return profiles;
        }
    }
}