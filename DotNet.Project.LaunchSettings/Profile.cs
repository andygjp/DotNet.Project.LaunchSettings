namespace DotNet.Project.LaunchSettings
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Newtonsoft.Json;

    public class Profile
    {
        public Profile(string commandName, string commandLineArgs, 
            string workingDirectory, bool launchBrowser, string applicationUrl)
            :this(commandName, commandLineArgs, workingDirectory, launchBrowser, 
                applicationUrl, new Dictionary<string, string>())
        {
        }
        
        [JsonConstructor]
        public Profile(string commandName, string commandLineArgs, 
            string workingDirectory, bool launchBrowser, string applicationUrl,
            IDictionary<string, string> environmentVariables)
        {
            CommandName = commandName;
            CommandLineArgs = commandLineArgs;
            WorkingDirectory = workingDirectory;
            LaunchBrowser = launchBrowser;
            ApplicationUrl = applicationUrl;
            EnvironmentVariables = new ReadOnlyDictionary<string, string>(environmentVariables);
        }

        public static Profile Empty { get; }
            = new Profile(default, default, default, default, default);
        
        public string CommandName { get; }
        public string CommandLineArgs { get; }
        public string WorkingDirectory { get; }
        public bool LaunchBrowser { get; }
        public string ApplicationUrl { get; }
        public IReadOnlyDictionary<string, string> EnvironmentVariables { get; }
    }
}