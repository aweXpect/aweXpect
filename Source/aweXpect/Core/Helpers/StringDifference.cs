using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testably.Expectations.Core.Helpers;

internal class StringDifference(
	string actualValue,
	string expectedValue,
	IEqualityComparer<string>? comparer = null)
{
	/// <summary>
	///     Returns the first index at which the two values do not match.
	/// </summary>
	public int IndexOfFirstMismatch
	{
		get
		{
			_indexOfFirstMismatch ??=
				GetIndexOfFirstMismatch(actualValue, expectedValue, _comparer);
			return _indexOfFirstMismatch.Value;
		}
	}

	private readonly IEqualityComparer<string> _comparer = comparer ?? StringComparer.Ordinal;
	private int? _indexOfFirstMismatch;

	public override string ToString()
	{
		return ToString("differs");
	}

	/// <summary>
	///     Writes a string representation of the difference, starting with the <paramref name="prefix" />.
	/// </summary>
	/// <param name="prefix">The prefix, e.g. <c>differs at index</c></param>
	/// <returns></returns>
	public string ToString(string prefix)
	{
		int firstIndexOfMismatch = IndexOfFirstMismatch;

		int trimStart =
			GetStartIndexOfPhraseToShowBeforeTheMismatchingIndex(actualValue, firstIndexOfMismatch);
		const string linePrefix = "  \"";
		const string suffix = "\"";
		const char arrowDown = '\u2193';
		const char arrowUp = '\u2191';

		int whiteSpaceCountBeforeArrow = firstIndexOfMismatch - trimStart + linePrefix.Length;

		if (trimStart > 0)
		{
			whiteSpaceCountBeforeArrow++;
		}

		string? visibleText = actualValue[trimStart..firstIndexOfMismatch];
		whiteSpaceCountBeforeArrow += visibleText.Count(c => c is '\r' or '\n');

		StringBuilder? sb = new();
		string? matchingString = actualValue[..IndexOfFirstMismatch];
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
		AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(sb, linePrefix, actualValue,
			trimStart, suffix);
		AppendPrefixAndEscapedPhraseToShowWithEllipsisAndSuffix(sb, linePrefix, expectedValue,
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
			.Substring(indexOfStartingPhrase, subjectLength).ToSingleLine());

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

		for (int index = 0; index < Math.Max(actualValue.Length, expectedValue.Length); index++)
		{
			string actualChar = actualValue.ElementAtOrDefault(index).ToString();
			string expectedChar = expectedValue.ElementAtOrDefault(index).ToString();
			if (index >= expectedValue.Length || !comparer.Equals(actualChar, expectedChar))
			{
				return index;
			}
		}

		if (actualValue.Length < expectedValue.Length)
		{
			return actualValue.Length;
		}

		return -1;
	}

	/// <summary>
	///     Calculates how many characters to keep in <paramref name="value" />.
	/// </summary>
	/// <remarks>
	///     If a word end is found between 15 and 25 characters, use this word end, otherwise keep 20 characters.
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
