using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTimeOffset
{
	/// <summary>
	///     Verifies that the hour of the subject…
	/// </summary>
	public static PropertyResult.Int<DateTimeOffset> HasHour(this IThat<DateTimeOffset> source)
		=> new(source, a => a.Hour, "hour");
}
