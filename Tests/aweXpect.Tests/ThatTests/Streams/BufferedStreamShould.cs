#if NET6_0_OR_GREATER
using System.IO;

namespace Testably.Expectations.Tests.ThatTests.Streams;

public sealed partial class BufferedStreamShould
{
	public static BufferedStream GetBufferedStream(int bufferSize)
		=> new(new MemoryStream(), bufferSize);
}
#endif
