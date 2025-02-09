using System.Collections.Generic;
using System.Text.RegularExpressions;
using aweXpect.Core;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

public partial class StringEqualityOptions
{
	/// <summary>
	///     Interprets the expected <see langword="string" /> as wildcard pattern.<br />
	///     Supports * to match zero or more characters and ? to match exactly one character.
	/// </summary>
	public StringEqualityOptions AsWildcard()
	{
		_matchType = WildcardMatch;
		return this;
	}

	private sealed class WildcardMatchType : IStringMatchType
	{
		private static string WildcardToRegularExpression(string value)
		{
			string regex = Regex.Escape(value)
				.Replace("\\?", ".")
				.Replace("\\*", "(.|\\n)*");
			return $"^{regex}$";
		}

		#region IStringMatchType Members

		/// <inheritdoc
		///     cref="IStringMatchType.GetExtendedFailure(string, string?, string?, bool, IEqualityComparer{string}, StringDifferenceSettings?)" />
		public string GetExtendedFailure(string it, string? actual, string? expected,
			bool ignoreCase,
			IEqualityComparer<string> comparer,
			StringDifferenceSettings? settings)
		{
			if (expected is null)
			{
				return $"could not compare the <null> wildcard pattern with {Formatter.Format(actual)}";
			}

			StringDifference stringDifference = new(actual, expected, comparer,
				settings.WithMatchType(StringDifference.MatchType.Wildcard));
			return $"{it} did not match{stringDifference.ToString("")}";
		}

		/// <inheritdoc cref="IStringMatchType.AreConsideredEqual(string?, string?, bool, IEqualityComparer{string})" />
		public bool AreConsideredEqual(string? actual, string? expected, bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (actual is null || expected is null)
			{
				return false;
			}

			RegexOptions options = ignoreCase
				? RegexOptions.Multiline | RegexOptions.IgnoreCase
				: RegexOptions.Multiline;

			return Regex.IsMatch(actual, WildcardToRegularExpression(expected), options,
				RegexTimeout);
		}

		/// <inheritdoc cref="IStringMatchType.GetExpectation(string?, ExpectationGrammars)" />
		public string GetExpectation(string? expected, ExpectationGrammars grammar)
			=> grammar.HasFlag(ExpectationGrammars.Active) switch
			{
				true =>
					$"matches {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}",
				false =>
					$"matching {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}"
			};

		/// <inheritdoc cref="IStringMatchType.GetTypeString()" />
		public string GetTypeString()
			=> " as wildcard";

		/// <inheritdoc cref="IStringMatchType.GetOptionString(bool, IEqualityComparer{string})" />
		public string GetOptionString(bool ignoreCase, IEqualityComparer<string>? comparer)
			=> "";

		#endregion
	}
}
