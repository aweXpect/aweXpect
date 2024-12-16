#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;
// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class HaveAtLeastTests
	{
		[Fact]
		public async Task ConsidersCancellationToken()
		{
			using CancellationTokenSource cts = new();
			CancellationToken token = cts.Token;
			IAsyncEnumerable<int> subject =
 GetCancellingAsyncEnumerable(6, cts, CancellationToken.None);

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(6, x => x.Satisfy(y => y < 6))
					.WithCancellation(token);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(0, x => x.Be(1))
					.And.HaveAtLeast(0, x => x.Be(1));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotMaterializeEnumerable()
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(2, x => x.Be(1));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Items_ConsidersCancellationToken()
		{
			using CancellationTokenSource cts = new();
			CancellationToken token = cts.Token;
			IAsyncEnumerable<int> subject =
				GetCancellingAsyncEnumerable(4, cts, CancellationToken.None);

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
		public async Task Items_WhenEnumerableContainsMatchingItems_ShouldSucceed()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(3).Items();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Items_WhenEnumerableContainsTooFewItems_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

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
		public async Task Items_WhenEnumerableContainsTooManyItems_ShouldSucceed()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(2).Items();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableContainsEnoughEqualItems_ShouldSucceed()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(3, x => x.Be(1));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableContainsTooFewEqualItems_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(5, x => x.Be(1));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at least 5 items be equal to 1,
				             but only 4 of 7 were
				             """);
		}

		[Fact]
		public async Task WhenEnumerableContainsTooFewEquivalentItems_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

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
}
#endif
