#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Expect that at most <paramref name="maximum" /> items of the <see cref="IAsyncEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> AtMost<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.AtMost(maximum));

	/// <summary>
	///     Expect that at most <paramref name="maximum" /> items of the <see cref="IAsyncEnumerable{TItem}" />…
	/// </summary>
	public static Elements AtMost(
		this IThat<IAsyncEnumerable<string?>?> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.AtMost(maximum));

	/// <summary>
	///     Verifies that the collection has at most <paramref name="maximum" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>>
		AtMost<TItem>(
			this IThatHas<IAsyncEnumerable<TItem>?> source,
			int maximum)
		=> new(new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AsyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.AtMost(maximum))),
			source.ExpectSubject()));
}
#endif
