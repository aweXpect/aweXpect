using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Testably.Expectations.Core.Adapters;

/// <summary>
///     Implements the NUnit test framework adapter.
/// </summary>
/// <remarks>
///     <see href="https://github.com/nunit/nunit" />
/// </remarks>
// ReSharper disable once UnusedMember.Global
internal class NUnitTestFrameworkAdapter : ITestFrameworkAdapter
{
	private Assembly? _assembly;

	#region ITestFrameworkAdapter Members

	public bool IsAvailable
	{
		get
		{
			const string prefix = "nunit.framework,";

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
		Type exceptionType = _assembly?.GetType("NUnit.Framework.IgnoreException")
		                     ?? throw new NotSupportedException(
			                     "Failed to create the NUnit skip assertion type");

		throw (Exception)Activator.CreateInstance(exceptionType, message)!;
	}

	[DoesNotReturn]
	[StackTraceHidden]
	public void Throw(string message)
	{
		Type exceptionType = _assembly?.GetType("NUnit.Framework.AssertionException")
		                     ?? throw new NotSupportedException(
			                     "Failed to create the NUnit fail assertion type");

		throw (Exception)Activator.CreateInstance(exceptionType, message)!;
	}

	#endregion
}
