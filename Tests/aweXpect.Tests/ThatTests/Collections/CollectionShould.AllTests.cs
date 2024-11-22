namespace Testably.Expectations.Tests.ThatTests.Collections;

public sealed partial class CollectionShould
{
	public sealed class AllTests
	{
		[Fact]
		public async Task WhenCollectionContainsOtherValues_ShouldFail()
		{
			int[] subject = [1, 1, 1, 1, 2, 2, 3];

			async Task Act()
				=> await That(subject).Should().All().Be(1);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have all items equal to 1,
				             but not all of 7 items were equal
				             """);
		}

		[Fact]
		public async Task WhenCollectionIsEmpty_ShouldSucceed()
		{
			bool[] subject = [];

			async Task Act()
				=> await That(subject).Should().All().Be(true);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenCollectionOnlyContainsEqualValues_ShouldSucceed()
		{
			int[] subject = [1, 1, 1, 1];

			async Task Act()
				=> await That(subject).Should().All().Be(1);

			await That(Act).Should().NotThrow();
		}
	}
}
