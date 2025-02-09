﻿using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTime
{
	/// <summary>
	///     Verifies that the second of the subject…
	/// </summary>
	public static PropertyResult.Int<DateTime> HasSecond(this IThat<DateTime> source)
		=> new(source, a => a.Second, "second");
}
