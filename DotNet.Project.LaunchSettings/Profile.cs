namespace DotNet.Project.LaunchSettings
{
    using System.Collections.Generic;

    public class Profile
    {
        public string CommandName { get; set; }
        public string CommandLineArgs { get; set; }
        public string WorkingDirectory { get; set; }
        public bool LaunchBrowser { get; set; }
        public string ApplicationUrl { get; set; }
        public IDictionary<string, string> EnvironmentVariables { get; set; } = new Dictionary<string, string>();
    }
}