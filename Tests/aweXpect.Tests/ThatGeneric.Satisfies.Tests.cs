namespace aweXpect.Tests;

public sealed partial class ThatGeneric
{
	public sealed class Satisfies
	{
		public sealed class Tests
		{
			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task ShouldFailWhenPredicateResultIsFalse(bool predicateResult)
			{
				Other subject = new();

				async Task Act()
					=> await That(subject).Satisfies(_ => predicateResult);

				await That(Act).Throws<XunitException>()
					.OnlyIf(!predicateResult)
					.WithMessage("""
					             Expected that subject
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
					=> await That(subject).DoesNotSatisfy(_ => predicateResult);

				await That(Act).Throws<XunitException>()
					.OnlyIf(predicateResult)
					.WithMessage("""
					             Expected that subject
					             not satisfy _ => predicateResult,
					             but it was Other {
					               Value = 0
					             }
					             """);
			}
		}
	}
}
