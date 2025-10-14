using System.Threading.Tasks;
using NUnit.Framework;

namespace aweXpect.Frameworks.NUnit3Adapter.Tests;

public sealed class NUnit3TestFrameworkTests
{
	[Test]
	public async Task OnFail_WhenUsingNUnit3AsTestFramework_ShouldThrowAssertionException()
	{
		void Act()
			=> Fail.Test("my message");

		await Expect.That(Act).Throws<AssertionException>()
			.WithMessage("my message");
	}

	[Test]
	public async Task OnInconclusive_WhenUsingNUnit3AsTestFramework_ShouldThrowAssertionException()
	{
		void Act()
			=> Fail.Inconclusive("my message");

		await Expect.That(Act).Throws<NUnit.Framework.InconclusiveException>()
			.WithMessage("my message");
	}

	[Test]
	public async Task OnSkip_WhenUsingNUnit3AsTestFramework_ShouldThrowIgnoreException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).Throws<IgnoreException>()
			.WithMessage("my message");
	}
}
