using System;
using System.Collections.Generic;
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
		MemberAccessor<TValue, TTarget> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null)
		where TTarget : default
		=> _inner?.AddMapping(memberAccessor, expectationTextGenerator);

	/// <inheritdoc />
	public override Node? AddAsyncMapping<TValue, TTarget>(
		MemberAccessor<TValue, Task<TTarget>> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null)
		where TTarget : default
		=> _inner?.AddAsyncMapping(memberAccessor, expectationTextGenerator);

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
				FurtherProcessingStrategy.IgnoreResult, default);
		}

		if (value is not TSource typedValue)
		{
			throw new InvalidOperationException(
				$"The member type for the actual value in the which node did not match.{Environment.NewLine}     Found: {Formatter.Format(value.GetType())}{Environment.NewLine}  Expected: {Formatter.Format(typeof(TSource))}");
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
		return CombineResults(parentResult, result, _separator ?? "", FurtherProcessingStrategy.IgnoreResult,
			matchingValue);
	}

	private static ConstraintResult CombineResults(ConstraintResult? leftResult,
		ConstraintResult rightResult,
		string separator,
		FurtherProcessingStrategy? furtherProcessingStrategy,
		TMember? value)
	{
		if (leftResult == null)
		{
			return rightResult;
		}

		return new WhichConstraintResult(leftResult, rightResult, separator,
			furtherProcessingStrategy ?? FurtherProcessingStrategy.Continue,
			value);
	}

	/// <inheritdoc />
	public override void SetReason(BecauseReason becauseReason)
		=> _inner?.SetReason(becauseReason);

	private sealed class WhichConstraintResult(
		ConstraintResult left,
		ConstraintResult right,
		string separator,
		FurtherProcessingStrategy furtherProcessingStrategy,
		TMember? value)
		: ConstraintResult(And(left.Outcome, right.Outcome), furtherProcessingStrategy)
	{
		// ReSharper disable once ReplaceWithPrimaryConstructorParameter
		private readonly TMember? _value = value;

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
			}
			else if (right.Outcome == Outcome.Failure)
			{
				right.AppendResult(stringBuilder, indentation);
			}
		}

		public override IEnumerable<Context> GetContexts()
		{
			foreach (Context context in left.GetContexts())
			{
				yield return context;
			}

			foreach (Context context in right.GetContexts())
			{
				yield return context;
			}
		}

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
			where TValue : default
		{
			if (_value is TValue typedValue)
			{
				value = typedValue;
				return true;
			}

			if (left.TryGetValue(out TValue? leftValue))
			{
				value = leftValue;
				return true;
			}

			if (right.TryGetValue(out TValue? rightValue))
			{
				value = rightValue;
				return true;
			}

			value = default;
			return false;
		}
	}
}
