using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection less than <paramref name="maximum" /> items…
	/// </summary>
	public static Elements<TItem, TEnumerable> LessThan<TItem, TEnumerable>(
		this IThat<TEnumerable> subject,
		int maximum)
	where TEnumerable : IEnumerable<TItem>
		=> new(subject,
			EnumerableQuantifier.LessThan(maximum, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection less than <paramref name="maximum" /> items…
	/// </summary>
	public static StringElements LessThan(
		this IThat<IEnumerable<string?>?> subject,
		int maximum)
		=> new(subject,
			EnumerableQuantifier.LessThan(maximum, subject.Get().ExpectationBuilder.ExpectationGrammars));
}
