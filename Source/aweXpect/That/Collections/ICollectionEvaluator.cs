using System;
using System.Threading;
using System.Threading.Tasks;

namespace Testably.Expectations;

/// <summary>
///     The evaluator of a <see cref="CollectionQuantifier" />.
/// </summary>
public interface ICollectionEvaluator<out TItem>
{
	/// <summary>
	///     Checks if the condition is satisfied by iterating over the <typeparamref name="TItem" />s
	///     until a decision can be reached.
	/// </summary>
	Task<CollectionEvaluatorResult> CheckCondition<TExpected>(
		TExpected expected,
		Func<TItem, TExpected, bool> predicate,
		CancellationToken cancellationToken);
}
