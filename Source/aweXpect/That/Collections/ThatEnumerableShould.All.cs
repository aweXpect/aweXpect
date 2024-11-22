using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that all items in the enumerable...
	/// </summary>
	public static QuantifiedCollectionResult.Sync
		<IThat<IEnumerable<TItem>>, TItem, IEnumerable<TItem>> All<TItem>(
			this IThat<IEnumerable<TItem>> source)
		=> new(
			source,
			source.ExpectationBuilder,
			CollectionQuantifier.All);
}
