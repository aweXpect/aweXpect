using System;
using aweXpect.Core;
using aweXpect.Helpers;

namespace aweXpect;

/// <summary>
///     Expectations on <see cref="TimeSpan" /> values.
/// </summary>
public static partial class ThatNullableTimeSpanShould
{
	/// <summary>
	///     Start expectations for current <see cref="TimeSpan" /> <paramref name="subject" />.
	/// </summary>
	public static IThat<TimeSpan?> Should(this IExpectSubject<TimeSpan?> subject)
		=> subject.Should(That.WithoutAction);

	private static bool IsWithinTolerance(TimeSpan? tolerance, TimeSpan? difference)
	{
		if (tolerance == null)
		{
			return difference == TimeSpan.Zero;
		}

		return difference <= tolerance.Value &&
		       difference >= tolerance.Value.Negate();
	}
}
