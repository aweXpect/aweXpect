﻿namespace aweXpect.Core.Tests.Core.Nodes;

public sealed class OrNodeTests
{
	[Fact]
	public async Task ToString_ShouldCombineAllNodes()
	{
#pragma warning disable CS4014
#pragma warning disable aweXpect0001
		IThat<bool> that = That(true);
		that.IsTrue().Or.IsFalse().Or.Implies(false);
#pragma warning restore aweXpect0001
#pragma warning restore CS4014

		string expectedResult = "is True or is False or implies False";

		string? result = ((IThatVerb<bool>)that).ExpectationBuilder.ToString();

		await That(result).IsEqualTo(expectedResult);
	}

	[Fact]
	public async Task WithFirstFailedTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsFalse().Or.IsTrue();

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task WithMultipleFailedTests_ShouldIncludeAllFailuresInMessage()
	{
		async Task Act()
			=> await That(true).IsFalse().Or.IsFalse().Or.Implies(false);

		await That(Act).ThrowsException()
			.WithMessage("""
			             Expected that true
			             is False or is False or implies False,
			             but it was True and it did not
			             """);
	}

	[Fact]
	public async Task WithSecondFailedTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsTrue().Or.IsFalse();

		await That(Act).DoesNotThrow();
	}

	[Fact]
	public async Task WithTwoSuccessfulTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsTrue().Or.IsNotEqualTo(false);

		await That(Act).DoesNotThrow();
	}
}
