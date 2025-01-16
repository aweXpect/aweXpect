using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
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
					IEnumerable<int> subject = GetCancellingEnumerable(4, cts);

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
				public async Task WhenArrayContainsMatchingItems_ShouldSucceed()
				{
					int[] subject = [1, 2, 3];

					async Task Act()
						=> await That(subject).Has().Between(3).And(6).Items();

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenArrayContainsTooFewItems_ShouldFail()
				{
					int[] subject = [1, 2];

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
				public async Task WhenArrayContainsTooManyItems_ShouldSucceed()
				{
					int[] subject = [1, 2, 3, 4, 5, 6, 7];

					async Task Act()
						=> await That(subject).Has().Between(3).And(6).Items();

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have between 3 and 6 items,
						             but found 7
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
				{
					IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

					async Task Act()
						=> await That(subject).Has().Between(3).And(6).Items();

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsTooFewItems_ShouldFail()
				{
					IEnumerable<int> subject = ToEnumerable([1, 2]);

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
					IEnumerable<int> subject = ToEnumerable([1, 2, 3, 4, 5, 6, 7]);

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
