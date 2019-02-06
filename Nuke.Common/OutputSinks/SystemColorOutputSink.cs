using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Nuke.Common.Utilities;

namespace Nuke.Common.OutputSinks
{
    [UsedImplicitly]
    [ExcludeFromCodeCoverage]
    internal class SystemColorOutputSink : ConsoleOutputSink
    {
        public override void WriteNormal(string text)
        {
            Console.WriteLine(text);
        }

        public override void WriteTrace(string text)
        {
            WriteWithColors(text, ConsoleColor.Gray);
        }

        public override void WriteInformation(string text)
        {
            WriteWithColors(text, ConsoleColor.Cyan);
        }

        public override void WriteWarning(string text, string details = null)
        {
            WriteWithColors(text, ConsoleColor.Yellow);
        }

        public override void WriteError(string text, string details = null)
        {
            WriteWithColors(text, ConsoleColor.Red);
        }

        public override void WriteSuccess(string text)
        {
            WriteWithColors(text, ConsoleColor.Green);
        }
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void WriteWithColors(string text, ConsoleColor foregroundColor)
        {
            var previousForegroundColor = Console.ForegroundColor;

            // using (DelegateDisposable.CreateBracket(
            //     () => Console.ForegroundColor = foregroundColor,
            //     () => Console.ForegroundColor = previousForegroundColor))
            {
                Console.WriteLine(text);
            }
        }
    }
}
