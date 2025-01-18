using System;
using aweXpect.Customization;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="TimeSpan" /> values.
/// </summary>
public static partial class ThatTimeSpan
{
	private static bool IsWithinTolerance(TimeSpan? tolerance, TimeSpan difference)
	{
		tolerance ??= Customize.aweXpect.Settings().DefaultTimeComparisonTolerance.Get();

		return difference <= tolerance.Value &&
		       difference >= tolerance.Value.Negate();
	}
}
