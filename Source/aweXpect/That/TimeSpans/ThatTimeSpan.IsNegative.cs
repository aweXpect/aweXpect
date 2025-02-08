using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatTimeSpan
{
	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<TimeSpan, IThat<TimeSpan>> IsNegative(this IThat<TimeSpan> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new IsNegativeConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not negative.
	/// </summary>
	public static AndOrResult<TimeSpan, IThat<TimeSpan>> IsNotNegative(this IThat<TimeSpan> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new IsNotNegativeConstraint(it)),
			source);

	private readonly struct IsNegativeConstraint(string it)
		: IValueConstraint<TimeSpan>
	{
		public ConstraintResult IsMetBy(TimeSpan actual)
		{
			if (actual < TimeSpan.Zero)
			{
				return new ConstraintResult.Success<TimeSpan>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> "be negative";
	}

	private readonly struct IsNotNegativeConstraint(string it)
		: IValueConstraint<TimeSpan>
	{
		public ConstraintResult IsMetBy(TimeSpan actual)
		{
			if (actual >= TimeSpan.Zero)
			{
				return new ConstraintResult.Success<TimeSpan>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> "not be negative";
	}
}
