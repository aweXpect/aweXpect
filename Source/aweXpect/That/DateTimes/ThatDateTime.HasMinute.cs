﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTime
{
	/// <summary>
	///     Verifies that the minute of the subject…
	/// </summary>
	public static PropertyResult.Int<DateTime> HasMinute(this IThat<DateTime> source)
		=> new(source, a => a.Minute, "minute");
}
