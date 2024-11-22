namespace Testably.Expectations.Tests.ThatTests.Collections;

public sealed partial class CollectionShould
{
	public sealed class BeEmptyTests
	{
		[Fact]
		public async Task WhenCollectionContainsValues_ShouldFail()
		{
			int[] subject = [1, 1, 2];

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be empty,
				             but it was [1, 1, 2]
				             """);
		}

		[Fact]
		public async Task WhenCollectionIsEmpty_ShouldSucceed()
		{
			int[] subject = [];

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeEmptyTests
	{
		[Fact]
		public async Task WhenCollectionContainsValues_ShouldSucceed()
		{
			int[] subject = [1, 1, 2];

			async Task Act()
				=> await That(subject)
					.Should().NotBeEmpty();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenCollectionIsEmpty_ShouldFail()
		{
			int[] subject = [];

			async Task Act()
				=> await That(subject)
					.Should().NotBeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be empty,
				             but it was
				             """);
		}
	}
}
