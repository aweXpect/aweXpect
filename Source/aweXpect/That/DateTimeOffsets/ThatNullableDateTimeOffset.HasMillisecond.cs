using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTimeOffset
{
	/// <summary>
	///     Verifies that the millisecond of the subject…
	/// </summary>
	public static PropertyResult.NullableInt<DateTimeOffset?> HasMillisecond(this IThat<DateTimeOffset?> source)
		=> new(source, a => a?.Millisecond, "millisecond");
}
