﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Threading;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed class LessThan
	{
		public sealed class ItemsTests
		{
			[Fact]
			public async Task ConsidersCancellationToken()
			{
				using CancellationTokenSource cts = new();
				CancellationToken token = cts.Token;
				IAsyncEnumerable<int> subject =
					GetCancellingAsyncEnumerable(6, cts, CancellationToken.None);

				async Task Act()
					=> await That(subject).LessThan(8).Satisfy(y => y < 6)
						.WithCancellation(token);

				await That(Act).Throws<InconclusiveException>()
					.WithMessage("""
					             Expected that subject
					             satisfies y => y < 6 for less than 8 items,
					             but could not verify, because it was already cancelled
					             
					             Collection:
					             [0, 1, 2, 3, 4, 5, (… and maybe others)]
					             """);
			}

			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).LessThan(3).AreEqualTo(1)
						.And.LessThan(3).AreEqualTo(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).LessThan(2).AreEqualTo(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to 1 for less than 2 items,
					             but at least 2 were
					             
					             Matching items:
					             [1, 1, (… and maybe others)]
					             
					             Collection:
					             [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, (… and maybe others)]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsSufficientlyFewEqualItems_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3,]);

				async Task Act()
					=> await That(subject).LessThan(4).AreEqualTo(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyEqualItems_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 1, 1, 1, 2, 2, 3,]);

				async Task Act()
					=> await That(subject).LessThan(4).AreEqualTo(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to 1 for less than 4 items,
					             but 4 of 7 were
					             
					             Matching items:
					             [1, 1, 1, 1]
					             
					             Collection:
					             [1, 1, 1, 1, 2, 2, 3]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).LessThan(1).AreEqualTo(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to 0 for less than one item,
					             but it was <null>
					             """);
			}
		}

		public sealed class StringTests
		{
			[Fact]
			public async Task ShouldSupportIgnoringCase()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["foo", "FOO", "bar",]);

				async Task Act()
					=> await That(subject).LessThan(2).AreEqualTo("foo").IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to "foo" ignoring case for less than 2 items,
					             but 2 of 3 were
					             
					             Matching items:
					             [
					               "foo",
					               "FOO"
					             ]
					             
					             Collection:
					             [
					               "foo",
					               "FOO",
					               "bar"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsExpectedNumberOfEqualItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["foo", "foo", "bar",]);

				async Task Act()
					=> await That(subject).LessThan(2).AreEqualTo("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to "foo" for less than 2 items,
					             but 2 of 3 were
					             
					             Matching items:
					             [
					               "foo",
					               "foo"
					             ]
					             
					             Collection:
					             [
					               "foo",
					               "foo",
					               "bar"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsTooManyEqualItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["foo", "foo", "bar",]);

				async Task Act()
					=> await That(subject).LessThan(1).AreEqualTo("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to "foo" for less than one item,
					             but 2 of 3 were
					             
					             Matching items:
					             [
					               "foo",
					               "foo"
					             ]
					             
					             Collection:
					             [
					               "foo",
					               "foo",
					               "bar"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).LessThan(1).AreEqualTo("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is equal to "foo" for less than one item,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
