using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Expect that between <paramref name="minimum" /> and…
	/// </summary>
	public static BetweenResult<Elements<TItem>> Between<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject,
		int minimum)
		=> new(maximum => new(subject, EnumerableQuantifier.Between(minimum, maximum)));

	/// <summary>
	///     Expect that between <paramref name="minimum" /> and…
	/// </summary>
	public static BetweenResult<Elements> Between(
		this IExpectSubject<IEnumerable<string>> subject,
		int minimum)
		=> new(maximum => new(subject, EnumerableQuantifier.Between(minimum, maximum)));
}
