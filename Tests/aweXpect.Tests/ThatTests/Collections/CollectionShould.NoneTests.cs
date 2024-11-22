namespace Testably.Expectations.Tests.ThatTests.Collections;

public sealed partial class CollectionShould
{
	public sealed class NoneTests
	{
		[Fact]
		public async Task WhenCollectionContainsEqualValues_ShouldFail()
		{
			int[] subject = [1, 1, 1, 1, 2, 2, 3];

			async Task Act()
				=> await That(subject).Should().None().Be(1);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have no items equal to 1,
				             but at least one items were equal
				             """);
		}

		[Fact]
		public async Task WhenCollectionIsEmpty_ShouldSucceed()
		{
			bool[] subject = [];

			async Task Act()
				=> await That(subject).Should().None().Be(true);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenCollectionOnlyContainsDifferentValues_ShouldSucceed()
		{
			int[] subject = [1, 1, 1, 1, 2, 2, 3];

			async Task Act()
				=> await That(subject).Should().None().Be(42);

			await That(Act).Should().NotThrow();
		}
	}
}
