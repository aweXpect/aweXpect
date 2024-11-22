using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Testably.Expectations.Core.Adapters;
using Testably.Expectations.Formatting;

namespace Testably.Expectations.Core.Ambient;

internal static class Initialization
{
	public static Lazy<InitializationState> State { get; } = new(Initialize);

	private static ITestFrameworkAdapter DetectFramework()
	{
		Type frameworkInterface = typeof(ITestFrameworkAdapter);
		foreach (Type frameworkType in frameworkInterface.Assembly
			.GetTypes()
			.Where(x => x.IsClass)
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
		ITestFrameworkAdapter testFramework = DetectFramework();
		return new InitializationState(testFramework);
	}

	internal class InitializationState
	{
		public ValueFormatter Formatter { get; }
		private readonly ITestFrameworkAdapter _testFramework;

		public InitializationState(ITestFrameworkAdapter testFramework)
		{
			_testFramework = testFramework;
			Formatter = new ValueFormatter();
		}

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
