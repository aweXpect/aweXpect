using System.Linq;
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

		await Expect.That(Act).Throws<XunitException>();
	}

#if DEBUG // TODO remove after next core update
	[Fact]
	public async Task OnInconclusive_WhenUsingXunit3AsTestFramework_ShouldThrowXunitException()
	{
		void Act()
			=> Fail.Inconclusive("my message");

		InconclusiveException exception = await Expect.That(Act).Throws<InconclusiveException>()
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

		await Expect.That(Act).Throws<SkipException>()
			.WithMessage("$XunitDynamicSkip$my message");
	}
}
