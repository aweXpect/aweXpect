namespace aweXpect.Tests;

public sealed partial class ThatGeneric
{
	public sealed class DoesNotSatisfy
	{
		public sealed class Tests
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
					             does not satisfy _ => predicateResult,
					             but it was Other {
					               Value = 0
					             }
					             """);
			}
		}
	}
}
