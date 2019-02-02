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
            
            actual.Should().BeEquivalentTo(StubbedProfiles.Empty);
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
                [profileName] = StubbedProfiles.Empty
            };
            
            var profiles = new Profiles(items);

            var actual = profiles.TryGet(profileName);

            actual.Should().Be((true, StubbedProfiles.Empty));
        }

        private static Profiles CreatEmptyProfiles()
        {
            return new Profiles(new Dictionary<string, Profile>());
        }
    }
}