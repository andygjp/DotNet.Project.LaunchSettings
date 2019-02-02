namespace DotNet.Project.LaunchSettings
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Newtonsoft.Json;

    public class Profiles
    {
        [JsonProperty("profiles")] 
        private Dictionary<string, Profile> _items;
        private static readonly Dictionary<string, Profile> MissingProfiles = new Dictionary<string, Profile>();

        [JsonConstructor]
        private Profiles()
        {
        }

        private Dictionary<string, Profile> Items => _items ?? MissingProfiles;

        public Profiles(IDictionary<string, Profile> items) 
            => _items = new Dictionary<string, Profile>(items);

        public Profile FirstOrEmpty() 
            => Items.Select(x => x.Value).DefaultIfEmpty(Profile.Empty).First();

        public (bool, Profile) TryGet(string profile) 
            => (Items.TryGetValue(profile, out var x), x);
    }
}