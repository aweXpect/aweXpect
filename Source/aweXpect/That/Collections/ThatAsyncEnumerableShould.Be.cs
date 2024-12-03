#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Helpers;
using aweXpect.Options;
using aweXpect.Results;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect;

public static partial class ThatAsyncEnumerableShould
{
	/// <summary>
	///     Verifies that the actual enumerable matches the provided <paramref name="expected" /> collection.
	/// </summary>
	public static ObjectCollectionMatchResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		Be<TItem>(
			this IThat<IAsyncEnumerable<TItem>> source,
			IEnumerable<TItem> expected,
			[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
	{
		ObjectEqualityOptions options = new ObjectEqualityOptions();
		CollectionMatchOptions matchOptions = new CollectionMatchOptions();
		return new ObjectCollectionMatchResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>(source
				.ExpectationBuilder
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
		: IAsyncContextConstraint<IAsyncEnumerable<TItem>>
	{
		public async Task<ConstraintResult> IsMetBy(IAsyncEnumerable<TItem> actual, IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			IAsyncEnumerable<TItem> materializedEnumerable =
				context.UseMaterializedAsyncEnumerable<TItem, IAsyncEnumerable<TItem>>(actual);
			using ICollectionMatcher<TItem, object?> matcher =
				matchOptions.GetCollectionMatcher<TItem, object?>(expected);
			await foreach (TItem item in materializedEnumerable.WithCancellation(cancellationToken))
			{
				string? failure = matcher.Verify(it, item, options);
				if (failure != null)
				{
					return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(), failure);
				}
			}

			string? lastFailure = matcher.VerifyComplete(it, options);
			if (lastFailure != null)
			{
				return new ConstraintResult.Failure<IAsyncEnumerable<TItem>>(actual, ToString(), lastFailure);
			}

			return new ConstraintResult.Success<IAsyncEnumerable<TItem>>(materializedEnumerable,
				ToString());
		}

		public override string ToString()
			=> $"match collection {expectedExpression}{matchOptions}";
	}
}
#endif
