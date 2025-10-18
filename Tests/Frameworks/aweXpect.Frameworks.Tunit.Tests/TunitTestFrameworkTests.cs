using System.Threading.Tasks;
using TUnit.Assertions.Exceptions;
using TUnit.Core.Exceptions;

namespace aweXpect.Frameworks.Tunit.Tests;

public sealed class TunitTestFrameworkTests
{
	[Test]
	public async Task OnFail_WhenUsingXunit2AsTestFramework_ShouldThrowXunitException()
	{
		void Act()
			=> Fail.Test("my message");

		await Expect.That(Act).Throws<AssertionException>()
			.WithMessage("my message");
	}

	[Test]
	public async Task OnInconclusive_WhenUsingXunit2AsTestFramework_ShouldThrowXunitException()
	{
		void Act()
			=> Fail.Inconclusive("my message");

		await Expect.That(Act).Throws<InconclusiveTestException>()
			.WithMessage("my message");
	}

	[Test]
	public async Task OnSkip_WhenUsingXunit2AsTestFramework_ShouldThrowSkipException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).Throws<SkipTestException>()
			.Whose(e => e.Reason, r => r.IsEqualTo("my message"));
	}
}
