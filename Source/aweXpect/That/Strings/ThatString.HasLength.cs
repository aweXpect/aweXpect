﻿using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatString
{
	/// <summary>
	///     Verifies that the subject has the <paramref name="expected" /> length.
	/// </summary>
	public static AndOrResult<string?, IThat<string?>> HasLength(
		this IThat<string?> source,
		int expected)
	{
		if (expected < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(expected), expected,
				"The expected length must be greater than or equal to zero.");
		}

		return new AndOrResult<string?, IThat<string?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new HaveLengthConstraint(it, expected)),
			source);
	}

	/// <summary>
	///     Verifies that the subject does not have the <paramref name="unexpected" /> length.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> DoesNotHaveLength(
		this IThat<string?> source,
		int unexpected)
	{
		if (unexpected < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(unexpected), unexpected,
				"The unexpected length must be greater than or equal to zero.");
		}

		return new AndOrResult<string, IThat<string?>>(
			source.ThatIs().ExpectationBuilder.AddConstraint(it =>
				new NotHaveLengthConstraint(it, unexpected)),
			source);
	}

	private readonly struct HaveLengthConstraint(string it, int expected)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual is null)
			{
				return new ConstraintResult.Failure<string?>(null, ToString(),
					$"{it} was <null>");
			}

			if (actual.Length != expected)
			{
				return new ConstraintResult.Failure<string?>(actual, ToString(),
					$"{it} did have a length of {actual.Length}:{Environment.NewLine}{Formatter.Format(actual).Indent()}");
			}

			return new ConstraintResult.Success<string?>(actual, ToString());
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"have length {Formatter.Format(expected)}";
	}

	private readonly struct NotHaveLengthConstraint(string it, int unexpected)
		: IValueConstraint<string?>
	{
		/// <inheritdoc />
		public ConstraintResult IsMetBy(string? actual)
		{
			if (actual?.Length == unexpected)
			{
				return new ConstraintResult.Failure<string?>(actual, ToString(),
					$"{it} did:{Environment.NewLine}{Formatter.Format(actual).Indent()}");
			}

			return new ConstraintResult.Success<string?>(actual, ToString());
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"not have length {Formatter.Format(unexpected)}";
	}
}
