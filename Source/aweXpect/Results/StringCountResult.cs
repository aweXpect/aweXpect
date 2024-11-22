using System.Collections.Generic;
using Testably.Expectations.Core;
using Testably.Expectations.Options;

namespace Testably.Expectations.Results;

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
	public StringCountResult<TType, TThat, TSelf> IgnoringCase()
	{
		options.IgnoringCase();
		return this;
	}

	/// <summary>
	///     Uses the provided <paramref name="comparer" /> for comparing <see langword="string" />s.
	/// </summary>
	public StringCountResult<TType, TThat, TSelf> Using(
		IEqualityComparer<string> comparer)
	{
		options.UsingComparer(comparer);
		return this;
	}
}
