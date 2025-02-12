using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace aweXpect.Helpers;

internal static class StringExtensions
{
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

	public static string PrependAOrAn(this string value)
	{
		char[] vocals = ['a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'];
		if (value.Length > 0 && vocals.Contains(value[0]))
		{
			return $"an {value}";
		}

		return $"a {value}";
	}
}
