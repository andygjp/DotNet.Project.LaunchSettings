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
            
            var launchSettings = new LaunchSettings(json);
            
            var profiles = launchSettings.GetProfiles();
            
            var actual = profiles.FirstOrEmpty();
            
            var expected = StubbedProfiles.First;
            
            actual.Should().BeEquivalentTo(expected);
        }
    }
}