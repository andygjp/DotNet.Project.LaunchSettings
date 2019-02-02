namespace DotNet.Project.LaunchSettings
{
    using System.IO;
    using System.Runtime.CompilerServices;

    public class VisualStudioLaunchSettings : FileLaunchSettings
    {
        private VisualStudioLaunchSettings(string filePath) 
            : base(filePath)
        {
        }

        public static FileLaunchSettings FromCaller([CallerFilePath] string filePath = default)
        {
            var directory = Path.GetDirectoryName(filePath);
            var vsLaunchSettings = Path.Combine(directory, "Properties\\launchSettings.json");
            return new VisualStudioLaunchSettings(vsLaunchSettings);
        }
    }
}