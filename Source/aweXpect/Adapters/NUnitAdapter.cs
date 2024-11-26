namespace aweXpect.Adapters;

/// <summary>
///     Implements the NUnit test framework adapter.
/// </summary>
/// <remarks>
///     <see href="https://github.com/nunit/nunit" />
/// </remarks>
// ReSharper disable once UnusedMember.Global
internal class NUnitAdapter() : TestFrameworkAdapter(
	"nunit.framework,",
	(a, m) => FromType("NUnit.Framework.AssertionException", a, m),
	(a, m) => FromType("NUnit.Framework.IgnoreException", a, m)
);
