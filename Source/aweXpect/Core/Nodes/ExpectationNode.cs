using System;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.EvaluationContext;
using Testably.Expectations.Core.Helpers;

namespace Testably.Expectations.Core.Nodes;

internal class ExpectationNode : Node
{
	private Func<ConstraintResult?, ConstraintResult, ConstraintResult>? _combineResults;
	private IConstraint? _constraint;

	private Node? _inner;

	private BecauseReason? _reason;

	/// <inheritdoc />
	public override void AddConstraint(IConstraint constraint)
	{
		if (_inner is not null)
		{
			_inner.AddConstraint(constraint);
		}
		else if (_constraint is null)
		{
			_constraint = constraint;
		}
		else
		{
			throw new InvalidOperationException(
				"You have to specify how to combine the expectations! Use `And()` or `Or()` in between adding expectations.");
		}
	}

	/// <inheritdoc />
	public override Node? AddMapping<TValue, TTarget>(
		PropertyAccessor<TValue, TTarget?> propertyAccessor,
		Func<PropertyAccessor, string, string>? expectationTextGenerator = null)
		where TTarget : default
	{
		MappingNode<TValue, TTarget> mappingNode =
			new(propertyAccessor,
				expectationTextGenerator);
		_inner = mappingNode;
		_combineResults = mappingNode.CombineResults;
		return mappingNode;
	}

	/// <inheritdoc />
	public override void AddNode(Node node, string? separator = null)
		=> throw new NotSupportedException(
			$"Don't specify the inner node for Expectation nodes directly. You can use {nameof(AddMapping)} instead.");

	/// <summary>
	///     Indicates, if the node is empty.
	/// </summary>
	public bool IsEmpty()
	{
		return _constraint is null && _inner is null;
	}

	/// <inheritdoc />
	public override async Task<ConstraintResult> IsMetBy<TValue>(TValue? value,
		IEvaluationContext context,
		CancellationToken cancellationToken) where TValue : default
	{
		ConstraintResult? result = null;
		if (_constraint is IValueConstraint<TValue?> valueConstraint)
		{
			result = valueConstraint.IsMetBy(value);
			result = _reason?.ApplyTo(result) ?? result;
		}
		else if (_constraint is IContextConstraint<TValue?> contextConstraint)
		{
			result = contextConstraint.IsMetBy(value, context);
			result = _reason?.ApplyTo(result) ?? result;
		}
		else if (_constraint is IAsyncConstraint<TValue?> asyncConstraint)
		{
			result = await asyncConstraint.IsMetBy(value, cancellationToken);
			result = _reason?.ApplyTo(result) ?? result;
		}
		else if (_constraint is IAsyncContextConstraint<TValue?> asyncContextConstraint)
		{
			result = await asyncContextConstraint.IsMetBy(value, context, cancellationToken);
			result = _reason?.ApplyTo(result) ?? result;
		}

		if (_inner != null)
		{
			ConstraintResult innerResult = await _inner.IsMetBy(value, context, cancellationToken);
			innerResult = _combineResults?.Invoke(result, innerResult) ?? innerResult;
			return innerResult;
		}

		if (_combineResults != null && result != null)
		{
			return _combineResults.Invoke(null, result);
		}

		return result ?? throw new InvalidOperationException(
			$"The expectation node does not support {typeof(TValue).Name} {value}");
	}

	/// <inheritdoc />
	public override void SetReason(BecauseReason becauseReason)
	{
		_reason = becauseReason;
	}

	/// <inheritdoc />
	public override string? ToString()
	{
		if (_constraint != null && _inner != null)
		{
			return _constraint + _inner.ToString();
		}

		if (_constraint != null)
		{
			return _constraint.ToString();
		}

		if (_inner != null)
		{
			return _inner.ToString();
		}

		return "<empty>";
	}
}
