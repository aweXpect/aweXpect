﻿using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace aweXpect.Frameworks.MsTest.Tests;

[TestClass]
public sealed class MsTestFrameworkTests
{
	[TestMethod]
	public async Task OnFail_WhenUsingMsTestAsTestFramework_ShouldThrowAssertFailedException()
	{
		void Act()
			=> Fail.Test("my message");

		await Expect.That(Act).Throws<AssertFailedException>()
			.WithMessage("my message");
	}

#if DEBUG // TODO remove after next core update
	[TestMethod]
	public async Task OnInconclusive_WhenUsingMsTestAsTestFramework_ShouldThrowAssertInconclusiveException()
	{
		void Act()
			=> Fail.Inconclusive("my message");

		await Expect.That(Act).Throws<AssertInconclusiveException>()
			.WithMessage("my message");
	}
#endif

	[TestMethod]
	public async Task OnSkip_WhenUsingMsTestAsTestFramework_ShouldThrowAssertInconclusiveException()
	{
		void Act()
			=> Skip.Test("my message");

		await Expect.That(Act).Throws<AssertInconclusiveException>()
			.WithMessage("my message");
	}
}
