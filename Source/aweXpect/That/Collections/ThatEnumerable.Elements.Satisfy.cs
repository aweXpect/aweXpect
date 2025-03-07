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
			=> new(_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
					=> new CollectionConstraint<string?>(
						it,
						_quantifier,
						() => grammar.HasFlag(ExpectationGrammars.Nested) switch
						{
							true => $"satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							_ => $"satisfies {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
						},
						predicate,
						"did")),
				_subject);
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
			=> new(_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => grammar.HasFlag(ExpectationGrammars.Nested) switch
						{
							true => $"satisfy {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
							_ => $"satisfies {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
						},
						predicate,
						"did")),
				_subject);
	}
}
