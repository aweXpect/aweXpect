﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class All
	{
		public sealed class Satisfy
		{
			public sealed class Tests
			{
				[Fact]
				public async Task DoesNotEnumerateTwice()
				{
					ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

					async Task Act()
						=> await That(subject).All().Satisfy(x => x > 0)
							.And.All().Satisfy(x => x < 2);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task DoesNotMaterializeAsyncEnumerable()
				{
					IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

					async Task Act()
						=> await That(subject).All().Satisfy(x => x <= 1);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items satisfy x => x <= 1,
						             but not all did
						             """);
				}

				[Fact]
				public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
				{
					IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers(20);

					async Task Act()
						=> await That(subject).All().Satisfy(x => x is > 4 and < 6);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items satisfy x => x is > 4 and < 6,
						             but not all did
						             """);
				}

				[Fact]
				public async Task WhenNoItemsDiffer_ShouldSucceed()
				{
					int constantValue = 42;
					IAsyncEnumerable<int> subject = Factory.GetConstantValueAsyncEnumerable(constantValue, 20);

					async Task Act()
						=> await That(subject).All().Satisfy(x => x == constantValue);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					IAsyncEnumerable<string>? subject = null;

					async Task Act()
						=> await That(subject!).All().Satisfy(_ => true);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected subject to
						             have all items satisfy _ => true,
						             but it was <null>
						             """);
				}
			}
		}
	}
}
#endif
