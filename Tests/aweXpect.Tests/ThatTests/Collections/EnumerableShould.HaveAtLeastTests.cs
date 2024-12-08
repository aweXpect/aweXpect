using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class HaveAtLeastTests
	{
		[Fact]
		public async Task ConsidersCancellationToken()
		{
			using CancellationTokenSource cts = new();
			CancellationToken token = cts.Token;
			IEnumerable<int> subject = GetCancellingEnumerable(6, cts);

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(6.Times(), x => x.Satisfy(y => y < 6))
					.WithCancellation(token);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(0.Times(), x => x.Be(1))
					.And.HaveAtLeast(0, x => x.Be(1));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(2.Times(), x => x.Be(1));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableContainsEnoughEqualItems_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(3.Times(), x => x.Be(1));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableContainsTooFewEqualItems_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtLeast(5.Times(), x => x.Be(1));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at least 5 items be equal to 1,
				             but only 4 of 7 were
				             """);
		}
	}
}
