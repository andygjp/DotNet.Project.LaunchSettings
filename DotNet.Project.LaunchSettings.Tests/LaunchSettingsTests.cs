namespace DotNet.Project.LaunchSettings.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class LaunchSettingsTests
    {
        [Fact]
        public void GetProfiles_should_deserialize_json()
        {
            var json = StubbedProfiles.Json;
            
            var launchSettings = new LaunchSettings(json);
            
            var profiles = launchSettings.GetProfiles();
            
            var actual = profiles.FirstOrEmpty();
            
            var expected = new Profile("Project", "arg=x", "c:\\", true, "index",
                new Dictionary<string, string> 
                {
                    ["var1"] = "value1"
                }
            );
            
            actual.Should().BeEquivalentTo(expected);
        }
    }

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