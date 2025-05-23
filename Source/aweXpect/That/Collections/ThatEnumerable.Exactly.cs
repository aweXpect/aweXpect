using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static Elements<TItem, TEnumerable> Exactly<TItem, TEnumerable>(
		this IThat<TEnumerable> subject,
		int expected)
	where TEnumerable : IEnumerable<TItem>
		=> new(subject,
			EnumerableQuantifier.Exactly(expected, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static StringElements Exactly(
		this IThat<IEnumerable<string?>?> subject,
		int expected)
		=> new(subject,
			EnumerableQuantifier.Exactly(expected, subject.Get().ExpectationBuilder.ExpectationGrammars));
}
