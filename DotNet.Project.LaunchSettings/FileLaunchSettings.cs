namespace DotNet.Project.LaunchSettings
{
    using System.IO;

    public class FileLaunchSettings : LaunchSettings
    {
        private readonly string _filePath;

        public FileLaunchSettings(string filePath) 
            => _filePath = filePath;

        protected override TextReader GetReader()
        {
            return new StreamReader(_filePath);
        }
    }
}