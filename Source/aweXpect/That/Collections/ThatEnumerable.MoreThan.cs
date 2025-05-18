using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection more than <paramref name="minimum" /> items…
	/// </summary>
	public static Elements<TItem> MoreThan<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.MoreThan(minimum, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection more than <paramref name="minimum" /> items…
	/// </summary>
	public static Elements MoreThan(
		this IThat<IEnumerable<string?>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.MoreThan(minimum));
}
