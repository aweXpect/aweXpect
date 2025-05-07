using System.Diagnostics.CodeAnalysis;
#if NET8_0_OR_GREATER
using System;
#endif

namespace aweXpect;

/// <summary>
///     Expectations on numeric values.
/// </summary>
public static partial class ThatNumber
{
	private static bool IsFinite<T>([NotNullWhen(true)] T? value) => value switch
	{
		null => false,
		double d => !double.IsNaN(d) && !double.IsInfinity(d),
		float f => !float.IsNaN(f) && !float.IsInfinity(f),
#if NET8_0_OR_GREATER
		Half h => !Half.IsNaN(h) && !Half.IsInfinity(h),
#endif
		_ => true
	};

#if NET8_0_OR_GREATER
	private static TNumber? CalculateDifference<TNumber>(TNumber actual, TNumber expected)
		where TNumber : struct, INumber<TNumber>
	{
		if (actual == expected)
		{
			return default(TNumber);
		}

		if (!IsFinite(actual))
		{
			return !IsFinite(expected) ? default(TNumber) : null;
		}

		try
		{
			checked
			{
				return actual < expected
					? expected - actual
					: actual - expected;
			}
		}
		catch (OverflowException)
		{
			return null;
		}
	}
#endif
}
