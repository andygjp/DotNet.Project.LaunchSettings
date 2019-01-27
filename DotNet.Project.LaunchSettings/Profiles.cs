namespace DotNet.Project.LaunchSettings
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class Profiles
    {
        [JsonProperty("profiles")]
        private IDictionary<string, Profile> _items;

        public Profiles() 
            => _items = new Dictionary<string, Profile>();

        public Profiles(IDictionary<string, Profile> items) 
            => _items = new Dictionary<string, Profile>(items);

        public Profile FirstOrEmpty() 
            => _items.Select(x => x.Value).DefaultIfEmpty(new Profile()).First();

        public (bool, Profile) TryGet(string profile) 
            => (_items.TryGetValue(profile, out var x), x);
    }
}