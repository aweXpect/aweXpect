using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static Elements<TItem, TEnumerable> AtLeast<TItem, TEnumerable>(
		this IThat<TEnumerable> subject,
		int minimum)
	where TEnumerable : IEnumerable<TItem>
		=> new(subject,
			EnumerableQuantifier.AtLeast(minimum, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static StringElements AtLeast(
		this IThat<IEnumerable<string?>?> subject,
		int minimum)
		=> new(subject,
			EnumerableQuantifier.AtLeast(minimum, subject.Get().ExpectationBuilder.ExpectationGrammars));
}
