// Copyright 2018 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Linq;

namespace Nuke.Common.Tools.Git
{
    public static partial class GitTasks
    {
        public static bool GitIsDetached()
        {
            return GitIsDetached(EnvironmentInfo.WorkingDirectory);
        }

        public static bool GitIsDetached(string workingDirectory)
        {
            return !Git("symbolic-ref --short -q HEAD", workingDirectory, logOutput: false).Any();
        }

        public static bool GitHasCleanWorkingCopy()
        {
            return GitHasCleanWorkingCopy(EnvironmentInfo.WorkingDirectory);
        }

        public static bool GitHasCleanWorkingCopy(string workingDirectory)
        {
            return !Git("status --short", workingDirectory, logOutput: false).Any();
        }

        public static string GitCurrentBranch()
        {
            return GitCurrentBranch(EnvironmentInfo.WorkingDirectory);
        }

        private static string GitCurrentBranch(string workingDirectory)
        {
            return Git("rev-parse --abbrev-ref HEAD", workingDirectory, logOutput: false).Select(x => x.Text).Single();
        }
    }
}
