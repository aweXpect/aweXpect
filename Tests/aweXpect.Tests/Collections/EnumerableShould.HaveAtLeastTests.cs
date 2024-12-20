using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class HaveAtLeast
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ConsidersCancellationToken()
			{
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;
				IEnumerable<int> subject = GetCancellingEnumerable(6, cts);

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(6, x => x.Satisfy(y => y < 6))
						.WithCancellation(token);

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(0, x => x.Be(1))
						.And.HaveAtLeast(0, x => x.Be(1));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(2, x => x.Be(1));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsEnoughEqualItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(3, x => x.Be(1));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewEqualItems_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(5, x => x.Be(1));

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at least 5 items be equal to 1,
					             but only 4 of 7 were
					             """);
			}
		}

		public sealed class ItemsTests
		{
			[Fact]
			public async Task ConsidersCancellationToken()
			{
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;
				IEnumerable<int> subject = GetCancellingEnumerable(4, cts);

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(6).Items()
						.WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at least 6 items,
					             but could not verify, because it was cancelled early
					             """);
			}

			[Fact]
			public async Task WhenArrayContainsMatchingItems_ShouldSucceed()
			{
				int[] subject = [1, 2, 3];

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(3).Items();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenArrayContainsTooFewItems_ShouldFail()
			{
				int[] subject = [1, 2, 3];

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(4).Items();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at least 4 items,
					             but found only 3
					             """);
			}

			[Fact]
			public async Task WhenArrayContainsTooManyItems_ShouldSucceed()
			{
				int[] subject = [1, 2, 3];

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(2).Items();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(3).Items();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewItems_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(4).Items();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at least 4 items,
					             but found only 3
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveAtLeast(2).Items();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
