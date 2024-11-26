using aweXpect.Core.Adapters;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace aweXpect.Adapters;

/// <summary>
///     Implements the MS test framework adapter.
/// </summary>
/// <remarks>
///     <see href="https://github.com/microsoft/testfx" />
/// </remarks>
// ReSharper disable once UnusedMember.Global
internal class MsTestAdapter : ITestFrameworkAdapter
{
	private Assembly? _assembly;

	#region ITestFrameworkAdapter Members

	public bool IsAvailable
	{
		get
		{
			const string prefix = "Microsoft.VisualStudio.TestPlatform.TestFramework,";

			_assembly = AppDomain.CurrentDomain.GetAssemblies()
				.FirstOrDefault(a
					=> a.FullName?.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) == true);

			return _assembly is not null;
		}
	}

	[DoesNotReturn]
	[StackTraceHidden]
	public void Skip(string message)
	{
		Type exceptionType = _assembly?.GetType("Microsoft.VisualStudio.TestTools.UnitTesting.AssertInconclusiveException")
		                     ?? throw new NotSupportedException(
			                     "Failed to create the MSTest skip assertion type");

		throw (Exception)Activator.CreateInstance(exceptionType, message)!;
	}

	[DoesNotReturn]
	[StackTraceHidden]
	public void Throw(string message)
	{
		Type exceptionType = _assembly?.GetType("Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException")
		                     ?? throw new NotSupportedException(
			                     "Failed to create the MSTest fail assertion type");

		throw (Exception)Activator.CreateInstance(exceptionType, message)!;
	}

	#endregion
}
