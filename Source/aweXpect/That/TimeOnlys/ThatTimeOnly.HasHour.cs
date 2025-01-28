﻿#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatTimeOnly
{
	/// <summary>
	///     Verifies that the hour of the subject…
	/// </summary>
	public static PropertyResult.Int<TimeOnly> HasHour(this IThat<TimeOnly> source)
		=> new(source, a => a.Hour, "hour");
}
#endif
