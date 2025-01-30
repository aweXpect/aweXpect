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
	///     Verifies that the subject is equivalent to the <paramref name="expected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IThat<object?>> IsEquivalentTo(
		this IThat<object?> source,
		object? expected,
		Func<EquivalencyOptions, EquivalencyOptions>? optionsCallback = null,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		EquivalencyOptions equivalencyOptions =
			optionsCallback?.Invoke(new EquivalencyOptions()) ?? new EquivalencyOptions();
		ObjectEqualityOptions options = new();
		options.Equivalent(equivalencyOptions);
		return new ObjectEqualityResult<object?, IThat<object?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new IsEqualToConstraint(it, expected, doNotPopulateThisValue, options)),
			source,
			options);
	}

	/// <summary>
	///     Verifies that the subject is not equivalent to the <paramref name="unexpected" /> value.
	/// </summary>
	public static ObjectEqualityResult<object?, IThat<object?>> NotEquivalentTo(
		this IThatIs<object?> source,
		object? unexpected,
		Func<EquivalencyOptions, EquivalencyOptions>? optionsCallback = null,
		[CallerArgumentExpression("unexpected")]
		string doNotPopulateThisValue = "")
	{
		EquivalencyOptions equivalencyOptions =
			optionsCallback?.Invoke(new EquivalencyOptions()) ?? new EquivalencyOptions();
		ObjectEqualityOptions options = new();
		options.Equivalent(equivalencyOptions);
		return new ObjectEqualityResult<object?, IThat<object?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new IsNotEqualToConstraint(it, unexpected, doNotPopulateThisValue, options)),
			source.ExpectSubject(),
			options);
	}
}
