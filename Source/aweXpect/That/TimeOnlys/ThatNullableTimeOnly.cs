#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="TimeOnly" />? values.
/// </summary>
public static partial class ThatNullableTimeOnly
{
	private class ConditionConstraintWithTolerance(
		string it,
		ExpectationGrammars grammars,
		TimeOnly? expected,
		Func<TimeOnly?, TimeTolerance, string> expectation,
		Func<TimeOnly?, TimeOnly?, TimeSpan, bool> condition,
		Func<TimeOnly?, TimeOnly?, string, string> failureMessageFactory,
		TimeTolerance tolerance)
		: ConstraintResult.WithValue<TimeOnly?>(grammars),
			IValueConstraint<TimeOnly?>
	{
		public ConstraintResult IsMetBy(TimeOnly? actual)
		{
			Actual = actual;
			Outcome = condition(actual, expected, tolerance.Tolerance
			                                      ?? Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get())
				? Outcome.Success : Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(expectation(expected, tolerance));
		}

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
		{
			stringBuilder.Append(failureMessageFactory(Actual, expected, it));
		}

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();
	}
}
#endif
