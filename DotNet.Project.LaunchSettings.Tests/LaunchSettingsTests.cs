namespace DotNet.Project.LaunchSettings.Tests
{
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
}