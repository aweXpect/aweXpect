#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that in the collection between <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<Elements<TItem>> Between<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> subject,
		int minimum)
		=> new(maximum => new Elements<TItem>(subject, EnumerableQuantifier.Between(minimum, maximum)));

	/// <summary>
	///     Verifies that in the collection between <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<Elements> Between(
		this IThat<IAsyncEnumerable<string?>?> subject,
		int minimum)
		=> new(maximum => new Elements(subject, EnumerableQuantifier.Between(minimum, maximum)));

	/// <summary>
	///     Verifies that the collection has between <paramref name="minimum" />…
	/// </summary>
	public static
		BetweenResult<ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>>>
		Between<TItem>(
			this IThatHas<IAsyncEnumerable<TItem>?> source,
			int minimum)
		=> new(maximum
			=> new ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>>(
				new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(
					source.ExpectationBuilder.AddConstraint((it, form)
						=> new AsyncCollectionCountConstraint<TItem>(it,
							EnumerableQuantifier.Between(minimum, maximum))),
					source.ExpectSubject())));
}
#endif
