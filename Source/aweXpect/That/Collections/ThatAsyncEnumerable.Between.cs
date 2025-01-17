#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Expect that between <paramref name="minimum" /> and…
	/// </summary>
	public static BetweenResult<Elements<TItem>> Between<TItem>(
		this IExpectSubject<IAsyncEnumerable<TItem>> subject,
		int minimum)
		=> new(maximum => new Elements<TItem>(subject, EnumerableQuantifier.Between(minimum, maximum)));

	/// <summary>
	///     Expect that between <paramref name="minimum" /> and…
	/// </summary>
	public static BetweenResult<Elements> Between(
		this IExpectSubject<IAsyncEnumerable<string>> subject,
		int minimum)
		=> new(maximum => new Elements(subject, EnumerableQuantifier.Between(minimum, maximum)));

	/// <summary>
	///     Verifies that the collection has between <paramref name="minimum" /> and…
	/// </summary>
	public static
		BetweenResult<ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>>>
		Between<TItem>(
			this IThatHas<IAsyncEnumerable<TItem>> source,
			int minimum)
		=> new(maximum
			=> new ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>>(
				new AndOrResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>(
					source.ExpectationBuilder.AddConstraint(it
						=> new AsyncCollectionCountConstraint<TItem>(it,
							EnumerableQuantifier.Between(minimum, maximum))),
					source.ExpectSubject())));
}
#endif
