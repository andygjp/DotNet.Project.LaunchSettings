# DotNet.Project.LaunchSettings
Parses Visual Studio launchSettings.json

[![](https://img.shields.io/azure-devops/build/andygjp0828/DotNet.Project.LaunchSettings/1.svg?style=flat)](https://dev.azure.com/andygjp0828/DotNet.Project.LaunchSettings/_build?definitionId=1)
![](https://img.shields.io/azure-devops/tests/andygjp0828/DotNet.Project.LaunchSettings/1.svg?style=flat)
[![](https://img.shields.io/nuget/v/DotNet.Project.LaunchSettings.svg?style=plastic)](https://www.nuget.org/packages/DotNet.Project.LaunchSettings/)

# Why does this exist?

I have a suite of acceptance tests that interact with authenticated environments. 
The tests get the logon details from environment variables, because
I'd rather manage those secrets in my build system instead of a configuration file.

Occasionally I need to investigate issues in these environments and need the logon
details. I don't want to edit environment variables when I test different environments, 
so instead I keep those logon details in the `launchSettings.json`, which is ignored, 
of the acceptance tests project. 

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

## Named profile

## First profile