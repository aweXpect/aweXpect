using System;
using aweXpect.Options;
#if NET8_0_OR_GREATER
using System.Numerics;
#endif

namespace aweXpect.Helpers;

internal static class NumberToleranceExtensions
{
	public static bool IsGreaterThan<TNumber>(
		this NumberTolerance<TNumber> tolerance, TNumber? actual, TNumber? expected)
#if NET8_0_OR_GREATER
		where TNumber : struct, INumber<TNumber>
#else
		where TNumber : struct, IComparable<TNumber>
#endif
	{
		if (!IsFiniteValue(actual) || !IsFiniteValue(expected))
		{
			return false;
		}

		int cmp = actual!.Value.CompareTo(expected!.Value);
		if (tolerance.Tolerance is null)
		{
			return cmp > 0;
		}

		if (cmp >= 0)
		{
			return true;
		}

		TNumber? diff = TryCalculateDifference(tolerance, actual.Value, expected.Value);
		return diff is not null && diff.Value.CompareTo(tolerance.Tolerance.Value) <= 0;
	}

	public static bool IsGreaterThanOrEqualTo<TNumber>(
		this NumberTolerance<TNumber> tolerance, TNumber? actual, TNumber? expected)
#if NET8_0_OR_GREATER
		where TNumber : struct, INumber<TNumber>
#else
		where TNumber : struct, IComparable<TNumber>
#endif
	{
		if (!IsFiniteValue(actual) || !IsFiniteValue(expected))
		{
			return false;
		}

		int cmp = actual!.Value.CompareTo(expected!.Value);
		if (cmp >= 0)
		{
			return true;
		}

		if (tolerance.Tolerance is null)
		{
			return false;
		}

		TNumber? diff = TryCalculateDifference(tolerance, actual.Value, expected.Value);
		return diff is not null && diff.Value.CompareTo(tolerance.Tolerance.Value) <= 0;
	}

	public static bool IsInRange<TNumber>(
		this NumberTolerance<TNumber> tolerance, TNumber? actual, TNumber? minimum, TNumber? maximum)
#if NET8_0_OR_GREATER
		where TNumber : struct, INumber<TNumber>
#else
		where TNumber : struct, IComparable<TNumber>
#endif
		=> tolerance.IsGreaterThanOrEqualTo(actual, minimum) && tolerance.IsLessThanOrEqualTo(actual, maximum);

	public static bool IsLessThan<TNumber>(
		this NumberTolerance<TNumber> tolerance, TNumber? actual, TNumber? expected)
#if NET8_0_OR_GREATER
		where TNumber : struct, INumber<TNumber>
#else
		where TNumber : struct, IComparable<TNumber>
#endif
	{
		if (!IsFiniteValue(actual) || !IsFiniteValue(expected))
		{
			return false;
		}

		int cmp = actual!.Value.CompareTo(expected!.Value);
		if (tolerance.Tolerance is null)
		{
			return cmp < 0;
		}

		if (cmp <= 0)
		{
			return true;
		}

		TNumber? diff = TryCalculateDifference(tolerance, actual.Value, expected.Value);
		return diff is not null && diff.Value.CompareTo(tolerance.Tolerance.Value) <= 0;
	}

	public static bool IsLessThanOrEqualTo<TNumber>(
		this NumberTolerance<TNumber> tolerance, TNumber? actual, TNumber? expected)
#if NET8_0_OR_GREATER
		where TNumber : struct, INumber<TNumber>
#else
		where TNumber : struct, IComparable<TNumber>
#endif
	{
		if (!IsFiniteValue(actual) || !IsFiniteValue(expected))
		{
			return false;
		}

		int cmp = actual!.Value.CompareTo(expected!.Value);
		if (cmp <= 0)
		{
			return true;
		}

		if (tolerance.Tolerance is null)
		{
			return false;
		}

		TNumber? diff = TryCalculateDifference(tolerance, actual.Value, expected.Value);
		return diff is not null && diff.Value.CompareTo(tolerance.Tolerance.Value) <= 0;
	}

	private static bool IsFiniteValue<TNumber>(TNumber? value)
#if NET8_0_OR_GREATER
		where TNumber : struct, INumber<TNumber>
#else
		where TNumber : struct, IComparable<TNumber>
#endif
		=> value switch
		{
			null => false,
			double d => !double.IsNaN(d) && !double.IsInfinity(d),
			float f => !float.IsNaN(f) && !float.IsInfinity(f),
#if NET8_0_OR_GREATER
			Half h => !Half.IsNaN(h) && !Half.IsInfinity(h),
#endif
			_ => true,
		};

	private static TNumber? TryCalculateDifference<TNumber>(
		NumberTolerance<TNumber> tolerance, TNumber actual, TNumber expected)
#if NET8_0_OR_GREATER
		where TNumber : struct, INumber<TNumber>
#else
		where TNumber : struct, IComparable<TNumber>
#endif
	{
		try
		{
			return tolerance.CalculateDifference(actual, expected);
		}
		catch (OverflowException)
		{
			return null;
		}
	}
}
