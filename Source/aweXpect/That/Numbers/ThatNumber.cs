using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on numeric values.
/// </summary>
public static partial class ThatNumber
{
	private readonly struct GenericConstraint<T>(
		string it,
		T? expected,
		Func<T?, string> expectation,
		Func<T, T?, bool> condition,
		Func<T, T?, string, string> failureMessageFactory)
		: IValueConstraint<T>
		where T : struct
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation(expected);
	}

	private readonly struct NullableGenericConstraint<T>(
		string it,
		T? expected,
		Func<T?, string> expectation,
		Func<T?, T?, bool> condition,
		Func<T?, T?, string, string> failureMessageFactory)
		: IValueConstraint<T?>
		where T : struct
	{
		public ConstraintResult IsMetBy(T? actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<T?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation(expected);
	}

	private readonly struct GenericArrayConstraint<T>(
		string it,
		T[] expected,
		Func<T[], string> expectation,
		Func<T, T[], bool> condition,
		Func<T, T[], string, string> failureMessageFactory)
		: IValueConstraint<T>
		where T : struct
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation(expected);
	}

	private readonly struct GenericArrayConstraintWithNullableValues<T>(
		string it,
		T?[] expected,
		Func<T?[], string> expectation,
		Func<T, T?[], bool> condition,
		Func<T, T?[], string, string> failureMessageFactory)
		: IValueConstraint<T>
		where T : struct
	{
		public ConstraintResult IsMetBy(T actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<T>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation(expected);
	}

	private readonly struct NullableGenericArrayConstraint<T>(
		string it,
		T[] expected,
		Func<T[], string> expectation,
		Func<T?, T[], bool> condition,
		Func<T?, T[], string, string> failureMessageFactory)
		: IValueConstraint<T?>
		where T : struct
	{
		public ConstraintResult IsMetBy(T? actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<T?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation(expected);
	}

	private readonly struct NullableGenericArrayConstraintWithNullableValues<T>(
		string it,
		T?[] expected,
		Func<T?[], string> expectation,
		Func<T?, T?[], bool> condition,
		Func<T?, T?[], string, string> failureMessageFactory)
		: IValueConstraint<T?>
		where T : struct
	{
		public ConstraintResult IsMetBy(T? actual)
		{
			if (condition(actual, expected))
			{
				return new ConstraintResult.Success<T?>(actual, ToString());
			}

			return new ConstraintResult.Failure(ToString(),
				failureMessageFactory(actual, expected, it));
		}

		public override string ToString()
			=> expectation(expected);
	}
}
