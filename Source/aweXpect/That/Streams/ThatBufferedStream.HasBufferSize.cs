#if NET8_0_OR_GREATER
using System.IO;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatBufferedStream
{
	/// <summary>
	///     Verifies that the buffer size of the <see cref="BufferedStream" /> subject…
	/// </summary>
	public static PropertyResult.NullableInt<BufferedStream?> HasBufferSize(this IThat<BufferedStream?> source)
		=> new(source, a => a?.BufferSize, "buffer size");
}
#endif
