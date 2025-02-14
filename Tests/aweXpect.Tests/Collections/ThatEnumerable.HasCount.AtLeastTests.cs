﻿using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class HasCount
	{
		public sealed class AtLeastTests
		{
			[Fact]
			public async Task ConsidersCancellationToken()
			{
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;
				IEnumerable<int> subject = GetCancellingEnumerable(4, cts);

				async Task Act()
					=> await That(subject).HasCount().AtLeast(6)
						.WithCancellation(token);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has at least 6 items,
					             but could not verify, because it was cancelled early
					             """);
			}

			[Fact]
			public async Task WhenArrayContainsMatchingItems_ShouldSucceed()
			{
				int[] subject = [1, 2, 3];

				async Task Act()
					=> await That(subject).HasCount().AtLeast(3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenArrayContainsTooFewItems_ShouldFail()
			{
				int[] subject = [1, 2, 3];

				async Task Act()
					=> await That(subject).HasCount().AtLeast(4);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has at least 4 items,
					             but found only 3
					             """);
			}

			[Fact]
			public async Task WhenArrayContainsTooManyItems_ShouldSucceed()
			{
				int[] subject = [1, 2, 3];

				async Task Act()
					=> await That(subject).HasCount().AtLeast(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsMatchingItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).HasCount().AtLeast(3);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewItems_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).HasCount().AtLeast(4);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has at least 4 items,
					             but found only 3
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3]);

				async Task Act()
					=> await That(subject).HasCount().AtLeast(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasCount().AtLeast(2);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has at least 2 items,
					             but it was <null>
					             """);
			}
		}
	}
}
