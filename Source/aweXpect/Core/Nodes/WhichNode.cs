using System;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.EvaluationContext;
using Testably.Expectations.Core.Helpers;

namespace Testably.Expectations.Core.Nodes;

internal class WhichNode<TSource, TProperty> : Node
{
	private Node? _inner;
	private readonly Func<TSource, TProperty?> _propertyAccessor;

	public WhichNode(
		Func<TSource, TProperty?> propertyAccessor)
	{
		_propertyAccessor = propertyAccessor;
	}

	/// <inheritdoc />
	public override void AddConstraint(IConstraint constraint)
		=> _inner?.AddConstraint(constraint);

	/// <inheritdoc />
	public override Node? AddMapping<TValue, TTarget>(
		PropertyAccessor<TValue, TTarget?> propertyAccessor,
		Func<PropertyAccessor, string, string>? expectationTextGenerator = null)
		where TTarget : default
		=> _inner?.AddMapping(propertyAccessor, expectationTextGenerator);

	/// <inheritdoc />
	public override void AddNode(Node node, string? separator = null)
	{
		_inner = node;
	}

	/// <inheritdoc />
	public override Task<ConstraintResult> IsMetBy<TValue>(
		TValue? value,
		IEvaluationContext context,
		CancellationToken cancellationToken) where TValue : default
	{
		if (value is not TSource typedValue)
		{
			throw new InvalidOperationException(
				$"The property type for the actual value in the which node did not match.{Environment.NewLine}Expected {typeof(TValue).Name},{Environment.NewLine}but found {value?.GetType().Name}");
		}

		if (_inner == null)
		{
			throw new InvalidOperationException("No inner node specified for the which node.");
		}

		TProperty? matchingValue = _propertyAccessor(typedValue);
		return _inner.IsMetBy(matchingValue, context, cancellationToken);
	}

	/// <inheritdoc />
	public override void SetReason(BecauseReason becauseReason)
		=> _inner?.SetReason(becauseReason);

	/// <inheritdoc />
	public override string ToString()
		=> _propertyAccessor + base.ToString();
}
