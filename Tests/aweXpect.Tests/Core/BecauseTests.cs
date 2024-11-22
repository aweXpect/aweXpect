using Testably.Expectations.Tests.TestHelpers;

namespace Testably.Expectations.Tests.Core;

public class BecauseTests
{
	[Fact]
	public async Task ASpecifiedBecauseReason_ShouldBeIncludedInMessage()
	{
		string because = "I want to test 'because'";
		bool subject = true;

		async Task Act()
			=> await That(subject).Should().BeFalse().Because(because);

		await That(Act).Should().ThrowException().WithMessage($"*{because}*").AsWildcard();
	}

	[Fact]
	public async Task Delegate_ShouldApplyBecauseReason()
	{
		string because = "this is the reason";
		Action subject = () => throw new MyException();

		async Task Act()
			=> await That(subject).Should().NotThrow().Because(because);

		await That(Act).Should().ThrowException().WithMessage($"*{because}*").AsWildcard();
	}

	[Theory]
	[InlineData("we prefix the reason", "because we prefix the reason")]
	[InlineData("  we ignore whitespace", "because we ignore whitespace")]
	[InlineData("because we honor a leading 'because'", "because we honor a leading 'because'")]
	public async Task ShouldPrefixReasonWithBecause(string because, string expectedWithPrefix)
	{
		bool subject = true;

		async Task Act()
			=> await That(subject).Should().BeFalse().Because(because);

		await That(Act).Should().ThrowException().WithMessage($"*{expectedWithPrefix}*")
			.AsWildcard();
	}

	[Fact]
	public async Task WhenApplyBecauseReasonMultipleTimes_ShouldNotOverwritePreviousReason()
	{
		string because1 = "this is the first reason";
		string because2 = "this is the second reason";
		bool subject = false;

		async Task Act()
			=> await That(subject).Should().BeTrue().Because(because1)
				.And.BeFalse().Because(because2);

		await That(Act).Should().ThrowException().WithMessage($"*{because1}*").AsWildcard();
	}

	[Fact]
	public async Task WhenCombineWithAnd_ShouldApplyBecauseReason()
	{
		string because1 = "this is the first reason";
		string because2 = "this is the second reason";
		bool subject = true;

		async Task Act()
			=> await That(subject).Should().BeTrue().Because(because1)
				.And.BeFalse().Because(because2);

		await That(Act).Should().ThrowException().WithMessage($"*{because2}*").AsWildcard();
	}

	[Fact]
	public async Task WhenCombineWithAnd_ShouldApplyBecauseReasonOnlyOnPreviousConstraint()
	{
		string because = "we only apply it to previous constraints";
		bool subject = true;

		async Task Act()
			=> await That(subject).Should().BeTrue().Because(because)
				.And.BeFalse();

		await That(Act).Should().ThrowException()
			.WithMessage("""
			             Expected subject to
			             be True, because we only apply it to previous constraints and be False,
			             but it was True
			             """);
	}

	[Fact]
	public async Task WhenCombineWithOr_ShouldApplyBecauseReason()
	{
		string because1 = "this is the first reason";
		string because2 = "this is the second reason";
		bool subject = true;

		async Task Act()
			=> await That(subject).Should().BeFalse().Because(because1)
				.Or.BeFalse().Because(because2);

		await That(Act).Should().ThrowException().WithMessage($"*{because1}*{because2}*")
			.AsWildcard();
	}

	[Fact]
	public async Task WhenNoBecauseReasonIsGiven_ShouldNotIncludeBecause()
	{
		bool subject = true;

		async Task Act()
			=> await That(subject).Should().BeFalse();

		await That(Act).Should().ThrowException()
			.WithMessage("""
			             Expected subject to
			             be False,
			             but it was True
			             """);
	}

	[Fact]
	public async Task WhenReasonStartsWithBecause_ShouldHonorExistingPrefix()
	{
		string because = "because we honor a leading 'because'";
		bool subject = true;

		async Task Act()
			=> await That(subject).Should().BeFalse().Because(because);

		Exception exception = await That(Act).Should().ThrowException()
			.WithMessage("*because*").AsWildcard();
		await That(exception.Message).Should().NotContain("because because");
	}
}
