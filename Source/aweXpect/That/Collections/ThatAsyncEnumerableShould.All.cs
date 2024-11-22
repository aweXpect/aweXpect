#if NET6_0_OR_GREATER
using System.Collections.Generic;
using Testably.Expectations.Core;

namespace Testably.Expectations;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that all items in the enumerable...
	/// </summary>
	public static QuantifiedCollectionResult.Async
		<IThat<IAsyncEnumerable<TItem>>, TItem, IAsyncEnumerable<TItem>> All<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source)
		=> new(source,
			source.ExpectationBuilder,
			CollectionQuantifier.All);
}
#endif
