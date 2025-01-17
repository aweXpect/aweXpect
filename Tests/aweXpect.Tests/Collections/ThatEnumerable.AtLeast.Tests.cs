﻿using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed class AtLeast
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
					=> await That(subject).AtLeast(6).Satisfy(y => y < 6)
						.WithCancellation(token);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).AtLeast(0).Are(1)
						.And.AtLeast(0).Are(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).AtLeast(2).Are(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsEnoughEqualItems_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).AtLeast(3).Are(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooFewEqualItems_ShouldFail()
			{
				IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

				async Task Act()
					=> await That(subject).AtLeast(5).Are(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at least 5 items equal to 1,
					             but only 4 of 7 were
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).AtLeast(1).Are(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have at least one item equal to 0,
					             but it was <null>
					             """);
			}
		}
	}
}
