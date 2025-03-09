#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="DateOnly" /> values.
/// </summary>
public static partial class ThatDateOnly
{
	private class ConditionConstraintWithTolerance(
		string it,
		ExpectationGrammars grammars,
		DateOnly? expected,
		Func<DateOnly?, TimeTolerance, string> expectation,
		Func<DateOnly, DateOnly?, TimeSpan, bool> condition,
		Func<DateOnly, DateOnly?, string, string> failureMessageFactory,
		TimeTolerance tolerance)
		: ConstraintResult.WithValue<DateOnly>(grammars),
			IValueConstraint<DateOnly>
	{
		public ConstraintResult IsMetBy(DateOnly actual)
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
