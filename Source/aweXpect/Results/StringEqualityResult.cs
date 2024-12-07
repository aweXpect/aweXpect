﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying
///     options on the <see cref="StringEqualityOptions" />.
/// </summary>
public class StringEqualityResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	StringEqualityOptions options)
	: StringEqualityResult<TType, TThat,
		StringEqualityResult<TType, TThat>>(
		expectationBuilder,
		returnValue,
		options);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying
///     options on the <see cref="StringEqualityOptions" />.
/// </summary>
public class StringEqualityResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	StringEqualityOptions options)
	: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue)
	where TSelf : StringEqualityResult<TType, TThat, TSelf>
{
	/// <summary>
	///     Ignores casing when comparing the <see langword="string" />s,
	///     according to the <paramref name="ignoreCase" /> parameter.
	/// </summary>
	public TSelf IgnoringCase(bool ignoreCase = true)
	{
		options.IgnoringCase(ignoreCase);
		return (TSelf)this;
	}

	/// <summary>
	///     Ignores the newline style when comparing <see langword="string" />s,
	///     according to the <paramref name="ignoreNewlineStyle" /> parameter.
	/// </summary>
	/// <remarks>
	///     Enabling this option will replace all occurrences of <c>\r\n</c> and <c>\r</c> with <c>\n</c> in the strings before
	///     comparing them.
	/// </remarks>
	public TSelf IgnoringNewlineStyle(bool ignoreNewlineStyle = true)
	{
		options.IgnoringNewlineStyle(ignoreNewlineStyle);
		return (TSelf)this;
	}

	/// <summary>
	///     Uses the provided <paramref name="comparer" /> for comparing <see langword="string" />s.
	/// </summary>
	public TSelf Using(
		IEqualityComparer<string> comparer)
	{
		options.UsingComparer(comparer);
		return (TSelf)this;
	}
	/// <summary>
	///     Interprets the expected <see langword="string" /> as <see cref="Regex" /> pattern.
	/// </summary>
	public TSelf AsRegex()
	{
		options.AsRegex();
		return (TSelf)this;
	}

	/// <summary>
	///     Interprets the expected <see langword="string" /> as wildcard pattern.<br />
	///     Supports * to match zero or more characters and ? to match exactly one character.
	/// </summary>
	public TSelf AsWildcard()
	{
		options.AsWildcard();
		return (TSelf)this;
	}

	/// <summary>
	///     Interprets the expected <see langword="string" /> to be exactly equal.
	/// </summary>
	public TSelf Exactly()
	{
		options.Exactly();
		return (TSelf)this;
	}
}
