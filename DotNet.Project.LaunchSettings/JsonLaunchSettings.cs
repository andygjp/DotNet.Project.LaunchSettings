namespace DotNet.Project.LaunchSettings
{
    using System.IO;

    public class JsonLaunchSettings : LaunchSettings
    {
        private readonly TextReader _reader;

        public JsonLaunchSettings(string json)
            : this(new StringReader(json))
        {
        }

        private JsonLaunchSettings(TextReader reader)
        {
            _reader = reader;
        }

        protected override TextReader GetReader()
        {
            return _reader;
        }
    }
}