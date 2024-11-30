namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class CollectionShould
{
	public sealed class BetweenTests
	{
		[Fact]
		public async Task WhenCollectionContainsSufficientlyEqualItems_ShouldSucceed()
		{
			int[] subject = [1, 1, 1, 1, 2, 2, 3];

			async Task Act()
				=> await That(subject).Should().Between(3).And(4.Times(), x => x.Be(1));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenCollectionContainsTooFewEqualItems_ShouldFail()
		{
			int[] subject = [1, 1, 1, 1, 2, 2, 3];

			async Task Act()
				=> await That(subject).Should().Between(3).And(4.Times(), x => x.Be(2));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have between 3 and 4 items be equal to 2,
				             but only 2 of 7 were
				             """);
		}

		[Fact]
		public async Task WhenCollectionContainsTooManyEqualItems_ShouldFail()
		{
			int[] subject = [1, 1, 1, 1, 2, 2, 3];

			async Task Act()
				=> await That(subject).Should().Between(1).And(3.Times(), x => x.Be(1));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have between 1 and 3 items be equal to 1,
				             but at least 4 were
				             """);
		}
	}
}
