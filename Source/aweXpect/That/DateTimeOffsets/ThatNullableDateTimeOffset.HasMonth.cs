using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTimeOffset
{
	/// <summary>
	///     Verifies that the month of the subject…
	/// </summary>
	public static PropertyResult.NullableInt<DateTimeOffset?> HasMonth(this IThat<DateTimeOffset?> source)
		=> new(source, a => a?.Month, "month");
}
