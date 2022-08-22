#if NETSTANDARD2_0 || NETSTANDARD2_1

using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

// Workaround to add init members support to net standard
[EditorBrowsable(EditorBrowsableState.Never)]
internal static class IsExternalInit { }

#endif