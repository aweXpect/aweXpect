using System.Collections.Generic;
using System.Threading;
using Testably.Expectations.Tests.TestHelpers;
// ReSharper disable PossibleMultipleEnumeration

namespace Testably.Expectations.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class NoneTests
	{
		[Fact]
		public async Task ConsidersCancellationToken()
		{
			using CancellationTokenSource cts = new();
			CancellationToken token = cts.Token;
			IEnumerable<int> subject = GetCancellingEnumerable(6, cts);

			async Task Act()
				=> await That(subject).Should().None().Satisfy(x => x < 0)
					.WithCancellation(token);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             none satisfy "x => x < 0",
				             but could not verify, because it was cancelled early
				             """);
		}

		[Fact]
		public async Task DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().None().Be(15)
					.And.None().Be(81);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().None().Be(5);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have no items equal to 5,
				             but at least one items were equal
				             """);
		}

		[Fact]
		public async Task WhenEnumerableContainsEqualValues_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().None().Be(1);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have no items equal to 1,
				             but at least one items were equal
				             """);
		}

		[Fact]
		public async Task WhenEnumerableIsEmpty_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([]);

			async Task Act()
				=> await That(subject).Should().None().Be(0);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableOnlyContainsDifferentValues_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().None().Be(42);

			await That(Act).Should().NotThrow();
		}
	}
}
