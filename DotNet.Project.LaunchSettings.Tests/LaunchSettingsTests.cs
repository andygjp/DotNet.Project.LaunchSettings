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
            var json = 
                @"{
                    'profiles': {
                        'profile1': {
                            'commandName': 'Project',
                            'commandLineArgs': 'arg=x',
                            'workingDirectory': 'c:\\',
                            'launchBrowser': true,
                            'applicationUrl': 'index',
                            'environmentVariables': {
                                'var1': 'value1'
                            }
                        },
                        'profile2': {
                            'commandName': 'Project',
                            'commandLineArgs': 'arg=y',
                            'workingDirectory': 'c:\\',
                            'launchBrowser': false,
                            'applicationUrl': '\\',
                            'environmentVariables': {
                                'var2': 'value2'
                            }
                        }
                    }
                }";
            
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

        private class LaunchSettings
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
}