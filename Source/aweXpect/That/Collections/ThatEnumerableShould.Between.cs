using System.Collections.Generic;
using Testably.Expectations.Core;

namespace Testably.Expectations;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that between <paramref name="minimum" />...
	/// </summary>
	public static BetweenResult<QuantifiedCollectionResult.Sync
			<IThat<IEnumerable<TItem>>, TItem, IEnumerable<TItem>>>
		Between<TItem>(
			this IThat<IEnumerable<TItem>> source,
			int minimum)
	{
		return new BetweenResult<QuantifiedCollectionResult.Sync<IThat<IEnumerable<TItem>>, TItem,
			IEnumerable<TItem>>>(
			maximum => new QuantifiedCollectionResult.Sync<IThat<IEnumerable<TItem>>, TItem,
				IEnumerable<TItem>>(
				source,
				source.ExpectationBuilder,
				CollectionQuantifier.Between(minimum, maximum)));
	}
}
