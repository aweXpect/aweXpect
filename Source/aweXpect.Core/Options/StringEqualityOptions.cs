using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Helpers;

namespace aweXpect.Options;

/// <summary>
///     Equality options for <see langword="string" />s.
/// </summary>
public partial class StringEqualityOptions : IOptionsEquality<string?>
{
	private const int DefaultMaxLength = 30;

	private static readonly TimeSpan RegexTimeout = TimeSpan.FromMilliseconds(1000);
	private IEqualityComparer<string>? _comparer;
	private bool _ignoreCase;
	private bool _ignoreLeadingWhiteSpace;
	private bool _ignoreNewlineStyle;
	private bool _ignoreTrailingWhiteSpace;
	private IStringMatchType _matchType = ExactMatch;

	/// <inheritdoc />
#if NET8_0_OR_GREATER
	public async ValueTask<bool> AreConsideredEqual<TExpected>(string? actual, TExpected expected)
#else
	public async Task<bool> AreConsideredEqual<TExpected>(string? actual, TExpected expected)
#endif
	{
		bool result;
		if (expected is not string expectedString)
		{
			result = await _matchType.AreConsideredEqual(actual, null, _ignoreCase,
				_comparer);
			return result;
		}

		if (_ignoreNewlineStyle)
		{
			actual = actual.RemoveNewlineStyle();
			expectedString = expectedString.RemoveNewlineStyle();
		}

		if (_ignoreLeadingWhiteSpace)
		{
			actual = actual?.TrimStart();
			expectedString = expectedString.TrimStart();
		}

		if (_ignoreTrailingWhiteSpace)
		{
			actual = actual?.TrimEnd();
			expectedString = expectedString.TrimEnd();
		}

		result = await _matchType.AreConsideredEqual(actual, expectedString, _ignoreCase, _comparer);
		return result;
	}

	/// <summary>
	///     Specifies a new <see cref="IStringMatchType" /> to use for matching two strings.
	/// </summary>
	public void SetMatchType(IStringMatchType matchType) => _matchType = matchType;

	/// <summary>
	///     Get the expectations text.
	/// </summary>
	public string GetExpectation(string? expected, ExpectationGrammars grammars)
		=> _matchType.GetExpectation(expected, grammars) + GetOptionString();

	/// <summary>
	///     Get an extended failure text.
	/// </summary>
	public string GetExtendedFailure(string it, ExpectationGrammars grammars, string? actual, string? expected)
	{
		if (grammars.HasFlag(ExpectationGrammars.Negated))
		{
			return $"{it} was {Formatter.Format(actual)}";
		}

		StringDifferenceSettings? settings = null;
		if (_ignoreLeadingWhiteSpace && actual is not null)
		{
			int ignoredLineCount = 0;
			int ignoredColumnCount = 0;
			foreach (char c in actual)
			{
				if (c == '\n')
				{
					ignoredLineCount++;
					ignoredColumnCount = 0;
				}
				else if (char.IsWhiteSpace(c))
				{
					ignoredColumnCount++;
				}
				else
				{
					break;
				}
			}

			settings = new StringDifferenceSettings(ignoredLineCount, ignoredColumnCount);
		}

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


		return _matchType.GetExtendedFailure(it, actual, expected, _ignoreCase,
			_comparer ?? UseDefaultComparer(_ignoreCase), settings);
	}

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
	public override string ToString() => _matchType.GetTypeString() + GetOptionString();

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

	private static StringComparer UseDefaultComparer(bool ignoreCase)
		=> ignoreCase ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal;


	private string GetOptionString()
	{
		if (!_ignoreLeadingWhiteSpace && !_ignoreTrailingWhiteSpace && !_ignoreNewlineStyle)
		{
			return _matchType.GetOptionString(_ignoreCase, _comparer);
		}

		string? whiteSpaceToken = (_ignoreLeadingWhiteSpace, _ignoreTrailingWhiteSpace) switch
		{
			(true, true) => " white-space",
			(true, false) => " leading white-space",
			(false, true) => " trailing white-space",
			(false, false) => null,
		};

		string? newlineStyleToken = _ignoreNewlineStyle
			? " newline style"
			: null;

		StringBuilder sb = new();
		string initialString = _matchType.GetOptionString(_ignoreCase, _comparer);
		sb.Append(initialString);
		if (initialString.Contains("ignoring"))
		{
			sb.Append(whiteSpaceToken == null || newlineStyleToken == null ? " and" : ",");
		}
		else
		{
			sb.Append(" ignoring");
		}

		if (whiteSpaceToken != null)
		{
			sb.Append(whiteSpaceToken);
			if (newlineStyleToken != null)
			{
				sb.Append(" and");
			}
		}

		if (newlineStyleToken != null)
		{
			sb.Append(newlineStyleToken);
		}

		return sb.ToString();
	}
}
