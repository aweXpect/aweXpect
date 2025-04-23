using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace aweXpect.Frameworks.XUnit2.Tests;

public sealed class XUnit2TestFrameworkTests
{
	[Fact]
	public async Task OnFail_WhenUsingXunit2AsTestFramework_ShouldThrowXunitException()
	{
		void Act()
			=> Fail.Test("my message");

		await Expect.That(Act).Throws<XunitException>();
	}

	[Fact]
	public async Task OnInconclusive_WhenUsingXunit2AsTestFramework_ShouldThrowInconclusiveException()
	{
		void Act()
			=> Fail.Inconclusive("my message");

		await Expect.That(Act).Throws<InconclusiveException>()
			.WithMessage("my message");
	}

	[Fact]
	public async Task OnSkip_WhenUsingXunit2AsTestFramework_ShouldThrowSkipException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).Throws<SkipException>()
			.WithMessage("SKIPPED: my message (xunit v2 does not support skipping test)");
	}
}
