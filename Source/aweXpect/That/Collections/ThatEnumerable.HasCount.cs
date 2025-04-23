using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Constraints;
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
			subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountConstraint<TItem>(it, grammars, quantifier)),
			subject));

	/// <summary>
	///     Verifies that the <paramref name="subject" /> has exactly <paramref name="expected" /> items.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>> HasCount<TItem>(
		this IThat<IEnumerable<TItem>?> subject, int expected)
		=> new(
			subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountConstraint<TItem>(it, grammars, EnumerableQuantifier.Exactly(expected))),
			subject);

	/// <summary>
	///     Verifies that the <paramref name="subject" /> does not have <paramref name="unexpected" /> items.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>> DoesNotHaveCount<TItem>(
		this IThat<IEnumerable<TItem>?> subject, int unexpected)
		=> new(
			subject.Get().ExpectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountConstraint<TItem>(
					it, grammars, EnumerableQuantifier.Exactly(unexpected)).Invert()),
			subject);
}
