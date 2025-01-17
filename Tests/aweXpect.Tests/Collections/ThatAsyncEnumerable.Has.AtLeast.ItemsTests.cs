#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class Has
	{
		public sealed class AtLeast
		{
			public sealed class ItemsTests
			{
				[Fact]
				public async Task ConsidersCancellationToken()
				{
					using CancellationTokenSource cts = new();
					CancellationToken token = cts.Token;
					IAsyncEnumerable<int> subject =
						GetCancellingAsyncEnumerable(4, cts, CancellationToken.None);

					async Task Act()
						=> await That(subject).Has().AtLeast(6).Items()
							.WithCancellation(token);

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have at least 6 items,
						             but could not verify, because it was cancelled early
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtLeast(3).Items();

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsTooFewItems_ShouldFail()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtLeast(4).Items();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have at least 4 items,
						             but found only 3
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsTooManyItems_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtLeast(2).Items();

					await That(Act).Does().NotThrow();
				}
			}
		}
	}
}
#endif
