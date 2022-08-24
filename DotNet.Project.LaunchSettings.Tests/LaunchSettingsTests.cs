namespace DotNet.Project.LaunchSettings.Tests;

using System;
using System.IO;
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

        var expected = StubbedProfiles.Empty;

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Empty_content_should_return_empty_StubbedProfiles()
    {
        var empty = "";

        var actual = GetFirstOrEmptyProfile(new JsonLaunchSettings(empty));

        var expected = StubbedProfiles.Empty;

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Empty_file_should_return_empty_profile()
    {
        var filePath = Path.GetTempFileName();

        // ReSharper disable once ExplicitCallerInfoArgument
        var actual = GetFirstOrEmptyProfile(VisualStudioLaunchSettings.FromCaller(filePath));

        var expected = StubbedProfiles.Empty;

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Missing_visual_studio_launch_settings_file_should_return_empty_profile()
    {
        var filePath = "non-existent-file.json";

        // ReSharper disable once ExplicitCallerInfoArgument
        var actual = GetFirstOrEmptyProfile(VisualStudioLaunchSettings.FromCaller(filePath));

        var expected = StubbedProfiles.Empty;

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Path_to_directories_that_dont_exist_should_return_empty_profile()
    {
        var filePath = "/path/to/somewhere/that/does/not/exist/file.json";
        if (OperatingSystem.IsWindows())
        {
            filePath = "C:" + filePath;
        }

        // ReSharper disable once ExplicitCallerInfoArgument
        var actual = GetFirstOrEmptyProfile(VisualStudioLaunchSettings.FromCaller(filePath));

        var expected = StubbedProfiles.Empty;

        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void VisualStudio_launch_settings_should_deserialize_correctly()
    {
        var actual = GetFirstOrEmptyProfile(VisualStudioLaunchSettings.FromCaller());

        var expected = StubbedProfiles.First;

        actual.Should().BeEquivalentTo(expected);
    }

    private static Profile GetFirstOrEmptyProfile(LaunchSettings launchSettings)
    {
        var profiles = launchSettings.GetProfiles();

        var actual = profiles.FirstOrEmpty();
        return actual;
    }
}