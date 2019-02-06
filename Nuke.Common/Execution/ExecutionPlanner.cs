// Copyright 2019 Maintainers of NUKE.
// Distributed under the MIT License.
// https://github.com/nuke-build/nuke/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NuGet.Packaging;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;

namespace Nuke.Common.Execution
{
    /// <summary>
    /// Given the invoked target names, creates an execution plan under consideration of execution, ordering and trigger dependencies.
    /// </summary>
    internal static class ExecutionPlanner
    {
        public static IReadOnlyCollection<ExecutableTarget> GetExecutionPlan(
            IReadOnlyCollection<ExecutableTarget> executableTargets,
            [CanBeNull] IReadOnlyCollection<string> invokedTargetNames)
        {
            var invokedTargets = invokedTargetNames?.Select(x => GetExecutableTarget(x, executableTargets)).ToArray() ??
                                 GetDefaultTarget(executableTargets);
            invokedTargets.ForEach(x => x.Invoked = true);

            // Repeat to create the plan with triggers taken into account until plan doesn't change
            IReadOnlyCollection<ExecutableTarget> executionPlan;
            IReadOnlyCollection<ExecutableTarget> additionallyTriggered;
            do
            {
                executionPlan = GetExecutionPlanInternal(executableTargets, invokedTargets);
                additionallyTriggered = executionPlan.SelectMany(x => x.Triggers).Except(executionPlan).ToList();
                invokedTargets = executionPlan.Concat(additionallyTriggered).ToArray();
            } while (additionallyTriggered.Count > 0);

            return executionPlan;
        }

        private static IReadOnlyCollection<ExecutableTarget> GetExecutionPlanInternal(
            IReadOnlyCollection<ExecutableTarget> executableTargets,
            ICollection<ExecutableTarget> invokedTargets)
        {
            var vertexDictionary = GetVertexDictionary(executableTargets);
            var graphAsList = vertexDictionary.Values.ToList();
            var executingTargets = new List<ExecutableTarget>();

            while (graphAsList.Any())
            {
                var independents = graphAsList.Where(x => !graphAsList.Any(y => y.Dependencies.Contains(x))).ToList();
                if (EnvironmentInfo.ArgumentSwitch("strict") && independents.Count > 1)
                {
                    ControlFlow.Fail(
                        new[] { "Incomplete target definition order." }
                            .Concat(independents.Select(x => $"  - {x.Value.Name}"))
                            .JoinNewLine());
                }

                var independent = independents.FirstOrDefault();
                if (independent == null)
                {
                    var scc = new StronglyConnectedComponentFinder<ExecutableTarget>();
                    var cycles = scc.DetectCycle(graphAsList)
                        .Cycles()
                        .Select(x => string.Join(" -> ", x.Select(y => y.Value.Name)));

                    ControlFlow.Fail(
                        new[] { "Circular dependencies between target definitions." }
                            .Concat(independents.Select(x => $"  - {cycles}"))
                            .JoinNewLine());
                }

                graphAsList.Remove(independent);

                var executableTarget = independent.Value;
                if (!invokedTargets.Contains(executableTarget) &&
                    !executingTargets.SelectMany(x => x.ExecutionDependencies).Contains(executableTarget))
                    continue;

                executingTargets.Add(executableTarget);
            }

            executingTargets.Reverse();

            return executingTargets;
        }

        private static IReadOnlyDictionary<ExecutableTarget, Vertex<ExecutableTarget>> GetVertexDictionary(
            IReadOnlyCollection<ExecutableTarget> executableTargets)
        {
            var vertexDictionary = executableTargets.ToDictionary(x => x, x => new Vertex<ExecutableTarget>(x));
            foreach (var (executable, vertex) in vertexDictionary)
                vertex.Dependencies.AddRange(executable.AllDependencies.Select(x => vertexDictionary[x]));
            
            return vertexDictionary;
        }

        private static ExecutableTarget GetExecutableTarget(
            string targetName,
            IReadOnlyCollection<ExecutableTarget> executableTargets)
        {
            var executableTarget = executableTargets.SingleOrDefault(x => x.Name.EqualsOrdinalIgnoreCase(targetName));
            if (executableTarget == null)
                ControlFlow.Fail($"Target with name '{targetName}' is not available.");

            return executableTarget;
        }

        private static ExecutableTarget[] GetDefaultTarget(IReadOnlyCollection<ExecutableTarget> executableTargets)
        {
            var target = executableTargets.SingleOrDefault(x => x.IsDefault);
            if (target == null)
                Fail("No target has been marked to be the default.", executableTargets);
            
            return new[] { target };
        }

        private static void Fail(string message, IReadOnlyCollection<ExecutableTarget> executableTargets)
        {
            ControlFlow.Fail(new StringBuilder()
                .AppendLine(message)
                .AppendLine()
                .AppendLine(HelpTextService.GetTargetsText(executableTargets)).ToString());
        }
    }
}
