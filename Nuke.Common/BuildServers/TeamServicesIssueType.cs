// Copyright 2018 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Linq;
using JetBrains.Annotations;

namespace Nuke.Common.BuildServers
{
    [PublicAPI]
    public enum TeamServicesIssueType
    {
        Warning,
        Error
    }
}
