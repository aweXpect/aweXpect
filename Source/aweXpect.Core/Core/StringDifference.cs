﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aweXpect.Core.Helpers;

namespace aweXpect.Core;

/// <summary>
///     Highlights the string difference between the <paramref name="actual" /> and
///     <paramref name="expected" /> values.
/// </summary>
/// <remarks>
///     If no <paramref name="comparer" /> is specified, uses the <see cref="StringComparer.Ordinal" /> string comparer.
/// </remarks>
public sealed class StringDifference(
	string? actual,
	string? expected,
	IEqualityComparer<string>? comparer = null,
	StringDifferenceSettings? settings = null)
{
	/// <summary>
	///     The supported match types in the <see cref="StringDifference" />.
	/// </summary>
	public enum MatchType
	{
		/// <summary>
		///     The strings are compared for equality.
		/// </summary>
		Equality,

		/// <summary>
		///     The expected string is treated as a wildcard pattern.
		/// </summary>
		Wildcard,

		/// <summary>
		///     The expected string is treated as a regex pattern.
		/// </summary>
		Regex
	}

	private const string ActualIndicator = " (actual)";

	private readonly IEqualityComparer<string> _comparer = comparer ?? StringComparer.Ordinal;
	private int? _indexOfFirstMismatch;

	/// <summary>
	///     Returns the first index at which the two values do not match exactly.
	/// </summary>
	public int IndexOfFirstMismatch(MatchType matchType)
	{
		if (matchType is MatchType.Wildcard or MatchType.Regex)
		{
			return 0;
		}

		_indexOfFirstMismatch ??= GetIndexOfFirstMismatch(actual, expected, _comparer);
		return _indexOfFirstMismatch.Value;
	}


	/// <inheritdoc />
	public override string ToString() => ToString("differs");

	/// <summary>
	///     Writes a string representation of the difference, starting with the <paramref name="prefix" />.
	/// </summary>
	/// <param name="prefix">The prefix, e.g. <c>differs at index</c></param>
	public string ToString(string prefix)
	{
		const char arrowDown = '\u2193';
		const char arrowUp = '\u2191';
		const string linePrefix = "  \"";
		const string suffix = "\"";

		if (actual == null)
		{
			StringBuilder sb = new();
			sb.Append(prefix).AppendLine(":");
			sb.Append("  ").Append(arrowDown).AppendLine(ActualIndicator);
			sb.AppendLine("  <null>");
			AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(sb, linePrefix, expected!,
				0, suffix);
			sb.Append("  ").Append(arrowUp).Append(GetExpected(settings?.MatchType));
			return sb.ToString();
		}

		if (expected == null)
		{
			StringBuilder sb = new();
			sb.Append(prefix).AppendLine(":");
			sb.Append("  ").Append(arrowDown).AppendLine(ActualIndicator);
			AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(sb, linePrefix, actual!,
				0, suffix);
			sb.AppendLine("  <null>");
			sb.Append("  ").Append(arrowUp).Append(GetExpected(settings?.MatchType));
			return sb.ToString();
		}

		return settings?.MatchType switch
		{
			MatchType.Wildcard => ToPatternString(MatchType.Wildcard, prefix, actual, expected),
			MatchType.Regex => ToPatternString(MatchType.Regex, prefix, actual, expected),
			_ => ToEqualityString(prefix, actual, expected, IndexOfFirstMismatch(MatchType.Equality), settings)
		};
	}

	private static string ToEqualityString(string prefix, string actual, string expected, int indexOfFirstMismatch,
		StringDifferenceSettings? settings)
	{
		const char arrowDown = '\u2193';
		const char arrowUp = '\u2191';
		const string linePrefix = "  \"";
		const string suffix = "\"";

		if (indexOfFirstMismatch < 0)
		{
			return prefix;
		}

		int column = settings?.IgnoredTrailingColumns ?? 0;
		StringBuilder sb = new();
		int trimStart =
			GetStartIndexOfPhraseToShowBeforeTheMismatchingIndex(actual, indexOfFirstMismatch);

		int whiteSpaceCountBeforeArrow = indexOfFirstMismatch - trimStart + linePrefix.Length;

		if (trimStart > 0)
		{
			whiteSpaceCountBeforeArrow++;
		}

		string visibleText = actual[trimStart..indexOfFirstMismatch];
		whiteSpaceCountBeforeArrow += visibleText.Count(c => c is '\r' or '\n');

		string matchingString = actual[..indexOfFirstMismatch];
		int lineNumber = matchingString.Count(c => c == '\n');
		if (settings is not null)
		{
			lineNumber += settings.IgnoredTrailingLines;
		}

		if (settings?.IgnoredTrailingLines > 0 || actual.Any(c => c == '\n'))
		{
			int indexOfLastNewlineBeforeMismatch = matchingString.LastIndexOf('\n');
			column += matchingString.Length - indexOfLastNewlineBeforeMismatch;
			sb.Append(prefix).Append(" on line ").Append(lineNumber + 1).Append(" and column ")
				.Append(column).AppendLine(":");
		}
		else
		{
			sb.Append(prefix).Append(" at index ").Append(indexOfFirstMismatch + column).AppendLine(":");
		}

		sb.Append(' ', whiteSpaceCountBeforeArrow).Append(arrowDown).AppendLine(ActualIndicator);
		AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(sb, linePrefix, actual,
			trimStart, suffix);
		AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(sb, linePrefix, expected,
			trimStart, suffix);
		sb.Append(' ', whiteSpaceCountBeforeArrow).Append(arrowUp).Append(GetExpected(settings?.MatchType));

		return sb.ToString();
	}

	private static string ToPatternString(MatchType matchType, string prefix, string actual, string expected)
	{
		const char arrowDown = '\u2193';
		const char arrowUp = '\u2191';

		StringBuilder sb = new();
		sb.Append(prefix).AppendLine(":");
		sb.Append("  ").Append(arrowDown).AppendLine(ActualIndicator);
		sb.Append("  ");
		Formatter.Format(sb, actual.DisplayWhitespace().TruncateWithEllipsisOnWord(300));
		sb.AppendLine();
		sb.Append("  ");
		Formatter.Format(sb, expected.DisplayWhitespace().TruncateWithEllipsisOnWord(300));
		sb.AppendLine();
		sb.Append("  ").Append(arrowUp).Append(GetExpected(matchType));
		return sb.ToString();
	}

	/// <summary>
	///     Appends the <paramref name="prefix" />, the escaped visible <paramref name="text" /> phrase decorated with ellipsis
	///     and the <paramref name="suffix" /> to the <paramref name="stringBuilder" />.
	/// </summary>
	/// <remarks>
	///     When text phrase starts at <paramref name="indexOfStartingPhrase" /> and with a calculated length omits text
	///     on start or end, an ellipsis is added.
	/// </remarks>
	private static void AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(
		StringBuilder stringBuilder,
		string prefix, string text, int indexOfStartingPhrase, string suffix)
	{
		int subjectLength = GetLengthOfPhraseToShowOrDefaultLength(text[indexOfStartingPhrase..]);
		const char ellipsis = '\u2026';

		stringBuilder.Append(prefix);

		if (indexOfStartingPhrase > 0)
		{
			stringBuilder.Append(ellipsis);
		}

		stringBuilder.Append(text
			.Substring(indexOfStartingPhrase, subjectLength).DisplayWhitespace().ToSingleLine());

		if (text.Length > indexOfStartingPhrase + subjectLength)
		{
			stringBuilder.Append(ellipsis);
		}

		stringBuilder.AppendLine(suffix);
	}

	private static int GetIndexOfFirstMismatch(string? actualValue, string? expectedValue,
		IEqualityComparer<string> comparer)
	{
		if (comparer.Equals(actualValue, expectedValue))
		{
			return -1;
		}

		if (actualValue is null || expectedValue is null)
		{
			return 0;
		}

		int maxCommonLength = Math.Min(actualValue.Length, expectedValue.Length);
		int min = 0;
		int max = maxCommonLength + 1;
		while (min <= max)
		{
			int mid = (min + max) / 2;
			if (mid == min)
			{
				break;
			}

			if (comparer.Equals(actualValue[..mid], expectedValue[..mid]))
			{
				min = mid;
			}
			else if (mid <= min + 1)
			{
				break;
			}
			else
			{
				max = mid;
			}
		}

		return min;
	}

	/// <summary>
	///     Calculates how many characters to keep in <paramref name="value" />.
	/// </summary>
	/// <remarks>
	///     If a word end is found between 45 and 60 characters, use this word end, otherwise keep 50 characters.
	/// </remarks>
	private static int GetLengthOfPhraseToShowOrDefaultLength(string value)
	{
		const int defaultLength = 50;
		const int minLength = 45;
		const int maxLength = 60;
		const int lengthOfWhitespace = 1;

		int indexOfWordBoundary = value
			.LastIndexOf(' ', Math.Min(maxLength + lengthOfWhitespace, value.Length) - 1);

		if (indexOfWordBoundary >= minLength)
		{
			return indexOfWordBoundary;
		}

		return Math.Min(defaultLength, value.Length);
	}

	/// <summary>
	///     Calculates the start index of the visible segment from <paramref name="value" /> when highlighting the difference
	///     at <paramref name="indexOfFirstMismatch" />.
	/// </summary>
	/// <remarks>
	///     Either keep the last 10 characters before <paramref name="indexOfFirstMismatch" /> or a word begin (separated by
	///     whitespace) between 15 and 5 characters before <paramref name="indexOfFirstMismatch" />.
	/// </remarks>
	private static int GetStartIndexOfPhraseToShowBeforeTheMismatchingIndex(string value,
		int indexOfFirstMismatch)
	{
		const int defaultCharactersToKeep = 10;
		const int minCharactersToKeep = 5;
		const int maxCharactersToKeep = 15;
		const int lengthOfWhitespace = 1;
		const int phraseLengthToCheckForWordBoundary =
			maxCharactersToKeep - minCharactersToKeep + lengthOfWhitespace;

		if (indexOfFirstMismatch <= defaultCharactersToKeep)
		{
			return 0;
		}

		int indexToStartSearchingForWordBoundary =
			Math.Max(indexOfFirstMismatch - (maxCharactersToKeep + lengthOfWhitespace), 0);

		int indexOfWordBoundary = value
			                          .IndexOf(' ', indexToStartSearchingForWordBoundary,
				                          phraseLengthToCheckForWordBoundary) -
		                          indexToStartSearchingForWordBoundary;

		if (indexOfWordBoundary >= 0)
		{
			return indexToStartSearchingForWordBoundary + indexOfWordBoundary + lengthOfWhitespace;
		}

		return indexOfFirstMismatch - defaultCharactersToKeep;
	}

	private static string GetExpected(MatchType? matchType)
		=> matchType switch
		{
			MatchType.Wildcard => " (wildcard pattern)",
			MatchType.Regex => " (regex pattern)",
			_ => " (expected)"
		};
}
