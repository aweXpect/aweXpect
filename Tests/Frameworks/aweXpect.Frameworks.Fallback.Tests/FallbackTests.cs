using Xunit;

namespace aweXpect.Frameworks.Fallback.Tests;

public sealed class FallbackTests
{
	[Fact]
	public void OnFail_WhenNotSpecifyingAnyTestFramework_ShouldFallbackToThrowingAFailException()
	{
		void Act()
			=> Fail.Test("my message");

		FailException exception = Assert.Throws<FailException>(Act);
		Assert.Equal("my message", exception.Message);
	}

	[Fact]
	public void OnSkip_WhenNotSpecifyingAnyTestFramework_ShouldFallbackToThrowingASkipException()
	{
		void Act()
			=> Skip.Test("my message");

		SkipException exception = Assert.Throws<SkipException>(Act);
		Assert.Equal("my message", exception.Message);
	}
}
