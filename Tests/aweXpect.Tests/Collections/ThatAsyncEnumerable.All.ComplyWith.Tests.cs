﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class All
	{
		public sealed class ComplyWith
		{
			public sealed class Tests
			{
				[Fact]
				public async Task ConsidersCancellationToken()
				{
					using CancellationTokenSource cts = new();
					CancellationToken token = cts.Token;
					IAsyncEnumerable<int> subject = GetCancellingAsyncEnumerable(5, cts, token);

					async Task Act()
						=> await That(subject).All().ComplyWith(x => x.IsLessThan(6)).WithCancellation(token);

					await That(Act).Throws<InconclusiveException>()
						.WithMessage("""
						             Expected that subject
						             is less than 6 for all items,
						             but could not verify, because it was already cancelled
						             """);
				}

				[Fact]
				public async Task DoesNotEnumerateTwice()
				{
					ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

					async Task Act()
						=> await That(subject).All().ComplyWith(x => x.IsGreaterThan(-1))
							.And.All().ComplyWith(x => x.IsGreaterThan(-1));

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

					async Task Act()
						=> await That(subject).All().ComplyWith(x => x.IsEqualTo(1));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to 1 for all items,
						             but not all were
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsDifferentValues_ShouldFail()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

					async Task Act()
						=> await That(subject).All().ComplyWith(x => x.IsEqualTo(1));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to 1 for all items,
						             but not all were
						             """);
				}

				[Fact]
				public async Task WhenEnumerableIsEmpty_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable((int[]) []);

					async Task Act()
						=> await That(subject).All().ComplyWith(x => x.IsEqualTo(0));

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableOnlyContainsEqualValues_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 1, 1, 1]);

					async Task Act()
						=> await That(subject).All().ComplyWith(x => x.IsEqualTo(1));

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					IAsyncEnumerable<int>? subject = null;

					async Task Act()
						=> await That(subject).All().ComplyWith(x => x.IsEqualTo(0));

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to 0 for all items,
						             but it was <null>
						             """);
				}
			}
		}
	}
}
#endif
