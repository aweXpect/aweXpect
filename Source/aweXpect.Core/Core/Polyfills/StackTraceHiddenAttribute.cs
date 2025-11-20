#if !NET8_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;

// ReSharper disable once CheckNamespace
namespace System.Diagnostics;

/// <summary>
///     Types and Methods attributed with StackTraceHidden will be omitted from the stack trace text shown in
///     StackTrace.ToString() and Exception.StackTrace
/// </summary>
[AttributeUsage(AttributeTargets.Class |
                AttributeTargets.Method |
                AttributeTargets.Constructor |
                AttributeTargets.Struct,
	Inherited = false)]
[ExcludeFromCodeCoverage]
internal sealed class StackTraceHiddenAttribute : Attribute;
#endif
