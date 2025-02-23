using System;
using System.Collections.Generic;
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
	private readonly Action<MemberAccessor<TSource, TTarget>, StringBuilder>
		_expectationTextGenerator;

	private readonly MemberAccessor<TSource, TTarget> _memberAccessor;
	private readonly Func<TSource?, Task<ConstraintResult.Context>>? _context;

	public MappingNode(MemberAccessor<TSource, TTarget> memberAccessor,
		Action<MemberAccessor, StringBuilder>? expectationTextGenerator = null,
		Func<TSource?, Task<ConstraintResult.Context>>? context = null)
	{
		_memberAccessor = memberAccessor;
		_context = context;
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
			return await AddContext(result.Fail("it was <null>", value), default, _context);
		}

		if (value is not TSource typedValue)
		{
			throw new InvalidOperationException(
				$"The member type for the actual value in the which node did not match.{Environment.NewLine}Expected: {Formatter.Format(typeof(TSource))},{Environment.NewLine}   Found: {Formatter.Format(value.GetType())}");
		}

		TTarget matchingValue = _memberAccessor.AccessMember(typedValue);
		ConstraintResult memberResult = await base.IsMetBy(matchingValue, context, cancellationToken);
		return await AddContext(memberResult.UseValue(value), typedValue, _context);
	}

	internal ConstraintResult CombineResults(
		ConstraintResult? combinedResult,
		ConstraintResult result)
	{
		if (combinedResult == null)
		{
			return result.PrependExpectationText(e => _expectationTextGenerator(_memberAccessor, e));
		}

		return new MappingConstraintResult(combinedResult, result, _expectationTextGenerator, _memberAccessor);
	}

	private static async Task<ConstraintResult> AddContext(ConstraintResult result, TSource? value,
		Func<TSource?, Task<ConstraintResult.Context>>? context)
	{
		if (context is null)
		{
			return result;
		}

		ConstraintResult.Context? usedContext = await context(value);
		return result.WithContext(usedContext.Title, usedContext.Content);
	}

	private static void DefaultExpectationTextGenerator(
		MemberAccessor<TSource, TTarget> memberAccessor,
		StringBuilder expectation)
		=> expectation.Append(memberAccessor);

	private sealed class MappingConstraintResult(
		ConstraintResult left,
		ConstraintResult right,
		Action<MemberAccessor<TSource, TTarget>, StringBuilder>? expectationTextGenerator,
		MemberAccessor<TSource, TTarget> memberAccessor)
		: ConstraintResult(And(left.Outcome, right.Outcome), FurtherProcessingStrategy.Continue)
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
				    left.FurtherProcessingStrategy == FurtherProcessingStrategy.Continue &&
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
