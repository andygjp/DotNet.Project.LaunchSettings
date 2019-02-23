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

        private (bool, Profile) TryGetImpl(string profile) 
            => (Items.TryGetValue(profile, out var x), x);

        public Result TryGet(string profile)
        {
            var x = TryGetImpl(profile);
            return new Result(x.Item1, x.Item2);
        }
    }
    
    public class Result
    {
        private readonly bool _success;
        private readonly Profile _profile;

        public Result(bool success, Profile profile)
        {
            _success = success;
            _profile = profile;
        }

        public void Deconstruct(out bool success, out Profile profile)
        {
            success = _success;
            profile = _profile;
        }
    }
}