using System;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatDateTimeOffset
{
	/// <summary>
	///     Verifies that the offset of the subject…
	/// </summary>
	public static PropertyResult.TimeSpan<DateTimeOffset> HasOffset(this IThat<DateTimeOffset> source)
		=> new(source, a => a.Offset, "offset");
}
