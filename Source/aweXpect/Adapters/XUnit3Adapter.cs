using aweXpect.Core.Adapters;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace aweXpect.Adapters;

/// <summary>
///     Implements the XUnit v3 test framework adapter.
/// </summary>
/// <remarks>
///     <see href="https://github.com/xunit/xunit" />
/// </remarks>
// ReSharper disable once UnusedMember.Global
internal class XUnit3Adapter : ITestFrameworkAdapter
{
	private Assembly? _assembly;

	internal class XUnit3CoreAdapter : ITestFrameworkAdapter
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
					_assembly = Assembly.Load(new AssemblyName("xunit.v3.core"));

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
		public void Skip(string message) => throw new SkipException($"$XunitDynamicSkip${message}");

		[DoesNotReturn]
		[StackTraceHidden]
		public void Throw(string message) => throw new XunitException(message);

		private interface IAssertionException
		{
		}

		private class XunitException(string message) : Exception(message), IAssertionException
		{
		}

		#endregion
	}

	#region ITestFrameworkAdapter Members

	public bool IsAvailable
	{
		get
		{
			try
			{
				// For netfx the assembly is not in AppDomain by default, so we can't just scan AppDomain.CurrentDomain
				_assembly = Assembly.Load(new AssemblyName("xunit.v3.assert"));

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
	public void Skip(string message) => throw new SkipException($"$XunitDynamicSkip${message}");

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
