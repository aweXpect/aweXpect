#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class Has
	{
		public sealed class AtMost
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
						=> await That(subject).Has().AtMost(6).Items()
							.WithCancellation(token);

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have at most 6 items,
						             but could not verify, because it was cancelled early
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtMost(3).Items();

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsTooFewItems_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtMost(4).Items();

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsTooManyItems_ShouldFail()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtMost(2).Items();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have at most 2 items,
						             but found at least 3
						             """);
				}
			}
		}
	}
}
#endif
