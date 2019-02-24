namespace Examples
{
    using System;
    using DotNet.Project.LaunchSettings;

    class Program
    {
        static void Main()
        {
            Examples.FirstOrEmpty();
            Examples.CheckingIfNamedProfileExists();
            Examples.UseNamedProfile();
            Examples.Match();
            Examples.FunctionalMatch();
        }
    }

    class Examples
    {
        public static void FirstOrEmpty()
        {
            var launchSettings = VisualStudioLaunchSettings.FromCaller();
            var profiles = launchSettings.GetProfiles();
            var profile = profiles.FirstOrEmpty();
            WriteOut.EnvironmentalVariables(profile);
        }
        
        public static void CheckingIfNamedProfileExists()
        {
            var launchSettings = VisualStudioLaunchSettings.FromCaller();
            var profiles = launchSettings.GetProfiles();
            var (exists, _) = profiles.Use("does-not-exist");
            // Do something else if it doesn't exist
            WriteOut.Line($"The profile does {(exists ? "" : "not")} exist."); 
        }
        
        public static void UseNamedProfile()
        {
            var launchSettings = VisualStudioLaunchSettings.FromCaller();
            var profiles = launchSettings.GetProfiles();
            var (exists, profile) = profiles.Use("does-not-exist");
            
            try
            {
                // You can not just use it. If the profile is missing, it will blow up!
                WriteOut.EnvironmentalVariables(profile);
            }
            catch (NullReferenceException)
            {
                WriteOut.Line("The profile was null and it caused an exception!");
            }
        }

        public static void Match()
        {
            var launchSettings = VisualStudioLaunchSettings.FromCaller();
            var profiles = launchSettings.GetProfiles();
            profiles
                .Use("does-not-exist")
                .Match(
                    () => { WriteOut.Line("The profile does not exist."); },
                    WriteOut.EnvironmentalVariables);
        }

        public static void FunctionalMatch()
        {
            var launchSettings = VisualStudioLaunchSettings.FromCaller();
            var profiles = launchSettings.GetProfiles();
            var profile = profiles
                .Use("does-not-exist")
                .Match(() => default,x => x);

            WriteOut.Line($"The profile does {(profile is null ? "not" : "")} exist.");
        }
    }

    class WriteOut
    {
        public static void EnvironmentalVariables(Profile profile)
        {
            var count = profile.EnvironmentVariables.Count;
            Console.Write($"The profile has {count} environment variables");
            if (count is 0)
            {
                Line(".");
                return;
            }

            foreach (var (key, value) in profile.EnvironmentVariables)
            {
                Line($"key={key}, value={value}");
            }
        }

        public static void Line(string message)
        {
            Console.WriteLine(message);
        }
    }
}