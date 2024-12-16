using System.Text.RegularExpressions;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     Allows specifying the equality type for the string equality check.
/// </summary>
public class StringCollectionBeTypeResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	StringEqualityOptions options,
	CollectionMatchOptions collectionMatchOptions)
	: StringCollectionBeResult<TType, TThat>(
		expectationBuilder,
		returnValue,
		options,
		collectionMatchOptions)
{
	private readonly StringEqualityOptions _options = options;

	/// <summary>
	///     Interprets the expected <see langword="string" /> as <see cref="Regex" /> pattern.
	/// </summary>
	public StringCollectionBeResult<TType, TThat> AsRegex()
	{
		_options.AsRegex();
		return this;
	}

	/// <summary>
	///     Interprets the expected <see langword="string" /> as wildcard pattern.<br />
	///     Supports * to match zero or more characters and ? to match exactly one character.
	/// </summary>
	public StringCollectionBeResult<TType, TThat> AsWildcard()
	{
		_options.AsWildcard();
		return this;
	}

	/// <summary>
	///     Interprets the expected <see langword="string" /> to be exactly equal.
	/// </summary>
	public StringCollectionBeResult<TType, TThat> Exactly()
	{
		_options.Exactly();
		return this;
	}
}
