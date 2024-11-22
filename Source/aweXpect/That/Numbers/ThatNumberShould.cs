using System;
using Testably.Expectations.Core;
using Testably.Expectations.Core.Constraints;

namespace Testably.Expectations;

/// <summary>
///     Expectations on numeric values.
/// </summary>
public static partial class ThatNumberShould
{
	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<int> Should(this IExpectSubject<int> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<int?> Should(this IExpectSubject<int?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="uint" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<uint> Should(this IExpectSubject<uint> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="uint" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<uint?> Should(this IExpectSubject<uint?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<byte> Should(this IExpectSubject<byte> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<byte?> Should(this IExpectSubject<byte?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<sbyte> Should(this IExpectSubject<sbyte> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<sbyte?> Should(this IExpectSubject<sbyte?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<short> Should(this IExpectSubject<short> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<short?> Should(this IExpectSubject<short?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<ushort> Should(this IExpectSubject<ushort> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<ushort?> Should(this IExpectSubject<ushort?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<long> Should(this IExpectSubject<long> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<long?> Should(this IExpectSubject<long?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<ulong> Should(this IExpectSubject<ulong> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<ulong?> Should(this IExpectSubject<ulong?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<float> Should(this IExpectSubject<float> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<float?> Should(this IExpectSubject<float?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<double> Should(this IExpectSubject<double> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<double?> Should(this IExpectSubject<double?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<decimal> Should(this IExpectSubject<decimal> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<decimal?> Should(this IExpectSubject<decimal?> subject)
		=> subject.Should(ExpectationBuilder.NoAction);

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
