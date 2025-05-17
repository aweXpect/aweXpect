﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	public partial class Elements
	{
		/// <summary>
		///     …satisfy the <paramref name="predicate" />.
		/// </summary>
		public AndOrResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
			Satisfy(
				Func<string?, bool> predicate,
				[CallerArgumentExpression("predicate")]
				string doNotPopulateThisValue = "")
		{
			predicate.ThrowIfNull();
			ExpectationBuilder expectationBuilder = _subject.Get().ExpectationBuilder;
			return new AndOrResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<string?>(
						expectationBuilder,
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
		public AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>
			Satisfy(
				Func<TItem, bool> predicate,
				[CallerArgumentExpression("predicate")]
				string doNotPopulateThisValue = "")
		{
			predicate.ThrowIfNull();
			ExpectationBuilder expectationBuilder = _subject.Get().ExpectationBuilder;
			return new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new CollectionConstraint<TItem>(
						expectationBuilder,
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
