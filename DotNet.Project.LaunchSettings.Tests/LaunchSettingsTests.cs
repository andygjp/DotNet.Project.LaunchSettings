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
            
            var actual = GetFirstOrEmptyProfile(new JsonLaunchSettings(json));

            var expected = StubbedProfiles.First;
            
            actual.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public void Empty_json_should_return_empty_profile()
        {
            var json = "{ }";
            
            var actual = GetFirstOrEmptyProfile(new JsonLaunchSettings(json));

            var expected = Profile.Empty;
            
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void File_based_LaunchSettings_should_deserialize_correctly()
        {
            var filePath = StubbedProfiles.GetPathToTemporaryJsonFile();

            var actual = GetFirstOrEmptyProfile(new FileLaunchSettings(filePath));
            
            var expected = StubbedProfiles.First;
            
            actual.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public void VisualStudio_launch_settings_should_deserialize_correctly()
        {
            var actual = GetFirstOrEmptyProfile(VisualStudioLaunchSettings.FromCaller());

            var expected = Profile.Empty;
            
            actual.Should().BeEquivalentTo(expected);
        }

        private static Profile GetFirstOrEmptyProfile(LaunchSettings launchSettings)
        {
            var profiles = launchSettings.GetProfiles();

            var actual = profiles.FirstOrEmpty();
            return actual;
        }
    }
}