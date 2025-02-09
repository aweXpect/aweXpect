﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
		if (value == null || string.IsNullOrEmpty(indentation))
		{
			return value;
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

	[return: NotNullIfNotNull(nameof(value))]
	public static string? RemoveNewlineStyle(this string? value)
	{
		if (value == null)
		{
			return null;
		}

		return value.Replace("\r\n", "\n", StringComparison.Ordinal)
			.Replace("\r", "\n", StringComparison.Ordinal);
	}

	public static string SubstringUntilFirst(this string name, char c)
	{
		int index = name.IndexOf(c);
		if (index >= 0)
		{
			return name.Substring(0, index);
		}

		return name;
	}

	[return: NotNullIfNotNull(nameof(value))]
	public static string? ToSingleLine(this string? value)
		=> value?.Replace("\n", "\\n").Replace("\r", "\\r");

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

	[return: NotNullIfNotNull(nameof(value))]
	public static string? TruncateWithEllipsisOnWord(this string? value, int maxLength)
	{
		if (value is null || value.Length <= maxLength)
		{
			return value;
		}

		int indexOfWordBoundary = value[..maxLength].LastIndexOf(' ');
		if (indexOfWordBoundary < maxLength * 0.8)
		{
			indexOfWordBoundary = maxLength;
		}

		const char ellipsis = '\u2026';
		return $"{value.Substring(0, indexOfWordBoundary)}{ellipsis}";
	}
}
