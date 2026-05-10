using System;
using aweXpect.Core.Helpers;
#if NET8_0_OR_GREATER
using System.Numerics;
#endif

namespace aweXpect.Options;

/// <summary>
///     Tolerance for number comparisons.
/// </summary>
public class NumberTolerance<TNumber>(
	Func<TNumber, TNumber, TNumber?> calculateDifference)
#if NET8_0_OR_GREATER
	where TNumber : struct, INumber<TNumber>
#else
	where TNumber : struct, IComparable<TNumber>
#endif
{
	/// <summary>
	///     The tolerance to apply on the number comparisons.
	/// </summary>
	public TNumber? Tolerance { get; private set; }

	/// <summary>
	///     Sets the tolerance to apply on the number comparisons.
	/// </summary>
	public void SetTolerance(TNumber tolerance)
	{
		if (tolerance.CompareTo(default) < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(tolerance),
					"Tolerance must be non-negative")
				.LogTrace();
		}

		Tolerance = tolerance;
	}

	/// <inheritdoc />
	public override string ToString()
	{
		if (Tolerance == null)
		{
			return "";
		}

		const char plusMinus = '\u00b1';
		return $" {plusMinus} {Formatter.Format(Tolerance)}";
	}

	/// <summary>
	///     Calculates the difference between the <paramref name="actual" /> number
	///     and the <paramref name="expected" /> number.
	/// </summary>
	public TNumber? CalculateDifference(TNumber? actual, TNumber? expected)
	{
		if (actual == null || expected == null)
		{
			return null;
		}

		return calculateDifference(actual.Value, expected.Value);
	}

	/// <summary>
	///     Verifies if the <paramref name="actual" /> number is within the tolerance to the
	///     <paramref name="expected" /> number.
	/// </summary>
	public bool IsWithinTolerance(TNumber? actual, TNumber? expected)
	{
		try
		{
			checked
			{
				return (actual, expected) switch
				{
					(null, null) => true,
					(_, null) => false,
					(null, _) => false,
#if NET8_0_OR_GREATER
					(_, _) => actual.Equals(expected) || calculateDifference(actual.Value, expected.Value) <= Tolerance,
#else
					(_, _) => actual.Equals(expected) ||
					          calculateDifference(actual.Value, expected.Value)?.CompareTo(Tolerance ?? default) <= 0,
#endif
				};
			}
		}
		catch (OverflowException)
		{
			return false;
		}
	}

	/// <summary>
	///     Verifies if the <paramref name="actual" /> number is greater than the <paramref name="expected" /> number,
	///     accounting for the configured tolerance (which widens the boundary inclusively when set).
	/// </summary>
	public bool IsGreaterThan(TNumber? actual, TNumber? expected)
	{
		if (!IsFiniteValue(actual) || !IsFiniteValue(expected))
		{
			return false;
		}

		int cmp = actual!.Value.CompareTo(expected!.Value);
		if (Tolerance is null)
		{
			return cmp > 0;
		}

		if (cmp >= 0)
		{
			return true;
		}

		TNumber? diff = TryCalculateDifference(actual.Value, expected.Value);
		return diff is not null && diff.Value.CompareTo(Tolerance.Value) <= 0;
	}

	/// <summary>
	///     Verifies if the <paramref name="actual" /> number is greater than or equal to the
	///     <paramref name="expected" /> number, accounting for the configured tolerance.
	/// </summary>
	public bool IsGreaterThanOrEqualTo(TNumber? actual, TNumber? expected)
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

		if (Tolerance is null)
		{
			return false;
		}

		TNumber? diff = TryCalculateDifference(actual.Value, expected.Value);
		return diff is not null && diff.Value.CompareTo(Tolerance.Value) <= 0;
	}

	/// <summary>
	///     Verifies if the <paramref name="actual" /> number is less than the <paramref name="expected" /> number,
	///     accounting for the configured tolerance (which widens the boundary inclusively when set).
	/// </summary>
	public bool IsLessThan(TNumber? actual, TNumber? expected)
	{
		if (!IsFiniteValue(actual) || !IsFiniteValue(expected))
		{
			return false;
		}

		int cmp = actual!.Value.CompareTo(expected!.Value);
		if (Tolerance is null)
		{
			return cmp < 0;
		}

		if (cmp <= 0)
		{
			return true;
		}

		TNumber? diff = TryCalculateDifference(actual.Value, expected.Value);
		return diff is not null && diff.Value.CompareTo(Tolerance.Value) <= 0;
	}

	/// <summary>
	///     Verifies if the <paramref name="actual" /> number is less than or equal to the
	///     <paramref name="expected" /> number, accounting for the configured tolerance.
	/// </summary>
	public bool IsLessThanOrEqualTo(TNumber? actual, TNumber? expected)
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

		if (Tolerance is null)
		{
			return false;
		}

		TNumber? diff = TryCalculateDifference(actual.Value, expected.Value);
		return diff is not null && diff.Value.CompareTo(Tolerance.Value) <= 0;
	}

	private TNumber? TryCalculateDifference(TNumber actual, TNumber expected)
	{
		try
		{
			return calculateDifference(actual, expected);
		}
		catch (OverflowException)
		{
			return null;
		}
	}

	/// <summary>
	///     Verifies if the <paramref name="actual" /> number is in the closed range [<paramref name="minimum" />,
	///     <paramref name="maximum" />], widened by the configured tolerance on both ends.
	/// </summary>
	public bool IsInRange(TNumber? actual, TNumber? minimum, TNumber? maximum)
		=> IsGreaterThanOrEqualTo(actual, minimum) && IsLessThanOrEqualTo(actual, maximum);

	private static bool IsFiniteValue(TNumber? value) => value switch
	{
		null => false,
		double d => !double.IsNaN(d) && !double.IsInfinity(d),
		float f => !float.IsNaN(f) && !float.IsInfinity(f),
#if NET8_0_OR_GREATER
		Half h => !Half.IsNaN(h) && !Half.IsInfinity(h),
#endif
		_ => true,
	};
}
