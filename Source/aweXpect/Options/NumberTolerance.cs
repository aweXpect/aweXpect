using System;
using Testably.Expectations.Formatting;

namespace Testably.Expectations.Options;

/// <summary>
///     Tolerance for number comparisons.
/// </summary>
public class NumberTolerance<TNumber>(
	Func<TNumber, TNumber, TNumber?, bool> isWithinTolerance)
	where TNumber : struct, IComparable<TNumber>
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
				"Tolerance must be non-negative");
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

	internal bool IsWithinTolerance(TNumber? actual, TNumber? expected)
		=> (expected == null && actual == null) ||
		   (expected != null && actual != null &&
		    isWithinTolerance(actual.Value, expected.Value, Tolerance));
}
