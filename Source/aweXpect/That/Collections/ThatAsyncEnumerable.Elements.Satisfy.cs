#if NET8_0_OR_GREATER
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
		///     Verifies that all items in the collection satisfy the <paramref name="predicate" />.
		/// </summary>
		public AndOrResult<IAsyncEnumerable<string?>, IExpectSubject<IAsyncEnumerable<string?>>>
			Satisfy(
				Func<string?, bool> predicate,
				[CallerArgumentExpression("predicate")]
				string doNotPopulateThisValue = "")
			=> new(_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<string?>(
						it,
						_quantifier,
						() => $"satisfy {doNotPopulateThisValue}",
						predicate,
						"did")),
				_subject);
	}

	public partial class Elements<TItem>
	{
		/// <summary>
		///     Verifies that all items in the collection satisfy the <paramref name="predicate" />.
		/// </summary>
		public AndOrResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>
			Satisfy(
				Func<TItem, bool> predicate,
				[CallerArgumentExpression("predicate")]
				string doNotPopulateThisValue = "")
			=> new(_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new CollectionConstraint<TItem>(
						it,
						_quantifier,
						() => $"satisfy {doNotPopulateThisValue}",
						predicate,
						"did")),
				_subject);
	}
}
#endif
