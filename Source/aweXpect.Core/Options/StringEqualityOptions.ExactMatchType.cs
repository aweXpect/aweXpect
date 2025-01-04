using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

public partial class StringEqualityOptions
{
	private sealed class ExactMatchType : IMatchType
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

		/// <inheritdoc />
		public string GetExtendedFailure(string it, string? actual, string? pattern,
			bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (actual == null || pattern == null)
			{
				return $"{it} was {Formatter.Format(actual)}";
			}

			string prefix =
				$"{it} was {Formatter.Format(actual.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}";
			int minCommonLength = Math.Min(actual.Length, pattern.Length);
			StringDifference stringDifference = new(actual, pattern, comparer);
			if (stringDifference.IndexOfFirstMismatch == 0 &&
			    comparer.Equals(actual.TrimStart(), pattern))
			{
				return
					$"{prefix} which has unexpected whitespace (\"{actual.Substring(0, GetIndexOfFirstMatch(actual, pattern, comparer)).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the beginning)";
			}

			if (stringDifference.IndexOfFirstMismatch == 0 &&
			    comparer.Equals(actual, pattern.TrimStart()))
			{
				return
					$"{prefix} which misses some whitespace (\"{pattern.Substring(0, GetIndexOfFirstMatch(pattern, actual, comparer)).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the beginning)";
			}

			if (stringDifference.IndexOfFirstMismatch == minCommonLength &&
			    comparer.Equals(actual.TrimEnd(), pattern))
			{
				return
					$"{prefix} which has unexpected whitespace (\"{actual.Substring(stringDifference.IndexOfFirstMismatch).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the end)";
			}

			if (stringDifference.IndexOfFirstMismatch == minCommonLength &&
			    comparer.Equals(actual, pattern.TrimEnd()))
			{
				return
					$"{prefix} which misses some whitespace (\"{pattern.Substring(stringDifference.IndexOfFirstMismatch).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the end)";
			}

			if (actual.Length < pattern.Length &&
			    stringDifference.IndexOfFirstMismatch == actual.Length)
			{
				return
					$"{prefix} with a length of {actual.Length} which is shorter than the expected length of {pattern.Length} and misses:{Environment.NewLine}  \"{pattern.Substring(actual.Length).TruncateWithEllipsis(100)}\"";
			}

			if (actual.Length > pattern.Length &&
			    stringDifference.IndexOfFirstMismatch == pattern.Length)
			{
				return
					$"{prefix} with a length of {actual.Length} which is longer than the expected length of {pattern.Length} and has superfluous:{Environment.NewLine}  \"{actual.Substring(pattern.Length).TruncateWithEllipsis(100)}\"";
			}

			return $"{prefix} which {stringDifference}";
		}

		public bool Matches(string? value, string? pattern, bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (value is null && pattern is null)
			{
				return true;
			}

			if (value is null || pattern is null)
			{
				return false;
			}

			return comparer.Equals(value, pattern);
		}

		public string GetExpectation(string? expected, bool useActiveGrammaticVoice)
			=> useActiveGrammaticVoice switch
			{
				true =>
					$"be equal to {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}",
				false =>
					$"equal to {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}"
			};

		#endregion
	}
}
