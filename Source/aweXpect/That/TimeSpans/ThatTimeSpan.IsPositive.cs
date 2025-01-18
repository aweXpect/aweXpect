using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatTimeSpan
{
	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<TimeSpan, IThat<TimeSpan>> IsPositive(this IThat<TimeSpan> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BePositiveConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not positive.
	/// </summary>
	public static AndOrResult<TimeSpan, IThat<TimeSpan>> IsNotPositive(this IThat<TimeSpan> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NotBePositiveConstraint(it)),
			source);

	private readonly struct BePositiveConstraint(string it)
		: IValueConstraint<TimeSpan>
	{
		public ConstraintResult IsMetBy(TimeSpan actual)
		{
			if (actual > TimeSpan.Zero)
			{
				return new ConstraintResult.Success<TimeSpan>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> "be positive";
	}

	private readonly struct NotBePositiveConstraint(string it)
		: IValueConstraint<TimeSpan>
	{
		public ConstraintResult IsMetBy(TimeSpan actual)
		{
			if (actual <= TimeSpan.Zero)
			{
				return new ConstraintResult.Success<TimeSpan>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				$"{it} was {Formatter.Format(actual)}");
		}

		public override string ToString()
			=> "not be positive";
	}
}
