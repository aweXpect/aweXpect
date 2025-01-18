using System.Diagnostics.CodeAnalysis;

namespace aweXpect.Tests;

internal static class StringExtensions
{
	[return: NotNullIfNotNull(nameof(value))]
	public static string? DisplayWhitespace(this string? value)
		=> value?.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
}
