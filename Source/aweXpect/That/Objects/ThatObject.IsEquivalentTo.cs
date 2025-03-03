using System;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Customization;
using aweXpect.Equivalency;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatObject
{
	/// <summary>
	///     Verifies that the subject is equivalent to the <paramref name="expected" /> value.
	/// </summary>
	public static AndOrResult<TSubject, IThat<TSubject>> IsEquivalentTo<TSubject, TExpected>(
		this IThat<TSubject> source,
		TExpected expected,
		Func<EquivalencyOptions<TExpected>, EquivalencyOptions>? options = null,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		EquivalencyOptions equivalencyOptions = Customize.aweXpect.Equivalency().DefaultEquivalencyOptions.Get();
		if (options != null)
		{
			equivalencyOptions = options(new EquivalencyOptions<TExpected>(equivalencyOptions));
		}

		ObjectEqualityOptions<TSubject> equalityOptions = new();
		equalityOptions.Equivalent(equivalencyOptions);
		return new AndOrResult<TSubject, IThat<TSubject>>(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
				=> new IsEqualToConstraint<TSubject, TExpected>(it, grammars, expected, doNotPopulateThisValue.TrimCommonWhiteSpace(), equalityOptions)),
			source);
	}
}
