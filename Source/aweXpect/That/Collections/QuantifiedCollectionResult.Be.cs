using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.EvaluationContext;
using Testably.Expectations.Results;

namespace Testably.Expectations;

/// <summary>
///     A container for specific instances of a quantified result.
/// </summary>
public class QuantifiedCollectionResult
{
	/// <summary>
	///     A quantifiable result matching items against the expected
	///     <see cref="QuantifiedCollectionResult{TResult}.Quantity" />.
	/// </summary>
	public class Sync<TResult, TItem, TCollection>(
		TResult result,
		ExpectationBuilder expectationBuilder,
		CollectionQuantifier quantity)
		: QuantifiedCollectionResult<TResult>(result, expectationBuilder, quantity)
		where TCollection : IEnumerable<TItem>
		where TResult : IThat<TCollection>
	{
		/// <summary>
		///     ...are of type <typeparamref name="TExpected" />.
		/// </summary>
		public AndOrResult<TCollection, IThat<TCollection>> Be<TExpected>()
			=> new(ExpectationBuilder
					.AddConstraint(_ =>
						new BeOfTypeConstraint<TItem, TCollection, TExpected>(
							Quantity,
							(a, c) => Quantity.GetEvaluator<TItem, TCollection>(a, c))),
				Result);
	}

#if NET6_0_OR_GREATER
	/// <summary>
	///     A quantifiable result matching items against the expected
	///     <see cref="QuantifiedCollectionResult{TResult}.Quantity" />.
	/// </summary>
	public class Async<TResult, TItem, TCollection>(
		TResult result,
		ExpectationBuilder expectationBuilder,
		CollectionQuantifier quantity)
		: QuantifiedCollectionResult<TResult>(result, expectationBuilder, quantity)
		where TCollection : IAsyncEnumerable<TItem>
		where TResult : IThat<TCollection>
	{
		/// <summary>
		///     ...are of type <typeparamref name="TExpected" />.
		/// </summary>
		public AndOrResult<TCollection, IThat<TCollection>> Be<TExpected>()
			=> new(ExpectationBuilder
					.AddConstraint(_ =>
						new BeOfTypeConstraint<TItem, TCollection, TExpected>(
							Quantity,
							(a, c) => Quantity.GetAsyncEvaluator<TItem, TCollection>(a, c))),
				Result);
	}
#endif

	private readonly struct BeOfTypeConstraint<TItem, TCollection, TExpected>(
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
				.CheckCondition(default(TItem), (a, _) => a is TExpected, cancellationToken)
				.ConfigureAwait(false);

			return result.IsSuccess switch
			{
				true => new ConstraintResult.Success<TCollection>(actual, ToString()),
				false => new ConstraintResult.Failure(ToString(), $"{result.Error} items were"),
				_ => new ConstraintResult.Failure(ToString(), result.Error)
			};
		}

		public override string ToString()
			=> $"have {quantifier} of type {typeof(TExpected).Name}";
	}
}
