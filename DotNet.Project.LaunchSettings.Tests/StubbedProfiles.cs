namespace DotNet.Project.LaunchSettings.Tests
{
    using System.Collections.Generic;
    using System.IO;

    internal static class StubbedProfiles
    {
        public static string Json =>
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

        public static Profile First =>
            new Profile("Project", "arg=x", "c:\\", true, "index",
                new Dictionary<string, string>
                {
                    ["var1"] = "value1"
                }
            );

        public static string GetPathToTemporaryJsonFile()
        {
            var filePath = Path.GetTempFileName();
            File.WriteAllText(filePath, Json);
            return filePath;
        }
    }
}