using System.Collections.Generic;
using Testably.Expectations.Core;

namespace Testably.Expectations;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that at least <paramref name="minimum" /> items...
	/// </summary>
	public static QuantifiedCollectionResult.Sync
		<IThat<IEnumerable<TItem>>, TItem, IEnumerable<TItem>> AtLeast<TItem>(
			this IThat<IEnumerable<TItem>> source,
			int minimum)
		=> new(
			source,
			source.ExpectationBuilder,
			CollectionQuantifier.AtLeast(minimum));
}
