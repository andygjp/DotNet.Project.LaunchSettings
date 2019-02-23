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

        [JsonConstructor]
        private Profiles()
        {
        }

        public Profiles(IDictionary<string, Profile> items) 
            => _items = new Dictionary<string, Profile>(items);

        internal static Profiles Empty { get; } = new Profiles();

        private Dictionary<string, Profile> Items 
            => _items ?? (_items = new Dictionary<string, Profile>());

        public Profile FirstOrEmpty() 
            => Items.Select(x => x.Value).DefaultIfEmpty(Profile.Empty).First();

        public Result TryGet(string profile) 
            => new Result(Items.TryGetValue(profile, out var value), value);
    }
}