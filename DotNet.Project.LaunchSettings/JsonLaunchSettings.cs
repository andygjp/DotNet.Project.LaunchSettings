namespace DotNet.Project.LaunchSettings
{
    using System.IO;

    public class JsonLaunchSettings : LaunchSettings
    {
        private readonly string _json;

        public JsonLaunchSettings(string json) 
            => _json = json;

        protected override TextReader GetReader()
        {
            return new StringReader(_json);
        }
    }
}