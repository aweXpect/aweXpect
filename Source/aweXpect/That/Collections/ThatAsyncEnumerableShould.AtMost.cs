#if NET6_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that at most <paramref name="maximum" /> items...
	/// </summary>
	public static QuantifiedCollectionResult.Async
		<IThat<IAsyncEnumerable<TItem>>, TItem, IAsyncEnumerable<TItem>> AtMost<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			int maximum)
		=> new(source,
			source.ExpectationBuilder,
			CollectionQuantifier.AtMost(maximum));
}
#endif
