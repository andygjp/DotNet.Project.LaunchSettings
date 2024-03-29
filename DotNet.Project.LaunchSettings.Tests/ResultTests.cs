namespace DotNet.Project.LaunchSettings.Tests;

using FluentAssertions;
using Xunit;

public class ResultTests
{
    [Fact]
    public void SuccessfulResultsShouldCallCorrectCallback()
    {
        Profile? actual = null;

        var expected = StubbedProfiles.First;

        var result = new Result(true, expected);
        result.Match(() => { }, x => { actual = x; });

        actual.Should().Be(expected);
    }

    [Fact]
    public void SuccessfulResultsShouldReturnResult()
    {
        var expected = StubbedProfiles.First;

        var result = new Result(true, expected);

        var actual = result.Match<Profile?>(() => null, x => x);

        actual.Should().Be(expected);
    }

    [Fact]
    public void UnsuccessfulResultsShouldCallCorrectCallback()
    {
        var actual = 0;

        var result = new Result(false, null);
        result.Match(() => { actual++; }, _ => { });

        actual.Should().Be(1);
    }

    [Fact]
    public void UnsuccessfulResultsShouldReturnBackup()
    {
        var result = new Result(false, null);
        var actual = result.Match(() => -1, _ => 1);

        actual.Should().Be(-1);
    }
}