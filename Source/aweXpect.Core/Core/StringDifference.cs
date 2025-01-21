using System;
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
public class StringDifference(
	string? actual,
	string? expected,
	IEqualityComparer<string>? comparer = null)
{
	private readonly IEqualityComparer<string> _comparer = comparer ?? StringComparer.Ordinal;
	private int? _indexOfFirstMismatch;

	/// <summary>
	///     Returns the first index at which the two values do not match.
	/// </summary>
	public int IndexOfFirstMismatch
	{
		get
		{
			_indexOfFirstMismatch ??=
				GetIndexOfFirstMismatch(actual, expected, _comparer);
			return _indexOfFirstMismatch.Value;
		}
	}

	/// <inheritdoc />
	public override string ToString() => ToString("differs");

	/// <summary>
	///     Writes a string representation of the difference, starting with the <paramref name="prefix" />.
	/// </summary>
	/// <param name="prefix">The prefix, e.g. <c>differs at index</c></param>
	private string ToString(string prefix)
	{
		const char arrowDown = '\u2193';
		const char arrowUp = '\u2191';
		const string linePrefix = "  \"";
		const string suffix = "\"";

		int firstIndexOfMismatch = IndexOfFirstMismatch;
		if (firstIndexOfMismatch < 0)
		{
			return prefix;
		}

		StringBuilder sb = new();
		if (actual == null)
		{
			sb.Append(prefix).Append(" at index ").Append(firstIndexOfMismatch).AppendLine(":");
			sb.Append("  ").Append(arrowDown).AppendLine(" (actual)");
			sb.AppendLine("  <null>");
			AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(sb, linePrefix, expected!,
				0, suffix);
			sb.Append("  ").Append(arrowUp).Append(" (expected)");
			return sb.ToString();
		}

		if (expected == null)
		{
			sb.Append(prefix).Append(" at index ").Append(firstIndexOfMismatch).AppendLine(":");
			sb.Append("  ").Append(arrowDown).AppendLine(" (actual)");
			AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(sb, linePrefix, actual!,
				0, suffix);
			sb.AppendLine("  <null>");
			sb.Append("  ").Append(arrowUp).Append(" (expected)");
			return sb.ToString();
		}

		int trimStart =
			GetStartIndexOfPhraseToShowBeforeTheMismatchingIndex(actual, firstIndexOfMismatch);

		int whiteSpaceCountBeforeArrow = firstIndexOfMismatch - trimStart + linePrefix.Length;

		if (trimStart > 0)
		{
			whiteSpaceCountBeforeArrow++;
		}

		string visibleText = actual[trimStart..firstIndexOfMismatch];
		whiteSpaceCountBeforeArrow += visibleText.Count(c => c is '\r' or '\n');

		string matchingString = actual[..IndexOfFirstMismatch];
		int lineNumber = matchingString.Count(c => c == '\n');

		if (lineNumber > 0)
		{
			int indexOfLastNewlineBeforeMismatch = matchingString.LastIndexOf('\n');
			int column = matchingString.Length - indexOfLastNewlineBeforeMismatch;
			sb.Append(prefix).Append(" on line ").Append(lineNumber + 1).Append(" and column ")
				.Append(column).Append(" (index ").Append(firstIndexOfMismatch).AppendLine("):");
		}
		else
		{
			sb.Append(prefix).Append(" at index ").Append(firstIndexOfMismatch).AppendLine(":");
		}

		sb.Append(' ', whiteSpaceCountBeforeArrow).Append(arrowDown).AppendLine(" (actual)");
		AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(sb, linePrefix, actual,
			trimStart, suffix);
		AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(sb, linePrefix, expected,
			trimStart, suffix);
		sb.Append(' ', whiteSpaceCountBeforeArrow).Append(arrowUp).Append(" (expected)");

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
		for (int index = 0; index < maxCommonLength; index++)
		{
			string actualChar = actualValue.ElementAt(index).ToString();
			string expectedChar = expectedValue.ElementAt(index).ToString();
			if (!comparer.Equals(actualChar, expectedChar))
			{
				return index;
			}
		}

		return maxCommonLength;
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
}
