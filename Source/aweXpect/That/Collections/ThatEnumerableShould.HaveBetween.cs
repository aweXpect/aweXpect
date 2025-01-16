using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that between <paramref name="minimum" />...
	/// </summary>
	public static BetweenResult<AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>, IThatShould<TItem>>
		HaveBetween<TItem>(this IThatShould<IEnumerable<TItem>> source, int minimum)
		=> new((maximum, expectations) => new AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionConstraint<TItem>(it, EnumerableQuantifier.Between(minimum, maximum),
					expectations)),
			source),
			maximum => new AndOrResult<IEnumerable<TItem>, IThatShould<IEnumerable<TItem>>>(
				source.ExpectationBuilder.AddConstraint(it
					=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.Between(minimum, maximum))),
				source));
}
