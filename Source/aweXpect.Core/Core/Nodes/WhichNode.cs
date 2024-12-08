using System;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Nodes;

internal class WhichNode<TSource, TMember> : Node
{
	private readonly Func<TSource, TMember?> _memberAccessor;
	private Node? _inner;

	public WhichNode(
		Func<TSource, TMember?> memberAccessor)
	{
		_memberAccessor = memberAccessor;
	}

	/// <inheritdoc />
	public override void AddConstraint(IConstraint constraint)
		=> _inner?.AddConstraint(constraint);

	/// <inheritdoc />
	public override Node? AddMapping<TValue, TTarget>(
		MemberAccessor<TValue, TTarget?> memberAccessor,
		Func<MemberAccessor, string, string>? expectationTextGenerator = null)
		where TTarget : default
		=> _inner?.AddMapping(memberAccessor, expectationTextGenerator);

	/// <inheritdoc />
	public override void AddNode(Node node, string? separator = null)
		=> _inner = node;

	/// <inheritdoc />
	public override Task<ConstraintResult> IsMetBy<TValue>(
		TValue? value,
		IEvaluationContext context,
		CancellationToken cancellationToken) where TValue : default
	{
		if (value is not TSource typedValue)
		{
			throw new InvalidOperationException(
				$"The member type for the actual value in the which node did not match.{Environment.NewLine}Expected {typeof(TValue).Name},{Environment.NewLine}but found {value?.GetType().Name}");
		}

		if (_inner == null)
		{
			throw new InvalidOperationException("No inner node specified for the which node.");
		}

		TMember? matchingValue = _memberAccessor(typedValue);
		return _inner.IsMetBy(matchingValue, context, cancellationToken);
	}

	/// <inheritdoc />
	public override void SetReason(BecauseReason becauseReason)
		=> _inner?.SetReason(becauseReason);

	/// <inheritdoc />
	public override string ToString()
		=> _memberAccessor + base.ToString();
}
