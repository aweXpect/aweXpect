#if NET8_0_OR_GREATER
using System.Collections;
using System.Collections.Immutable;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class DoesNotHaveCount
	{
		public sealed class ImmutableArrayTests
		{
			[Fact]
			public async Task WhenImmutableArrayContainsMatchingItems_ShouldFail()
			{
				ImmutableArray<int> subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).DoesNotHaveCount(3);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not have exactly 3 items,
					             but it did

					             Collection:
					             [1, 2, 3]
					             """);
			}

			[Fact]
			public async Task WhenImmutableArrayContainsTooFewItems_ShouldSucceed()
			{
				ImmutableArray<int> subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).DoesNotHaveCount(4);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenImmutableArrayContainsTooManyItems_ShouldSucceed()
			{
				ImmutableArray<int> subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).DoesNotHaveCount(2);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class EnumerableTests
		{
			[Fact]
			public async Task WhenEnumerableContainsMatchingItems_ShouldFail()
			{
				IEnumerable subject = new[]
				{
					1, 2, 3,
				};

				async Task Act()
					=> await That(subject).DoesNotHaveCount(3);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not have exactly 3 items,
					             but it did

					             Collection:
					             [1, 2, 3]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewItems_ShouldSucceed()
			{
				IEnumerable subject = new[]
				{
					1, 2, 3,
				};

				async Task Act()
					=> await That(subject).DoesNotHaveCount(4);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyItems_ShouldSucceed()
			{
				IEnumerable subject = new[]
				{
					1, 2, 3,
				};

				async Task Act()
					=> await That(subject).DoesNotHaveCount(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable? subject = null;

				async Task Act()
					=> await That(subject)!.DoesNotHaveCount(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not have exactly 2 items,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
