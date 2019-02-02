namespace DotNet.Project.LaunchSettings
{
    using System.IO;

    internal class FileLaunchSettings : LaunchSettings
    {
        private readonly string _filePath;

        public FileLaunchSettings(string filePath) 
            => _filePath = filePath;

        protected override TextReader GetReader() 
            => new StreamReader(_filePath);
    }
}