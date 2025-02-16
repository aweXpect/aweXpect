﻿using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
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
					IEnumerable<int> subject = GetCancellingEnumerable(6, cts);

					async Task Act()
						=> await That(subject).None().AreEqualTo(8)
							.WithCancellation(token);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             is equal to 8 for no items,
						             but could not verify, because it was cancelled early
						             """);
				}

				[Fact]
				public async Task DoesNotEnumerateTwice()
				{
					ThrowWhenIteratingTwiceEnumerable subject = new();

					async Task Act()
						=> await That(subject).None().AreEqualTo(15)
							.And.None().AreEqualTo(81);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IEnumerable<int> subject = Factory.GetFibonacciNumbers();

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
					IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

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
					IEnumerable<int> subject = ToEnumerable((int[]) []);

					async Task Act()
						=> await That(subject).None().AreEqualTo(0);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableOnlyContainsDifferentValues_ShouldSucceed()
				{
					IEnumerable<int> subject = ToEnumerable([1, 1, 1, 1, 2, 2, 3]);

					async Task Act()
						=> await That(subject).None().AreEqualTo(42);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					IEnumerable<int>? subject = null;

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
					IEnumerable<string> subject = ToEnumerable(["FOO", "BAR", "BAZ"]);

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
					IEnumerable<string> subject = ToEnumerable(["foo", "bar", "baz"]);

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
					IEnumerable<string> subject = [];

					async Task Act()
						=> await That(subject).None().AreEqualTo("foo");

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableOnlyContainsDifferentValues_ShouldSucceed()
				{
					IEnumerable<string> subject = ToEnumerable(["FOO", "BAR", "BAZ"]);

					async Task Act()
						=> await That(subject).None().AreEqualTo("bar");

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenSubjectIsNull_ShouldFail()
				{
					IEnumerable<string>? subject = null;

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
