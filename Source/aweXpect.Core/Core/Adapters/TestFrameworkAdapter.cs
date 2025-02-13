using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace aweXpect.Core.Adapters;

internal abstract class TestFrameworkAdapter(
	string assemblyName,
	Func<Assembly, string, Exception?> failException,
	Func<Assembly, string, Exception?> skipException)
	: ITestFrameworkAdapter
{
	private Assembly? _assembly;

	protected static Exception? FromType(string typeName, Assembly assembly, string message)
	{
		Type? exceptionType = assembly.GetType(typeName);
		if (exceptionType is null)
		{
			return null;
		}

		return (Exception?)Activator.CreateInstance(exceptionType, message);
	}

	#region ITestFrameworkAdapter Members

	/// <inheritdoc cref="ITestFrameworkAdapter.IsAvailable" />
	public bool IsAvailable
	{
		get
		{
			try
			{
				// For netfx the assembly is not in AppDomain by default, so we can't just scan AppDomain.CurrentDomain
				_assembly = AppDomain.CurrentDomain.GetAssemblies()
					.First(a => a.FullName?.StartsWith(assemblyName, StringComparison.OrdinalIgnoreCase) == true);
			}
			catch
			{
				// Ignore any exception while trying to load the assembly
			}

			return _assembly is not null;
		}
	}

	/// <inheritdoc cref="ITestFrameworkAdapter.Skip(string)" />
	[DoesNotReturn]
	[StackTraceHidden]
	public void Skip(string message)
	{
		if (_assembly is null)
		{
			throw new NotSupportedException("Failed to create the skip assertion type");
		}

		throw skipException(_assembly, message)
		      ?? new NotSupportedException("Failed to create the skip assertion type");
	}

	/// <inheritdoc cref="ITestFrameworkAdapter.Throw(string)" />
	[DoesNotReturn]
	[StackTraceHidden]
	public void Throw(string message)
	{
		if (_assembly is null)
		{
			throw new NotSupportedException("Failed to create the fail assertion type");
		}

		throw failException(_assembly, message)
		      ?? new NotSupportedException("Failed to create the fail assertion type");
	}

	#endregion
}
