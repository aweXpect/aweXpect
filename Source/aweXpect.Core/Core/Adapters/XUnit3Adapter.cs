using System;
using System.Diagnostics.CodeAnalysis;

namespace aweXpect.Adapters;

/// <summary>
///     Implements the XUnit v3 test framework adapter.
/// </summary>
/// <remarks>
///     <see href="https://github.com/xunit/xunit" />
/// </remarks>
// ReSharper disable once UnusedMember.Global
[ExcludeFromCodeCoverage]
internal class XUnit3Adapter() : TestFrameworkAdapter(
	"xunit.v3.assert",
	(a, m) => FromType("Xunit.Sdk.XunitException", a, m),
	(_, m) => new SkipException($"$XunitDynamicSkip${m}"))
{
	internal class XUnit3CoreAdapter() : TestFrameworkAdapter(
		"xunit.v3.core",
		(_, m) => new XunitException(m),
		(_, m) => new SkipException($"$XunitDynamicSkip${m}"))
	{
		/// <summary>
		///     Interface is required by xunit v3 to identify an assertion exception.
		/// </summary>
		private interface IAssertionException;

		private sealed class XunitException(string message)
			: Exception(message), IAssertionException;
	}
}
