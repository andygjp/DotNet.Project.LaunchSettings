# DotNet.Project.LaunchSettings
Parses Visual Studio launchSettings.json

[![](https://img.shields.io/azure-devops/build/andygjp0828/DotNet.Project.LaunchSettings/1.svg?style=flat)](https://dev.azure.com/andygjp0828/DotNet.Project.LaunchSettings/_build?definitionId=1)
![](https://img.shields.io/azure-devops/tests/andygjp0828/DotNet.Project.LaunchSettings/1.svg?style=flat)
[![](https://img.shields.io/nuget/v/DotNet.Project.LaunchSettings.svg?style=plastic)](https://www.nuget.org/packages/DotNet.Project.LaunchSettings/)
![](https://img.shields.io/github/license/andygjp/DotNet.Project.LaunchSettings.svg)

# Why does this exist?

I have a suite of acceptance tests that interact with authenticated environments. 
The tests get the logon details from environment variables, because
I'd rather manage those secrets in my build system instead of a configuration file.

Occasionally I need to investigate issues in these environments and need the logon
details. I don't want to edit environment variables when I test different environments, 
so instead I keep those logon details in the `launchSettings.json` of the acceptance 
tests project. (.gitignore this file if you want to do the same.)

```c#
async Task Example()
{
    var launchSettings = VisualStudioLaunchSettings.FromCaller();
    var profiles = launchSettings.GetProfiles();
    var profile = profiles.FirstOrEmpty();

    var client = await AppEnvironment.FromEnvironment(profile.EnvironmentVariables).CreateClient();
    // use the client
}
```

# How to use it?

There are two ways I use this library. If you want to use it, first you need to add it to your
project - you can find it on [Nuget](https://www.nuget.org/packages/DotNet.Project.LaunchSettings/)

## First profile
Just grab the first one.

```c#
var launchSettings = VisualStudioLaunchSettings.FromCaller();
var profiles = launchSettings.GetProfiles();
var profile = profiles.FirstOrEmpty();
```

Really simple if you have one, but if you have many and don't want to use a named profile,
~~you can use this dotnet cli command to~~ (I've lost the link) you'll have to order you profiles.

## Named profile
If you intend to use a named profile, you need to ensure it exists before you attempt to use it.

```c#
var launchSettings = VisualStudioLaunchSettings.FromCaller();
var profiles = launchSettings.GetProfiles();
var (exists, profile) = profiles.Use("does-not-exist");
// Check exists before you use it!
```

### Match
Theres is alternative syntax to deconstructing the values - invoking Match() instead:

```c#
var launchSettings = VisualStudioLaunchSettings.FromCaller();
var profiles = launchSettings.GetProfiles();
profiles
    .Use("does-not-exist")
    .Match(
        () => { Console.WriteLine($"The profile does not exist."); },
        profile => { Console.WriteLine($"The profile exists."););
```

There is a functional version too:

```c#
var launchSettings = VisualStudioLaunchSettings.FromCaller();
var profiles = launchSettings.GetProfiles();
var profile = profiles
    .Use("does-not-exist")
    .Match(() => default, (Profile x) => x);
```

# Note Regarding Versioning
The project uses GitVersion. Versions are bumped by including `+semver:{n}`, where `{n}` is major, minor or patch, in the commit message. See [GitVersion configuration documentation](https://gitversion.readthedocs.io/en/latest/configuration/#global-configuration).
