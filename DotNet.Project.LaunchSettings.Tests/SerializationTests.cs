namespace DotNet.Project.LaunchSettings.Tests;

using System.Text.Json;
using FluentAssertions;
using Xunit;

public class SerializationTests
{
    [Fact]
    public void JsonShouldBeDeserializedCorrectly()
    {
        var json = StubbedProfiles.Json;

        var profiles = JsonSerializer.Deserialize<Profiles>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        var actual = profiles.FirstOrEmpty();

        var expected = StubbedProfiles.First;

        actual.Should().BeEquivalentTo(expected);
    }
}