using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.Equivalency;
using Testably.Expectations.Core.EvaluationContext;
using Testably.Expectations.Options;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatQuantifiedCollectionResultShouldSync
{
	/// <summary>
	///     ...are equal to <paramref name="expected" />.
	/// </summary>
	public static AndOrResult<TCollection, IThat<TCollection>> BeEquivalentTo<TItem,
		TCollection>(
		this QuantifiedCollectionResult<IThat<TCollection>> source,
		TItem expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		where TCollection : IEnumerable<TItem>
	{
		EquivalencyOptions options = new();
		return new AndOrResult<TCollection, IThat<TCollection>>(source.ExpectationBuilder
				.AddConstraint(_ =>
					new ThatQuantifiedCollectionResultShould.BeEquivalentToConstraint<TItem,
						TCollection>(
						expected,
						doNotPopulateThisValue,
						source.Quantity,
						options,
						(a, c) => source.Quantity.GetEvaluator<TItem, TCollection>(a, c))),
			source.Result);
	}
}

#if NET6_0_OR_GREATER
public static partial class ThatQuantifiedCollectionResultShouldAsync
{
	/// <summary>
	///     ...are equal to <paramref name="expected" />.
	/// </summary>
	public static AndOrResult<TCollection, IThat<TCollection>> BeEquivalentTo<TItem,
		TCollection>(
		this QuantifiedCollectionResult<IThat<TCollection>> source,
		TItem expected,
		[CallerArgumentExpression("expected")] string doNotPopulateThisValue = "")
		where TCollection : IAsyncEnumerable<TItem>
	{
		EquivalencyOptions options = new();
		return new AndOrResult<TCollection, IThat<TCollection>>(source.ExpectationBuilder
				.AddConstraint(_ =>
					new ThatQuantifiedCollectionResultShould.BeEquivalentToConstraint<TItem,
						TCollection>(
						expected,
						doNotPopulateThisValue,
						source.Quantity,
						options,
						(a, c) => source.Quantity.GetAsyncEvaluator<TItem, TCollection>(a, c))),
			source.Result);
	}
}
#endif

public static partial class ThatQuantifiedCollectionResultShould
{
	internal readonly struct BeEquivalentToConstraint<TItem, TCollection>(
		TItem expected,
		string expectedExpression,
		CollectionQuantifier quantifier,
		EquivalencyOptions options,
		Func<TCollection, IEvaluationContext, ICollectionEvaluator<TItem>> evaluatorFactory)
		: IAsyncContextConstraint<TCollection>
	{
		public async Task<ConstraintResult> IsMetBy(
			TCollection actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			string[] memberToIgnore = [.. options.MembersToIgnore];
			ICollectionEvaluator<TItem> evaluator = evaluatorFactory(actual, context);
			CollectionEvaluatorResult result = await evaluator
				.CheckCondition(
					expected,
					(a, e) => !Compare.CheckEquivalent(a, e,
						new CompareOptions
						{
							MembersToIgnore = memberToIgnore,
						}).Any(),
					cancellationToken)
				.ConfigureAwait(false);

			return result.IsSuccess switch
			{
				true => new ConstraintResult.Success<TCollection>(actual, ToString()),
				false => new ConstraintResult.Failure(ToString(),
					$"{result.Error} items were equivalent"),
				_ => new ConstraintResult.Failure(ToString(), result.Error)
			};
		}

		public override string ToString()
			=> $"have {quantifier} equivalent to {expectedExpression}";
	}
}
