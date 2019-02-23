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
            var (exists, _) = profiles.TryGet("does-not-exist");
            // Do something else if it doesn't exist
            Console.WriteLine($"The profile does {(exists ? "" : "not")} exist."); 
        }
        
        public static void UseNamedProfile()
        {
            var launchSettings = VisualStudioLaunchSettings.FromCaller();
            var profiles = launchSettings.GetProfiles();
            var (exists, profile) = profiles.TryGet("does-not-exist");
            
            try
            {
                // You can not just use it. If the profile is missing, it will blow up!
                WriteOut.EnvironmentalVariables(profile);
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("The profile was null and it caused an exception!");
            }
        }

        public static void Match()
        {
            var launchSettings = VisualStudioLaunchSettings.FromCaller();
            var profiles = launchSettings.GetProfiles();
            profiles
                .TryGet("does-not-exist")
                .Match(
                    () => { Console.WriteLine($"The profile does not exist."); },
                    WriteOut.EnvironmentalVariables);
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
                Console.WriteLine(".");
                return;
            }

            foreach (var (key, value) in profile.EnvironmentVariables)
            {
                Console.WriteLine($"key={key}, value={value}");
            }
        }
    }
}