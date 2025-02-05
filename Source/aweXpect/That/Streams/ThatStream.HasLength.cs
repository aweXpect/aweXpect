using System;
using System.IO;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStream
{
	/// <summary>
	///     Verifies that the length of the <see cref="Stream" /> subject…
	/// </summary>
#if DEBUG
	public static PropertyResult.Long<Stream?> HasLength(this IThat<Stream?> source)
		=> new(source, a => a?.Length, "length", (value, paramName) =>
#else
	public static PropertyResult.NullableLong<Stream?> HasLength(this IThat<Stream?> source)
		=> new(source, a => a?.Length, "length", (value, paramName) =>
#endif
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException(paramName, value,
					// ReSharper disable once LocalizableElement
					$"The {paramName} length must be greater than or equal to zero.");
			}
		});
}
