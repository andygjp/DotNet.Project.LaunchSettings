namespace DotNet.Project.LaunchSettings.Tests
{
    using System.IO;
    using FluentAssertions;
    using Xunit;

    public class LaunchSettingsTests
    {
        [Fact]
        public void GetProfiles_should_deserialize_json()
        {
            var json = StubbedProfiles.Json;
            
            var launchSettings = new JsonLaunchSettings(json);
            
            var profiles = launchSettings.GetProfiles();
            
            var actual = profiles.FirstOrEmpty();
            
            var expected = StubbedProfiles.First;
            
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void File_based_LaunchSettings_should_deserialize_correctly()
        {
            string filePath = StubbedProfiles.GetPathToTemporaryJsonFile();

            File.Exists(filePath).Should().BeTrue();
        }
    }
}