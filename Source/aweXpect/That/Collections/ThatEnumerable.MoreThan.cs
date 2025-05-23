using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection more than <paramref name="minimum" /> items…
	/// </summary>
	public static Elements<TItem, TEnumerable> MoreThan<TItem, TEnumerable>(
		this IThat<TEnumerable> subject,
		int minimum)
	where TEnumerable : IEnumerable<TItem>
		=> new(subject,
			EnumerableQuantifier.MoreThan(minimum, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection more than <paramref name="minimum" /> items…
	/// </summary>
	public static StringElements MoreThan(
		this IThat<IEnumerable<string?>?> subject,
		int minimum)
		=> new(subject,
			EnumerableQuantifier.MoreThan(minimum, subject.Get().ExpectationBuilder.ExpectationGrammars));
}
