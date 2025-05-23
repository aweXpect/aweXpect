using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatEnumerable
{
	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		IsContainedIn<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<TItem, TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection is contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		IsContainedIn(this IThat<IEnumerable<string?>?> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringCollectionBeContainedInResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<string?, string?>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions)),
			source,
			options,
			matchOptions);
	}
	/// <summary>
	///     Verifies that the collection is not contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>
		IsNotContainedIn<TItem>(
			this IThat<IEnumerable<TItem>?> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions<TItem> options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>?>, TItem>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<TItem, TItem>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}

	/// <summary>
	///     Verifies that the collection is not contained in the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>
		IsNotContainedIn(this IThat<IEnumerable<string?>?> source,
			IEnumerable<string?> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		ExpectationBuilder expectationBuilder = source.Get().ExpectationBuilder;
		return new StringCollectionBeContainedInResult<IEnumerable<string?>, IThat<IEnumerable<string?>?>>(
			expectationBuilder.AddConstraint((it, grammars) =>
				new IsEqualToConstraint<string?, string?>(expectationBuilder, it, grammars,
					doNotPopulateThisValue.TrimCommonWhiteSpace(),
					expected,
					options,
					matchOptions).Invert()),
			source,
			options,
			matchOptions);
	}
}
