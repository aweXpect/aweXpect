using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection between <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<Elements<TItem>> Between<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int minimum)
		=> new(maximum => new Elements<TItem>(subject,
			EnumerableQuantifier.Between(minimum, maximum, subject.Get().ExpectationBuilder.ExpectationGrammars)));

	/// <summary>
	///     Verifies that in the collection between <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<Elements> Between(
		this IThat<IEnumerable<string?>?> subject,
		int minimum)
		=> new(maximum => new Elements(subject,
			EnumerableQuantifier.Between(minimum, maximum, subject.Get().ExpectationBuilder.ExpectationGrammars)));
}
