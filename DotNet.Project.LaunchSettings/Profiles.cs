namespace DotNet.Project.LaunchSettings
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;

    public class Profiles
    {
        [JsonProperty("profiles")]
        private IReadOnlyDictionary<string, Profile> _items;

        [JsonConstructor]
        private Profiles()
        {
        }

        public Profiles(IDictionary<string, Profile> items) 
            => _items = new ReadOnlyDictionary<string, Profile>(items);

        public Profile FirstOrEmpty() 
            => _items.Select(x => x.Value).DefaultIfEmpty(Profile.Empty).First();

        public (bool, Profile) TryGet(string profile) 
            => (_items.TryGetValue(profile, out var x), x);
    }
}