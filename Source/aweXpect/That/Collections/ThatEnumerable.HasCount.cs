using System.Collections;
using System.Collections.Generic;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;
#if NET8_0_OR_GREATER
using System.Collections.Immutable;
#endif

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the <paramref name="subject" /> has an item count of…
	/// </summary>
	public static CollectionCountResult<AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>> HasCount<TItem>(
		this IThat<IEnumerable<TItem>?> subject)
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new CollectionCountResult<AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>>(quantifier
			=> new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new SyncCollectionCountConstraint<TItem>(expectationBuilder, it, grammars, quantifier)),
				subject));
	}

	/// <summary>
	///     Verifies that the <paramref name="subject" /> has exactly <paramref name="expected" /> items.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>> HasCount<TItem>(
		this IThat<IEnumerable<TItem>?> subject, int expected)
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountConstraint<TItem>(expectationBuilder, it, grammars,
					EnumerableQuantifier.Exactly(expected))),
			subject);
	}

	/// <summary>
	///     Verifies that the <paramref name="subject" /> has an item count of…
	/// </summary>
	public static CollectionCountResult<AndOrResult<TItem[], IThat<TItem[]?>>> HasCount<TItem>(
		this IThat<TItem[]?> subject)
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new CollectionCountResult<AndOrResult<TItem[], IThat<TItem[]?>>>(quantifier
			=> new AndOrResult<TItem[], IThat<TItem[]?>>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new SyncCollectionCountConstraint<TItem>(expectationBuilder, it, grammars, quantifier)),
				subject));
	}

	/// <summary>
	///     Verifies that the <paramref name="subject" /> has exactly <paramref name="expected" /> items.
	/// </summary>
	public static AndOrResult<TItem[], IThat<TItem[]?>> HasCount<TItem>(
		this IThat<TItem[]?> subject, int expected)
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new AndOrResult<TItem[], IThat<TItem[]?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountConstraint<TItem>(expectationBuilder, it, grammars,
					EnumerableQuantifier.Exactly(expected))),
			subject);
	}

	/// <summary>
	///     Verifies that the <paramref name="subject" /> has an item count of…
	/// </summary>
	public static CollectionCountResult<AndOrResult<TEnumerable, IThat<TEnumerable?>>> HasCount<TEnumerable>(
		this IThat<TEnumerable?> subject)
		where TEnumerable : IEnumerable
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new CollectionCountResult<AndOrResult<TEnumerable, IThat<TEnumerable?>>>(quantifier
			=> new AndOrResult<TEnumerable, IThat<TEnumerable?>>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new SyncCollectionCountForEnumerableConstraint<TEnumerable>(expectationBuilder, it, grammars,
						quantifier)),
				subject));
	}

	/// <summary>
	///     Verifies that the <paramref name="subject" /> has exactly <paramref name="expected" /> items.
	/// </summary>
	public static AndOrResult<TEnumerable, IThat<TEnumerable?>> HasCount<TEnumerable>(
		this IThat<TEnumerable?> subject, int expected)
		where TEnumerable : IEnumerable
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new AndOrResult<TEnumerable, IThat<TEnumerable?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountForEnumerableConstraint<TEnumerable>(expectationBuilder, it, grammars,
					EnumerableQuantifier.Exactly(expected))),
			subject);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the <paramref name="subject" /> has an item count of…
	/// </summary>
	public static CollectionCountResult<AndOrResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>?>>>
		HasCount<TItem>(
			this IThat<ImmutableArray<TItem>?> subject)
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new CollectionCountResult<AndOrResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>?>>>(quantifier
			=> new AndOrResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>?>>(
				expectationBuilder.AddConstraint((it, grammars)
					=> new SyncCollectionCountConstraint<TItem>(expectationBuilder, it, grammars, quantifier)),
				subject));
	}
#endif

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the <paramref name="subject" /> has exactly <paramref name="expected" /> items.
	/// </summary>
	public static AndOrResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>?>> HasCount<TItem>(
		this IThat<ImmutableArray<TItem>?> subject, int expected)
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new AndOrResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountConstraint<TItem>(expectationBuilder, it, grammars,
					EnumerableQuantifier.Exactly(expected))),
			subject);
	}
#endif

	/// <summary>
	///     Verifies that the <paramref name="subject" /> does not have <paramref name="unexpected" /> items.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>> DoesNotHaveCount<TItem>(
		this IThat<IEnumerable<TItem>?> subject, int unexpected)
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountConstraint<TItem>(expectationBuilder, it, grammars,
					EnumerableQuantifier.Exactly(unexpected)).Invert()),
			subject);
	}

	/// <summary>
	///     Verifies that the <paramref name="subject" /> does not have <paramref name="unexpected" /> items.
	/// </summary>
	public static AndOrResult<TItem[], IThat<TItem[]?>> DoesNotHaveCount<TItem>(
		this IThat<TItem[]?> subject, int unexpected)
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new AndOrResult<TItem[], IThat<TItem[]?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountConstraint<TItem>(expectationBuilder, it, grammars,
					EnumerableQuantifier.Exactly(unexpected)).Invert()),
			subject);
	}

	/// <summary>
	///     Verifies that the <paramref name="subject" /> does not have <paramref name="unexpected" /> items.
	/// </summary>
	public static AndOrResult<TEnumerable, IThat<TEnumerable?>> DoesNotHaveCount<TEnumerable>(
		this IThat<TEnumerable?> subject, int unexpected)
		where TEnumerable : IEnumerable
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new AndOrResult<TEnumerable, IThat<TEnumerable?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountForEnumerableConstraint<TEnumerable>(expectationBuilder, it, grammars,
					EnumerableQuantifier.Exactly(unexpected)).Invert()),
			subject);
	}

#if NET8_0_OR_GREATER
	/// <summary>
	///     Verifies that the <paramref name="subject" /> does not have <paramref name="unexpected" /> items.
	/// </summary>
	public static AndOrResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>?>> DoesNotHaveCount<TItem>(
		this IThat<ImmutableArray<TItem>?> subject, int unexpected)
	{
		ExpectationBuilder expectationBuilder = subject.Get().ExpectationBuilder;
		return new AndOrResult<ImmutableArray<TItem>, IThat<ImmutableArray<TItem>?>>(
			expectationBuilder.AddConstraint((it, grammars)
				=> new SyncCollectionCountConstraint<TItem>(expectationBuilder, it, grammars,
					EnumerableQuantifier.Exactly(unexpected)).Invert()),
			subject);
	}
#endif
}
