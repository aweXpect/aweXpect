#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class Has
	{
		public sealed class Exactly
		{
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
						=> await That(subject).Has().Exactly(6).Items()
							.WithCancellation(token);

					await That(Act).Throws<XunitException>()
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
						=> await That(subject).Has().Exactly(3).Items();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsTooFewItems_ShouldFail()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().Exactly(4).Items();

					await That(Act).Throws<XunitException>()
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
						=> await That(subject).Has().Exactly(2).Items();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have exactly 2 items,
						             but found at least 3
						             """);
				}
			}
		}
	}
}
#endif
