using System;
using aweXpect.Core;
using aweXpect.Helpers;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTime
{
	/// <summary>
	///     Verifies that the year of the subject…
	/// </summary>
	public static PropertyResult.NullableInt<DateTime?> HasYear(this IThat<DateTime?> source)
		=> new(source, a => a?.Year, "year");
}
