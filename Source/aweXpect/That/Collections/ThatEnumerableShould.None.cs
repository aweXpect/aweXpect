using System.Collections.Generic;
using Testably.Expectations.Core;

namespace Testably.Expectations;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that no items in the enumerable...
	/// </summary>
	public static QuantifiedCollectionResult.Sync<IThat<IEnumerable<TItem>>, TItem,
			IEnumerable<TItem>>
		None<TItem>(this IThat<IEnumerable<TItem>> source)
		=> new(
			source,
			source.ExpectationBuilder,
			CollectionQuantifier.None);
}
