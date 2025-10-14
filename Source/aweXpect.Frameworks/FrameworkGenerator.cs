using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace aweXpect.Frameworks;

/// <summary>
///     The <see cref="IIncrementalGenerator" /> for the registration of mocks.
/// </summary>
[Generator]
public class FrameworkGenerator : IIncrementalGenerator
{
	void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext context)
	{
		var settings = context.CompilationProvider
			.Select((c, _)  =>
			{
				var hasMsTest = c.ReferencedAssemblyNames.Any(x => x.Name == "Microsoft.VisualStudio.TestPlatform.TestFramework");
				var hasNunit = c.ReferencedAssemblyNames.Any(x => x.Name == "nunit.framework");
				var hasTUnit = c.ReferencedAssemblyNames.Any(x => x.Name == "TUnit.Core") &&
				               c.ReferencedAssemblyNames.Any(x => x.Name == "TUnit.Assertions");
				var hasXunit2 = c.ReferencedAssemblyNames.Any(x => x.Name == "xunit.assert");
				var hasXunit3Core = c.ReferencedAssemblyNames.Any(x => x.Name == "xunit.v3.core");
				var hasXunit3Assert = c.ReferencedAssemblyNames.Any(x => x.Name == "xunit.v3.assert");
				return (hasMsTest, hasNunit, hasTUnit, hasXunit2, hasXunit3Core, hasXunit3Assert);
			});
		
		// Generate the source from the captured values
		context.RegisterSourceOutput(settings, static (spc, opts) =>
		{
			if (opts.hasMsTest)
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

	private static string MsTestAdapter =>
		"""
		using System.Diagnostics;
		using System.Diagnostics.CodeAnalysis;
		using aweXpect.Core.Adapters;

		namespace aweXpect.Frameworks;

		internal class MsTest() : ITestFrameworkAdapter
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

		internal class Nunit2() : ITestFrameworkAdapter
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

		internal class TUnit() : ITestFrameworkAdapter
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
				=> throw new TUnit.Core.Exceptions.InconclusiveTestException(message);
		}
		""";

	private static string Xunit2Adapter =>
		"""
		using System.Diagnostics;
		using System.Diagnostics.CodeAnalysis;
		using aweXpect.Core.Adapters;

		namespace aweXpect.Frameworks;

		internal class Xunit2() : ITestFrameworkAdapter
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

		internal class Xunit3() : ITestFrameworkAdapter
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

		internal class Xunit3() : ITestFrameworkAdapter
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
}
