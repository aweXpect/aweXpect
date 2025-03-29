﻿#if NET8_0_OR_GREATER
using System.Collections.Immutable;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatImmutableArray
{
	public sealed partial class HasCount
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ConsidersCancellationToken()
			{
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;
				IEnumerable<int> subject = GetCancellingEnumerable(4, cts);

				async Task Act()
					=> await That(subject).HasCount(6)
						.WithCancellation(token);

				await That(Act).Throws<InconclusiveException>()
					.WithMessage("""
					             Expected that subject
					             has exactly 6 items,
					             but could not verify, because it was already cancelled
					             """);
			}

			[Fact]
			public async Task WhenArrayContainsMatchingItems_ShouldSucceed()
			{
				int[] subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).HasCount(3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenArrayContainsTooFewItems_ShouldFail()
			{
				int[] subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).HasCount(4);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has exactly 4 items,
					             but found only 3
					             """);
			}

			[Fact]
			public async Task WhenArrayContainsTooManyItems_ShouldFail()
			{
				int[] subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).HasCount(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has exactly 2 items,
					             but found 3
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).HasCount(3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewItems_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).HasCount(4);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has exactly 4 items,
					             but found only 3
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyItems_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).HasCount(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has exactly 2 items,
					             but found at least 3
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasCount(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has exactly 2 items,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
