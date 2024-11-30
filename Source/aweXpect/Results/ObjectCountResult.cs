using System;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Options;

namespace aweXpect.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="CountResult{TResult,TValue}" />, allows specifying
///     options on the <see cref="ObjectEqualityOptions" />.
/// </summary>
public class ObjectCountResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	Quantifier quantifier,
	ObjectEqualityOptions options)
	: ObjectCountResult<TType, TThat,
		ObjectCountResult<TType, TThat>>(
		expectationBuilder,
		returnValue,
		quantifier,
		options);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="CountResult{TResult,TValue}" />, allows specifying
///     options on the <see cref="ObjectEqualityOptions" />.
/// </summary>
public class ObjectCountResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	Quantifier quantifier,
	ObjectEqualityOptions options)
	: CountResult<TType, TThat, TSelf>(expectationBuilder, returnValue, quantifier)
	where TSelf : ObjectCountResult<TType, TThat, TSelf>
{
	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public ObjectCountResult<TType, TThat, TSelf> Equivalent(
		Func<EquivalencyOptions, EquivalencyOptions>? optionsCallback = null)
	{
		EquivalencyOptions? equivalencyOptions =
			optionsCallback?.Invoke(new EquivalencyOptions()) ?? new EquivalencyOptions();
		options.Equivalent(equivalencyOptions);
		return this;
	}

	/// <summary>
	///     Uses the provided <paramref name="comparer" /> for comparing <see langword="object" />s.
	/// </summary>
	public ObjectCountResult<TType, TThat, TSelf> Using(
		IEqualityComparer<object> comparer)
	{
		options.Using(comparer);
		return this;
	}
}
