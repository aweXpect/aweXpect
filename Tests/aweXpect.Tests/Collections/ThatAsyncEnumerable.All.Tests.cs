﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class All
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ConsidersCancellationToken()
			{
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;
				IAsyncEnumerable<int> subject = GetCancellingAsyncEnumerable(6, cts, CancellationToken.None);

				async Task Act()
					=> await That(subject).All().Are(item => item.IsLessThan(6)).WithCancellation(token);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have all items be less than 6,
					             but could not verify, because it was cancelled early
					             """);
			}

			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).All().Satisfy(_ => true).And
						.All().Satisfy(_ => true);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).All().Are(item => item.Is(1));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have all items be equal to 1,
					             but not all were
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsDifferentValues_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).All().Are(item => item.Is(1));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have all items be equal to 1,
					             but not all were
					             """);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

				async Task Act()
					=> await That(subject).All().Are(item => item.Is(0));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableOnlyContainsEqualValues_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 1, 1, 1]);

				async Task Act()
					=> await That(subject).All().Are(item => item.Is(1));

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).All().Are(item => item.Is(0));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have all items be equal to 0,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
