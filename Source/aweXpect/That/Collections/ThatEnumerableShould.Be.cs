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
	///     Verifies that the actual enumerable matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		Be<TItem>(
			this IThat<IEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new();
		CollectionMatchOptions matchOptions = new();
		return new ObjectCollectionMatchResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>(source.ExpectationBuilder
				.AddConstraint(it
					=> new BeConstraint<TItem>(it, doNotPopulateThisValue, expected, options, matchOptions)),
			source,
			options,
			matchOptions);
	}

	private readonly struct BeConstraint<TItem>(
		string it,
		string expectedExpression,
		IEnumerable<TItem> expected,
		ObjectEqualityOptions options,
		CollectionMatchOptions matchOptions)
		: IContextConstraint<IEnumerable<TItem>>
	{
		public ConstraintResult IsMetBy(IEnumerable<TItem> actual, IEvaluationContext context)
		{
			IEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedEnumerable<TItem, IEnumerable<TItem>>(actual);
			using ICollectionMatcher<TItem, object?> matcher =
				matchOptions.GetCollectionMatcher<TItem, object?>(expected);
			foreach (TItem item in materializedEnumerable)
			{
				string? failure = matcher.Verify(it, item, options);
				if (failure != null)
				{
					return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(), failure);
				}
			}

			string? lastFailure = matcher.VerifyComplete(it, options);
			if (lastFailure != null)
			{
				return new ConstraintResult.Failure<IEnumerable<TItem>>(actual, ToString(), lastFailure);
			}

			return new ConstraintResult.Success<IEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		public override string ToString()
			=> $"match collection {expectedExpression}{matchOptions}";
	}
}
