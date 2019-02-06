// Copyright 2019 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Nuke.Common.Execution;

namespace Nuke.Common.Tooling
{
    /// <summary>
    ///     Injects a delegate for process execution. The path relative to the root directory is passed as constructor argument.
    /// </summary>
    /// <example>
    ///     <code>
    /// [LocalExecutable("./tools/custom.exe")] readonly Tool Custom;
    /// Target FooBar => _ => _
    ///     .Executes(() =>
    ///     {
    ///         var output = Custom("test");
    ///     });
    ///     </code>
    /// </example>
    public class LocalExecutableAttribute : InjectionAttributeBase
    {
        private readonly string _path;

        public LocalExecutableAttribute(string path)
        {
            _path = path;
        }

        public override object GetValue(MemberInfo member, object instance)
        {
            var toolPath = Path.Combine(NukeBuild.RootDirectory, _path);
            ControlFlow.Assert(File.Exists(toolPath), $"File.Exists({toolPath})");
            return new Tool(new ToolExecutor(toolPath).Execute);
        }
    }
}
