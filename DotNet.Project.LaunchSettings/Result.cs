namespace DotNet.Project.LaunchSettings
{
    using System;
    
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

        public void Match(Action unsuccessful, Action<Profile> successful)
        {
            if (_success)
            {
                successful(_profile);
            }
            else
            {
                unsuccessful();
            }
        }
    }
}