using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection at most <paramref name="maximum" /> items…
	/// </summary>
	public static Elements<TItem> AtMost<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int maximum)
		=> new(subject,
			EnumerableQuantifier.AtMost(maximum, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection at most <paramref name="maximum" /> items…
	/// </summary>
	public static Elements AtMost(
		this IThat<IEnumerable<string?>?> subject,
		int maximum)
		=> new(subject,
			EnumerableQuantifier.AtMost(maximum, subject.Get().ExpectationBuilder.ExpectationGrammars));
}
