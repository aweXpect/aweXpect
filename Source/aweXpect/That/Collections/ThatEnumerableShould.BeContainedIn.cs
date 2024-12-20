using System.Collections.Generic;
using System.Runtime.CompilerServices;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatEnumerableShould
{
	/// <summary>
	///     Verifies that the collection matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		BeContainedIn<TItem>(
			this IThat<IEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		matchOptions.SetEquivalenceRelation(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		return new ObjectCollectionBeContainedInResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(source.ExpectationBuilder
				.AddConstraint(it
					=> new BeConstraint<TItem, object?>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}
	/// <summary>
	///     Verifies that the collection matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static StringCollectionBeContainedInResult<IEnumerable<string>, IThat<IEnumerable<string>>>
		BeContainedIn(this IThat<IEnumerable<string>> source,
			IEnumerable<string> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		StringEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		matchOptions.SetEquivalenceRelation(CollectionMatchOptions.EquivalenceRelations.IsContainedIn);
		return new StringCollectionBeContainedInResult<IEnumerable<string>, IThat<IEnumerable<string>>>(source.ExpectationBuilder
				.AddConstraint(it
					=> new BeConstraint<string, string>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}
}
