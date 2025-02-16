using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTime
{
	/// <summary>
	///     Verifies that the millisecond of the subject…
	/// </summary>
	public static PropertyResult.Int<DateTime> HasMillisecond(this IThat<DateTime> source)
		=> new(source, a => a.Millisecond, "millisecond");
}
