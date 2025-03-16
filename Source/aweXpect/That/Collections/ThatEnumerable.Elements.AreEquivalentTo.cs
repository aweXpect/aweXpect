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
			ExpectationBuilder expectationBuilder = _subject.Get().ExpectationBuilder;
			EquivalencyOptions equivalencyOptions = Customize.aweXpect.Equivalency().DefaultEquivalencyOptions.Get();
			if (options != null)
			{
				equivalencyOptions = options(new EquivalencyOptions<TExpected>(equivalencyOptions));
			}

			expectationBuilder.AddEquivalencyContext(equivalencyOptions);

			ObjectEqualityOptions<TItem> equalityOptions = new();
			equalityOptions.Equivalent(equivalencyOptions);
			return new ObjectEqualityResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						it, grammars,
						_quantifier,
						g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
								g.IsNegated()) switch
							{
								(true, false) => $"are equivalent to {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
								(false, false) => $"is equivalent to {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
								(true, true) =>
									$"are not equivalent to {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
								(false, true) =>
									$"is not equivalent to {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							},
						a => equalityOptions.AreConsideredEqual(a, expected),
						"were")),
				_subject,
				equalityOptions);
		}
	}
}
