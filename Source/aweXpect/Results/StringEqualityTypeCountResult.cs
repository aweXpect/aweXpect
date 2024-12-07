using System.Text.RegularExpressions;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     Allows specifying the equality type for the string equality check.
/// </summary>
public class StringEqualityTypeCountResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	Quantifier quantifier,
	StringEqualityOptions options)
	: StringCountResult<TType, TThat>(expectationBuilder, returnValue, quantifier, options)
{
	private readonly StringEqualityOptions _options = options;

	/// <summary>
	///     Interprets the expected <see langword="string" /> as <see cref="Regex" /> pattern.
	/// </summary>
	public StringCountResult<TType, TThat> AsRegex()
	{
		_options.AsRegex();
		return this;
	}

	/// <summary>
	///     Interprets the expected <see langword="string" /> as wildcard pattern.<br />
	///     Supports * to match zero or more characters and ? to match exactly one character.
	/// </summary>
	public StringCountResult<TType, TThat> AsWildcard()
	{
		_options.AsWildcard();
		return this;
	}

	/// <summary>
	///     Interprets the expected <see langword="string" /> to be exactly equal.
	/// </summary>
	public StringCountResult<TType, TThat> Exactly()
	{
		_options.Exactly();
		return this;
	}
}
