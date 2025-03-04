// ReSharper disable UnusedType.Global

namespace aweXpect.Core.Adapters;

/// <summary>
///     Implements the XUnit v2 test framework adapter.
/// </summary>
/// <remarks>
///     <see href="https://github.com/xunit/xunit" />
/// </remarks>
internal class XUnit2Adapter() : TestFrameworkAdapter(
	"xunit.assert",
	(a, m) => FromType("Xunit.Sdk.XunitException", a, m),
	(_, m) => new SkipException($"SKIPPED: {m} (xunit v2 does not support skipping test)"),
	(_, m) => new InconclusiveException(m)
);
