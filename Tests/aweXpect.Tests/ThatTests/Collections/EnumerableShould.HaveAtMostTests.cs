using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class HaveAtMostTests
	{
		[Fact]
		public async Task ConsidersCancellationToken()
		{
			using CancellationTokenSource cts = new();
			CancellationToken token = cts.Token;
			IEnumerable<int> subject = GetCancellingEnumerable(6, cts);

			async Task Act()
				=> await That(subject).Should().HaveAtMost(8, x => x.Satisfy(y => y < 6))
					.WithCancellation(token);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most 8 items satisfy y => y < 6,
				             but could not verify, because it was cancelled early
				             """);
		}

		[Fact]
		public async Task DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().HaveAtMost(3, x => x.Be(1))
					.And.HaveAtMost(3, x => x.Be(1));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().HaveAtMost(1, x => x.Be(1));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most one item be equal to 1,
				             but at least 2 were
				             """);
		}

		[Fact]
		public async Task Items_ConsidersCancellationToken()
		{
			using CancellationTokenSource cts = new();
			CancellationToken token = cts.Token;
			IEnumerable<int> subject = GetCancellingEnumerable(4, cts);

			async Task Act()
				=> await That(subject).Should().HaveAtMost(6).Items()
					.WithCancellation(token);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most 6 items,
				             but could not verify, because it was cancelled early
				             """);
		}

		[Fact]
		public async Task Items_WhenArrayContainsMatchingItems_ShouldSucceed()
		{
			int[] subject = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().HaveAtMost(3).Items();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Items_WhenArrayContainsTooFewItems_ShouldSucceed()
		{
			int[] subject = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().HaveAtMost(4).Items();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Items_WhenArrayContainsTooManyItems_ShouldFail()
		{
			int[] subject = [1, 2, 3];

			async Task Act()
				=> await That(subject).Should().HaveAtMost(2).Items();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most 2 items,
				             but found 3
				             """);
		}

		[Fact]
		public async Task Items_WhenEnumerableContainsMatchingItems_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtMost(3).Items();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Items_WhenEnumerableContainsTooFewItems_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtMost(4).Items();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Items_WhenEnumerableContainsTooManyItems_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtMost(2).Items();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most 2 items,
				             but found at least 3
				             """);
		}

		[Fact]
		public async Task WhenArrayContainsSufficientlyFewEqualItems_ShouldSucceed()
		{
			int[] subject = [1, 1, 1, 1, 2, 2, 3];

			async Task Act()
				=> await That(subject).Should().HaveAtMost(3, x => x.Be(2));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenArrayContainsTooManyEqualItems_ShouldFail()
		{
			int[] subject = [1, 1, 1, 1, 2, 2, 3];

			async Task Act()
				=> await That(subject).Should().HaveAtMost(3, x => x.Be(1));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most 3 items be equal to 1,
				             but 4 of 7 were
				             """);
		}

		[Fact]
		public async Task WhenEnumerableContainsSufficientlyFewEqualItems_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtMost(3, x => x.Be(2));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableContainsTooManyEqualItems_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtMost(3, x => x.Be(1));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most 3 items be equal to 1,
				             but at least 4 were
				             """);
		}
	}
}
