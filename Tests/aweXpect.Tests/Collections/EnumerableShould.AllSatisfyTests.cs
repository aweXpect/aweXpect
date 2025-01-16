using System.Collections.Generic;
using System.Linq;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class AllSatisfy
	{
		public sealed class Tests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().AllSatisfy(x => x > 0)
						.And.AllSatisfy(x => x < 2);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().AllSatisfy(x => x <= 1);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have all items satisfy x => x <= 1,
					             but it contained at least 10 other items: [
					               2,
					               3,
					               5,
					               8,
					               13,
					               21,
					               34,
					               55,
					               89,
					               144,
					               …
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
			{
				int[] subject = Factory.GetFibonacciNumbers(20).ToArray();

				async Task Act()
					=> await That(subject).Should().AllSatisfy(x => x is > 4 and < 6);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have all items satisfy x => x is > 4 and < 6,
					             but it contained at least 10 other items: [
					               1,
					               1,
					               2,
					               3,
					               8,
					               13,
					               21,
					               34,
					               55,
					               89,
					               …
					             ]
					             """);
			}

			[Fact]
			public async Task WhenNoItemsDiffer_ShouldSucceed()
			{
				int constantValue = 42;
				int[] subject = Factory.GetConstantValueEnumerable(constantValue, 20).ToArray();

				async Task Act()
					=> await That(subject).Should().AllSatisfy(x => x == constantValue);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject!).Should().AllSatisfy(_ => true);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             have all items satisfy _ => true,
					             but it was <null>
					             """);
			}
		}
	}
}
