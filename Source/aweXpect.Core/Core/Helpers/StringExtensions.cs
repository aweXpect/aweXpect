using System.Diagnostics.CodeAnalysis;

namespace aweXpect.Core.Helpers;

internal static class StringExtensions
{
	[return: NotNullIfNotNull(nameof(value))]
	public static string? DisplayWhitespace(this string? value) =>
		value?.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");

	[return: NotNullIfNotNull(nameof(value))]
	public static string? Indent(this string? value, string indentation = "  ",
		bool indentFirstLine = true)
	{
		if (value == null)
		{
			return null;
		}

		return (indentFirstLine ? indentation : "")
		       + value.Replace("\n", $"\n{indentation}");
	}

	[return: NotNullIfNotNull(nameof(value))]
	public static string? TruncateWithEllipsis(this string? value, int maxLength)
	{
		if (value is null || value.Length <= maxLength)
		{
			return value;
		}

		const char ellipsis = '\u2026';
		return $"{value.Substring(0, maxLength)}{ellipsis}";
	}
}
