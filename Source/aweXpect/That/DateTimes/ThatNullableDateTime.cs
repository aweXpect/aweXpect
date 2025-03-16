using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Customization;
using aweXpect.Options;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="DateTime" />? values.
/// </summary>
public static partial class ThatNullableDateTime
{
	private sealed class ConditionConstraint(
		string it,
		ExpectationGrammars grammars,
		DateTime? expected,
		string expectation,
		Func<DateTime?, DateTime?, TimeSpan, bool> condition,
		Func<DateTime?, DateTime?, string, string> failureMessageFactory,
		TimeTolerance tolerance)
		: ConstraintResult.WithValue<DateTime?>(grammars),
			IValueConstraint<DateTime?>
	{
		public ConstraintResult IsMetBy(DateTime? actual)
		{
			Actual = actual;
			Outcome = condition(actual, expected, tolerance.Tolerance
			                                      ?? Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get())
				? Outcome.Success
				: Outcome.Failure;
			return this;
		}

		protected override void AppendNormalExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(expectation).Append(tolerance);

		protected override void AppendNormalResult(StringBuilder stringBuilder, string? indentation = null)
			=> stringBuilder.Append(failureMessageFactory(Actual, expected, it));

		protected override void AppendNegatedExpectation(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();

		protected override void AppendNegatedResult(StringBuilder stringBuilder, string? indentation = null)
			=> throw new NotImplementedException();
	}
}
