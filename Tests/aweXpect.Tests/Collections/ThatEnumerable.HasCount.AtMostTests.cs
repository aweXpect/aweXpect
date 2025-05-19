﻿using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class HasCount
	{
		public sealed class AtMostTests
		{
			[Fact]
			public async Task ConsidersCancellationToken()
			{
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;
				IEnumerable<int> subject = GetCancellingEnumerable(4, cts);

				async Task Act()
					=> await That(subject).HasCount().AtMost(6)
						.WithCancellation(token);

				await That(Act).Throws<InconclusiveException>()
					.WithMessage("""
					             Expected that subject
					             has at most 6 items,
					             but could not verify, because it was already cancelled
					             
					             Collection:
					             [
					               0,
					               1,
					               2,
					               3,
					               4,
					               5,
					               6,
					               7,
					               8,
					               9,
					               (… and maybe others)
					             ]
					             """);
			}

			[Fact]
			public async Task WhenArrayContainsMatchingItems_ShouldSucceed()
			{
				int[] subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).HasCount().AtMost(3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenArrayContainsTooFewItems_ShouldSucceed()
			{
				int[] subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).HasCount().AtMost(4);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenArrayContainsTooManyItems_ShouldFail()
			{
				int[] subject = [1, 2, 3,];

				async Task Act()
					=> await That(subject).HasCount().AtMost(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has at most 2 items,
					             but found 3
					             
					             Collection:
					             [1, 2, 3]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).HasCount().AtMost(3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).HasCount().AtMost(4);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyItems_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).HasCount().AtMost(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has at most 2 items,
					             but found at least 3
					             
					             Collection:
					             [
					               1,
					               2,
					               3,
					               (… and maybe others)
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasCount().AtMost(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has at most 2 items,
					             but it was <null>
					             """);
			}
		}
	}
}
