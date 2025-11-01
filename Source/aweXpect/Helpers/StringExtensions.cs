using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace aweXpect.Helpers;

internal static class StringExtensions
{
	[return: NotNullIfNotNull(nameof(value))]
	public static string? Indent(this string? value, string? indentation = "  ",
		bool indentFirstLine = true)
	{
		if (value == null)
		{
			return null;
		}

		if (!string.IsNullOrEmpty(indentation))
		{
			return (indentFirstLine ? indentation : "")
			       + value.Replace("\n", $"\n{indentation}");
		}

		return value;
	}

	public static string PrependAOrAn(this string value)
	{
		char[] vocals = ['a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U',];
		if (value.Length > 0 && vocals.Contains(value[0]))
		{
			return $"an {value}";
		}

		return $"a {value}";
	}

	public static string TrimCommonWhiteSpace(this string value)
	{
		string[] lines = value.Split(Environment.NewLine);
		if (lines.Length <= 1)
		{
			return value;
		}

		StringBuilder sb = new();
		foreach (char c in lines[1].TakeWhile(char.IsWhiteSpace))
		{
			sb.Append(c);
		}

		string commonWhiteSpace = sb.ToString();

		foreach (string line in lines.Skip(2).Where(line => !line.StartsWith(commonWhiteSpace)))
		{
			for (int i = 0; i < Math.Min(line.Length, commonWhiteSpace.Length); i++)
			{
				if (line[i] != commonWhiteSpace[i])
				{
					commonWhiteSpace = commonWhiteSpace[..i];
				}
			}
		}

		sb.Clear();
		sb.Append(lines[0]);
		foreach (string? line in lines.Skip(1))
		{
			sb.Append(Environment.NewLine).Append(line[commonWhiteSpace.Length..]);
		}

		return sb.ToString();
	}
}
