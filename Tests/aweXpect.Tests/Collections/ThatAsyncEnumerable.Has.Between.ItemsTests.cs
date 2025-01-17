#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class Has
	{
		public sealed class Between
		{
			public sealed class ItemsTests
			{
				[Fact]
				public async Task ConsidersCancellationToken()
				{
					using CancellationTokenSource cts = new();
					CancellationToken token = cts.Token;
					IAsyncEnumerable<int> subject = GetCancellingAsyncEnumerable(6, cts, token);

					async Task Act()
						=> await That(subject).Has().Between(3).And(6).Items()
							.WithCancellation(token);

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have between 3 and 6 items,
						             but could not verify, because it was cancelled early
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().Between(3).And(6).Items();

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsTooFewItems_ShouldFail()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2]);

					async Task Act()
						=> await That(subject).Has().Between(3).And(6).Items();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have between 3 and 6 items,
						             but found only 2
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsTooManyItems_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3, 4, 5, 6, 7]);

					async Task Act()
						=> await That(subject).Has().Between(3).And(6).Items();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have between 3 and 6 items,
						             but found at least 7
						             """);
				}
			}
		}
	}
}
#endif
