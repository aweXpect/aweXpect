using System.Collections;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Helpers;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static Elements<TItem> Exactly<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int expected)
		=> new(subject,
			EnumerableQuantifier.Exactly(expected, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static Elements Exactly(
		this IThat<IEnumerable<string?>?> subject,
		int expected)
		=> new(subject,
			EnumerableQuantifier.Exactly(expected, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static ElementsForEnumerable<IEnumerable> Exactly(
		this IThat<IEnumerable> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected, subject.Get().ExpectationBuilder.ExpectationGrammars));

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static ElementsForStructEnumerable<ImmutableArray<TItem>, TItem> Exactly<TItem>(
		this IThat<ImmutableArray<TItem>> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected, subject.Get().ExpectationBuilder.ExpectationGrammars));
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that in the collection exactly <paramref name="expected" /> items…
	/// </summary>
	public static ElementsForStructEnumerable<ImmutableArray<string?>> Exactly(
		this IThat<ImmutableArray<string?>> subject,
		int expected)
		=> new(subject, EnumerableQuantifier.Exactly(expected, subject.Get().ExpectationBuilder.ExpectationGrammars));
#endif
}
