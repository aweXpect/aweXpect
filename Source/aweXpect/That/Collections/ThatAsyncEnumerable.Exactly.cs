#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Expect that exactly <paramref name="expected" /> items of the <see cref="IAsyncEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> Exactly<TItem>(
		this IExpectSubject<IAsyncEnumerable<TItem>> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected));

	/// <summary>
	///     Expect that exactly <paramref name="expected" /> items of the <see cref="IAsyncEnumerable{TItem}" />…
	/// </summary>
	public static Elements Exactly(
		this IExpectSubject<IAsyncEnumerable<string>> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected));

	/// <summary>
	///     Verifies that the collection has exactly <paramref name="expected" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>>
		Exactly<TItem>(
			this IThatHas<IAsyncEnumerable<TItem>> source,
			int expected)
		=> new(new AndOrResult<IAsyncEnumerable<TItem>, IExpectSubject<IAsyncEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new AsyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.Exactly(expected))),
			source.ExpectSubject()));
}
#endif
