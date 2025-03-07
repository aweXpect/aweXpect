﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableTimeSpan
{
	/// <summary>
	///     Verifies that the subject is positive.
	/// </summary>
	public static AndOrResult<TimeSpan?, IThat<TimeSpan?>> IsPositive(this IThat<TimeSpan?> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new IsPositiveConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not positive.
	/// </summary>
	public static AndOrResult<TimeSpan?, IThat<TimeSpan?>> IsNotPositive(
		this IThat<TimeSpan?> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint((it, grammar) =>
				new IsNotPositiveConstraint(it)),
			source);

	private readonly struct IsPositiveConstraint(string it)
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
			=> "is positive";
	}

	private readonly struct IsNotPositiveConstraint(string it)
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
			=> "is not positive";
	}
}
