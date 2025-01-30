using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTime
{
	/// <summary>
	///     Verifies that the hour of the subject…
	/// </summary>
	public static PropertyResult.NullableInt<DateTime?> HasHour(this IThat<DateTime?> source)
		=> new(source, a => a?.Hour, "hour");
}
