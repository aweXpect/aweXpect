using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Expect that exactly <paramref name="expected" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> Exactly<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected));

	/// <summary>
	///     Expect that exactly <paramref name="expected" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements Exactly(
		this IThat<IEnumerable<string?>?> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected));

	/// <summary>
	///     Verifies that the collection has exactly <paramref name="expected" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>> Exactly<TItem>(
		this IThatHas<IEnumerable<TItem>?> source,
		int expected)
		=> new(new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.Exactly(expected))),
			source.ExpectSubject()));
}
