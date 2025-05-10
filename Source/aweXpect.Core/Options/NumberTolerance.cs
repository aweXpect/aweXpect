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
}
