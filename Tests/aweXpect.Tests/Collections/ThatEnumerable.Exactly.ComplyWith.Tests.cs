namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class Exactly
	{
		public sealed class ComplyWithTests
		{
			[Fact]
			public async Task WhenExactlyOneItemMatches_ShouldSucceed()
			{
				int[] subject = [1, 2, 3, 4, 5,];

				async Task Act()
					=> await That(subject).Exactly(1).ComplyWith(it => it.IsEqualTo(3));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenMoreItemsMatchThanExpected_ShouldFail()
			{
				int[] subject = [1, 2, 3, 2, 5,];

				async Task Act()
					=> await That(subject).Exactly(1).ComplyWith(it => it.IsEqualTo(2));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to 2 for exactly one item,
					             but found 2
					             """);
			}

			[Fact]
			public async Task WhenNoItemsMatch_ShouldFail()
			{
				int[] subject = [1, 2, 3, 4, 5,];

				async Task Act()
					=> await That(subject).Exactly(1).ComplyWith(it => it.IsEqualTo(99));

				await That(Act).Throws<XunitException>();
			}

			[Fact]
			public async Task WhenRichExpectationMatchesExactCount_ShouldSucceed()
			{
				int[] subject = [5, 10, 15, 20, 25, 30, 35,];

				async Task Act()
					=> await That(subject).Exactly(3)
						.ComplyWith(it => it.IsGreaterThan(10).And.IsLessThan(30));

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class AtLeastComplyWithTests
		{
			[Fact]
			public async Task WhenAtLeastOneItemMatches_ShouldSucceed()
			{
				int[] subject = [1, 2, 3, 4, 5,];

				async Task Act()
					=> await That(subject).AtLeast(1).ComplyWith(it => it.IsEqualTo(3));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenNoItemsMatch_ShouldFail()
			{
				int[] subject = [1, 2, 3, 4, 5,];

				async Task Act()
					=> await That(subject).AtLeast(1).ComplyWith(it => it.IsEqualTo(99));

				await That(Act).Throws<XunitException>();
			}
		}
	}
}
