using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that at most <paramref name="maximum" /> items...
	/// </summary>
	public static QuantifiedCollectionResult.Sync
		<IThat<IEnumerable<TItem>>, TItem, IEnumerable<TItem>> AtMost<TItem>(
			this IThat<IEnumerable<TItem>> source,
			int maximum)
		=> new(
			source,
			source.ExpectationBuilder,
			CollectionQuantifier.AtMost(maximum));
}
