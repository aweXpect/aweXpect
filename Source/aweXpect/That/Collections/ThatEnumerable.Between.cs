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
	public static BetweenResult<Elements<TItem, TEnumerable>> Between<TItem, TEnumerable>(
		this IThat<TEnumerable> subject,
		int minimum)
	where TEnumerable : IEnumerable<TItem>
		=> new(maximum => new Elements<TItem, TEnumerable>(subject,
			EnumerableQuantifier.Between(minimum, maximum, subject.Get().ExpectationBuilder.ExpectationGrammars)));

	/// <summary>
	///     Verifies that in the collection between <paramref name="minimum" />…
	/// </summary>
	public static BetweenResult<StringElements> Between(
		this IThat<IEnumerable<string?>?> subject,
		int minimum)
		=> new(maximum => new StringElements(subject,
			EnumerableQuantifier.Between(minimum, maximum, subject.Get().ExpectationBuilder.ExpectationGrammars)));
}
