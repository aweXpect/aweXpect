using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Nodes;

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
					"You have to specify how to combine the expectations! Use `And()` or `Or()` in between adding expectations.")
				.LogTrace();
		}
	}

	/// <inheritdoc />
	public override Node? AddMapping<TValue, TTarget>(MemberAccessor<TValue, TTarget> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null)
		where TValue : default
		where TTarget : default
	{
		MappingNode<TValue, TTarget> mappingNode =
			new(memberAccessor, expectationTextGenerator);
		_inner = mappingNode;
		_combineResults = mappingNode.CombineResults;
		return mappingNode;
	}

	/// <inheritdoc />
	public override Node? AddAsyncMapping<TValue, TTarget>(
		MemberAccessor<TValue, Task<TTarget>> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null)
		where TValue : default
		where TTarget : default
	{
		AsyncMappingNode<TValue, TTarget> mappingNode =
			new(memberAccessor, expectationTextGenerator);
		_inner = mappingNode;
		_combineResults = mappingNode.CombineResults;
		return mappingNode;
	}

	/// <inheritdoc />
	public override void AddNode(Node node, string? separator = null)
		=> throw new NotSupportedException(
				$"Don't specify the inner node for Expectation nodes directly. Use {nameof(AddMapping)}() instead!")
			.LogTrace();

	/// <summary>
	///     Indicates, if the node is empty.
	/// </summary>
	public bool IsEmpty() => _constraint is null && _inner is null;

	/// <inheritdoc />
	public override async Task<ConstraintResult> IsMetBy<TValue>(TValue? value,
		IEvaluationContext context,
		CancellationToken cancellationToken) where TValue : default
	{
		ConstraintResult? result = null;
		try
		{
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
		}
		catch (Exception e) when (e is not ArgumentException && _constraint is not null)
		{
			throw new InvalidOperationException(
					$"Error evaluating {Formatter.Format(_constraint.GetType())} constraint with value {Formatter.Format(value)}: {e.Message}",
					e)
				.LogTrace();
		}

		if (_inner != null)
		{
			ConstraintResult innerResult = await _inner.IsMetBy(value, context, cancellationToken);
			innerResult = _combineResults?.Invoke(result, innerResult) ?? innerResult;
			return innerResult;
		}

		return result ?? throw new InvalidOperationException(
				$"The expectation node does not support {Formatter.Format(typeof(TValue))} with value {Formatter.Format(value)}")
			.LogTrace();
	}

	/// <inheritdoc />
	public override void SetReason(BecauseReason becauseReason) => _reason = becauseReason;

	public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
	{
		_constraint?.AppendExpectation(stringBuilder, indentation);
		_inner?.AppendExpectation(stringBuilder, indentation);
	}
	
	/// <inheritdoc cref="object.Equals(object?)" />
	public override bool Equals(object? obj) => obj is ExpectationNode other && Equals(other);

	private bool Equals(ExpectationNode other)
	{
		if (_constraint is null && other._constraint is null)
		{
			return _inner?.Equals(other._inner) == true;
		}

		if (_constraint is null || other._constraint is null)
		{
			return false;
		}
		var sb1 = new StringBuilder();
		var sb2 = new StringBuilder();
		_constraint.AppendExpectation(sb1);
		other._constraint.AppendExpectation(sb2);
		return sb1.ToString() == sb2.ToString() && _inner?.Equals(other._inner) != false;
	}

	/// <inheritdoc cref="object.GetHashCode()" />
	public override int GetHashCode() => _inner?.GetHashCode() ?? 23;
}
