using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.EvaluationContext;
using Testably.Expectations.Formatting;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatQuantifiedCollectionResultShouldSync
{
	/// <summary>
	///     ...are equal to <paramref name="expected" />.
	/// </summary>
	public static AndOrResult<TCollection, IThat<TCollection>> Be<TItem, TCollection>(
		this QuantifiedCollectionResult<IThat<TCollection>> source,
		TItem expected)
		where TCollection : IEnumerable<TItem>
		=> new(source.ExpectationBuilder
				.AddConstraint(_ =>
					new ThatQuantifiedCollectionResultShould.BeEqualConstraint<TItem, TCollection>(
						expected,
						source.Quantity,
						(a, c) => source.Quantity.GetEvaluator<TItem, TCollection>(a, c))),
			source.Result);
}

#if NET6_0_OR_GREATER
public static partial class ThatQuantifiedCollectionResultShouldAsync
{
	/// <summary>
	///     ...are equal to <paramref name="expected" />.
	/// </summary>
	public static AndOrResult<TCollection, IThat<TCollection>> Be<TItem, TCollection>(
		this QuantifiedCollectionResult<IThat<TCollection>> source,
		TItem expected)
		where TCollection : IAsyncEnumerable<TItem>
		=> new(source.ExpectationBuilder
				.AddConstraint(_ =>
					new ThatQuantifiedCollectionResultShould.BeEqualConstraint<TItem, TCollection>(
						expected,
						source.Quantity,
						(a, c) => source.Quantity.GetAsyncEvaluator<TItem, TCollection>(a, c))),
			source.Result);
}
#endif

public static partial class ThatQuantifiedCollectionResultShould
{
	internal readonly struct BeEqualConstraint<TItem, TCollection>(
		TItem expected,
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
				.CheckCondition(expected, (a, e) => a?.Equals(e) == true, cancellationToken)
				.ConfigureAwait(false);

			return result.IsSuccess switch
			{
				true => new ConstraintResult.Success<TCollection>(actual, ToString()),
				false => new ConstraintResult.Failure(ToString(),
					$"{result.Error} items were equal"),
				_ => new ConstraintResult.Failure(ToString(), result.Error)
			};
		}

		public override string ToString()
			=> $"have {quantifier} equal to {Formatter.Format(expected)}";
	}
}
