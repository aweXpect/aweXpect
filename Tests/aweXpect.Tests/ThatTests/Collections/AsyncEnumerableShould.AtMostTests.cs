#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;
using Testably.Expectations.Tests.TestHelpers;
// ReSharper disable PossibleMultipleEnumeration

namespace Testably.Expectations.Tests.ThatTests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class AtMostTests
	{
		[Fact]
		public async Task ConsidersCancellationToken()
		{
			using CancellationTokenSource cts = new();
			CancellationToken token = cts.Token;
			IAsyncEnumerable<int> subject =
 GetCancellingAsyncEnumerable(6, cts, CancellationToken.None);

			async Task Act()
				=> await That(subject).Should().AtMost(8).Satisfy(x => x < 6)
					.WithCancellation(token);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             at most 8 satisfy "x => x < 6",
				             but could not verify, because it was cancelled early
				             """);
		}

		[Fact]
		public async Task DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().AtMost(3).Be(1)
					.And.AtMost(3).Be(1);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotMaterializeEnumerable()
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().AtMost(1).Be(1);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most 1 item equal to 1,
				             but at least 2 items were equal
				             """);
		}

		[Fact]
		public async Task WhenEnumerableContainsSufficientlyFewEqualItems_ShouldSucceed()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().AtMost(3).Be(2);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableContainsTooManyEqualItems_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().AtMost(3).Be(1);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most 3 items equal to 1,
				             but at least 4 items were equal
				             """);
		}
	}
}
#endif
