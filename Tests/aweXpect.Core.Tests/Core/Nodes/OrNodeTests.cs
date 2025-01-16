﻿namespace aweXpect.Core.Tests.Core.Nodes;

public sealed class OrNodeTests
{
	[Fact]
	public async Task ToString_ShouldCombineAllNodes()
	{
#pragma warning disable CS4014
		IExpectSubject<bool> that = That(true);
		that.IsTrue().Or.IsFalse().Or.Implies(false);
#pragma warning restore CS4014

		string expectedResult = "be True or be False or imply False";

		string? result = that.Should(_ => { }).ExpectationBuilder.ToString();

		await That(result).Should().Be(expectedResult);
	}

	[Fact]
	public async Task WithFirstFailedTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsFalse().Or.IsTrue();

		await That(Act).Does().NotThrow();
	}

	[Fact]
	public async Task WithMultipleFailedTests_ShouldIncludeAllFailuresInMessage()
	{
		async Task Act()
			=> await That(true).IsFalse().Or.IsFalse().Or.Implies(false);

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected true to
			             be False or be False or imply False,
			             but it was True and it did not
			             """);
	}

	[Fact]
	public async Task WithSecondFailedTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsTrue().Or.IsFalse();

		await That(Act).Does().NotThrow();
	}

	[Fact]
	public async Task WithTwoSuccessfulTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsTrue().Or.IsNot(false);

		await That(Act).Does().NotThrow();
	}
}
