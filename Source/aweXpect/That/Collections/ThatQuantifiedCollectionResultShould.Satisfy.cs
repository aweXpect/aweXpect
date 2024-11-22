using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.EvaluationContext;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatQuantifiedCollectionResultShouldSync
{
	/// <summary>
	///     ...satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<TCollection, IThat<TCollection>>
		Satisfy<TItem, TCollection>(
			this QuantifiedCollectionResult<IThat<TCollection>> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		where TCollection : IEnumerable<TItem>
		=> new(source.ExpectationBuilder
				.AddConstraint(_ =>
					new ThatQuantifiedCollectionResultShould.SatisfyConstraint<TItem, TCollection>(
						predicate,
						doNotPopulateThisValue,
						source.Quantity,
						(a, c) => source.Quantity.GetEvaluator<TItem, TCollection>(a, c))),
			source.Result);

	/// <summary>
	///     ...satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<IEnumerable<TItem>, IThat<IEnumerable<TItem>>>
		Satisfy<TItem>(
			this QuantifiedCollectionResult<IThat<IEnumerable<TItem>>> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		=> new(source.ExpectationBuilder
				.AddConstraint(_ =>
					new ThatQuantifiedCollectionResultShould.SatisfyConstraint<TItem,
						IEnumerable<TItem>>(
						predicate,
						doNotPopulateThisValue,
						source.Quantity,
						(a, c) => source.Quantity.GetEvaluator<TItem, IEnumerable<TItem>>(a, c))),
			source.Result);
}

#if NET6_0_OR_GREATER
public static partial class ThatQuantifiedCollectionResultShouldAsync
{
	/// <summary>
	///     ...satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<TCollection, IThat<TCollection>>
		Satisfy<TItem, TCollection>(
			this QuantifiedCollectionResult<IThat<TCollection>> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		where TCollection : IAsyncEnumerable<TItem>
		=> new(source.ExpectationBuilder
				.AddConstraint(_ =>
					new ThatQuantifiedCollectionResultShould.SatisfyConstraint<TItem, TCollection>(
						predicate,
						doNotPopulateThisValue,
						source.Quantity,
						(a, c) => source.Quantity.GetAsyncEvaluator<TItem, TCollection>(a, c))),
			source.Result);

	/// <summary>
	///     ...satisfy the <paramref name="predicate" />.
	/// </summary>
	public static AndOrResult<IAsyncEnumerable<TItem>, IThat<IAsyncEnumerable<TItem>>>
		Satisfy<TItem>(
			this QuantifiedCollectionResult<IThat<IAsyncEnumerable<TItem>>> source,
			Func<TItem, bool> predicate,
			[CallerArgumentExpression("predicate")]
			string doNotPopulateThisValue = "")
		=> new(source.ExpectationBuilder
				.AddConstraint(_ =>
					new ThatQuantifiedCollectionResultShould.SatisfyConstraint<TItem,
						IAsyncEnumerable<TItem>>(
						predicate,
						doNotPopulateThisValue,
						source.Quantity,
						(a, c)
							=> source.Quantity.GetAsyncEvaluator<TItem, IAsyncEnumerable<TItem>>(a,
								c))),
			source.Result);
}
#endif

public static partial class ThatQuantifiedCollectionResultShould
{
	internal readonly struct SatisfyConstraint<TItem, TCollection>(
		Func<TItem, bool> predicate,
		string expression,
		CollectionQuantifier quantifier,
		Func<TCollection, IEvaluationContext, ICollectionEvaluator<TItem>> evaluatorFactory)
		: IAsyncContextConstraint<TCollection>
	{
		public async Task<ConstraintResult> IsMetBy(
			TCollection actual,
			IEvaluationContext context,
			CancellationToken cancellationToken)
		{
			ICollectionEvaluator<TItem> evaluator = evaluatorFactory(actual, context);
			CollectionEvaluatorResult result = await evaluator
				.CheckCondition(predicate, (a, p) => p(a), cancellationToken)
				.ConfigureAwait(false);

			return result.IsSuccess switch
			{
				true => new ConstraintResult.Success<TCollection>(actual, ToString()),
				false => new ConstraintResult.Failure(ToString(), $"{result.Error} did"),
				_ => new ConstraintResult.Failure(ToString(), result.Error),
			};
		}

		public override string ToString()
			=> $"{quantifier.ToString(false)} satisfy {Formatter.Format(expression)}";
	}
}
