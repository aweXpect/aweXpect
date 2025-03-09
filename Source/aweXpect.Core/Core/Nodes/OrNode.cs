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

internal class OrNode : Node
{
	private const string DefaultSeparator = " or ";
	private readonly List<(string, Node)> _nodes = new();
	private string? _currentSeparator;

	public OrNode(Node node)
	{
		Current = node;
	}

	internal Node Current { get; set; }

	/// <inheritdoc />
	public override void AddConstraint(IConstraint constraint)
		=> Current.AddConstraint(constraint);

	/// <inheritdoc />
	public override Node? AddMapping<TValue, TTarget>(MemberAccessor<TValue, TTarget> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null)
		where TValue : default
		where TTarget : default
		=> Current.AddMapping(memberAccessor, expectationTextGenerator);

	/// <inheritdoc />
	public override Node? AddAsyncMapping<TValue, TTarget>(
		MemberAccessor<TValue, Task<TTarget>> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null)
		where TValue : default
		where TTarget : default
		=> Current.AddAsyncMapping(memberAccessor, expectationTextGenerator);

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
			ConstraintResult result = await node.IsMetBy(value, context, cancellationToken);
			combinedResult = CombineResults(combinedResult, result, separator,
				combinedResult?.FurtherProcessingStrategy);
			if (result.FurtherProcessingStrategy == FurtherProcessingStrategy.IgnoreCompletely)
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
	public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
	{
		foreach (Node node in _nodes.Select(n => n.Item2))
		{
			node.AppendExpectation(stringBuilder, indentation);
			stringBuilder.Append(DefaultSeparator);
		}

		Current.AppendExpectation(stringBuilder, indentation);
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

		return new OrConstraintResult(combinedResult, result, separator,
			furtherProcessingStrategy ?? FurtherProcessingStrategy.Continue);
	}

	private sealed class OrConstraintResult : ConstraintResult
	{
		private readonly FurtherProcessingStrategy _furtherProcessingStrategy;
		private readonly ConstraintResult _left;
		private readonly ConstraintResult _right;
		private readonly string _separator;
		private bool _isNegated;

		public OrConstraintResult(ConstraintResult left,
			ConstraintResult right,
			string separator,
			FurtherProcessingStrategy furtherProcessingStrategy) : base(furtherProcessingStrategy)
		{
			_left = left;
			_right = right;
			_separator = separator;
			_furtherProcessingStrategy = furtherProcessingStrategy;
			Outcome = Or(left.Outcome, right.Outcome);
		}

		private static Outcome Or(Outcome left, Outcome right)
			=> (left, right) switch
			{
				(Outcome.Failure, Outcome.Failure) => Outcome.Failure,
				(_, Outcome.Undecided) => Outcome.Undecided,
				(Outcome.Undecided, _) => Outcome.Undecided,
				(_, _) => Outcome.Success,
			};

		public override void AppendExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			_left.AppendExpectation(stringBuilder);
			if (_separator == DefaultSeparator && _isNegated)
			{
				stringBuilder.Append(" and ");
			}
			else
			{
				stringBuilder.Append(_separator);
			}

			_right.AppendExpectation(stringBuilder);
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (_left.Outcome == Outcome.Failure)
			{
				_left.AppendResult(stringBuilder, indentation);
				if (_right.Outcome == Outcome.Failure &&
				    _furtherProcessingStrategy != FurtherProcessingStrategy.IgnoreResult &&
				    !_left.HasSameResultTextAs(_right))
				{
					stringBuilder.Append(" and ");
					_right.AppendResult(stringBuilder, indentation);
				}
			}
			else if (_right.Outcome == Outcome.Failure &&
			         _furtherProcessingStrategy != FurtherProcessingStrategy.IgnoreResult)
			{
				_right.AppendResult(stringBuilder, indentation);
			}
		}

		public override bool TryGetValue<TValue>([NotNullWhen(true)] out TValue? value)
			where TValue : default
		{
			if (_left.TryGetValue(out TValue? leftValue))
			{
				value = leftValue;
				return true;
			}

			if (_right.TryGetValue(out TValue? rightValue))
			{
				value = rightValue;
				return true;
			}

			value = default;
			return false;
		}

		public override ConstraintResult Negate()
		{
			_isNegated = !_isNegated;
			Outcome = Outcome switch
			{
				Outcome.Failure => Outcome.Success,
				Outcome.Success => Outcome.Failure,
				_ => Outcome,
			};
			_left.Negate();
			_right.Negate();
			return this;
		}
	}
}
