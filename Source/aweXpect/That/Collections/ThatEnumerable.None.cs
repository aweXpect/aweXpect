using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection no items…
	/// </summary>
	public static Elements<TItem, TEnumerable> None<TItem, TEnumerable>(
		this IThat<TEnumerable> subject)
	where TEnumerable : IEnumerable<TItem>
		=> new(subject,
			EnumerableQuantifier.None(subject.Get().ExpectationBuilder.ExpectationGrammars |
			                          ExpectationGrammars.Plural));

	/// <summary>
	///     Verifies that in the collection no items…
	/// </summary>
	public static StringElements None(
		this IThat<IEnumerable<string?>?> subject)
		=> new(subject,
			EnumerableQuantifier.None(subject.Get().ExpectationBuilder.ExpectationGrammars |
			                          ExpectationGrammars.Plural));
}
