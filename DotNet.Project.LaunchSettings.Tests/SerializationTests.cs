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
                        }
                    }
                }";

            var profiles = JsonConvert.DeserializeObject<Profiles>(json);

            var actual = profiles.FirstOrEmpty();

            var expected = new Profile
            {
                CommandName = "Project",
                CommandLineArgs = "arg=x",
                WorkingDirectory = "c:\\",
                LaunchBrowser = true,
                ApplicationUrl = "index",
                EnvironmentVariables = new Dictionary<string, string>
                {
                    ["var1"] = "value1"
                }
            };
            
            actual.Should().BeEquivalentTo(expected);
        }
    }
}