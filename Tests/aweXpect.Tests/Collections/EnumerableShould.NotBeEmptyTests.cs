using System.Collections.Generic;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class NotBeEmpty
	{
		public sealed class Tests
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
			public async Task WhenArrayContainsValues_ShouldSucceed()
			{
				string[] subject = ["foo"];

				async Task Act()
					=> await That(subject).Should().NotBeEmpty();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task WhenArrayIsEmpty_ShouldFail()
			{
				string[] subject = [];

				async Task Act()
					=> await That(subject).Should().NotBeEmpty();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not be empty,
					             but it was
					             """);
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
				IEnumerable<int> subject = ToEnumerable((int[]) []);

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
}
