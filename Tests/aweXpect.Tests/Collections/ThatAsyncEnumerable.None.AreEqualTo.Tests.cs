﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class None
	{
		public sealed class AreEqualTo
		{
			public sealed class Tests
			{
				[Fact]
				public async Task ConsidersCancellationToken()
				{
					using CancellationTokenSource cts = new();
					CancellationToken token = cts.Token;
					IAsyncEnumerable<int> subject = GetCancellingAsyncEnumerable(6, cts);

					async Task Act()
						=> await That(subject).None().AreEqualTo(8)
							.WithCancellation(token);

					await That(Act).Throws<InconclusiveException>()
						.WithMessage("""
						             Expected that subject
						             is equal to 8 for no items,
						             but could not verify, because it was already cancelled
						             """);
				}

				[Fact]
				public async Task DoesNotEnumerateTwice()
				{
					ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

					async Task Act()
						=> await That(subject).None().AreEqualTo(15)
							.And.None().AreEqualTo(81);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

					async Task Act()
						=> await That(subject).None().AreEqualTo(5);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to 5 for no items,
						             but at least one was
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsEqualValues_ShouldFail()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

					async Task Act()
						=> await That(subject).None().AreEqualTo(1);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to 1 for no items,
						             but at least one was
						             """);
				}

				[Fact]
				public async Task WhenEnumerableIsEmpty_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable((int[]) []);

					async Task Act()
						=> await That(subject).None().AreEqualTo(0);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableOnlyContainsDifferentValues_ShouldSucceed()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3]);

					async Task Act()
						=> await That(subject).None().AreEqualTo(42);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					IAsyncEnumerable<int>? subject = null;

					async Task Act()
						=> await That(subject).None().AreEqualTo(0);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to 0 for no items,
						             but it was <null>
						             """);
				}
			}

			public sealed class StringTests
			{
				[Fact]
				public async Task ShouldSupportIgnoringCase()
				{
					IAsyncEnumerable<string> subject = ToAsyncEnumerable(["FOO", "BAR", "BAZ"]);

					async Task Act()
						=> await That(subject).None().AreEqualTo("bar").IgnoringCase();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to "bar" ignoring case for no items,
						             but at least one was
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsEqualValues_ShouldFail()
				{
					IAsyncEnumerable<string> subject = ToAsyncEnumerable(["foo", "bar", "baz"]);

					async Task Act()
						=> await That(subject).None().AreEqualTo("bar");

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to "bar" for no items,
						             but at least one was
						             """);
				}

				[Fact]
				public async Task WhenEnumerableIsEmpty_ShouldSucceed()
				{
					IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());

					async Task Act()
						=> await That(subject).None().AreEqualTo("foo");

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableOnlyContainsDifferentValues_ShouldSucceed()
				{
					IAsyncEnumerable<string> subject = ToAsyncEnumerable(["FOO", "BAR", "BAZ"]);

					async Task Act()
						=> await That(subject).None().AreEqualTo("bar");

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					IAsyncEnumerable<string>? subject = null;

					async Task Act()
						=> await That(subject).None().AreEqualTo("");

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to "" for no items,
						             but it was <null>
						             """);
				}
			}
		}
	}
}
#endif
