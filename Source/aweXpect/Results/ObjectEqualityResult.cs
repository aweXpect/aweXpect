using System;
using System.Collections.Generic;
using Testably.Expectations.Core;
using Testably.Expectations.Options;

namespace Testably.Expectations.Results;

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying
///     options on the <see cref="ObjectEqualityOptions" />.
/// </summary>
public class ObjectEqualityResult<TType, TThat>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityOptions options)
	: ObjectEqualityResult<TType, TThat,
		ObjectEqualityResult<TType, TThat>>(
		expectationBuilder,
		returnValue,
		options);

/// <summary>
///     The result of an expectation with an underlying value of type <typeparamref name="TType" />.
///     <para />
///     In addition to the combinations from <see cref="AndOrResult{TType,TThat}" />, allows specifying
///     options on the <see cref="ObjectEqualityOptions" />.
/// </summary>
public class ObjectEqualityResult<TType, TThat, TSelf>(
	ExpectationBuilder expectationBuilder,
	TThat returnValue,
	ObjectEqualityOptions options)
	: AndOrResult<TType, TThat, TSelf>(expectationBuilder, returnValue)
	where TSelf : ObjectEqualityResult<TType, TThat, TSelf>
{
	/// <summary>
	///     Use equivalency to compare objects.
	/// </summary>
	public ObjectEqualityResult<TType, TThat, TSelf> Equivalent(
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
	public ObjectEqualityResult<TType, TThat, TSelf> Using(
		IEqualityComparer<object> comparer)
	{
		options.Using(comparer);
		return this;
	}
}
