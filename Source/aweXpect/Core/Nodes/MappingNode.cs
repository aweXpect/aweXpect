using System;
using System.Threading;
using System.Threading.Tasks;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.EvaluationContext;

namespace Testably.Expectations.Core.Nodes;

internal class MappingNode<TSource, TTarget> : ExpectationNode
{
	private readonly Func<PropertyAccessor<TSource, TTarget?>, string, string>
		_expectationTextGenerator;

	private readonly PropertyAccessor<TSource, TTarget?> _propertyAccessor;

	public MappingNode(
		PropertyAccessor<TSource, TTarget?> propertyAccessor,
		Func<PropertyAccessor, string, string>? expectationTextGenerator = null)
	{
		_propertyAccessor = propertyAccessor;
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
		if (value is not TSource typedValue)
		{
			throw new InvalidOperationException(
				$"The property type for the actual value in the which node did not match.{Environment.NewLine}Expected {typeof(TSource).Name},{Environment.NewLine}but found {value?.GetType().Name}");
		}

		if (_propertyAccessor.TryAccessProperty(
			typedValue,
			out TTarget? matchingValue))
		{
			ConstraintResult result = await base.IsMetBy(matchingValue, context, cancellationToken);
			return result.UseValue(value);
		}

		throw new InvalidOperationException(
			$"The property type for the which node did not match.{Environment.NewLine}Expected {typeof(TTarget).Name},{Environment.NewLine}but found {matchingValue?.GetType().Name}");
	}

	/// <inheritdoc />
	public override string ToString()
		=> _propertyAccessor + base.ToString();

	internal ConstraintResult CombineResults(
		ConstraintResult? combinedResult,
		ConstraintResult result)
	{
		if (combinedResult == null)
		{
			return result.UpdateExpectationText(
				e => _expectationTextGenerator(_propertyAccessor, e.ExpectationText));
		}

		string combinedExpectation =
			$"{combinedResult.ExpectationText}{_expectationTextGenerator(_propertyAccessor, result.ExpectationText)}";

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
		PropertyAccessor<TSource, TTarget?> propertyAccessor,
		string expectationText)
	{
		return propertyAccessor + expectationText;
	}
}
