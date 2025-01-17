using System.Threading.Tasks;
using NUnit.Framework;

namespace aweXpect.Frameworks.NUnit4.Tests;

public sealed class NUnit4TestFrameworkTests
{
	[Test]
	public async Task OnFail_WhenUsingNUnit4AsTestFramework_ShouldThrowAssertionException()
	{
		void Act()
			=> Fail.Test("my message");

		await Expect.That(Act).Does().Throw<AssertionException>();
	}

	[Test]
	public async Task OnSkip_WhenUsingNUnit4AsTestFramework_ShouldThrowIgnoreException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).Does().Throw<IgnoreException>();
	}
}
