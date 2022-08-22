namespace DotNet.Project.LaunchSettings.Tests.Folder1.Folder2;

using Xunit;
using FluentAssertions;

// Nesting this several levels down - it failed if the caller was not in the project root
public class LaunchSettingsTests
{
    [Fact]
    public void VisualStudio_launch_settings_should_deserialize_correctly()
    {
        VisualStudioLaunchSettings.FromCaller().GetProfiles().Items.Should().NotBeEmpty();
    }
}