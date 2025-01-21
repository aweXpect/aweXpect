using System;
using System.Threading;
using System.Threading.Tasks;
using aweXpect.Core.Constraints;
using aweXpect.Core.EvaluationContext;
using aweXpect.Core.Sources;

namespace aweXpect.Core.Nodes;

internal class MappingNode<TSource, TTarget> : ExpectationNode
{
	private readonly Func<MemberAccessor<TSource, TTarget?>, string, string>
		_expectationTextGenerator;

	private readonly MemberAccessor<TSource, TTarget?> _memberAccessor;

	public MappingNode(
		MemberAccessor<TSource, TTarget?> memberAccessor,
		Func<MemberAccessor, string, string>? expectationTextGenerator = null)
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

		if (_memberAccessor.TryAccessMember(
			    typedValue,
			    out TTarget? matchingValue))
		{
			ConstraintResult result = await base.IsMetBy(matchingValue, context, cancellationToken);
			return result.UseValue(value);
		}

		throw new InvalidOperationException(
			$"The member type for the which node did not match.{Environment.NewLine}Expected: {Formatter.Format(typeof(TTarget))},{Environment.NewLine}   Found: {Formatter.Format(matchingValue?.GetType())}");
	}

	/// <inheritdoc />
	public override string ToString()
		=> _memberAccessor + base.ToString();

	internal ConstraintResult CombineResults(
		ConstraintResult? combinedResult,
		ConstraintResult result)
	{
		if (combinedResult == null)
		{
			return result.UpdateExpectationText(
				e => _expectationTextGenerator(_memberAccessor, e.ExpectationText));
		}

		string combinedExpectation =
			$"{combinedResult.ExpectationText}{_expectationTextGenerator(_memberAccessor, result.ExpectationText)}";

		if (combinedResult is ConstraintResult.Failure leftFailure &&
		    result is ConstraintResult.Failure rightFailure)
		{
			return leftFailure.CombineWith(
				combinedExpectation,
				CombineResultTexts(leftFailure.ResultText, rightFailure.ResultText));
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

	private static string CombineResultTexts(string leftResultText, string rightResultText)
	{
		if (leftResultText == rightResultText)
		{
			return leftResultText;
		}

		return $"{leftResultText} and {rightResultText}";
	}

	private static string DefaultExpectationTextGenerator(
		MemberAccessor<TSource, TTarget?> memberAccessor,
		string expectationText)
		=> memberAccessor + expectationText;
}
