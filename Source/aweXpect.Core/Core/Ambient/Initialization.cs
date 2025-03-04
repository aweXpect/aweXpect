using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using aweXpect.Core.Adapters;
using aweXpect.Customization;

namespace aweXpect.Core.Ambient;

internal static class Initialization
{
	public static Lazy<InitializationState> State { get; } = new(Initialize);

	internal static ITestFrameworkAdapter DetectFramework(IEnumerable<Type> types)
	{
		Type frameworkInterface = typeof(ITestFrameworkAdapter);
		foreach (Type frameworkType in types
			         .Where(x => x is { IsClass: true, IsAbstract: false, })
			         .Where(frameworkInterface.IsAssignableFrom))
		{
			try
			{
				ITestFrameworkAdapter? testFramework =
					(ITestFrameworkAdapter?)Activator.CreateInstance(frameworkType);
				if (testFramework?.IsAvailable == true)
				{
					return testFramework;
				}
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(
					$"Could not instantiate test framework '{frameworkType.Name}'!", ex);
			}
		}

		return new FallbackTestFramework();
	}

	private static InitializationState Initialize()
	{
		ITestFrameworkAdapter testFramework = DetectFramework(AppDomain.CurrentDomain.GetAssemblies()
			.Where(assembly => Customize.aweXpect.Reflection().ExcludedAssemblyPrefixes.Get()
				.All(excludedAssemblyPrefix => !assembly.FullName!.StartsWith(excludedAssemblyPrefix)))
			.SelectMany(assembly => assembly.GetTypes().Where(x => !x.IsNestedPrivate)));
		return new InitializationState(testFramework);
	}

	internal class InitializationState(ITestFrameworkAdapter testFramework)
	{
		public ValueFormatter Formatter { get; } = new();

		/// <summary>
		///     Throws a framework-specific exception to indicate a skipped unit test.
		/// </summary>
		[DoesNotReturn]
		[StackTraceHidden]
		public void Skip(string message)
			=> testFramework.Skip(message);

		/// <summary>
		///     Throws a framework-specific exception to indicate a failing unit test.
		/// </summary>
		[DoesNotReturn]
		[StackTraceHidden]
		public void Fail(string message)
			=> testFramework.Fail(message);

		/// <summary>
		///     Throws a framework-specific exception to indicate an inconclusive unit test.
		/// </summary>
		[DoesNotReturn]
		[StackTraceHidden]
		public void Inconclusive(string message)
			=> testFramework.Inconclusive(message);
	}

	private sealed class FallbackTestFramework : ITestFrameworkAdapter
	{
		#region ITestFrameworkAdapter Members

		public bool IsAvailable => false;

		[DoesNotReturn]
		[StackTraceHidden]
		public void Fail(string message)
			=> throw new FailException(message);

		[DoesNotReturn]
		[StackTraceHidden]
		public void Skip(string message)
			=> throw new SkipException(message);

		[DoesNotReturn]
		[StackTraceHidden]
		public void Inconclusive(string message)
			=> throw new InconclusiveException(message);

		#endregion
	}
}
