namespace DotNet.Project.LaunchSettings;

using System;

public record Result(bool Success, Profile? Profile)
{
    public void Match(Action unsuccessful, Action<Profile> successful)
    {
        if (Success)
        {
            successful(Profile ?? Invalid());
        }
        else
        {
            unsuccessful();
        }
    }

    public T Match<T>(Func<T> unsuccessful, Func<Profile, T> successful)
        => Success ? successful(Profile ?? Invalid()) : unsuccessful();

    private static Profile Invalid() 
        => throw new InvalidOperationException("Profile is null when Success is true.");
}