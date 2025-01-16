using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that all items in the collection satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>
		AllSatisfy<TItem>(
			this IThatShould<IEnumerable<TItem>> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		=> new(source.ExpectationBuilder.AddConstraint(it
				=> new AllBeConstraint<TItem>(
					it,
					() => $"have all items satisfy {doNotPopulateThisValue}",
					predicate)),
			source);
}
