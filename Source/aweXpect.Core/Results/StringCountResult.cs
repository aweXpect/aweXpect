using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="CountResult{TResult,TValue}" />, allows specifying
///     options on the <see cref="StringEqualityOptions" />.
/// </summary>
public class StringCountResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	Quantifier quantifier,
	StringEqualityOptions options)
	: StringCountResult<TType, TThat,
		StringCountResult<TType, TThat>>(
		expectationBuilder,
		returnValue,
		quantifier,
		options);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="CountResult{TResult,TValue}" />, allows specifying
///     options on the <see cref="StringEqualityOptions" />.
/// </summary>
public class StringCountResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	Quantifier quantifier,
	StringEqualityOptions options)
	: CountResult<TType, TThat, TSelf>(expectationBuilder, returnValue, quantifier)
	where TSelf : StringCountResult<TType, TThat, TSelf>
{
	/// <summary>
	///     Ignores casing when comparing the <see langword="string" />s.
	/// </summary>
	public TSelf IgnoringCase(bool ignoreCase = true)
	{
		options.IgnoringCase(ignoreCase);
		return (TSelf)this;
	}

	/// <summary>
	///     Ignores the newline style when comparing <see langword="string" />s.
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
}
