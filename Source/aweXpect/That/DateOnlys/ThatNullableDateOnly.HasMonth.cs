#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateOnly
{
	/// <summary>
	///     Verifies that the month of the subject…
	/// </summary>
	public static PropertyResult.NullableInt<DateOnly?> HasMonth(this IThat<DateOnly?> source)
		=> new(source, a => a?.Month, "month");
}
#endif
