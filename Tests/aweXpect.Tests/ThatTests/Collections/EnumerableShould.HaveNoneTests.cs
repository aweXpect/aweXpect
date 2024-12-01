using System.Collections.Generic;
using System.Threading;
using aweXpect.Tests.TestHelpers;
// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class HaveNoneTests
	{
		[Fact]
		public async Task ConsidersCancellationToken()
		{
			using CancellationTokenSource cts = new();
			CancellationToken token = cts.Token;
			IEnumerable<int> subject = GetCancellingEnumerable(6, cts);

			async Task Act()
				=> await That(subject).Should().HaveNone(x => x.Satisfy(y => y < 0))
					.WithCancellation(token);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have no items satisfy y => y < 0,
				             but could not verify, because it was cancelled early
				             """);
		}

		[Fact]
		public async Task DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().HaveNone(x => x.Be(15))
					.And.HaveNone(x => x.Be(81));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().HaveNone(x => x.Be(5));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have no items be equal to 5,
				             but at least one was
				             """);
		}

		[Fact]
		public async Task WhenEnumerableContainsEqualValues_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveNone(x => x.Be(1));

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have no items be equal to 1,
				             but at least one was
				             """);
		}

		[Fact]
		public async Task WhenEnumerableIsEmpty_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([]);

			async Task Act()
				=> await That(subject).Should().HaveNone(x => x.Be(0));

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableOnlyContainsDifferentValues_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveNone(x => x.Be(42));

			await That(Act).Should().NotThrow();
		}
	}
}
