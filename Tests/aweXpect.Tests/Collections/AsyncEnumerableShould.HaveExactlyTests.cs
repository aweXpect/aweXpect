#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class HaveExactly
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ConsidersCancellationToken()
			{
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;
				IAsyncEnumerable<int> subject =
					GetCancellingAsyncEnumerable(6, cts, CancellationToken.None);

				async Task Act()
					=> await That(subject).Should().HaveExactly(6, x => x.Satisfy(y => y < 6))
						.WithCancellation(token);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly 6 items satisfy y => y < 6,
					             but could not verify, because it was cancelled early
					             """);
			}

			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().HaveExactly(1, x => x.Be(1))
						.And.HaveExactly(1, x => x.Be(1));

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().HaveExactly(1, x => x.Be(1));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly one item be equal to 1,
					             but at least 2 were
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsExpectedNumberOfEqualItems_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveExactly(4, x => x.Be(1));

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewEqualItems_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveExactly(4, x => x.Be(2));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly 4 items be equal to 2,
					             but only 2 of 7 were
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyEqualItems_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveExactly(3, x => x.Be(1));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly 3 items be equal to 1,
					             but at least 4 were
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().HaveExactly(1, x => x.Be(0));

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly one item be equal to 0,
					             but it was <null>
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
				IAsyncEnumerable<int> subject =
					GetCancellingAsyncEnumerable(6, cts, CancellationToken.None);

				async Task Act()
					=> await That(subject).Should().HaveExactly(6).Items()
						.WithCancellation(token);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly 6 items,
					             but could not verify, because it was cancelled early
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveExactly(3).Items();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewItems_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveExactly(4).Items();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly 4 items,
					             but found only 3
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyItems_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().HaveExactly(2).Items();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly 2 items,
					             but found at least 3
					             """);
			}
		}
	}
}
#endif
