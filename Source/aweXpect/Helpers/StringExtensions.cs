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

		if (string.IsNullOrEmpty(indentation))
		{
			return value;
		}

		return (indentFirstLine ? indentation : "")
		       + value.Replace("\n", $"\n{indentation}");
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
		string[] lines = value.Split('\n');
		if (lines.Length <= 1)
		{
			return value;
		}

		StringBuilder sb = new();
		foreach (char c in lines[1])
		{
			if (char.IsWhiteSpace(c))
			{
				sb.Append(c);
			}
			else
			{
				break;
			}
		}

		string commonWhiteSpace = sb.ToString();

		for (int l = 2; l < lines.Length; l++)
		{
			if (lines[l].StartsWith(commonWhiteSpace))
			{
				continue;
			}

			for (int i = 0; i < Math.Min(lines[l].Length, commonWhiteSpace.Length); i++)
			{
				if (lines[l][i] != commonWhiteSpace[i])
				{
					commonWhiteSpace = commonWhiteSpace[..i];
					break;
				}
			}
		}

		sb.Clear();
		sb.Append(lines[0]);
		foreach (string? line in lines.Skip(1))
		{
			sb.Append('\n').Append(line[commonWhiteSpace.Length..]);
		}

		return sb.ToString();
	}
}
