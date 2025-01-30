using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed class AtMost
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ConsidersCancellationToken()
			{
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;
				IEnumerable<int> subject = GetCancellingEnumerable(6, cts);

				async Task Act()
					=> await That(subject).AtMost(8).Satisfy(y => y < 6)
						.WithCancellation(token);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at most 8 items satisfy y => y < 6,
					             but could not verify, because it was cancelled early
					             """);
			}

			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).AtMost(3).Are(1)
						.And.AtMost(3).Are(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).AtMost(1).Are(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at most one item equal to 1,
					             but at least 2 were
					             """);
			}

			[Fact]
			public async Task WhenArrayContainsSufficientlyFewEqualItems_ShouldSucceed()
			{
				int[] subject = [1, 1, 1, 1, 2, 2, 3];

				async Task Act()
					=> await That(subject).AtMost(3).Are(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenArrayContainsTooManyEqualItems_ShouldFail()
			{
				int[] subject = [1, 1, 1, 1, 2, 2, 3];

				async Task Act()
					=> await That(subject).AtMost(3).Are(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at most 3 items equal to 1,
					             but 4 of 7 were
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsSufficientlyFewEqualItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).AtMost(3).Are(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyEqualItems_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).AtMost(3).Are(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at most 3 items equal to 1,
					             but at least 4 were
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).AtMost(1).Are(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at most one item equal to 0,
					             but it was <null>
					             """);
			}
		}
	}
}
