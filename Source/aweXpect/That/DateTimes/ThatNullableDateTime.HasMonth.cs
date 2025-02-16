﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableDateTime
{
	/// <summary>
	///     Verifies that the month of the subject…
	/// </summary>
	public static PropertyResult.Int<DateTime?> HasMonth(this IThat<DateTime?> source)
		=> new(source, a => a?.Month, "month");
}
