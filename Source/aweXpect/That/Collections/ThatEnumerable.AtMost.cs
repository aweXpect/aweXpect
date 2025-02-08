using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection at most <paramref name="maximum" /> items…
	/// </summary>
	public static Elements<TItem> AtMost<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.AtMost(maximum));

	/// <summary>
	///     Verifies that in the collection at most <paramref name="maximum" /> items…
	/// </summary>
	public static Elements AtMost(
		this IThat<IEnumerable<string?>?> subject,
		int maximum)
		=> new(subject, EnumerableQuantifier.AtMost(maximum));

	/// <summary>
	///     Verifies that the collection has at most <paramref name="maximum" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>> AtMost<TItem>(
		this IThatHas<IEnumerable<TItem>?> source,
		int maximum)
		=> new(new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			source.ExpectationBuilder.AddConstraint((it, form)
				=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.AtMost(maximum))),
			source.ExpectSubject()));
}
