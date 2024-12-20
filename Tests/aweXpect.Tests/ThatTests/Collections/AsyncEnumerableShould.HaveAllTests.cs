#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class HaveAll
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
					=> await That(subject).Should().HaveAll(x => x.BeLessThan(6)).WithCancellation(token);

				await That(Act).Should().Throw<XunitException>()
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
					=> await That(subject).Should()
						.HaveAll(x => x.Satisfy(_ => true))
						.And.HaveAll(x => x.Satisfy(_ => true));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().HaveAll(x => x.Be(1));

				await That(Act).Should().Throw<XunitException>()
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
					=> await That(subject).Should().HaveAll(x => x.Be(1));

				await That(Act).Should().Throw<XunitException>()
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
					=> await That(subject).Should().HaveAll(x => x.Be(0));

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableOnlyContainsEqualValues_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 1, 1, 1]);

				async Task Act()
					=> await That(subject).Should().HaveAll(x => x.Be(1));

				await That(Act).Should().NotThrow();
			}
		}
	}
}
#endif
