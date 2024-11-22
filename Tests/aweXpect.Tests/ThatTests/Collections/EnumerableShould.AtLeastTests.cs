using System.Collections.Generic;
using System.Threading;
using Testably.Expectations.Tests.TestHelpers;
// ReSharper disable PossibleMultipleEnumeration

namespace Testably.Expectations.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class AtLeastTests
	{
		[Fact]
		public async Task ConsidersCancellationToken()
		{
			using CancellationTokenSource cts = new();
			CancellationToken token = cts.Token;
			IEnumerable<int> subject = GetCancellingEnumerable(6, cts);

			async Task Act()
				=> await That(subject).Should().AtLeast(6).Satisfy(x => x < 6)
					.WithCancellation(token);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().AtLeast(0).Be(1)
					.And.AtLeast(0).Be(1);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().AtLeast(2).Be(1);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableContainsEnoughEqualItems_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().AtLeast(3).Be(1);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableContainsTooFewEqualItems_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().AtLeast(5).Be(1);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have at least 5 items equal to 1,
				             but only 4 of 7 items were equal
				             """);
		}
	}
}
