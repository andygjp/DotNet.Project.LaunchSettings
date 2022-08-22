namespace DotNet.Project.LaunchSettings
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;

    public class Profiles
    {
        [JsonPropertyName("profiles")] 
        public Dictionary<string, Profile> Items { get; init; } = new();

        [JsonIgnore] 
        internal static Profiles Empty { get; } = new();

        public Profile FirstOrEmpty()
            => Items.Select(x => x.Value).DefaultIfEmpty(Profile.Empty).First();

        public Result Use(string profile) => new(Items.TryGetValue(profile, out var value), value);
    }
}