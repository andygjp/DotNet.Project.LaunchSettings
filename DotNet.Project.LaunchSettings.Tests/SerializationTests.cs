namespace DotNet.Project.LaunchSettings.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class SerializationTests
    {
        [Fact]
        public void JsonShouldBeDeserializedCorrectly()
        {
            var json = Json;

            var profiles = JsonConvert.DeserializeObject<Profiles>(json);

            var actual = profiles.FirstOrEmpty();

            var expected = new Profile("Project", "arg=x", "c:\\", true, "index",
                new Dictionary<string, string> 
                {
                    ["var1"] = "value1"
                }
            );
            
            actual.Should().BeEquivalentTo(expected);
        }

        private static string Json
        {
            get
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
                return json;
            }
        }
    }
}