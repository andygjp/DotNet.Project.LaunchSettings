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
            var filePath = StubbedProfiles.GetPathToTemporaryJsonFile();

            var launchSettings = new FileLaunchSettings(filePath);
            
            var profiles = launchSettings.GetProfiles();
            
            var actual = profiles.FirstOrEmpty();
            
            var expected = StubbedProfiles.First;
            
            actual.Should().BeEquivalentTo(expected);
        }

        public class FileLaunchSettings : LaunchSettings
        {
            private readonly string _filePath;

            public FileLaunchSettings(string filePath)
            {
                _filePath = filePath;
            }

            protected override TextReader GetReader()
            {
                return new StreamReader(_filePath);
            }
        }
    }
}