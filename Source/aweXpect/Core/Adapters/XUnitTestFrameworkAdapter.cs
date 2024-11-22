using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Testably.Expectations.Core.Adapters;

/// <summary>
///     Implements the XUnit test framework adapter.
/// </summary>
/// <remarks>
///     <see href="https://github.com/xunit/xunit" />
/// </remarks>
// ReSharper disable once UnusedMember.Global
internal class XUnitTestFrameworkAdapter : ITestFrameworkAdapter
{
	private Assembly? _assembly;

	#region ITestFrameworkAdapter Members

	public bool IsAvailable
	{
		get
		{
			try
			{
				// For netfx the assembly is not in AppDomain by default, so we can't just scan AppDomain.CurrentDomain
				_assembly = Assembly.Load(new AssemblyName("xunit.assert"));

				return _assembly is not null;
			}
			catch
			{
				return false;
			}
		}
	}

	[DoesNotReturn]
	[StackTraceHidden]
	public void Skip(string message)
	{
		throw new SkipException($"SKIPPED: {message} (xunit v2 does not support skipping test)");
	}

	[DoesNotReturn]
	[StackTraceHidden]
	public void Throw(string message)
	{
		Type exceptionType = _assembly?.GetType("Xunit.Sdk.XunitException")
		                     ?? throw new NotSupportedException(
			                     "Failed to create the xunit fail assertion type");

		throw (Exception)Activator.CreateInstance(exceptionType, message)!;
	}

	#endregion
}
