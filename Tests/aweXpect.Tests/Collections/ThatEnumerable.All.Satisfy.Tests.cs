using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class All
	{
		public sealed class Satisfy
		{
			public sealed class Tests
			{
				[Fact]
				public async Task ConsidersCancellationToken()
				{
					using CancellationTokenSource cts = new();
					CancellationToken token = cts.Token;
					IEnumerable<int> subject = GetCancellingEnumerable(5, cts);

					async Task Act()
						=> await That(subject).All().Satisfy(x => x < 6).WithCancellation(token);

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items satisfy x => x < 6,
						             but could not verify, because it was cancelled early
						             """);
				}

				[Fact]
				public async Task DoesNotEnumerateTwice()
				{
					ThrowWhenIteratingTwiceEnumerable subject = new();

					async Task Act()
						=> await That(subject).All().Satisfy(_ => true)
							.And.All().Satisfy(_ => true);

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IEnumerable<int> subject = Factory.GetFibonacciNumbers();

					async Task Act()
						=> await That(subject).All().Satisfy(x => x == 1);

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items satisfy x => x == 1,
						             but not all did
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsDifferentValues_ShouldFail()
				{
					int[] subject = [1, 1, 1, 1, 2, 2, 3];

					async Task Act()
						=> await That(subject).All().Satisfy(x => x == 1);

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items satisfy x => x == 1,
						             but only 4 of 7 did
						             """);
				}

				[Fact]
				public async Task WhenEnumerableIsEmpty_ShouldSucceed()
				{
					IEnumerable<int> subject = ToEnumerable((int[]) []);

					async Task Act()
						=> await That(subject).All().Satisfy(x => x == 0);

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenEnumerableOnlyContainsEqualValues_ShouldSucceed()
				{
					IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 1, 1, 1]);

					async Task Act()
						=> await That(subject).All().Satisfy(x => x == 1);

					await That(Act).Does().NotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					IEnumerable<int>? subject = null;

					async Task Act()
						=> await That(subject!).All().Satisfy(x => x == 0);

					await That(Act).Does().Throw<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items satisfy x => x == 0,
						             but it was <null>
						             """);
				}
			}
		}
	}
}
