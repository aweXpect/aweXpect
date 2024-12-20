using System.Collections.Generic;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class BeEmpty
	{
		public sealed class Tests
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
					             but it was [
					               1,
					               1,
					               2,
					               3,
					               5,
					               8,
					               13,
					               21,
					               34,
					               55,
					               …
					             ]
					             """);
			}

			[Fact]
			public async Task WhenArrayContainsValues_ShouldFail()
			{
				string[] subject = ["foo"];

				async Task Act()
					=> await That(subject).Should().BeEmpty();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             be empty,
					             but it was [
					               "foo"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenArrayIsEmpty_ShouldSucceed()
			{
				string[] subject = [];

				async Task Act()
					=> await That(subject).Should().BeEmpty();

				await That(Act).Should().NotThrow();
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
					             but it was [
					               1,
					               1,
					               2
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldSucceed()
			{
				IEnumerable<int> subject = ToEnumerable((int[]) []);

				async Task Act()
					=> await That(subject).Should().BeEmpty();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
