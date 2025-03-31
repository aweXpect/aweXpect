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
		double d => !double.IsNaN(d),
		float f => !float.IsNaN(f),
#if NET8_0_OR_GREATER
		Half h => !Half.IsNaN(h),
#endif
		_ => true
	};

#if NET8_0_OR_GREATER
	private static bool IsWithinTolerance<TNumber>(TNumber actual, TNumber expected, TNumber? tolerance)
		where TNumber : struct, INumber<TNumber>
	{
		if (actual == expected)
		{
			return true;
		}

		if (!IsFinite(actual))
		{
			return !IsFinite(expected);
		}

		return actual < expected
			? expected - actual <= (tolerance ?? default(TNumber))
			: actual - expected <= (tolerance ?? default(TNumber));
	}
#endif
}
