namespace aweXpect.Tests;

public sealed partial class GenericShould
{
	public sealed class SatisfyTests
	{
		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public async Task ShouldFailWhenPredicateResultIsFalse(bool predicateResult)
		{
			Other subject = new();

			async Task Act()
				=> await That(subject).Should().Satisfy(_ => predicateResult);

			await That(Act).Should().Throw<XunitException>()
				.OnlyIf(!predicateResult)
				.WithMessage("""
				             Expected subject to
				             satisfy _ => predicateResult,
				             but it was Other {
				               Value = 0
				             }
				             """);
		}
	}

	public sealed class NotSatisfyTests
	{
		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public async Task ShouldFailWhenPredicateResultIsTrue(bool predicateResult)
		{
			Other subject = new();

			async Task Act()
				=> await That(subject).Should().NotSatisfy(_ => predicateResult);

			await That(Act).Should().Throw<XunitException>()
				.OnlyIf(predicateResult)
				.WithMessage("""
				             Expected subject to
				             not satisfy _ => predicateResult,
				             but it was Other {
				               Value = 0
				             }
				             """);
		}
	}
}
