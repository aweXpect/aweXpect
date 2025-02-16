using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the <paramref name="subject" /> has an item count of…
	/// </summary>
	public static CollectionCountResult<AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>> HasCount<TItem>(
		this IThat<IEnumerable<TItem>?> subject)
		=> new(quantifier => new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			subject.ThatIs().ExpectationBuilder.AddConstraint((it, grammar)
				=> new SyncCollectionCountConstraint<TItem>(it, quantifier)),
			subject));
}
