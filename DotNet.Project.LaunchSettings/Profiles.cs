namespace DotNet.Project.LaunchSettings
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class Profiles
    {
        [JsonProperty("profiles")]
        private Dictionary<string, Profile> _items;

        public Profiles()
            :this(new Dictionary<string, Profile>())
        {
        }

        [JsonConstructor]
        public Profiles(Dictionary<string, Profile> items) 
            => _items = items;

        public Profile FirstOrEmpty() 
            => _items.Select(x => x.Value).DefaultIfEmpty(new Profile()).First();

        public (bool, Profile) TryGet(string profile) 
            => (_items.TryGetValue(profile, out var x), x);
    }
}