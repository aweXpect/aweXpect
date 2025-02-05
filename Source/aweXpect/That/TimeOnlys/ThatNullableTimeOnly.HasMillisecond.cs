﻿#if NET8_0_OR_GREATER
using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatNullableTimeOnly
{
	/// <summary>
	///     Verifies that the millisecond of the subject…
	/// </summary>
	public static PropertyResult.Int<TimeOnly?> HasMillisecond(this IThat<TimeOnly?> source)
		=> new(source, a => a?.Millisecond, "millisecond");
}
#endif
