namespace DotNet.Project.LaunchSettings.Tests
{
    internal static class StubbedProfiles
    {
        public static string Json
        {
            get
            {
                var json =
                    @"{
                    'profiles': {
                        'profile1': {
                            'commandName': 'Project',
                            'commandLineArgs': 'arg=x',
                            'workingDirectory': 'c:\\',
                            'launchBrowser': true,
                            'applicationUrl': 'index',
                            'environmentVariables': {
                                'var1': 'value1'
                            }
                        },
                        'profile2': {
                            'commandName': 'Project',
                            'commandLineArgs': 'arg=y',
                            'workingDirectory': 'c:\\',
                            'launchBrowser': false,
                            'applicationUrl': '\\',
                            'environmentVariables': {
                                'var2': 'value2'
                            }
                        }
                    }
                }";
                return json;
            }
        }
    }
}