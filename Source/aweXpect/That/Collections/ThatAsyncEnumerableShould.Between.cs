#if NET6_0_OR_GREATER
using System.Collections.Generic;
using Testably.Expectations.Core;

namespace Testably.Expectations;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that between <paramref name="minimum" />...
	/// </summary>
	public static BetweenResult<QuantifiedCollectionResult.Async
			<IThat<IAsyncEnumerable<TItem>>, TItem, IAsyncEnumerable<TItem>>>
		Between<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			int minimum)
	{
		return new BetweenResult<QuantifiedCollectionResult.Async
			<IThat<IAsyncEnumerable<TItem>>, TItem, IAsyncEnumerable<TItem>>>(
			maximum => new QuantifiedCollectionResult.Async
				<IThat<IAsyncEnumerable<TItem>>, TItem, IAsyncEnumerable<TItem>>(source,
					source.ExpectationBuilder,
					CollectionQuantifier.Between(minimum, maximum)));
	}
}
#endif
