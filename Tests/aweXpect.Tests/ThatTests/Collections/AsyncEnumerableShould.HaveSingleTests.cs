﻿#if NET6_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class HaveSingleTests
	{
		[Fact]
		public async Task ShouldReturnSingleItem()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([42]);

			int result = await That(subject).Should().HaveSingle();

			await That(result).Should().Be(42);
		}

		[Fact]
		public async Task WhenAsyncEnumerableContainsMoreThanOneElement_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveSingle();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have a single item,
				             but it contained more than one item
				             """);
		}

		[Fact]
		public async Task WhenAsyncEnumerableContainsSingleElement_ShouldSucceed()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1]);

			int result = await That(subject).Should().HaveSingle();

			await That(result).Should().Be(1);
		}

		[Fact]
		public async Task WhenAsyncEnumerableIsEmpty_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

			async Task Act()
				=> await That(subject).Should().HaveSingle();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have a single item,
				             but it was empty
				             """);
		}

		[Fact]
		public async Task Which_ShouldReturnSingleItem()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([42]);

			int result = await That(subject).Should().HaveSingle().Which.Should().BeGreaterThan(41).And.BeLessThan(43);

			await That(result).Should().Be(42);
		}

		[Fact]
		public async Task Which_WhenAsyncEnumerableContainsMoreThanOneElement_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([1, 2, 3]);

			async Task Act()
				=> await That(subject).Should().HaveSingle().Which.Should().BeGreaterThan(4);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have a single item which should be greater than 4,
				             but it contained more than one item
				             """);
		}

		[Fact]
		public async Task Which_WhenAsyncEnumerableIsEmpty_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Array.Empty<int>());

			async Task Act()
				=> await That(subject).Should().HaveSingle().Which.Should().BeGreaterThan(4);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have a single item which should be greater than 4,
				             but it was empty
				             """);
		}

		[Fact]
		public async Task Which_WhenSingleItemDoesNotSatisfyExpectation_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([3]);

			async Task Act()
				=> await That(subject).Should().HaveSingle().Which.Should().BeGreaterThan(4);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have a single item which should be greater than 4,
				             but it was 3
				             """);
		}

		[Fact]
		public async Task Which_WhenSingleItemSatisfiesExpectation_ShouldSucceed()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable([3]);

			async Task Act()
				=> await That(subject).Should().HaveSingle().Which.Should().BeGreaterThan(2);

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
