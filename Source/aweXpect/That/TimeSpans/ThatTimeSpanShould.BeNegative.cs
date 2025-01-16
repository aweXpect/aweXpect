using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatTimeSpanShould
{
	/// <summary>
	///     Verifies that the subject is negative.
	/// </summary>
	public static AndOrResult<TimeSpan, IThatShould<TimeSpan>> BeNegative(this IThatShould<TimeSpan> source)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new BeNegativeConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not negative.
	/// </summary>
	public static AndOrResult<TimeSpan, IThatShould<TimeSpan>> NotBeNegative(this IThatShould<TimeSpan> source)
		=> new(
			source.ExpectationBuilder.AddConstraint(it
				=> new NotBeNegativeConstraint(it)),
			source);

	private readonly struct BeNegativeConstraint(string it)
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

	private readonly struct NotBeNegativeConstraint(string it)
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
