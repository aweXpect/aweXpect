using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Helpers;

namespace aweXpect.Core.Nodes;

internal class AndNode : Node
{
	private const string DefaultSeparator = " and ";
	private readonly List<(string, Node)> _nodes = new();
	private string? _currentSeparator;

	public AndNode(Node node)
	{
		Current = node;
	}

	private Node Current { get; set; }

	/// <inheritdoc />
	public override void AddConstraint(IConstraint constraint)
		=> Current.AddConstraint(constraint);

	/// <inheritdoc />
	public override Node? AddMapping<TValue, TTarget>(MemberAccessor<TValue, TTarget> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null,
		Func<TValue?, Task<ConstraintResult.Context?[]>>? contexts = null)
		where TValue : default
		where TTarget : default
		=> Current.AddMapping(memberAccessor, expectationTextGenerator, contexts);

	/// <inheritdoc />
	public override Node? AddAsyncMapping<TValue, TTarget>(
		MemberAccessor<TValue, Task<TTarget>> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null,
		Func<TValue?, Task<ConstraintResult.Context?[]>>? contexts = null)
		where TValue : default
		where TTarget : default
		=> Current.AddAsyncMapping(memberAccessor, expectationTextGenerator, contexts);

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
			    FurtherProcessingStrategy.IgnoreCompletely)
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

	private static ConstraintResult CombineResults(
		ConstraintResult? combinedResult,
		ConstraintResult result,
		string separator,
		FurtherProcessingStrategy? furtherProcessingStrategy)
	{
		if (combinedResult == null)
		{
			return result;
		}

		return new AndConstraintResult(combinedResult, result, separator,
			furtherProcessingStrategy ?? FurtherProcessingStrategy.Continue);
	}

	private sealed class AndConstraintResult(
		ConstraintResult left,
		ConstraintResult right,
		string separator,
		FurtherProcessingStrategy furtherProcessingStrategy)
		: ConstraintResult(And(left.Outcome, right.Outcome), furtherProcessingStrategy)
	{
		private readonly FurtherProcessingStrategy _furtherProcessingStrategy = furtherProcessingStrategy;

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
				    _furtherProcessingStrategy != FurtherProcessingStrategy.IgnoreResult &&
				    !left.HasSameResultTextAs(right))
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
