#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class Has
	{
		// ReSharper disable once MemberHidesStaticFromOuterClass
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

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has at least 6 items,
						             but could not verify, because it was cancelled early
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtLeast(3).Items();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsTooFewItems_ShouldFail()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtLeast(4).Items();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has at least 4 items,
						             but found only 3
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsTooManyItems_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtLeast(2).Items();

					await That(Act).DoesNotThrow();
				}
			}
		}
	}
}
#endif
