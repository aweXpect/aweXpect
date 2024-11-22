using System;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatNullableTimeSpanShould
{
	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<TimeSpan?, IThat<TimeSpan?>> BeNegative(this IThat<TimeSpan?> source)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeNegativeConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not negative.
	/// </summary>
	public static AndOrResult<TimeSpan?, IThat<TimeSpan?>> NotBeNegative(
		this IThat<TimeSpan?> source)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotBeNegativeConstraint(it)),
			source);

	private readonly struct BeNegativeConstraint(string it)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			if (actual < TimeSpan.Zero)
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> "be negative";
	}

	private readonly struct NotBeNegativeConstraint(string it)
		: IValueConstraint<TimeSpan?>
	{
		public ConstraintResult IsMetBy(TimeSpan? actual)
		{
			if (actual >= TimeSpan.Zero)
			{
				return new ConstraintResult.Success<TimeSpan?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> "not be negative";
	}
}
