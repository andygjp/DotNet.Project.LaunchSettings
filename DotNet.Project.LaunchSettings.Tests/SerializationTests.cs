namespace DotNet.Project.LaunchSettings.Tests
{
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class SerializationTests
    {
        [Fact]
        public void JsonShouldBeDeserializedCorrectly()
        {
            var json = StubbedProfiles.Json;

            var profiles = JsonConvert.DeserializeObject<Profiles>(json);

            var actual = profiles.FirstOrEmpty();

            var expected = StubbedProfiles.First;
            
            actual.Should().BeEquivalentTo(expected);
        }
    }
}