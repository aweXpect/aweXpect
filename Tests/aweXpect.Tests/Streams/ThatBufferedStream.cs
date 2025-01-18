#if NET8_0_OR_GREATER
using System.IO;

namespace aweXpect.Tests;

public sealed partial class ThatBufferedStream
{
	public static BufferedStream GetBufferedStream(int bufferSize)
		=> new(new MemoryStream(), bufferSize);
}
#endif
