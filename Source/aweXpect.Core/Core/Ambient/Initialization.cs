using aweXpect.Core.Adapters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace aweXpect.Core.Ambient;

internal static class Initialization
{
	public static Lazy<InitializationState> State { get; } = new(Initialize);

	private static List<string> ExcludedAssemblyNamespaces { get; } =
	[
		"mscorlib",
		"System",
		"Microsoft",
		"xunit",
		"Castle",
		"DynamicProxyGenAssembly2"
	];

	private static ITestFrameworkAdapter DetectFramework()
	{
		Type frameworkInterface = typeof(ITestFrameworkAdapter);
		foreach (Type frameworkType in AppDomain.CurrentDomain.GetAssemblies()
			         .Where(a => ExcludedAssemblyNamespaces.All(x => !a.FullName!.StartsWith(x)))
			         .SelectMany(a => a.GetTypes())
			         .Where(x => x.IsClass)
			         .Where(frameworkInterface.IsAssignableFrom))
		{
			if (frameworkType == typeof(FallbackTestFramework))
			{
				continue;
			}

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
		ITestFrameworkAdapter testFramework = DetectFramework();
		return new InitializationState(testFramework);
	}

	internal class InitializationState
	{
		private readonly ITestFrameworkAdapter _testFramework;

		public InitializationState(ITestFrameworkAdapter testFramework)
		{
			_testFramework = testFramework;
			Formatter = new ValueFormatter();
		}

		public ValueFormatter Formatter { get; }

		[DoesNotReturn]
		[StackTraceHidden]
		public void Skip(string message) => _testFramework.Skip(message);

		[DoesNotReturn]
		[StackTraceHidden]
		public void Throw(string message) => _testFramework.Throw(message);
	}

	private sealed class FallbackTestFramework : ITestFrameworkAdapter
	{
		#region ITestFrameworkAdapter Members

		public bool IsAvailable => false;

		[DoesNotReturn]
		[StackTraceHidden]
		public void Skip(string message)
			=> throw new SkipException(message);

		[DoesNotReturn]
		[StackTraceHidden]
		public void Throw(string message)
			=> throw new FailException(message);

		#endregion
	}
}
