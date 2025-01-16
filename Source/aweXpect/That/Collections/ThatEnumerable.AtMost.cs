using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Expect that at most <paramref name="maximum" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements<TItem> AtMost<TItem>(
		this IExpectSubject<IEnumerable<TItem>> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.AtMost(maximum));

	/// <summary>
	///     Expect that at most <paramref name="maximum" /> items of the <see cref="IEnumerable{TItem}" />…
	/// </summary>
	public static Elements AtMost(
		this IExpectSubject<IEnumerable<string>> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.AtMost(maximum));

	/// <summary>
	///     Verifies that the collection has at most <paramref name="maximum" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>> AtMost<TItem>(
		this IThatHas<IEnumerable<TItem>> source,
		int maximum)
		=> new(new AndOrResult<IEnumerable<TItem>, IExpectSubject<IEnumerable<TItem>>>(
			source.ExpectationBuilder.AddConstraint(it
				=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.AtMost(maximum))),
			source.ExpectSubject()));
}
