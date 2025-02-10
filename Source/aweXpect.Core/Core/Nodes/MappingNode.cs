using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Sources;

namespace aweXpect.Core.Nodes;

internal class MappingNode<TSource, TTarget> : ExpectationNode
{
	private readonly Action<MemberAccessor<TSource, TTarget?>, StringBuilder>
		_expectationTextGenerator;

	private readonly MemberAccessor<TSource, TTarget?> _memberAccessor;

	public MappingNode(
		MemberAccessor<TSource, TTarget?> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null)
	{
		_memberAccessor = memberAccessor;
		if (expectationTextGenerator == null)
		{
			_expectationTextGenerator = DefaultExpectationTextGenerator;
		}
		else
		{
			_expectationTextGenerator = expectationTextGenerator;
		}
	}

	/// <inheritdoc />
	public override async Task<ConstraintResult> IsMetBy<TValue>(
		TValue? value,
		IEvaluationContext context,
		CancellationToken cancellationToken) where TValue : default
	{
		if (value is null || value is DelegateValue { IsNull: true })
		{
			ConstraintResult result = await base.IsMetBy<TTarget>(default, context, cancellationToken);
			return new ConstraintResult.Failure<TValue?>(value, result.ExpectationText, "it was <null>");
		}

		if (value is not TSource typedValue)
		{
			throw new InvalidOperationException(
				$"The member type for the actual value in the which node did not match.{Environment.NewLine}Expected: {Formatter.Format(typeof(TSource))},{Environment.NewLine}   Found: {Formatter.Format(value.GetType())}");
		}

		TTarget? matchingValue = _memberAccessor.AccessMember(typedValue);
		ConstraintResult memberResult = await base.IsMetBy(matchingValue, context, cancellationToken);
		return memberResult.UseValue(value);
	}

	/// <inheritdoc />
	public override string ToString()
		=> _memberAccessor + base.ToString();

	// TODO VAB: Rework
	internal ConstraintResult CombineResults(
		ConstraintResult? combinedResult,
		ConstraintResult result)
	{
		if (combinedResult == null)
		{
			return result.UpdateExpectationText(
				e => _expectationTextGenerator(_memberAccessor, e));
		}

		return new MappingConstraintResult(combinedResult, result, _expectationTextGenerator, _memberAccessor);
	}

	private class MappingConstraintResult(
		ConstraintResult left,
		ConstraintResult right,
		Action<MemberAccessor<TSource, TTarget?>, StringBuilder>? expectationTextGenerator,
		MemberAccessor<TSource, TTarget?> memberAccessor)
		: ConstraintResult(And(left.Outcome, right.Outcome), FurtherProcessing.Continue)
	{
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
			if (expectationTextGenerator is not null)
			{
				expectationTextGenerator(memberAccessor, stringBuilder);
			}
			right.AppendExpectation(stringBuilder);
		}

		public override void AppendResult(StringBuilder stringBuilder, string? indentation = null)
		{
			if (left.Outcome == Outcome.Failure)
			{
				left.AppendResult(stringBuilder, indentation);
				if (right.Outcome == Outcome.Failure &&
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

	private static void DefaultExpectationTextGenerator(
		MemberAccessor<TSource, TTarget?> memberAccessor,
		StringBuilder expectation)
	{
		expectation.Append(memberAccessor);
	}
}
