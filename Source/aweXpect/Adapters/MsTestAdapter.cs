namespace aweXpect.Adapters;

/// <summary>
///     Implements the MS test framework adapter.
/// </summary>
/// <remarks>
///     <see href="https://github.com/microsoft/testfx" />
/// </remarks>
// ReSharper disable once UnusedMember.Global
internal class MsTestAdapter() : TestFrameworkAdapter(
	"Microsoft.VisualStudio.TestPlatform.TestFramework,",
	(a, m) => FromType("Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException", a, m),
	(a, m) => FromType("Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException", a, m)
);
