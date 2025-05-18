using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection all items…
	/// </summary>
	public static Elements<TItem> All<TItem>(
		this IThat<IEnumerable<TItem>?> subject)
		=> new(subject,
			EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection all items…
	/// </summary>
	public static Elements All(
		this IThat<IEnumerable<string?>?> subject)
		=> new(subject,
			EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));
}
