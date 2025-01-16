using System;
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
		///     Verifies that all items in the collection satisfy the <paramref name="predicate" />.
		/// </summary>
		public AndOrResult<IEnumerable<string?>, IExpectSubject<IEnumerable<string?>>>
			AllSatisfy(
				Func<string?, bool> predicate,
				[CallerArgumentExpression("predicate")]
				string doNotPopulateThisValue = "")
			=> new(_subject.ThatIs().ExpectationBuilder.AddConstraint(it
					=> new SyncCollectionConstraint2<string?>(
						it,
						_quantifier,
						() => $"satisfy {doNotPopulateThisValue}",
						predicate)),
				_subject);
	}

	public partial class Elements<TItem>
	{
		/// <summary>
		///     Verifies that all items in the collection satisfy the <paramref name="predicate" />.
		/// </summary>
		public AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>
			Satisfy(
				Func<TItem, bool> predicate,
				[CallerArgumentExpression("predicate")]
				string doNotPopulateThisValue = "")
			=> new(_subject.ThatIs().ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionConstraint2<TItem>(
					it,
					_quantifier,
					() => $"satisfy {doNotPopulateThisValue}",
					predicate)),
				_subject);
	}
}
