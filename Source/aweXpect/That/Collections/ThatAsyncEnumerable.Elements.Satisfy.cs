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
			=> new(_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
					=> new CollectionConstraint<string?>(
						it,
						_quantifier,
						() => $"satisfies {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
						predicate,
						"did")),
				_subject);
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
			=> new(_subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => $"satisfies {doNotPopulateThisValue.TrimCommonWhiteSpace()}",
						predicate,
						"did")),
				_subject);
	}
}
#endif
