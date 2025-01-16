using System;
using aweXpect.Core;
using aweXpect.Core.Constraints;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on numeric values.
/// </summary>
public static partial class ThatNumberShould
{
	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<int> Should(this IExpectSubject<int> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<int?> Should(this IExpectSubject<int?> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="uint" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<uint> Should(this IExpectSubject<uint> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="uint" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<uint?> Should(this IExpectSubject<uint?> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<byte> Should(this IExpectSubject<byte> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<byte?> Should(this IExpectSubject<byte?> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<sbyte> Should(this IExpectSubject<sbyte> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<sbyte?> Should(this IExpectSubject<sbyte?> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<short> Should(this IExpectSubject<short> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<short?> Should(this IExpectSubject<short?> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<ushort> Should(this IExpectSubject<ushort> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<ushort?> Should(this IExpectSubject<ushort?> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<long> Should(this IExpectSubject<long> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<long?> Should(this IExpectSubject<long?> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<ulong> Should(this IExpectSubject<ulong> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<ulong?> Should(this IExpectSubject<ulong?> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<float> Should(this IExpectSubject<float> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<float?> Should(this IExpectSubject<float?> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<double> Should(this IExpectSubject<double> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<double?> Should(this IExpectSubject<double?> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<decimal> Should(this IExpectSubject<decimal> subject)
		=> subject.Should(That.WithoutAction);

	/// <summary>
	///     Start expectations for the current <see cref="int" /> <paramref name="subject" />.
	/// </summary>
	public static IThatShould<decimal?> Should(this IExpectSubject<decimal?> subject)
		=> subject.Should(That.WithoutAction);

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
