using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
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
					IEnumerable<int> subject = GetCancellingEnumerable(4, cts);

					async Task Act()
						=> await That(subject).Has().AtMost(6).Items()
							.WithCancellation(token);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has at most 6 items,
						             but could not verify, because it was cancelled early
						             """);
				}

				[Fact]
				public async Task WhenArrayContainsMatchingItems_ShouldSucceed()
				{
					int[] subject = [1, 2, 3];

					async Task Act()
						=> await That(subject).Has().AtMost(3).Items();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenArrayContainsTooFewItems_ShouldSucceed()
				{
					int[] subject = [1, 2, 3];

					async Task Act()
						=> await That(subject).Has().AtMost(4).Items();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenArrayContainsTooManyItems_ShouldFail()
				{
					int[] subject = [1, 2, 3];

					async Task Act()
						=> await That(subject).Has().AtMost(2).Items();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has at most 2 items,
						             but found 3
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
				{
					IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtMost(3).Items();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsTooFewItems_ShouldSucceed()
				{
					IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtMost(4).Items();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsTooManyItems_ShouldFail()
				{
					IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().AtMost(2).Items();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has at most 2 items,
						             but found at least 3
						             """);
				}
			}
		}
	}
}
