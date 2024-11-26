using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace aweXpect.Frameworks.MsTest.Tests;

[TestClass]
public sealed class MsTestFrameworkTests
{
	[TestMethod]
	public async Task OnFail_WhenUsingMsTestAsTestFramework_ShouldThrowXunitException()
	{
		void Act()
			=> Fail.Test("my message");

		await Expect.That(Act).Should().Throw<AssertFailedException>();
	}

	[TestMethod]
	public async Task OnSkip_WhenUsingMsTestAsTestFramework_ShouldThrowSkipException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).Should().Throw<AssertInconclusiveException>()
			.WithMessage("my message");
	}
}
