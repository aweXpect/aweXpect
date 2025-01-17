using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
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
		=> new(maximum => new Elements<TItem>(subject, EnumerableQuantifier.Between(minimum, maximum)));

	/// <summary>
	///     Expect that between <paramref name="minimum" /> and…
	/// </summary>
	public static BetweenResult<Elements> Between(
		this IExpectSubject<IEnumerable<string>> subject,
		int minimum)
		=> new(maximum => new Elements(subject, EnumerableQuantifier.Between(minimum, maximum)));

	/// <summary>
	///     Verifies that the collection has between <paramref name="minimum" /> and…
	/// </summary>
	public static BetweenResult<ItemsResult<AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>>>
		Between<TItem>(
			this IThatHas<IEnumerable<TItem>> source,
			int minimum)
		=> new(maximum => new ItemsResult<AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>>(
			new AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
				source.ExpectationBuilder.AddConstraint(it
					=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.Between(minimum, maximum))),
				source.ExpectSubject())));
}
