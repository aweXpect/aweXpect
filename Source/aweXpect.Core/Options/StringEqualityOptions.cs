﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using aweXpect.Core;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

/// <summary>
///     Equality options for <see langword="string" />s.
/// </summary>
public partial class StringEqualityOptions : IOptionsEquality<string?>
{
	private const int DefaultMaxLength = 30;
	private const int LongMaxLength = 300;
	private static readonly IMatchType ExactMatch = new ExactMatchType();
	private static readonly IMatchType RegexMatch = new RegexMatchType();

	private static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(1000);
	private static readonly IMatchType WildcardMatch = new WildcardMatchType();
	private IEqualityComparer<string>? _comparer;
	private bool _ignoreCase;
	private bool _ignoreLeadingWhiteSpace;
	private bool _ignoreNewlineStyle;
	private bool _ignoreTrailingWhiteSpace;
	private IMatchType _type = ExactMatch;

	/// <inheritdoc />
	public bool AreConsideredEqual(string? actual, string? expected)
	{
		if (_ignoreNewlineStyle)
		{
			actual = actual.RemoveNewlineStyle();
			expected = expected.RemoveNewlineStyle();
		}

		if (_ignoreLeadingWhiteSpace)
		{
			actual = actual?.TrimStart();
			expected = expected?.TrimStart();
		}

		if (_ignoreTrailingWhiteSpace)
		{
			actual = actual?.TrimEnd();
			expected = expected?.TrimEnd();
		}

		return _type.Matches(actual, expected, _ignoreCase, _comparer ?? UseDefaultComparer(_ignoreCase));
	}

	/// <summary>
	///     Get the expectations text.
	/// </summary>
	public string GetExpectation(string? expected, bool useActiveGrammaticVoice)
		=> useActiveGrammaticVoice switch
		{
			true =>
				$"{(_type is ExactMatchType ? "be equal to" : "match")} {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}",
			false =>
				$"{(_type is ExactMatchType ? "equal to" : "matching")} {Formatter.Format(expected.TruncateWithEllipsisOnWord(DefaultMaxLength).ToSingleLine())}"
		};

	/// <summary>
	///     Get an extended failure text.
	/// </summary>
	public string GetExtendedFailure(string it, string? expected, string? actual)
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

	/// <summary>
	///     Ignores leading white-space when comparing <see langword="string" />s,
	///     according to the <paramref name="ignoreLeadingWhiteSpace" /> parameter.
	/// </summary>
	/// <remarks>
	///     Note:<br />
	///     This affects the index of first mismatch, as the removed whitespace is also ignored for the index calculation!
	/// </remarks>
	public StringEqualityOptions IgnoringLeadingWhiteSpace(bool ignoreLeadingWhiteSpace = true)
	{
		_ignoreLeadingWhiteSpace = ignoreLeadingWhiteSpace;
		return this;
	}

	/// <summary>
	///     Ignores trailing white-space when comparing <see langword="string" />s,
	///     according to the <paramref name="ignoreTrailingWhiteSpace" /> parameter.
	/// </summary>
	public StringEqualityOptions IgnoringTrailingWhiteSpace(bool ignoreTrailingWhiteSpace = true)
	{
		_ignoreTrailingWhiteSpace = ignoreTrailingWhiteSpace;
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
}