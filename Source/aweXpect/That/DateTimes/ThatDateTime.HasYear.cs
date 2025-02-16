using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTime
{
	/// <summary>
	///     Verifies that the year of the subject…
	/// </summary>
	public static PropertyResult.Int<DateTime> HasYear(this IThat<DateTime> source)
		=> new(source, a => a.Year, "year");
}
