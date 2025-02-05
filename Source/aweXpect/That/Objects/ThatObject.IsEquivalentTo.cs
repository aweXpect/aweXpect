using System;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;
using EquivalencyOptions = aweXpect.Equivalency.EquivalencyOptions;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies that the subject is equivalent to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<TSubject, IThat<TSubject>, TSubject> IsEquivalentTo<TSubject, TExpected>(
		this IThat<TSubject> source,
		TExpected expected,
		Func<EquivalencyOptions, EquivalencyOptions>? optionsCallback = null,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		EquivalencyOptions equivalencyOptions =
			optionsCallback?.Invoke(new EquivalencyOptions()) ?? new EquivalencyOptions();
		ObjectEqualityOptions<TSubject> options = new();
		options.Equivalent(equivalencyOptions);
		return new ObjectEqualityResult<TSubject, IThat<TSubject>, TSubject>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsEqualToConstraint<TSubject, TExpected>(it, expected, doNotPopulateThisValue, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equivalent to the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IThat<object?>, object?> NotEquivalentTo(
		this IThatIs<object?> source,
		object? unexpected,
		Func<EquivalencyOptions, EquivalencyOptions>? optionsCallback = null,
		[CallerArgumentExpression("unexpected")]
		string doNotPopulateThisValue = "")
	{
		EquivalencyOptions equivalencyOptions =
			optionsCallback?.Invoke(new EquivalencyOptions()) ?? new EquivalencyOptions();
		ObjectEqualityOptions<object?> options = new();
		options.Equivalent(equivalencyOptions);
		return new ObjectEqualityResult<object?, IThat<object?>, object?>(
			source.ExpectationBuilder.AddConstraint(it
				=> new IsNotEqualToConstraint(it, unexpected, doNotPopulateThisValue, options)),
			source.ExpectSubject(),
			options);
	}
}
