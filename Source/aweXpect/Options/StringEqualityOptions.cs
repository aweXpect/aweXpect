using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect.Options;

/// <summary>
///     Quantifier an occurrence.
/// </summary>
public class StringEqualityOptions : IOptionsEquality<string?>
{
	private const int DefaultMaxLength = 30;
	private const int LongMaxLength = 300;
	private static readonly IMatchType ExactMatch = new ExactMatchType();
	private static readonly IMatchType RegexMatch = new RegexMatchType();

	private static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(1000);
	private static readonly IMatchType WildcardMatch = new WildcardMatchType();
	private IEqualityComparer<string>? _comparer;
	private bool _ignoreCase;
	private bool _ignoreNewlineStyle;
	private IMatchType _type = ExactMatch;

	/// <inheritdoc />
	public bool AreConsideredEqual(string? actual, string? expected)
	{
		if (_ignoreNewlineStyle)
		{
			actual = actual.RemoveNewlineStyle();
			expected = expected.RemoveNewlineStyle();
		}

		return _type.Matches(actual, expected, _ignoreCase, _comparer ?? UseDefaultComparer(_ignoreCase));
	}

	internal string GetExpectation(string? expected, bool useActiveGrammaticVoice)
		=> useActiveGrammaticVoice switch
		{
			true =>
				$"{(_type is ExactMatchType ? "be equal to" : "match")} {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}",
			false =>
				$"{(_type is ExactMatchType ? "equal to" : "matching")} {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}"
		};

	internal string GetExtendedFailure(string it, string? expected, string? actual)
		=> _type.GetExtendedFailure(it, actual, expected, _ignoreCase,
			_comparer ?? UseDefaultComparer(_ignoreCase));

	/// <summary>
	///     Ignores casing when comparing the <see langword="string" />s.
	/// </summary>
	public StringEqualityOptions IgnoringCase(bool ignoreCase = true)
	{
		_ignoreCase = ignoreCase;
		return this;
	}

	/// <summary>
	///     Ignores the newline style when comparing <see langword="string" />s.
	/// </summary>
	/// <remarks>
	///     Enabling this option will replace all occurrences of <c>\r\n</c> and <c>\r</c> with <c>\n</c> in the strings before
	///     comparing them.
	/// </remarks>
	public StringEqualityOptions IgnoringNewlineStyle(bool ignoreNewlineStyle = true)
	{
		_ignoreNewlineStyle = ignoreNewlineStyle;
		return this;
	}

	/// <inheritdoc />
	public override string ToString()
	{
		if (_comparer != null)
		{
			return $" using {Formatter.Format(_comparer.GetType())}";
		}

		if (_ignoreCase)
		{
			return " ignoring case";
		}

		return "";
	}

	/// <summary>
	///     Specifies a specific <see cref="IEqualityComparer{T}" /> to use for comparing <see langword="string" />s.
	/// </summary>
	/// <remarks>
	///     If set to <see langword="null" /> (default), uses the <see cref="StringComparer.Ordinal" /> or
	///     <see cref="StringComparer.OrdinalIgnoreCase" /> depending on whether the casing is ignored.
	/// </remarks>
	public StringEqualityOptions UsingComparer(IEqualityComparer<string>? comparer)
	{
		_comparer = comparer;
		return this;
	}

	/// <summary>
	///     Interprets the expected <see langword="string" /> as <see cref="Regex" /> pattern.
	/// </summary>
	public StringEqualityOptions AsRegex()
	{
		_type = RegexMatch;
		return this;
	}

	/// <summary>
	///     Interprets the expected <see langword="string" /> as wildcard pattern.<br />
	///     Supports * to match zero or more characters and ? to match exactly one character.
	/// </summary>
	public StringEqualityOptions AsWildcard()
	{
		_type = WildcardMatch;
		return this;
	}

	/// <summary>
	///     Interprets the expected <see langword="string" /> to be exactly equal.
	/// </summary>
	public StringEqualityOptions Exactly()
	{
		_type = ExactMatch;
		return this;
	}

	private static StringComparer UseDefaultComparer(bool ignoreCase)
		=> ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;

	private interface IMatchType
	{
		string GetExtendedFailure(string it, string? actual, string? pattern, bool ignoreCase,
			IEqualityComparer<string> comparer);

		bool Matches(string? value, string? pattern, bool ignoreCase,
			IEqualityComparer<string> comparer);
	}

	private sealed class WildcardMatchType : IMatchType
	{
		private static string WildcardToRegularExpression(string value)
		{
			string regex = Regex.Escape(value)
				.Replace("\\?", ".")
				.Replace("\\*", "(.|\\n)*");
			return $"^{regex}$";
		}

		#region IMatchType Members

		/// <inheritdoc />
		public string GetExtendedFailure(string it, string? actual, string? pattern,
			bool ignoreCase,
			IEqualityComparer<string> comparer)
			=> $"{it} was {Formatter.Format(actual.TruncateWithEllipsisOnWord(LongMaxLength).Indent(indentFirstLine: false))}";

		public bool Matches(string? value, string? pattern, bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (value is null || pattern is null)
			{
				return false;
			}

			RegexOptions options = RegexOptions.Multiline;
			if (ignoreCase)
			{
				options |= RegexOptions.IgnoreCase;
			}

			return Regex.IsMatch(value, WildcardToRegularExpression(pattern), options,
				RegexTimeout);
		}

		#endregion
	}

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
				return $"{it} was <null>";
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

			return $"{prefix} which {new StringDifference(actual, pattern, comparer)}";
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

		#endregion
	}

	private sealed class RegexMatchType : IMatchType
	{
		#region IMatchType Members

		/// <inheritdoc />
		public string GetExtendedFailure(string it, string? actual, string? pattern,
			bool ignoreCase,
			IEqualityComparer<string> comparer)
			=> $"{it} was {Formatter.Format(actual.TruncateWithEllipsisOnWord(LongMaxLength).Indent(indentFirstLine: false))}";

		public bool Matches(string? value, string? pattern, bool ignoreCase,
			IEqualityComparer<string> comparer)
		{
			if (value is null || pattern is null)
			{
				return false;
			}

			RegexOptions options = RegexOptions.Multiline;
			if (ignoreCase)
			{
				options |= RegexOptions.IgnoreCase;
			}

			return Regex.IsMatch(value, pattern, options, RegexTimeout);
		}

		#endregion
	}
}
