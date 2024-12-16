using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace aweXpect.Frameworks.XUnit3.Core.Tests;

public class XUnit3TestFrameworkTests
{
	[Fact]
	public async Task OnFail_WhenUsingXunit3AsTestFramework_ShouldThrowXunitException()
	{
		void Act()
			=> Fail.Test("my message");

		Exception exception = await Expect.That(Act).Should().ThrowException()
			.WithMessage("my message");
		await Expect.That(exception.GetType().GetInterfaces().Select(e => e.Name))
			.Should().Contain("IAssertionException");
	}

	[Fact]
	public async Task OnSkip_WhenUsingXunit3AsTestFramework_ShouldThrowSkipException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).Should().ThrowException()
			.WithMessage("$XunitDynamicSkip$my message");
	}
}
