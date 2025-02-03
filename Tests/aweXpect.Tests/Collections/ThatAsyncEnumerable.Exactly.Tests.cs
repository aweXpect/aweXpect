#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed class Exactly
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
					=> await That(subject).Exactly(6).Satisfy(y => y < 6)
						.WithCancellation(token);

				await That(Act).Throws<XunitException>()
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
					=> await That(subject).Exactly(1).AreEqualTo(1)
						.And.Exactly(1).AreEqualTo(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).Exactly(1).AreEqualTo(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly one item equal to 1,
					             but at least 2 were
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsExpectedNumberOfEqualItems_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Exactly(4).AreEqualTo(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewEqualItems_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Exactly(4).AreEqualTo(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly 4 items equal to 2,
					             but only 2 of 7 were
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyEqualItems_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Exactly(3).AreEqualTo(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly 3 items equal to 1,
					             but at least 4 were
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).Exactly(1).AreEqualTo(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have exactly one item equal to 0,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
