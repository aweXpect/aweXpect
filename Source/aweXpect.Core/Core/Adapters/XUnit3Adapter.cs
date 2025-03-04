using System;
using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedType.Global

namespace aweXpect.Core.Adapters;

/// <summary>
///     Implements the XUnit v3 test framework adapter.
/// </summary>
/// <remarks>
///     <see href="https://github.com/xunit/xunit" />
/// </remarks>
[ExcludeFromCodeCoverage]
internal class XUnit3Adapter() : TestFrameworkAdapter(
	"xunit.v3.assert",
	(a, m) => FromType("Xunit.Sdk.XunitException", a, m),
	(_, m) => new SkipException($"$XunitDynamicSkip${m}"),
	(_, m) => new XunitTimeoutException(m))
{
	internal class XUnit3CoreAdapter() : TestFrameworkAdapter(
		"xunit.v3.core",
		(_, m) => new XunitException(m),
		(_, m) => new SkipException($"$XunitDynamicSkip${m}"),
		(_, m) => new XunitTimeoutException(m))
	{
		/// <summary>
		///     Interface is required by xunit v3 to identify an assertion exception.
		/// </summary>
		private interface IAssertionException;

#pragma warning disable S3871 // Exception types should be "public"
		private class XunitException(string message)
			: Exception(message), IAssertionException;
#pragma warning restore S3871 // Exception types should be "public"
	}

#pragma warning disable S3871 // Exception types should be "public"
	private sealed class XunitTimeoutException(string message)
		: InconclusiveException(message), ITestTimeoutException ;
#pragma warning restore S3871 // Exception types should be "public"
	private interface ITestTimeoutException;
}
