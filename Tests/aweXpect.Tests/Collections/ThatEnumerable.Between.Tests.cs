using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed class Between
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
					=> await That(subject).Between(6).And(8).Satisfy(y => y < 6)
						.WithCancellation(token);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have between 6 and 8 items satisfy y => y < 6,
					             but could not verify, because it was cancelled early
					             """);
			}

			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).Between(0).And(2).Are(1)
						.And.Between(0).And(1).Are(1);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).Between(0).And(1).Are(1);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have between 0 and 1 items equal to 1,
					             but at least 2 were
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsSufficientlyEqualItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Between(3).And(4).Are(1);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewEqualItems_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Between(3).And(4).Are(2);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have between 3 and 4 items equal to 2,
					             but only 2 of 7 were
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyEqualItems_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).Between(1).And(3).Are(1);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have between 1 and 3 items equal to 1,
					             but at least 4 were
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Between(0).And(1).Are(0);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have between 0 and 1 items equal to 0,
					             but it was <null>
					             """);
			}
		}
	}
}
