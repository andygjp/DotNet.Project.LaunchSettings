﻿// Copyright 2018 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Linq;

namespace Nuke.Common.BuildServers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NoConvertAttribute : Attribute
    {
    }
}
