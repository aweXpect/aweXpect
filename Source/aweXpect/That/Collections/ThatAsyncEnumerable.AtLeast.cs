#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Expect that at least <paramref name="minimum" /> items of the <see cref="IAsyncEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> AtLeast<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum));

	/// <summary>
	///     Expect that at least <paramref name="minimum" /> items of the <see cref="IAsyncEnumerable{TItem}" />…
	/// </summary>
	public static Elements AtLeast(
		this IThat<IAsyncEnumerable<string?>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum));

	/// <summary>
	///     Verifies that the collection has at least <paramref name="minimum" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>>
		AtLeast<TItem>(
			this IThatHas<IAsyncEnumerable<TItem>?> source,
			int minimum)
		=> new(new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AsyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.AtLeast(minimum))),
			source.ExpectSubject()));
}
#endif
