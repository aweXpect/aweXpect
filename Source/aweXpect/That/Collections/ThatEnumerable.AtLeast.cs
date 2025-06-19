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
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static Elements<TItem> AtLeast<TItem>(
		this IThat<IEnumerable<TItem>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static Elements AtLeast(
		this IThat<IEnumerable<string?>?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum, subject.Get().ExpectationBuilder.ExpectationGrammars));

	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static ElementsForEnumerable<IEnumerable?> AtLeast(
		this IThat<IEnumerable?> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum, subject.Get().ExpectationBuilder.ExpectationGrammars));

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static ElementsForStructEnumerable<ImmutableArray<TItem>, TItem> AtLeast<TItem>(
		this IThat<ImmutableArray<TItem>> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum, subject.Get().ExpectationBuilder.ExpectationGrammars));
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that in the collection at least <paramref name="minimum" /> items…
	/// </summary>
	public static ElementsForStructEnumerable<ImmutableArray<string?>> AtLeast(
		this IThat<ImmutableArray<string?>> subject,
		int minimum)
		=> new(subject, EnumerableQuantifier.AtLeast(minimum, subject.Get().ExpectationBuilder.ExpectationGrammars));
#endif
}
