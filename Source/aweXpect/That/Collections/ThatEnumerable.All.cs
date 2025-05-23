using System.Collections;
using System.Collections.Generic;
using System.Linq;
using aweXpect;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif
using aweXpect.Core;
using aweXpect.Helpers;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	extension<TItem, TEnumerable>(Elements<TItem, TEnumerable> subject)
		where TEnumerable : IEnumerable<TItem>
	{
		/// <summary>
		///     …satisfy the <paramref name="predicate" />.
		/// </summary>
		public AndOrResult<TEnumerable, IThat<TEnumerable>>
			Sat(
				Func<TItem, bool> predicate,
				[CallerArgumentExpression("predicate")]
				string doNotPopulateThisValue = "")
		{
			predicate.ThrowIfNull();
			ExpectationBuilder expectationBuilder = subject._subject.Get().ExpectationBuilder;
			return new AndOrResult<TEnumerable, IThat<TEnumerable>>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						expectationBuilder,
						it, grammars,
						subject._quantifier,
						g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
								g.IsNegated()) switch
						{
							(true, false) => $"satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							(false, false) => $"satisfies {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							(true, true) => $"do not satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							(false, true) => $"does not satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
						},
						predicate,
						"did")),
				subject._subject);
		}
	}

	extension(NonGenericElements<IEnumerable> subject)
	{
		/// <summary>
		///     …satisfy the <paramref name="predicate" />.
		/// </summary>
		public AndOrResult<IEnumerable, IThat<IEnumerable>>
			Sat<TItem>(Func<TItem, bool> predicate, [CallerArgumentExpression("predicate")] string doNotPopulateThisValue = "")
		{
			predicate.ThrowIfNull();
			ExpectationBuilder expectationBuilder = subject._subject.Get().ExpectationBuilder;
			return new AndOrResult<IEnumerable, IThat<IEnumerable>>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						expectationBuilder,
						it, grammars,
						subject._quantifier,
						g => (g.HasAnyFlag(ExpectationGrammars.Nested, ExpectationGrammars.Plural),
								g.IsNegated()) switch
						{
							(true, false) => $"satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							(false, false) => $"satisfies {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							(true, true) => $"do not satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							(false, true) => $"does not satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
						},
						predicate,
						"did")),
				subject._subject);
		}
	}


	extension<TItem>(IThat<IEnumerable<TItem>?> subject)
	{
		/// <summary>
		///     Verifies that in the collection all items…
		/// </summary>
		public Elements<TItem, IEnumerable<TItem>?> Foo()
			=> new(subject,
				EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));
	}

#if NET8_0_OR_GREATER
	extension<TItem>(IThat<ImmutableArray<TItem>> subject)
	{
		/// <summary>
		///     Verifies that in the collection all items…
		/// </summary>
		public Elements<TItem, ImmutableArray<TItem>> Foo()
			=> new(subject,
				EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));
	}
#endif

	extension(IThat<IEnumerable> subject)
	{
		/// <summary>
		///     Verifies that in the collection all items…
		/// </summary>
		public NonGenericElements<IEnumerable> Foo()
			=> new(subject,
				EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));
	}
	///// <summary>
	/////     Verifies that in the collection all items…
	///// </summary>
	//public static NonGenericElements<IEnumerable> All(
	//	this IThat<IEnumerable> subject)
	//	=> new(subject,
	//		EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));

#if NET8_0_OR_GREATER
	///// <summary>
	/////     Verifies that in the collection all items…
	///// </summary>
	//public static Elements<TItem, ImmutableArray<TItem>?> All<TItem>(
	//	this IThat<ImmutableArray<TItem>?> subject)
	//	=> new(subject,
	//		EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));
#endif


	/// <summary>
	///     Verifies that in the collection all items…
	/// </summary>
	public static aweXpect.ThatEnumerable.StringElements All(
		this IThat<IEnumerable<string?>?> subject)
		=> new(subject,
			EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));
}
