using System.Diagnostics.CodeAnalysis;
using System.Threading;
using aweXpect.Core.Adapters;
using aweXpect.Core.Initialization;

namespace aweXpect.Core.Tests.Core.Initialization;

public sealed class AweXpectInitializationTests
{
	[Fact]
	public async Task DetectFramework_WhenAllFrameworksAreNotAvailable_ShouldUseFallbackAdapter()
	{
		ITestFrameworkAdapter result = AweXpectInitialization.DetectFramework([typeof(UnavailableFrameworkAdapter),]);

		await That(result.IsAvailable).IsFalse();
		await That(result.GetType().Name).IsEqualTo("FallbackTestFramework");
	}

	[Fact]
	public async Task DetectFramework_WhenFrameworkAdapterThrows_ShouldThrowInvalidOperationException()
	{
		void Act() => AweXpectInitialization.DetectFramework([typeof(IncorrectFrameworkAdapter),]);

		await That(Act).Throws<InvalidOperationException>()
			.WithMessage(
				$"Could not instantiate test framework 'AweXpectInitializationTests.{nameof(IncorrectFrameworkAdapter)}'!");
	}

	[Fact]
	public async Task ShouldInitializeCustomInitializerOnceBeforeExpectationIsEvaluated()
	{
		int result = CustomInitializer.InitializationCount;

		await That(result).IsEqualTo(1);
	}

	public sealed class CustomInitializer : IAweXpectInitializer
	{
		private static int _initializationCount;
		public static int InitializationCount => _initializationCount;


		public void Initialize() => Interlocked.Increment(ref _initializationCount);
	}

	private sealed class UnavailableFrameworkAdapter : ITestFrameworkAdapter
	{
		public bool IsAvailable => false;

#pragma warning disable CS0436
		[DoesNotReturn]
		public void Fail(string message) => throw new NotSupportedException();

		[DoesNotReturn]
		public void Inconclusive(string message) => throw new NotSupportedException();

		[DoesNotReturn]
		public void Skip(string message) => throw new NotSupportedException();
#pragma warning restore CS0436
	}

	private sealed class IncorrectFrameworkAdapter : ITestFrameworkAdapter
	{
		public bool IsAvailable => throw new NotSupportedException("Could not load the IncorrectFrameworkAdapter");

#pragma warning disable CS0436
		[DoesNotReturn]
		public void Fail(string message) => throw new NotSupportedException();

		[DoesNotReturn]
		public void Inconclusive(string message) => throw new NotSupportedException();

		[DoesNotReturn]
		public void Skip(string message) => throw new NotSupportedException();
#pragma warning restore CS0436
	}
}
