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
            var profiles = CreatEmptyProfiles();

            var actual = profiles.FirstOrEmpty();
            
            actual.Should().NotBeNull()
                .And.Subject
                .Should().BeEquivalentTo(Profile.Empty);
        }

        [Fact]
        public void TryGetShouldReturnFalseIfProfilesIsEmpty()
        {
            var profiles = CreatEmptyProfiles();

            var actual = profiles.TryGet("does-not-exist");

            actual.Should().Be((false, default));
        }

        [Fact]
        public void TryGetShouldReturnTrueIfProfilesIsEmpty()
        {
            var profileName = "profile1";

            var items = new Dictionary<string, Profile>
            {
                [profileName] = Profile.Empty
            };
            
            var profiles = new Profiles(items);

            var actual = profiles.TryGet(profileName);

            actual.Should().Be((true, Profile.Empty));
        }

        private static Profiles CreatEmptyProfiles()
        {
            return new Profiles(new Dictionary<string, Profile>());
        }
    }
}