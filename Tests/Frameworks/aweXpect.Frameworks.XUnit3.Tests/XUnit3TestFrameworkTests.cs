using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace aweXpect.Frameworks.XUnit3.Tests;

public class XUnit3TestFrameworkTests
{
	[Fact]
	public async Task OnFail_WhenUsingXunit3AsTestFramework_ShouldThrowXunitException()
	{
		void Act()
			=> Fail.Test("my message");

		await Expect.That(Act).Does().Throw<XunitException>();
	}

	[Fact]
	public async Task OnSkip_WhenUsingXunit3AsTestFramework_ShouldThrowSkipException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).Does().Throw<SkipException>()
			.WithMessage("$XunitDynamicSkip$my message");
	}
}
