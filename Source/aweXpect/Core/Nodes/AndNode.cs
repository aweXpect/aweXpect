using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.EvaluationContext;
using Testably.Expectations.Core.Helpers;

namespace Testably.Expectations.Core.Nodes;

internal class AndNode : Node
{
	private const string DefaultSeparator = " and ";
	private Node Current { get; set; }
	private string? _currentSeparator;
	private readonly List<(string, Node)> _nodes = new();

	public AndNode(Node node)
	{
		Current = node;
	}

	/// <inheritdoc />
	public override void AddConstraint(IConstraint constraint)
		=> Current.AddConstraint(constraint);

	/// <inheritdoc />
	public override Node? AddMapping<TValue, TTarget>(
		PropertyAccessor<TValue, TTarget?> propertyAccessor,
		Func<PropertyAccessor, string, string>? expectationTextGenerator = null)
		where TTarget : default
		=> Current.AddMapping(propertyAccessor, expectationTextGenerator);

	public override void AddNode(Node node, string? separator = null)
	{
		_nodes.Add((_currentSeparator ?? DefaultSeparator, Current));
		Current = node;
		_currentSeparator = separator;
	}

	/// <inheritdoc />
	public override async Task<ConstraintResult> IsMetBy<TValue>(TValue? value,
		IEvaluationContext context,
		CancellationToken cancellationToken) where TValue : default
	{
		_nodes.Add((_currentSeparator ?? DefaultSeparator, Current));
		ConstraintResult? combinedResult = null;
		foreach ((string separator, Node node) in _nodes)
		{
			if (node is ExpectationNode expectationNode && expectationNode.IsEmpty())
			{
				continue;
			}

			ConstraintResult result = await node.IsMetBy(value, context, cancellationToken);
			combinedResult = CombineResults(combinedResult, result, separator,
				combinedResult?.FurtherProcessingStrategy);
			if (result.FurtherProcessingStrategy ==
			    ConstraintResult.FurtherProcessing.IgnoreCompletely)
			{
				return combinedResult;
			}
		}

		return combinedResult!;
	}

	/// <inheritdoc />
	public override void SetReason(BecauseReason becauseReason)
		=> Current.SetReason(becauseReason);

	/// <inheritdoc />
	public override string? ToString()
	{
		if (_nodes.Any())
		{
			return string.Join(DefaultSeparator, _nodes.Select(x => x.Item2))
			       + DefaultSeparator + Current;
		}

		return Current.ToString();
	}

	private ConstraintResult CombineResults(
		ConstraintResult? combinedResult,
		ConstraintResult result,
		string separator,
		ConstraintResult.FurtherProcessing? furtherProcessingStrategy)
	{
		if (combinedResult == null)
		{
			return result;
		}

		string combinedExpectation =
			$"{combinedResult.ExpectationText}{separator}{result.ExpectationText}";

		if (combinedResult is ConstraintResult.Failure leftFailure &&
		    result is ConstraintResult.Failure rightFailure)
		{
			return leftFailure.CombineWith(
				combinedExpectation,
				CombineResultTexts(
					leftFailure.ResultText,
					rightFailure.ResultText,
					furtherProcessingStrategy ?? ConstraintResult.FurtherProcessing.Continue));
		}

		if (combinedResult is ConstraintResult.Failure onlyLeftFailure)
		{
			return onlyLeftFailure.CombineWith(
				combinedExpectation,
				onlyLeftFailure.ResultText);
		}

		if (result is ConstraintResult.Failure onlyRightFailure)
		{
			return onlyRightFailure.CombineWith(
				combinedExpectation,
				onlyRightFailure.ResultText);
		}

		return combinedResult.CombineWith(combinedExpectation, "");
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
}
