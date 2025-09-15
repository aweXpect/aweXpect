using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

public partial class StringEqualityOptions
{
	private static readonly IStringMatchType SuffixMatch = new SuffixMatchType();

	/// <summary>
	///     Interprets the expected <see langword="string" /> to be a suffix for the actual string.
	/// </summary>
	public StringEqualityOptions AsSuffix()
	{
		_matchType = SuffixMatch;
		return this;
	}

	private sealed class SuffixMatchType : IStringMatchType
	{
		private static int GetIndexOfFirstMatch(string stringWithLeadingWhitespace, string value,
			IEqualityComparer<string> comparer)
		{
			int indexOfFirstMatch;
			for (indexOfFirstMatch = 0;
			     indexOfFirstMatch <= stringWithLeadingWhitespace.Length - value.Length;
			     indexOfFirstMatch++)
			{
				if (comparer.Equals(stringWithLeadingWhitespace.Substring(indexOfFirstMatch, value.Length), value))
				{
					break;
				}
			}

			return indexOfFirstMatch;
		}

		#region IMatchType Members

		/// <inheritdoc
		///     cref="IStringMatchType.GetExtendedFailure(string, string?, string?, bool, IEqualityComparer{string}, StringDifferenceSettings?)" />
		public string GetExtendedFailure(string it, string? actual, string? expected,
			bool ignoreCase,
			IEqualityComparer<string> comparer,
			StringDifferenceSettings? settings)
		{
			if (actual == null || expected == null)
			{
				return $"{it} was {Formatter.Format(actual)}";
			}

			string prefix =
				$"{it} was {Formatter.Format(actual.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}";
			int minCommonLength = Math.Min(actual.Length, expected.Length);
			StringDifference stringDifference = new(actual, expected, comparer,
				settings.WithMatchType(StringDifference.MatchType.Suffix));
			int indexOfFirstMismatch = stringDifference.IndexOfFirstMismatch(StringDifference.MatchType.Suffix);
			if (indexOfFirstMismatch == 0 && comparer.Equals(actual, expected.TrimStart()))
			{
				return
					$"{prefix} which misses some whitespace (\"{expected.Substring(0, GetIndexOfFirstMatch(expected, actual, comparer)).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the beginning)";
			}

			if (indexOfFirstMismatch == actual.Length)
			{
				string? trimmedActual = actual.TrimEnd();
				int commonLength = Math.Min(trimmedActual.Length, expected.Length);
				if (comparer.Equals(trimmedActual[^commonLength..], expected[^commonLength..]))
				{
					return
						$"{prefix} which has unexpected whitespace (\"{actual.Substring(trimmedActual.Length).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the end)";
				}
			}

			if (indexOfFirstMismatch == minCommonLength && comparer.Equals(actual, expected.TrimEnd()))
			{
				return
					$"{prefix} which misses some whitespace (\"{expected.Substring(indexOfFirstMismatch).DisplayWhitespace().TruncateWithEllipsis(100)}\" at the end)";
			}

			if (actual.Length < expected.Length && indexOfFirstMismatch == actual.Length)
			{
				return
					$"{prefix} with a length of {actual.Length} which is shorter than the expected length of {expected.Length} and misses:{Environment.NewLine}  \"{expected.Substring(actual.Length).TruncateWithEllipsis(100)}\"";
			}

			return $"{prefix} which {stringDifference}";
		}

		/// <inheritdoc cref="IStringMatchType.AreConsideredEqual(string?, string?, bool, IEqualityComparer{string})" />
#if NET8_0_OR_GREATER
		public ValueTask<bool>
#else
		public Task<bool>
#endif
		AreConsideredEqual(string? actual, string? expected, bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (actual is null && expected is null)
			{
#if NET8_0_OR_GREATER
				return ValueTask.FromResult(true);
#else
				return Task.FromResult(true);
#endif
			}

			if (actual is null || expected is null)
			{
#if NET8_0_OR_GREATER
				return ValueTask.FromResult(false);
#else
				return Task.FromResult(false);
#endif
			}

#if NET8_0_OR_GREATER
			return ValueTask.FromResult(actual.Length >= expected.Length && comparer.Equals(actual[^expected.Length..], expected));
#else
			return Task.FromResult(actual.Length >= expected.Length && comparer.Equals(actual[^expected.Length..], expected));
#endif
		}

		/// <inheritdoc cref="IStringMatchType.GetExpectation(string?, ExpectationGrammars)" />
		public string GetExpectation(string? expected, ExpectationGrammars grammars)
			=> (grammars.HasFlag(ExpectationGrammars.Active), grammars.HasFlag(ExpectationGrammars.Negated)) switch
			{
				(true, false) =>
					$"ends with {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}",
				(false, false) =>
					$"ending with {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}",
				(true, true) =>
					$"does not end with {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}",
				(false, true) =>
					$"not ending with {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}",
			};

		/// <inheritdoc cref="IStringMatchType.GetTypeString()" />
		public string GetTypeString()
			=> " as suffix";

		/// <inheritdoc cref="IStringMatchType.GetOptionString(bool, IEqualityComparer{string})" />
		public string GetOptionString(bool ignoreCase, IEqualityComparer<string>? comparer)
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
