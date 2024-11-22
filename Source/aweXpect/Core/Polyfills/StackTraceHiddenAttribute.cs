#if !NET6_0_OR_GREATER
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
public sealed class StackTraceHiddenAttribute : Attribute;
#endif
