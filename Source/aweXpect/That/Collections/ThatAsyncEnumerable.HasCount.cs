#if NET8_0_OR_GREATER
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatAsyncEnumerable
{
	/// <summary>
	///     Verifies that the <paramref name="subject" /> has an item count of…
	/// </summary>
	public static CollectionCountResult<AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>>
		HasCount<TItem>(this IThat<IAsyncEnumerable<TItem>?> subject)
		=> new(quantifier => new AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>(
			subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new AsyncCollectionCountConstraint<TItem>(it, quantifier)),
			subject));

	/// <summary>
	///     Verifies that the <paramref name="subject" /> has exactly <paramref name="expected" /> items.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>?>>
		HasCount<TItem>(this IThat<IAsyncEnumerable<TItem>?> subject, int expected)
		=> new(subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new AsyncCollectionCountConstraint<TItem>(it, EnumerableQuantifier.Exactly(expected))),
			subject);
}
#endif
