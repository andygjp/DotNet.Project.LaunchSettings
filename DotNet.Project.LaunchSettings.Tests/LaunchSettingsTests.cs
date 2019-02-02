namespace DotNet.Project.LaunchSettings.Tests
{
    using System.IO;
    using System.Runtime.CompilerServices;
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

        [Fact]
        public void VisualStudio_launch_settings_should_deserialize_correctly()
        {
            var launchSettings = VisualStudioLaunchSettings.FromCaller();
            
            var profiles = launchSettings.GetProfiles();
            
            var actual = profiles.FirstOrEmpty();

            var expected = Profile.Empty;
            
            actual.Should().BeEquivalentTo(expected);
        }
    }

    public class VisualStudioLaunchSettings : FileLaunchSettings
    {
        private VisualStudioLaunchSettings(string filePath) 
            : base(filePath)
        {
        }

        public static FileLaunchSettings FromCaller([CallerFilePath] string filePath = default)
        {
            var directory = Path.GetDirectoryName(filePath);
            var vsLaunchSettings = Path.Combine(directory, "Properties\\launchSettings.json");
            return new VisualStudioLaunchSettings(vsLaunchSettings);
        }
    }
}