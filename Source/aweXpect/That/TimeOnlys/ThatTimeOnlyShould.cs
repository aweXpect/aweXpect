#if NET6_0_OR_GREATER
using System;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Options;

namespace Testably.Expectations;

/// <summary>
///     Expectations on <see cref="TimeOnly" /> values.
/// </summary>
public static partial class ThatTimeOnlyShould
{
	/// <summary>
	///     Start expectations for current <see cref="TimeOnly" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<TimeOnly> Should(this IExpectSubject<TimeOnly> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	private readonly struct ConditionConstraint(
		string it,
		TimeOnly expected,
		Func<TimeOnly, TimeOnly, bool> condition,
		string expectation) : IValueConstraint<TimeOnly>
	{
		public ConstraintResult IsMetBy(TimeOnly actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<TimeOnly>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> expectation;
	}

	private readonly struct PropertyConstraint<T>(
		string it,
		T expected,
		Func<TimeOnly, T, bool> condition,
		string expectation) : IValueConstraint<TimeOnly>
	{
		public ConstraintResult IsMetBy(TimeOnly actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<TimeOnly>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(), $"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> expectation;
	}

	private readonly struct ConditionConstraintWithTolerance(
		string it,
		TimeOnly? expected,
		Func<TimeOnly?, TimeTolerance, string> expectation,
		Func<TimeOnly, TimeOnly?, TimeSpan, bool> condition,
		Func<TimeOnly, TimeOnly?, string, string> failureMessageFactory,
		TimeTolerance tolerance)
		: IValueConstraint<TimeOnly>
	{
		public ConstraintResult IsMetBy(TimeOnly actual)
		{
			if (expected is null)
			{
				return new ConstraintResult.Failure(ToString(),
					failureMessageFactory(actual, expected, it));
			}

			if (condition(actual, expected.Value, tolerance.Tolerance ?? TimeSpan.Zero))
			{
				return new ConstraintResult.Success<TimeOnly>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected.Value, it));
		}

		public override string ToString()
			=> expectation(expected, tolerance);
	}
}
#endif
