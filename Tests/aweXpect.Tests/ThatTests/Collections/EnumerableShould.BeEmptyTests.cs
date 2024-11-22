using System.Collections.Generic;
using Testably.Expectations.Tests.TestHelpers;
// ReSharper disable PossibleMultipleEnumeration

namespace Testably.Expectations.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class BeEmptyTests
	{
		[Fact]
		public async Task DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be empty,
				             but it was [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, …]
				             """);
		}

		[Fact]
		public async Task WhenEnumerableContainsValues_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 2]);

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             be empty,
				             but it was [1, 1, 2]
				             """);
		}

		[Fact]
		public async Task WhenEnumerableIsEmpty_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([]);

			async Task Act()
				=> await That(subject).Should().BeEmpty();

			await That(Act).Should().NotThrow();
		}
	}

	public sealed class NotBeEmptyTests
	{
		[Fact]
		public async Task DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().NotBeEmpty()
					.And.NotBeEmpty();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().NotBeEmpty();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableContainsValues_ShouldSucceed()
		{
			IEnumerable<int> subject = ToEnumerable([1, 1, 2]);

			async Task Act()
				=> await That(subject).Should().NotBeEmpty();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task WhenEnumerableIsEmpty_ShouldFail()
		{
			IEnumerable<int> subject = ToEnumerable([]);

			async Task Act()
				=> await That(subject).Should().NotBeEmpty();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not be empty,
				             but it was
				             """);
		}
	}
}
