namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class CollectionShould
{
	public sealed class AtMostTests
	{
		[Fact]
		public async Task WhenCollectionContainsSufficientlyFewEqualItems_ShouldSucceed()
		{
			int[] subject = [1, 1, 1, 1, 2, 2, 3];

			async Task Act()
				=> await That(subject).Should().AtMost(3.Times(), x => x.Be(2));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenCollectionContainsTooManyEqualItems_ShouldFail()
		{
			int[] subject = [1, 1, 1, 1, 2, 2, 3];

			async Task Act()
				=> await That(subject).Should().AtMost(3.Times(), x => x.Be(1));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most 3 items be equal to 1,
				             but at least 4 were
				             """);
		}
	}
}
