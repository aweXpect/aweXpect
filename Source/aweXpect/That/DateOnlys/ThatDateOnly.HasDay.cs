﻿#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateOnly
{
	/// <summary>
	///     Verifies that the day of the subject…
	/// </summary>
	public static PropertyResult.Int<DateOnly> HasDay(this IThat<DateOnly> source)
		=> new(source, a => a.Day, "day");
}
#endif
