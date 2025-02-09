#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static Elements<TItem> Exactly<TItem>(
		this IThat<IAsyncEnumerable<TItem>?> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected));

	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static Elements Exactly(
		this IThat<IAsyncEnumerable<string?>?> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected));

	/// <summary>
	///     Verifies that the collection has exactly <paramref name="expected" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>>
		Exactly<TItem>(
			this IThatHas<IAsyncEnumerable<TItem>?> source,
			int expected)
		=> new(new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(
			source.ExpectationBuilder.AddConstraint((it, grammar)
				=> new AsyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.Exactly(expected))),
			source.ExpectSubject()));
}
#endif
