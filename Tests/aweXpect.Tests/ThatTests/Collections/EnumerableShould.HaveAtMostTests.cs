using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;
// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class HaveAtMostTests
	{
		[Fact]
		public async Task ConsidersCancellationToken()
		{
			using CancellationTokenSource cts = new();
			CancellationToken token = cts.Token;
			IEnumerable<int> subject = GetCancellingEnumerable(6, cts);

			async Task Act()
				=> await That(subject).Should().HaveAtMost(8.Times(), x => x.Satisfy(y => y < 6))
					.WithCancellation(token);

			await That(Act).Should().Throw<XunitException>()
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
				=> await That(subject).Should().HaveAtMost(3.Times(), x => x.Be(1))
					.And.HaveAtMost(3, x => x.Be(1));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().HaveAtMost(1.Times(), x => x.Be(1));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most one item be equal to 1,
				             but at least 2 were
				             """);
		}

		[Fact]
		public async Task WhenEnumerableContainsSufficientlyFewEqualItems_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtMost(3.Times(), x => x.Be(2));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableContainsTooManyEqualItems_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveAtMost(3.Times(), x => x.Be(1));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at most 3 items be equal to 1,
				             but at least 4 were
				             """);
		}
	}
}
