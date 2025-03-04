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

		Exception exception = await Expect.That(Act).ThrowsException()
			.WithMessage("my message");
		await Expect.That(exception.GetType().GetInterfaces().Select(e => e.Name))
			.Contains("IAssertionException");
	}

#if DEBUG // TODO remove after next core update
	[Fact]
	public async Task OnInconclusive_WhenUsingXunit3AsTestFramework_ShouldThrowXunitException()
	{
		void Act()
			=> Fail.Inconclusive("my message");

		Exception exception = await Expect.That(Act).ThrowsException()
			.WithMessage("my message");
		await Expect.That(exception.GetType().GetInterfaces().Select(e => e.Name))
			.Contains("ITestTimeoutException");
	}
#endif

	[Fact]
	public async Task OnSkip_WhenUsingXunit3AsTestFramework_ShouldThrowSkipException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).ThrowsException()
			.WithMessage("$XunitDynamicSkip$my message");
	}
}
