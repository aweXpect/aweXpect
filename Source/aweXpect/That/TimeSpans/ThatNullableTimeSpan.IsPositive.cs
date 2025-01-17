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
	public static AndOrResult<TimeSpan?, IExpectSubject<TimeSpan?>> IsPositive(this IExpectSubject<TimeSpan?> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new BePositiveConstraint(it)),
			source);

	/// <summary>
	///     Verifies that the subject is not positive.
	/// </summary>
	public static AndOrResult<TimeSpan?, IExpectSubject<TimeSpan?>> IsNotPositive(
		this IExpectSubject<TimeSpan?> source)
		=> new(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NotBePositiveConstraint(it)),
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