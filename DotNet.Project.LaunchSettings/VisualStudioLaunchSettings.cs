namespace DotNet.Project.LaunchSettings
{
    using System.IO;
    using System.Runtime.CompilerServices;

    public class VisualStudioLaunchSettings : LaunchSettings
    {
        private readonly string _filePath;

        private VisualStudioLaunchSettings(string filePath)
        {
            _filePath = filePath;
        }

        public static VisualStudioLaunchSettings FromCaller([CallerFilePath] string filePath = default)
        {
            var directory = Path.GetDirectoryName(filePath);
            var vsLaunchSettings = Path.Combine(directory, "Properties", "launchSettings.json");
            return new VisualStudioLaunchSettings(vsLaunchSettings);
        }

        protected override TextReader GetReader()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    return new StreamReader(_filePath);
                }
            }
            catch (IOException)
            {
            }
            
            return new StringReader("");
        }
    }
}