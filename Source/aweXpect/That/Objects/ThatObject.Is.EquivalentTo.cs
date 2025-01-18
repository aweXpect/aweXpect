using System;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies that the subject is equal to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IExpectSubject<object?>> EquivalentTo(
		this IThatIs<object?> source,
		object? expected,
		Func<EquivalencyOptions, EquivalencyOptions>? optionsCallback = null,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		EquivalencyOptions equivalencyOptions =
			optionsCallback?.Invoke(new EquivalencyOptions()) ?? new EquivalencyOptions();
		ObjectEqualityOptions options = new();
		options.Equivalent(equivalencyOptions);
		return new ObjectEqualityResult<object?, IExpectSubject<object?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new IsEqualToConstraint(it, expected, doNotPopulateThisValue, options)),
			source.ExpectSubject(),
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equal to the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IExpectSubject<object?>> NotEquivalentTo(
		this IExpectSubject<object?> source,
		object? unexpected,
		Func<EquivalencyOptions, EquivalencyOptions>? optionsCallback = null,
		[CallerArgumentExpression("unexpected")]
		string doNotPopulateThisValue = "")
	{
		EquivalencyOptions equivalencyOptions =
			optionsCallback?.Invoke(new EquivalencyOptions()) ?? new EquivalencyOptions();
		ObjectEqualityOptions options = new();
		options.Equivalent(equivalencyOptions);
		return new ObjectEqualityResult<object?, IExpectSubject<object?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsNotEqualToConstraint(it, unexpected, doNotPopulateThisValue, options)),
			source,
			options);
	}
}
