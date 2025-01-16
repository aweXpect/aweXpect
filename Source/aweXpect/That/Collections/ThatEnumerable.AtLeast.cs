using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Expect that at least <paramref name="minimum" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> AtLeast<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum));

	/// <summary>
	///     Expect that at least <paramref name="minimum" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements AtLeast(
		this IExpectSubject<IEnumerable<string>> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum));

	/// <summary>
	///     Verifies that the collection has at least <paramref name="minimum" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>> AtLeast<TItem>(
		this IThatHas<IEnumerable<TItem>> source,
		int minimum)
		=> new(new AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.AtLeast(minimum))),
			source.ExpectSubject()));
}
