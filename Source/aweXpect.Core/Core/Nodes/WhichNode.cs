﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Helpers;
using aweXpect.Core.Sources;

namespace aweXpect.Core.Nodes;

internal class WhichNode<TSource, TMember> : Node
{
	private readonly Func<TSource, Task<TMember?>>? _asyncMemberAccessor;
	private readonly Func<TSource, TMember?>? _memberAccessor;
	private readonly Node? _parent;
	private readonly string? _separator;
	private Node? _inner;

	public WhichNode(
		Node? parent,
		Func<TSource, TMember?> memberAccessor,
		string? separator = null)
	{
		_parent = parent;
		_memberAccessor = memberAccessor;
		_separator = separator;
	}

	public WhichNode(
		Node? parent,
		Func<TSource, Task<TMember?>> asyncMemberAccessor,
		string? separator = null)
	{
		_parent = parent;
		_asyncMemberAccessor = asyncMemberAccessor;
		_separator = separator;
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
	public override async Task<ConstraintResult> IsMetBy<TValue>(
		TValue? value,
		IEvaluationContext context,
		CancellationToken cancellationToken) where TValue : default
	{
		ConstraintResult? parentResult = null;
		if (_parent != null)
		{
			parentResult = await _parent.IsMetBy(value, context, cancellationToken);
		}

		if (_inner == null)
		{
			throw new InvalidOperationException("No inner node specified for the which node.");
		}

		if (value is null || value is DelegateValue { IsNull: true })
		{
			ConstraintResult nullResult = await _inner.IsMetBy<TMember>(default, context, cancellationToken);
			return CombineResults(parentResult, nullResult, _separator ?? "",
				ConstraintResult.FurtherProcessing.IgnoreResult);
		}

		if (value is not TSource typedValue)
		{
			throw new InvalidOperationException(
				$"The member type for the actual value in the which node did not match.{Environment.NewLine}Expected {typeof(TValue).Name},{Environment.NewLine}but found {value.GetType().Name}");
		}

		TMember? matchingValue;
		if (_memberAccessor != null)
		{
			matchingValue = _memberAccessor(typedValue);
		}
		else
		{
			matchingValue = await _asyncMemberAccessor!.Invoke(typedValue);
		}

		ConstraintResult result = await _inner.IsMetBy(matchingValue, context, cancellationToken);
		return CombineResults(parentResult, result, _separator ?? "", ConstraintResult.FurtherProcessing.IgnoreResult)
			.UseValue(matchingValue);
	}

	private class WhichConstraintResult(
		ConstraintResult left,
		ConstraintResult right,
		string separator,
		ConstraintResult.FurtherProcessing furtherProcessingStrategy)
		: ConstraintResult(And(left.Outcome, right.Outcome), furtherProcessingStrategy)
	{
		private readonly FurtherProcessing _furtherProcessingStrategy = furtherProcessingStrategy;

		private static Outcome And(Outcome left, Outcome right)
			=> (left, right) switch
			{
				(Outcome.Success, Outcome.Success) => Outcome.Success,
				(_, Outcome.Failure) => Outcome.Failure,
				(Outcome.Failure, _) => Outcome.Failure,
				(_, _) => Outcome.Undecided,
			};

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			left.AppendExpectation(stringBuilder);
			stringBuilder.Append(separator);
			right.AppendExpectation(stringBuilder);
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (left.Outcome == Outcome.Failure)
			{
				left.AppendResult(stringBuilder, indentation);
				if (right.Outcome == Outcome.Failure &&
				    _furtherProcessingStrategy != FurtherProcessing.IgnoreResult &&
				    left.GetResultText() != right.GetResultText())
				{
					stringBuilder.Append(" and ");
					right.AppendResult(stringBuilder, indentation);
				}
			}
			else if (right.Outcome == Outcome.Failure)
			{
				right.AppendResult(stringBuilder, indentation);
			}
		}

		internal override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
			where TValue : default
		{
			if (left.TryGetValue<TValue>(out var leftValue))
			{
				value = leftValue;
				return true;
			}

			if (right.TryGetValue<TValue>(out var rightValue))
			{
				value = rightValue;
				return true;
			}

			value = default;
			return false;
		}
	}

	private static ConstraintResult CombineResults(
		ConstraintResult? leftResult,
		ConstraintResult rightResult,
		string separator,
		ConstraintResult.FurtherProcessing? furtherProcessingStrategy)
	{
		if (leftResult == null)
		{
			return rightResult;
		}

		return new WhichConstraintResult(leftResult, rightResult, separator,
			furtherProcessingStrategy ?? ConstraintResult.FurtherProcessing.Continue);
	}

	/// <inheritdoc />
	public override void SetReason(BecauseReason becauseReason)
		=> _inner?.SetReason(becauseReason);

	/// <inheritdoc />
	public override string ToString()
		=> _memberAccessor + base.ToString();
}
