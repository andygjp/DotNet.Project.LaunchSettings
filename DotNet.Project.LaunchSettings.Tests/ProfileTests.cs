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
        public void TryGet2ShouldReturnFalseIfProfilesIsEmpty()
        {
            var profiles = CreatEmptyProfiles();

            Result result = profiles.TryGet("does-not-exist");

            var (success, _) = result;

            success.Should().BeFalse();
        }

        [Fact]
        public void TryGet2ShouldReturnTrueIfProfilesIsEmpty()
        {
            var profileName = "profile1";

            var items = new Dictionary<string, Profile>
            {
                [profileName] = StubbedProfiles.Empty
            };
            
            var profiles = new Profiles(items);

            var result = profiles.TryGet(profileName);

            var (success, profile) = result;
            
            (success, profile).Should().Be((true, StubbedProfiles.Empty));
        }

        private static Profiles CreatEmptyProfiles()
        {
            return new Profiles(new Dictionary<string, Profile>());
        }
    }
}