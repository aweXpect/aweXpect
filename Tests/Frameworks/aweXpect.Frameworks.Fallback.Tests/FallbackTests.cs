using NUnit.Framework;

namespace aweXpect.Frameworks.Fallback.Tests;

public sealed class FallbackTests
{
	[Test]
	public void OnFail_WhenNotSpecifyingAnyTestFramework_ShouldFallbackToThrowingAFailException()
	{
		void Act()
			=> Fail.Test("my message");

		FailException? exception = Assert.Throws<FailException>(Act);
		Assert.That(exception!.Message, Is.EqualTo("my message"));
	}

	[Test]
	public void OnInconclusive_WhenNotSpecifyingAnyTestFramework_ShouldFallbackToThrowingAnInconclusiveException()
	{
		void Act()
			=> Fail.Inconclusive("my message");

		InconclusiveException? exception = Assert.Throws<InconclusiveException>(Act);
		Assert.That(exception!.Message, Is.EqualTo("my message"));
	}

	[Test]
	public void OnSkip_WhenNotSpecifyingAnyTestFramework_ShouldFallbackToThrowingASkipException()
	{
		void Act()
			=> Skip.Test("my message");

		SkipException? exception = Assert.Throws<SkipException>(Act);
		Assert.That(exception!.Message, Is.EqualTo("my message"));
	}
}
