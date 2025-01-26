using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using aweXpect.Core;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

public partial class StringEqualityOptions
{
	/// <summary>
	///     Interprets the expected <see langword="string" /> as <see cref="Regex" /> pattern.
	/// </summary>
	public StringEqualityOptions AsRegex()
	{
		_matchType = RegexMatch;
		return this;
	}

	private sealed class RegexMatchType : IStringMatchType
	{
		#region IMatchType Members

		/// <inheritdoc cref="IStringMatchType.GetExtendedFailure(string, string?, string?, bool, IEqualityComparer{string})" />
		public string GetExtendedFailure(string it, string? actual, string? expected,
			bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (expected is null)
			{
				return $"could not compare the <null> regex with {Formatter.Format(actual)}";
			}

			return
				$"{it} did not match{Environment.NewLine}  \u2193 (actual){Environment.NewLine}  {Formatter.Format(actual.DisplayWhitespace().TruncateWithEllipsisOnWord(LongMaxLength))}{Environment.NewLine}  {Formatter.Format(expected.DisplayWhitespace().TruncateWithEllipsis(LongMaxLength))}{Environment.NewLine}  \u2191 (regex)";
		}

		/// <inheritdoc cref="IStringMatchType.AreConsideredEqual(string?, string?, bool, IEqualityComparer{string})" />
		public bool AreConsideredEqual(string? actual, string? expected, bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (actual is null || expected is null)
			{
				return false;
			}

			RegexOptions options = RegexOptions.Multiline;
			if (ignoreCase)
			{
				options |= RegexOptions.IgnoreCase;
			}

			return Regex.IsMatch(actual, expected, options, RegexTimeout);
		}

		/// <inheritdoc cref="IStringMatchType.GetExpectation(string?, bool)" />
		public string GetExpectation(string? expected, bool useActiveGrammaticVoice)
			=> useActiveGrammaticVoice switch
			{
				true =>
					$"match regex {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}",
				false =>
					$"matching regex {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}"
			};

		/// <inheritdoc cref="IStringMatchType.ToString(bool, IEqualityComparer{string})" />
		public string ToString(bool ignoreCase, IEqualityComparer<string>? comparer)
			=> " as regex";

		#endregion
	}
}
