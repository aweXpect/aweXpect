#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed class AtLeast
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
					=> await That(subject).AtLeast(6).Satisfy(y => y < 6)
						.WithCancellation(token);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).AtLeast(0).Are(1)
						.And.AtLeast(0).Are(1);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).AtLeast(2).Are(1);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsEnoughItems_EqualShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).AtLeast(3).Are(1);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewItems_EqualShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).AtLeast(5).Are(1);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at least 5 items equal to 1,
					             but only 4 of 7 were
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewItems_EquivalentShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).AtLeast(5).Are(1);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at least 5 items equal to 1,
					             but only 4 of 7 were
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).AtLeast(1).Are(0);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at least one item equal to 0,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
