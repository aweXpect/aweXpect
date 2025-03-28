using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection between <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<Elements<TItem, IEnumerable<TItem>?>> Between<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int minimum)
		=> new(maximum => new Elements<TItem, IEnumerable<TItem>?>(subject, EnumerableQuantifier.Between(minimum, maximum)));

	/// <summary>
	///     Verifies that in the collection between <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<Elements<IEnumerable<string?>?>> Between(
		this IThat<IEnumerable<string?>?> subject,
		int minimum)
		=> new(maximum => new Elements<IEnumerable<string?>?>(subject, EnumerableQuantifier.Between(minimum, maximum)));
}
