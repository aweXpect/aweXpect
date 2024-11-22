using NUnit.Framework;
using System.Threading.Tasks;
using Testably.Expectations;

namespace TestFramework.NUnit4.Tests;

public sealed class NUnit4TestFrameworkTests
{
	[Test]
	public async Task OnFail_WhenUsingNUnit4AsTestFramework_ShouldThrowAssertionException()
	{
		void Act()
			=> Fail.Test("my message");

		await Expect.That(Act).Should().Throw<AssertionException>();
	}

	[Test]
	public async Task OnSkip_WhenUsingNUnit4AsTestFramework_ShouldThrowIgnoreException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).Should().Throw<IgnoreException>();
	}
}
