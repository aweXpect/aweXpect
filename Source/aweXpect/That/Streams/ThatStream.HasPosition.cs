using System.IO;
using aweXpect.Core;
using aweXpect.Results;

namespace aweXpect;

public static partial class ThatStream
{
	/// <summary>
	///     Verifies that the position of the <see cref="Stream" /> subject…
	/// </summary>
	public static PropertyResult.NullableLong<Stream?> HasPosition(this IThat<Stream?> source)
		=> new(source, a => a?.Position, "position");
}
