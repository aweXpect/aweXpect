using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class None
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
					IEnumerable<int> subject = GetCancellingEnumerable(6, cts);

					async Task Act()
						=> await That(subject).None().Satisfy(item => item < 0)
							.WithCancellation(token);

					await That(Act).Throws<InconclusiveException>()
						.WithMessage("""
						             Expected that subject
						             satisfies item => item < 0 for no items,
						             but could not verify, because it was already cancelled
						             """);
				}

				[Fact]
				public async Task DoesNotEnumerateTwice()
				{
					ThrowWhenIteratingTwiceEnumerable subject = new();

					async Task Act()
						=> await That(subject).None().Satisfy(item => item == 15)
							.And.None().Satisfy(item => item == 81);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IEnumerable<int> subject = Factory.GetFibonacciNumbers();

					async Task Act()
						=> await That(subject).None().Satisfy(item => item == 5);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             satisfies item => item == 5 for no items,
						             but at least one did
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsEqualValues_ShouldFail()
				{
					IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3,]);

					async Task Act()
						=> await That(subject).None().Satisfy(item => item == 1);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             satisfies item => item == 1 for no items,
						             but at least one did
						             """);
				}

				[Fact]
				public async Task WhenEnumerableIsEmpty_ShouldSucceed()
				{
					IEnumerable<int> subject = ToEnumerable((int[]) []);

					async Task Act()
						=> await That(subject).None().Satisfy(item => item == 0);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableOnlyContainsDifferentValues_ShouldSucceed()
				{
					IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3,]);

					async Task Act()
						=> await That(subject).None().Satisfy(item => item == 42);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					IEnumerable<int>? subject = null;

					async Task Act()
						=> await That(subject).None().Satisfy(item => item == 0);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             satisfies item => item == 0 for no items,
						             but it was <null>
						             """);
				}
			}
		}
	}
}
