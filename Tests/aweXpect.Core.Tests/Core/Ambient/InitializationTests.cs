using System.Diagnostics.CodeAnalysis;
using aweXpect.Core.Adapters;
using aweXpect.Core.Ambient;
using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core.Ambient;

public sealed class InitializationTests
{
	[Fact]
	public async Task DetectFramework_WhenAllFrameworksAreNotAvailable_ShouldUseFallbackAdapter()
	{
		ITestFrameworkAdapter result = Initialization.DetectFramework([typeof(UnavailableFrameworkAdapter)]);

		await That(result.IsAvailable).IsFalse();
		await That(result.GetType().Name).IsEqualTo("FallbackTestFramework");
	}

	[Fact]
	public async Task DetectFramework_WhenFrameworkAdapterThrows_ShouldThrowInvalidOperationException()
	{
		void Act() => Initialization.DetectFramework([typeof(IncorrectFrameworkAdapter)]);

		await That(Act).Throws<InvalidOperationException>()
			.WithMessage($"Could not instantiate test framework '{nameof(IncorrectFrameworkAdapter)}'!");
	}

	private sealed class UnavailableFrameworkAdapter : ITestFrameworkAdapter
	{
		public bool IsAvailable => false;

		[DoesNotReturn]
		public void Skip(string message) => throw new NotSupportedException();

		[DoesNotReturn]
		public void Throw(string message) => throw new NotSupportedException();
	}

	private sealed class IncorrectFrameworkAdapter : ITestFrameworkAdapter
	{
		public bool IsAvailable => throw new NotSupportedException("Could not load the IncorrectFrameworkAdapter");

		[DoesNotReturn]
		public void Skip(string message) => throw new NotSupportedException();

		[DoesNotReturn]
		public void Throw(string message) => throw new NotSupportedException();
	}
}
