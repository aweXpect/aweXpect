﻿#if NET8_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	public partial class Elements
	{
		/// <summary>
		///     …satisfy the <paramref name="predicate" />.
		/// </summary>
		public AndOrResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>
			Satisfy(
				Func<string?, bool> predicate,
				[CallerArgumentExpression("predicate")]
				string doNotPopulateThisValue = "")
		{
			predicate.ThrowIfNull();
			return new AndOrResult<IAsyncEnumerable<string?>, IThat<IAsyncEnumerable<string?>?>>(_subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<string?>(
						it, grammars,
						_quantifier,
						g => (g.IsNested(), g.IsNegated()) switch
						{
							(true, false) => $"satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							(false, false) => $"satisfies {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							(true, true) => $"do not satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							(false, true) => $"does not satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
						},
						predicate,
						"did")),
				_subject);
		}
	}

	public partial class Elements<TItem>
	{
		/// <summary>
		///     …satisfy the <paramref name="predicate" />.
		/// </summary>
		public AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>
			Satisfy(
				Func<TItem, bool> predicate,
				[CallerArgumentExpression("predicate")]
				string doNotPopulateThisValue = "")
		{
			predicate.ThrowIfNull();
			return new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(_subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						it, grammars,
						_quantifier,
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
				_subject);
		}
	}
}
#endif
