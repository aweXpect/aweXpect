using System;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.EvaluationContext;
using Testably.Expectations.Core.Helpers;

namespace Testably.Expectations.Core.Nodes;

internal abstract class Node
{
	/// <summary>
	///     Add a constraint to the current node.
	/// </summary>
	public abstract void AddConstraint(IConstraint constraint);

	/// <summary>
	///     Add a mapping constraint which maps the value according to the <paramref name="propertyAccessor" /> to a property
	///     and applies this value to the inner expectations.
	/// </summary>
	public abstract Node? AddMapping<TValue, TTarget>(
		PropertyAccessor<TValue, TTarget?> propertyAccessor,
		Func<PropertyAccessor, string, string>? expectationTextGenerator = null);

	/// <summary>
	///     Add a node as inner node.
	/// </summary>
	public abstract void AddNode(Node node, string? separator = null);

	/// <summary>
	///     Verifies, if the <paramref name="value" /> satisfies the expectations of the node.
	/// </summary>
	public abstract Task<ConstraintResult> IsMetBy<TValue>(
		TValue? value,
		IEvaluationContext context,
		CancellationToken cancellationToken);

	/// <summary>
	///     Set the <paramref name="becauseReason" /> on the current node.
	/// </summary>
	public abstract void SetReason(BecauseReason becauseReason);
}
