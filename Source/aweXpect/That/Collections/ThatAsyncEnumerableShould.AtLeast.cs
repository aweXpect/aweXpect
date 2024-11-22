#if NET6_0_OR_GREATER
using System.Collections.Generic;
using Testably.Expectations.Core;

namespace Testably.Expectations;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that at least <paramref name="minimum" /> items...
	/// </summary>
	public static QuantifiedCollectionResult.Async
		<IThat<IAsyncEnumerable<TItem>>, TItem, IAsyncEnumerable<TItem>> AtLeast<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			int minimum)
		=> new(source,
			source.ExpectationBuilder,
			CollectionQuantifier.AtLeast(minimum));
}
#endif
