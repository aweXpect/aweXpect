using System;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableTimeSpanShould
{
	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<TimeSpan?, IThat<TimeSpan?>> BePositive(this IThat<TimeSpan?> source)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new BePositiveConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not positive.
	/// </summary>
	public static AndOrResult<TimeSpan?, IThat<TimeSpan?>> NotBePositive(
		this IThat<TimeSpan?> source)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotBePositiveConstraint(it)),
			source);

	private readonly struct BePositiveConstraint(string it)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			if (actual > TimeSpan.Zero)
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> "be positive";
	}

	private readonly struct NotBePositiveConstraint(string it)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			if (actual <= TimeSpan.Zero)
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> "not be positive";
	}
}
