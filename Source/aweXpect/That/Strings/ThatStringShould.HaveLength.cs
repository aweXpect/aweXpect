using System;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;
using Testably.Expectations.Core.Helpers;
using Testably.Expectations.Results;

namespace Testably.Expectations;

public static partial class ThatStringShould
{
	/// <summary>
	///     Verifies that the subject has the <paramref name="expected" /> length.
	/// </summary>
	public static AndOrResult<string?, IThat<string?>> HaveLength(
		this IThat<string?> source,
		int expected)
	{
		if (expected < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(expected), expected,
				"The expected length must be greater than or equal to zero.");
		}

		return new AndOrResult<string?, IThat<string?>>(source.ExpectationBuilder.AddConstraint(it
				=> new HaveLengthConstraint(it, expected)),
			source);
	}

	/// <summary>
	///     Verifies that the subject does not have the <paramref name="unexpected" /> length.
	/// </summary>
	public static AndOrResult<string, IThat<string?>> NotHaveLength(
		this IThat<string?> source,
		int unexpected)
	{
		if (unexpected < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(unexpected), unexpected,
				"The unexpected length must be greater than or equal to zero.");
		}

		return new AndOrResult<string, IThat<string?>>(source.ExpectationBuilder.AddConstraint(it
				=> new NotHaveLengthConstraint(it, unexpected)),
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
					$"{it} did have a length of {actual.Length}:{Environment.NewLine}{StringExtensions.Indent(Formatter.Format(actual))}");
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
					$"{it} did:{Environment.NewLine}{StringExtensions.Indent(Formatter.Format(actual))}");
			}

			return new ConstraintResult.Success<string?>(actual, ToString());
		}

		/// <inheritdoc />
		public override string ToString()
			=> $"not have length {Formatter.Format(unexpected)}";
	}
}
