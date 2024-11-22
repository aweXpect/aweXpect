using System.Threading.Tasks;
using TUnit.Assertions.Exceptions;
using TUnit.Core.Exceptions;

namespace aweXpect.Frameworks.TUnit.Tests;

public sealed class TUnitTestFrameworkTests
{
	[Test]
	public async Task OnFail_WhenUsingXunit2AsTestFramework_ShouldThrowXunitException()
	{
		void Act()
			=> Fail.Test("my message");

		await Expect.That(Act).Should().Throw<AssertionException>();
	}

	[Test]
	public async Task OnSkip_WhenUsingXunit2AsTestFramework_ShouldThrowSkipException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).Should().Throw<SkipTestException>();
	}
}
