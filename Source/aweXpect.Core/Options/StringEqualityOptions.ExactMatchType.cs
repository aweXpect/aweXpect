using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

public partial class StringEqualityOptions
{
	/// <summary>
	///     Interprets the expected <see langword="string" /> to be exactly equal.
	/// </summary>
	public StringEqualityOptions Exactly()
	{
		_matchType = ExactMatch;
		return this;
	}

	private sealed class ExactMatchType : IStringMatchType
	{
		private static int GetIndexOfFirstMatch(string stringWithLeadingWhitespace, string value,
			IEqualityComparer<string> comparer)
		{
			for (int i = 0; i <= stringWithLeadingWhitespace.Length - value.Length; i++)
			{
				if (comparer.Equals(
					    stringWithLeadingWhitespace.Substring(i, value.Length), value))
				{
					return i;
				}
			}

			return 0;
		}

		#region IMatchType Members

		/// <inheritdoc cref="IStringMatchType.GetExtendedFailure(string, string?, string?, bool, IEqualityComparer{string})" />
		public string GetExtendedFailure(string it, string? actual, string? expected,
			bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (actual == null || expected == null)
			{
				return $"{it} was {Formatter.Format(actual)}";
			}

			string prefix =
				$"{it} was {Formatter.Format(actual.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}";
			int minCommonLength = Math.Min(actual.Length, expected.Length);
			StringDifference stringDifference = new(actual, expected, comparer);
			if (stringDifference.IndexOfFirstMismatch == 0 &&
			    comparer.Equals(actual.TrimStart(), expected))
			{
				return
					$"{prefix} which has unexpected whitespace (\"{actual.Substring(0, GetIndexOfFirstMatch(actual, expected, comparer)).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the beginning)";
			}

			if (stringDifference.IndexOfFirstMismatch == 0 &&
			    comparer.Equals(actual, expected.TrimStart()))
			{
				return
					$"{prefix} which misses some whitespace (\"{expected.Substring(0, GetIndexOfFirstMatch(expected, actual, comparer)).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the beginning)";
			}

			if (stringDifference.IndexOfFirstMismatch == minCommonLength &&
			    comparer.Equals(actual.TrimEnd(), expected))
			{
				return
					$"{prefix} which has unexpected whitespace (\"{actual.Substring(stringDifference.IndexOfFirstMismatch).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the end)";
			}

			if (stringDifference.IndexOfFirstMismatch == minCommonLength &&
			    comparer.Equals(actual, expected.TrimEnd()))
			{
				return
					$"{prefix} which misses some whitespace (\"{expected.Substring(stringDifference.IndexOfFirstMismatch).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the end)";
			}

			if (actual.Length < expected.Length &&
			    stringDifference.IndexOfFirstMismatch == actual.Length)
			{
				return
					$"{prefix} with a length of {actual.Length} which is shorter than the expected length of {expected.Length} and misses:{Environment.NewLine}  \"{expected.Substring(actual.Length).TruncateWithEllipsis(100)}\"";
			}

			if (actual.Length > expected.Length &&
			    stringDifference.IndexOfFirstMismatch == expected.Length)
			{
				return
					$"{prefix} with a length of {actual.Length} which is longer than the expected length of {expected.Length} and has superfluous:{Environment.NewLine}  \"{actual.Substring(expected.Length).TruncateWithEllipsis(100)}\"";
			}

			return $"{prefix} which {stringDifference}";
		}

		/// <inheritdoc cref="IStringMatchType.AreConsideredEqual(string?, string?, bool, IEqualityComparer{string})" />
		public bool AreConsideredEqual(string? actual, string? expected, bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (actual is null && expected is null)
			{
				return true;
			}

			if (actual is null || expected is null)
			{
				return false;
			}

			return comparer.Equals(actual, expected);
		}

		/// <inheritdoc cref="IStringMatchType.GetExpectation(string?, bool)" />
		public string GetExpectation(string? expected, bool useActiveGrammaticVoice)
			=> useActiveGrammaticVoice switch
			{
				true =>
					$"be equal to {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}",
				false =>
					$"equal to {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}"
			};

		public string ToString(bool ignoreCase, IEqualityComparer<string>? comparer) 
		{
			if (comparer != null)
			{
				return $" using {Formatter.Format(comparer.GetType())}";
			}

			if (ignoreCase)
			{
				return " ignoring case";
			}

			return "";
		}

		#endregion
	}
}
