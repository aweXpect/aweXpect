using aweXpect.Core.Tests.TestHelpers;

namespace aweXpect.Core.Tests.Core;

public class BecauseTests
{
	[Fact]
	public async Task ASpecifiedBecauseReason_ShouldBeIncludedInMessage()
	{
		string because = "I want to test 'because'";
		bool subject = true;

		async Task Act()
			=> await That(subject).IsFalse().Because(because);

		await That(Act).Does().ThrowException().WithMessage($"*{because}*").AsWildcard();
	}

	[Fact]
	public async Task Delegate_ShouldApplyBecauseReason()
	{
		string because = "this is the reason";
		Action subject = () => throw new MyException();

		async Task Act()
			=> await That(subject).Does().NotThrow().Because(because);

		await That(Act).Does().ThrowException().WithMessage($"*{because}*").AsWildcard();
	}

	[Theory]
	[InlineData("we prefix the reason", "because we prefix the reason")]
	[InlineData("  we ignore whitespace", "because we ignore whitespace")]
	[InlineData("because we honor a leading 'because'", "because we honor a leading 'because'")]
	public async Task ShouldPrefixReasonWithBecause(string because, string expectedWithPrefix)
	{
		bool subject = true;

		async Task Act()
			=> await That(subject).IsFalse().Because(because);

		await That(Act).Does().ThrowException().WithMessage($"*{expectedWithPrefix}*")
			.AsWildcard();
	}

	[Fact]
	public async Task WhenApplyBecauseReasonMultipleTimes_ShouldNotOverwritePreviousReason()
	{
		string because1 = "this is the first reason";
		string because2 = "this is the second reason";
		bool subject = false;

		async Task Act()
			=> await That(subject).IsTrue().Because(because1)
				.And.IsFalse().Because(because2);

		await That(Act).Does().ThrowException().WithMessage($"*{because1}*").AsWildcard();
	}

	[Fact]
	public async Task WhenCombineWithAnd_ShouldApplyBecauseReason()
	{
		string because1 = "this is the first reason";
		string because2 = "this is the second reason";
		bool subject = true;

		async Task Act()
			=> await That(subject).IsTrue().Because(because1)
				.And.IsFalse().Because(because2);

		await That(Act).Does().ThrowException().WithMessage($"*{because2}*").AsWildcard();
	}

	[Fact]
	public async Task WhenCombineWithAnd_ShouldApplyBecauseReasonOnlyOnPreviousConstraint()
	{
		string because = "we only apply it to previous constraints";
		bool subject = true;

		async Task Act()
			=> await That(subject).IsTrue().Because(because)
				.And.IsFalse();

		await That(Act).Does().ThrowException()
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
			=> await That(subject).IsFalse().Because(because1)
				.Or.IsFalse().Because(because2);

		await That(Act).Does().ThrowException().WithMessage($"*{because1}*{because2}*")
			.AsWildcard();
	}

	[Fact]
	public async Task WhenNoBecauseReasonIsGiven_ShouldNotIncludeBecause()
	{
		bool subject = true;

		async Task Act()
			=> await That(subject).IsFalse();

		await That(Act).Does().ThrowException()
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
			=> await That(subject).IsFalse().Because(because);

		Exception exception = await That(Act).Does().ThrowException()
			.WithMessage("*because*").AsWildcard();
		await That(exception.Message).DoesNotContain("because because");
	}
}
