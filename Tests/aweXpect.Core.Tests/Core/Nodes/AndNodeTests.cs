namespace aweXpect.Core.Tests.Core.Nodes;

public sealed class AndNodeTests
{
	[Fact]
	public async Task ToString_ShouldCombineAllNodes()
	{
#pragma warning disable CS4014
		IExpectSubject<bool> that = That(true);
		that.IsTrue().And.IsFalse().And.Implies(false);
#pragma warning restore CS4014

		string expectedResult = "be True and be False and imply False";

		string? result = that.Should(_ => { }).ExpectationBuilder.ToString();

		await That(result).Is(expectedResult);
	}

	[Fact]
	public async Task WithFirstFailedTests_ShouldIncludeSingleFailureInMessage()
	{
		async Task Act()
			=> await That(true).IsFalse().And.IsTrue();

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected true to
			             be False and be True,
			             but it was True
			             """);
	}

	[Fact]
	public async Task WithMultipleFailedTests_ShouldIncludeAllFailuresInMessage()
	{
		async Task Act()
			=> await That(true).IsFalse().And.IsFalse().And.Implies(false);

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected true to
			             be False and be False and imply False,
			             but it was True and it did not
			             """);
	}

	[Fact]
	public async Task WithSecondFailedTests_ShouldIncludeSingleFailureInMessage()
	{
		async Task Act()
			=> await That(true).IsTrue().And.IsFalse();

		await That(Act).Does().ThrowException()
			.WithMessage("""
			             Expected true to
			             be True and be False,
			             but it was True
			             """);
	}

	[Fact]
	public async Task WithTwoSuccessfulTests_ShouldNotThrow()
	{
		async Task Act()
			=> await That(true).IsTrue().And.IsNot(false);

		await That(Act).Does().NotThrow();
	}
}
