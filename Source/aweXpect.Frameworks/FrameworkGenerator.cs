using Microsoft.CodeAnalysis;

namespace aweXpect.Frameworks;

/// <summary>
///     The <see cref="IIncrementalGenerator" /> for generating test framework adapters.
/// </summary>
[Generator]
public class FrameworkGenerator : IIncrementalGenerator
{
	private static string MsTestAdapter =>
		"""
		using System.Diagnostics;
		using System.Diagnostics.CodeAnalysis;
		using aweXpect.Core.Adapters;

		namespace aweXpect.Frameworks;

		internal class MsTestAdapter() : ITestFrameworkAdapter
		{
			/// <inheritdoc cref="ITestFrameworkAdapter.IsAvailable" />
			public bool IsAvailable { get; } = true;

			/// <inheritdoc cref="ITestFrameworkAdapter.Skip(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Skip(string message)
				=> throw new Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException(message);

			/// <inheritdoc cref="ITestFrameworkAdapter.Fail(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Fail(string message)
				=> throw new Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException(message);

			/// <inheritdoc cref="ITestFrameworkAdapter.Inconclusive(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Inconclusive(string message)
				=> throw new Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException(message);
		}
		""";

	private static string NunitAdapter =>
		"""
		using System.Diagnostics;
		using System.Diagnostics.CodeAnalysis;
		using aweXpect.Core.Adapters;

		namespace aweXpect.Frameworks;

		internal class NunitAdapter() : ITestFrameworkAdapter
		{
			/// <inheritdoc cref="ITestFrameworkAdapter.IsAvailable" />
			public bool IsAvailable { get; } = true;

			/// <inheritdoc cref="ITestFrameworkAdapter.Skip(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Skip(string message)
				=> throw new NUnit.Framework.IgnoreException(message);

			/// <inheritdoc cref="ITestFrameworkAdapter.Fail(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Fail(string message)
				=> throw new NUnit.Framework.AssertionException(message);

			/// <inheritdoc cref="ITestFrameworkAdapter.Inconclusive(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Inconclusive(string message)
				=> throw new NUnit.Framework.InconclusiveException(message);
		}
		""";

	private static string TUnitAdapter =>
		"""
		using System.Diagnostics;
		using System.Diagnostics.CodeAnalysis;
		using aweXpect.Core.Adapters;

		namespace aweXpect.Frameworks;

		internal class TUnitAdapter() : ITestFrameworkAdapter
		{
			/// <inheritdoc cref="ITestFrameworkAdapter.IsAvailable" />
			public bool IsAvailable { get; } = true;

			/// <inheritdoc cref="ITestFrameworkAdapter.Skip(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Skip(string message)
				=> throw new TUnit.Core.Exceptions.SkipTestException(message);

			/// <inheritdoc cref="ITestFrameworkAdapter.Fail(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Fail(string message)
				=> throw new TUnit.Assertions.Exceptions.AssertionException(message);

			/// <inheritdoc cref="ITestFrameworkAdapter.Inconclusive(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Inconclusive(string message)
				=> throw new TUnit.Core.Exceptions.InconclusiveTestException(message, null);
		}
		""";

	private static string Xunit2Adapter =>
		"""
		using System.Diagnostics;
		using System.Diagnostics.CodeAnalysis;
		using aweXpect.Core.Adapters;

		namespace aweXpect.Frameworks;

		internal class Xunit2Adapter() : ITestFrameworkAdapter
		{
			/// <inheritdoc cref="ITestFrameworkAdapter.IsAvailable" />
			public bool IsAvailable { get; } = true;

			/// <inheritdoc cref="ITestFrameworkAdapter.Skip(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Skip(string message)
				=> throw new SkipException($"SKIPPED: {message} (xunit v2 does not support skipping test)");

			/// <inheritdoc cref="ITestFrameworkAdapter.Fail(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Fail(string message)
				=> throw new Xunit.Sdk.XunitException(message);

			/// <inheritdoc cref="ITestFrameworkAdapter.Inconclusive(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Inconclusive(string message)
				=> throw new InconclusiveException(message);
		}
		""";

	private static string Xunit3CoreAdapter =>
		"""
		using System;
		using System.Diagnostics;
		using System.Diagnostics.CodeAnalysis;
		using aweXpect;
		using aweXpect.Core.Adapters;

		namespace aweXpect.Frameworks;

		internal class Xunit3Adapter() : ITestFrameworkAdapter
		{
			/// <inheritdoc cref="ITestFrameworkAdapter.IsAvailable" />
			public bool IsAvailable { get; } = true;

			/// <inheritdoc cref="ITestFrameworkAdapter.Skip(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Skip(string message)
				=> throw new SkipException($"$XunitDynamicSkip${message}");

			/// <inheritdoc cref="ITestFrameworkAdapter.Fail(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Fail(string message)
				=> throw new XunitException(message);

			/// <inheritdoc cref="ITestFrameworkAdapter.Inconclusive(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Inconclusive(string message)
				=> throw new XunitTimeoutException(message);
				
			/// <summary>
			///     Interface is required by xunit v3 to identify an assertion exception.
			/// </summary>
			private interface IAssertionException;

		#pragma warning disable S3871 // Exception types should be "public"
			private sealed class XunitException(string message)
				: Exception(message), IAssertionException;
		#pragma warning restore S3871 // Exception types should be "public"

		#pragma warning disable S3871 // Exception types should be "public"
			private sealed class XunitTimeoutException(string message)
				: InconclusiveException(message), ITestTimeoutException;
		#pragma warning restore S3871 // Exception types should be "public"
			private interface ITestTimeoutException;
		}
		""";

	private static string Xunit3AssertAdapter =>
		"""
		using System;
		using System.Diagnostics;
		using System.Diagnostics.CodeAnalysis;
		using aweXpect;
		using aweXpect.Core.Adapters;

		namespace aweXpect.Frameworks;

		internal class Xunit3Adapter() : ITestFrameworkAdapter
		{
			/// <inheritdoc cref="ITestFrameworkAdapter.IsAvailable" />
			public bool IsAvailable { get; } = true;

			/// <inheritdoc cref="ITestFrameworkAdapter.Skip(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Skip(string message)
				=> throw new SkipException($"$XunitDynamicSkip${message}");

			/// <inheritdoc cref="ITestFrameworkAdapter.Fail(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Fail(string message)
				=> throw new Xunit.Sdk.XunitException(message);

			/// <inheritdoc cref="ITestFrameworkAdapter.Inconclusive(string)" />
			[DoesNotReturn]
			[StackTraceHidden]
			public void Inconclusive(string message)
				=> throw new XunitTimeoutException(message);
				
		#pragma warning disable S3871 // Exception types should be "public"
			private sealed class XunitTimeoutException(string message)
				: InconclusiveException(message), ITestTimeoutException;
		#pragma warning restore S3871 // Exception types should be "public"
			private interface ITestTimeoutException;
		}
		""";

	void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
	{
		IncrementalValueProvider<(bool hasMsTest3, bool hasMsTest4, bool hasNunit, bool hasTUnit, bool hasXunit2, bool hasXunit3Core, bool
			hasXunit3Assert)> settings = context.CompilationProvider
			.Select((c, _) =>
			{
				bool hasMsTest3 =
					c.ReferencedAssemblyNames.Any(x => x.Name == "Microsoft.VisualStudio.TestPlatform.TestFramework");
				bool hasMsTest4 =
					c.ReferencedAssemblyNames.Any(x => x.Name == "MSTest.TestFramework");
				bool hasNunit = c.ReferencedAssemblyNames.Any(x => x.Name == "nunit.framework");
				bool hasTUnit = c.ReferencedAssemblyNames.Any(x => x.Name == "TUnit.Core") &&
				                c.ReferencedAssemblyNames.Any(x => x.Name == "TUnit.Assertions");
				bool hasXunit2 = c.ReferencedAssemblyNames.Any(x => x.Name == "xunit.assert");
				bool hasXunit3Core = c.ReferencedAssemblyNames.Any(x => x.Name == "xunit.v3.core");
				bool hasXunit3Assert = c.ReferencedAssemblyNames.Any(x => x.Name == "xunit.v3.assert");
				return (hasMsTest3, hasMsTest4, hasNunit, hasTUnit, hasXunit2, hasXunit3Core, hasXunit3Assert);
			});

		// Generate the source from the captured values
		context.RegisterSourceOutput(settings, static (spc, opts) =>
		{
			if (opts.hasMsTest3 || opts.hasMsTest4)
			{
				spc.AddSource("MsTest.g.cs", MsTestAdapter);
			}

			if (opts.hasNunit)
			{
				spc.AddSource("Nunit.g.cs", NunitAdapter);
			}

			if (opts.hasTUnit)
			{
				spc.AddSource("TUnit.g.cs", TUnitAdapter);
			}

			if (opts.hasXunit2)
			{
				spc.AddSource("Xunit2.g.cs", Xunit2Adapter);
			}

			if (opts.hasXunit3Assert)
			{
				spc.AddSource("Xunit3.g.cs", Xunit3AssertAdapter);
			}
			else if (opts.hasXunit3Core)
			{
				spc.AddSource("Xunit3.g.cs", Xunit3CoreAdapter);
			}
		});
	}
}
