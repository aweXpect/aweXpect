#if NET6_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that between <paramref name="minimum" />...
	/// </summary>
	public static BetweenResult<AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>, IThat<TItem>>
		HaveBetween<TItem>(this IThat<IAsyncEnumerable<TItem>> source, int minimum)
		=> new((maximum, expectations) => new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AsyncCollectionConstraint<TItem>(it, EnumerableQuantifier.Between(minimum, maximum),
					expectations)),
			source),
			maximum => new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(
				source.ExpectationBuilder.AddConstraint(it
					=> new AsyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.Between(minimum, maximum))),
				source));
}
#endif
