using System;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Nodes;

internal class WhichNode<TSource, TMember> : Node
{
	private readonly Func<TSource, Task<TMember?>>? _asyncMemberAccessor;
	private readonly Func<TSource, TMember?>? _memberAccessor;
	private readonly string? _separator;
	private Node? _inner;
	private readonly Node? _parent;

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
		
		if (value is not TSource typedValue)
		{
			throw new InvalidOperationException(
				$"The member type for the actual value in the which node did not match.{Environment.NewLine}Expected {typeof(TValue).Name},{Environment.NewLine}but found {value?.GetType().Name}");
		}

		if (_inner == null)
		{
			throw new InvalidOperationException("No inner node specified for the which node.");
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
		var result = await _inner.IsMetBy(matchingValue, context, cancellationToken);
		return CombineResults(parentResult, result, _separator ?? "", ConstraintResult.FurtherProcessing.IgnoreResult);
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

		string combinedExpectation =
			$"{leftResult.ExpectationText}{separator}{rightResult.ExpectationText}";

		if (leftResult is ConstraintResult.Failure leftFailure &&
		    rightResult is ConstraintResult.Failure rightFailure)
		{
			return leftFailure.CombineWith(
				combinedExpectation,
				CombineResultTexts(
					leftFailure.ResultText,
					rightFailure.ResultText,
					furtherProcessingStrategy ?? ConstraintResult.FurtherProcessing.Continue));
		}

		if (leftResult is ConstraintResult.Failure onlyLeftFailure)
		{
			return onlyLeftFailure.CombineWith(
				combinedExpectation,
				onlyLeftFailure.ResultText);
		}

		if (rightResult is ConstraintResult.Failure onlyRightFailure)
		{
			return onlyRightFailure.CombineWith(
				combinedExpectation,
				onlyRightFailure.ResultText);
		}

		return leftResult.CombineWith(combinedExpectation, "");
	}

	private static string CombineResultTexts(
		string leftResultText,
		string rightResultText,
		ConstraintResult.FurtherProcessing furtherProcessingStrategy)
	{
		if (furtherProcessingStrategy == ConstraintResult.FurtherProcessing.IgnoreResult ||
		    leftResultText == rightResultText)
		{
			return leftResultText;
		}

		return $"{leftResultText} and {rightResultText}";
	}

	/// <inheritdoc />
	public override void SetReason(BecauseReason becauseReason)
		=> _inner?.SetReason(becauseReason);

	/// <inheritdoc />
	public override string ToString()
		=> _memberAccessor + base.ToString();
}
