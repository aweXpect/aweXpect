using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Customization;
using aweXpect.Equivalency;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	public partial class Elements<TItem>
	{
		/// <summary>
		///     …are equivalent to the <paramref name="expected" /> value.
		/// </summary>
		public ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
			AreEquivalentTo<TExpected>(TExpected expected,
				Func<EquivalencyOptions<TExpected>, EquivalencyOptions>? options = null,
				[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		{
			EquivalencyOptions equivalencyOptions = Customize.aweXpect.Equivalency().DefaultEquivalencyOptions.Get();
			if (options != null)
			{
				equivalencyOptions = options(new EquivalencyOptions<TExpected>(equivalencyOptions));
			}

			ObjectEqualityOptions<TItem> equalityOptions = new();
			equalityOptions.Equivalent(equivalencyOptions);
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
				_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => grammars == ExpectationGrammars.None
							? $"is equivalent to {doNotPopulateThisValue.TrimCommonWhiteSpace()}"
							: $"are equivalent to {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
						a => equalityOptions.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				equalityOptions);
		}
	}
}
