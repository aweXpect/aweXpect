using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static Elements<TItem> AtLeast<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum, subject.ThatIs().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static Elements AtLeast(
		this IThat<IEnumerable<string?>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum));

	/// <summary>
	///     Verifies that the collection has at least <paramref name="minimum" /> items.
	/// </summary>
	public static ItemsResult<AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>> AtLeast<TItem>(
		this IThatHas<IEnumerable<TItem>?> source,
		int minimum)
		=> new(new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			source.ExpectationBuilder.AddConstraint((it, form)
				=> new SyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.AtLeast(minimum))),
			source.ExpectSubject()));
}
