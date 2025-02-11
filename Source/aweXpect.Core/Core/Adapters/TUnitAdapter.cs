﻿#if NET8_0_OR_GREATER
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using aweXpect.Core.Adapters;

namespace aweXpect.Adapters;

/// <summary>
///     Implements the TUnit test framework adapter.
/// </summary>
/// <remarks>
///     <see href="https://github.com/thomhurst/TUnit" />
/// </remarks>
// ReSharper disable once UnusedMember.Global
internal class TUnitAdapter : ITestFrameworkAdapter
{
	private Assembly? _assertionsAssembly;
	private Assembly? _coreAssembly;

	#region ITestFrameworkAdapter Members

	/// <inheritdoc cref="ITestFrameworkAdapter.IsAvailable" />
	public bool IsAvailable
	{
		get
		{
			try
			{
				// For netfx the assembly is not in AppDomain by default, so we can't just scan AppDomain.CurrentDomain
				_coreAssembly = Assembly.Load(new AssemblyName("TUnit.Core"));
				_assertionsAssembly = Assembly.Load(new AssemblyName("TUnit.Assertions"));
				return _coreAssembly is not null;
			}
			catch
			{
				return false;
			}
		}
	}

	/// <inheritdoc cref="ITestFrameworkAdapter.Skip(string)" />
	[DoesNotReturn]
	[StackTraceHidden]
	public void Skip(string message)
	{
		Type exceptionType = _coreAssembly?.GetType("TUnit.Core.Exceptions.SkipTestException")
		                     ?? throw new NotSupportedException(
			                     "Failed to create the TUnit skip assertion type");

		throw (Exception)Activator.CreateInstance(exceptionType, message)!;
	}

	/// <inheritdoc cref="ITestFrameworkAdapter.Throw(string)" />
	[DoesNotReturn]
	[StackTraceHidden]
	public void Throw(string message)
	{
		if (_assertionsAssembly == null)
		{
			// When TUnit is used without its assertions library, use the default exception.
			throw new FailException(message);
		}

		Type exceptionType =
			_assertionsAssembly.GetType("TUnit.Assertions.Exceptions.AssertionException")
			?? throw new NotSupportedException(
				"Failed to create the TUnit fail assertion type");

		throw (Exception)Activator.CreateInstance(exceptionType, message)!;
	}

	#endregion
}
#endif
