using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection less than <paramref name="maximum" /> items…
	/// </summary>
	public static Elements<TItem> LessThan<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int maximum)
		=> new(subject,
			EnumerableQuantifier.LessThan(maximum, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection less than <paramref name="maximum" /> items…
	/// </summary>
	public static Elements LessThan(
		this IThat<IEnumerable<string?>?> subject,
		int maximum)
		=> new(subject,
			EnumerableQuantifier.LessThan(maximum, subject.Get().ExpectationBuilder.ExpectationGrammars));
}
