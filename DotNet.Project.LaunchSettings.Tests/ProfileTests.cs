namespace DotNet.Project.LaunchSettings.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Xunit;

    public class ProfileTests
    {
        [Fact]
        public void FirstOrEmptyShouldReturnUnsetProfileIfProfilesIsEmpty()
        {
            var profiles = new Profiles(new Dictionary<string, Profile>());

            var actual = profiles.FirstOrEmpty();
            
            actual.Should().NotBeNull()
                .And.Subject
                .Should().BeEquivalentTo(new Profile());
        }
        
        [Fact]
        public void TryGetShouldReturnFalseIfProfilesIsEmpty()
        {
            var profiles = new Profiles(new Dictionary<string, Profile>());

            var actual = profiles.TryGet("does-not-exist");

            actual.Should().Be((false, default));
        }
        
        [Fact]
        public void TryGetShouldReturnTrueIfProfilesIsEmpty()
        {
            var profile = new Profile();

            var profileName = "profile1";
            
            var items = new Dictionary<string, Profile>
            {
                [profileName] = profile
            };
            
            var profiles = new Profiles(items);

            var actual = profiles.TryGet(profileName);

            actual.Should().Be((true, profile));
        }
    }
}