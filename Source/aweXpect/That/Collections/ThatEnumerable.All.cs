using System.Collections.Generic;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection all items…
	/// </summary>
	public static Elements<TItem, IEnumerable<TItem>?> All<TItem>(
		this IThat<IEnumerable<TItem>?> subject)
		=> new(subject, EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that in the collection all items…
	/// </summary>
	public static Elements<TItem, ImmutableArray<TItem>> All<TItem>(
		this IThat<ImmutableArray<TItem>> subject)
		=> new(subject, EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));
#endif

	/// <summary>
	///     Verifies that in the collection all items…
	/// </summary>
	public static Elements<IEnumerable<string?>?> All(
		this IThat<IEnumerable<string?>?> subject)
		=> new(subject, EnumerableQuantifier.All(subject.Get().ExpectationBuilder.ExpectationGrammars));
}
