using System.Collections.Generic;
using System.Linq;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class ContainTests
	{
		[Fact]
		public async Task Item_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().Contain(1)
					.And.Contain(1);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Item_DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().Contain(5);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, true)]
		[InlineData(2, true)]
		[InlineData(3, false)]
		public async Task Item_ShouldSupportAtLeast(int minimum, bool expectSuccess)
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(1).AtLeast(minimum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected subject to
				              contain 1 at least {minimum} times,
				              but it contained it 2 times in [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, …]
				              """);
		}

		[Theory]
		[InlineData(1, false)]
		[InlineData(2, true)]
		[InlineData(3, true)]
		public async Task Item_ShouldSupportAtMost(int maximum, bool expectSuccess)
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(1).AtMost(maximum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage("""
				             Expected subject to
				             contain 1 at most once,
				             but it contained it at least 2 times in [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, …]
				             """);
		}

		[Theory]
		[InlineData(1, 2, true)]
		[InlineData(2, 3, true)]
		[InlineData(3, 4, false)]
		public async Task Item_ShouldSupportBetween(int minimum, int maximum, bool expectSuccess)
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(1).Between(minimum).And(maximum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected subject to
				              contain 1 between {minimum} and {maximum} times,
				              but it contained it 2 times in [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, …]
				              """);
		}

		[Fact]
		public async Task Item_ShouldSupportEquivalent()
		{
			IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(20).Select(x => new MyClass { Value = x });
			MyClass expected = new() { Value = 1 };

			async Task Act()
				=> await That(subject).Should().Contain(expected).AtLeast(1).Equivalent();

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, false)]
		[InlineData(2, true)]
		[InlineData(3, false)]
		public async Task Item_ShouldSupportExactly(int times, bool expectSuccess)
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(1).Exactly(times);

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected subject to
				              contain 1 exactly {(times == 1 ? "once" : $"{times} times")},
				              but it contained it {(times == 1 ? "at least " : "")}2 times in [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, …]
				              """);
		}

		[Theory]
		[AutoData]
		public async Task Item_WhenEnumerableContainsExpectedValue_ShouldSucceed(
			List<int> subject, int expected)
		{
			subject.Add(expected);

			async Task Act()
				=> await That(subject).Should().Contain(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task Item_WhenEnumerableDoesNotContainsExpectedValue_ShouldFail(
			int[] subject, int expected)
		{
			while (subject.Contains(expected))
			{
				expected++;
			}

			async Task Act()
				=> await That(subject).Should().Contain(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              contain {Formatter.Format(expected)} at least once,
				              but it contained it 0 times in [{string.Join(", ", subject.Select(s => Formatter.Format(s)))}]
				              """);
		}

		[Fact]
		public async Task Predicate_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().Contain(_ => true)
					.And.Contain(_ => true);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 5);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(1, true)]
		[InlineData(2, true)]
		[InlineData(3, false)]
		public async Task Predicate_ShouldSupportAtLeast(int minimum, bool expectSuccess)
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 1).AtLeast(minimum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected subject to
				              contain item matching x => x == 1 at least {minimum} times,
				              but it contained it 2 times in [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, …]
				              """);
		}

		[Theory]
		[InlineData(1, false)]
		[InlineData(2, true)]
		[InlineData(3, true)]
		public async Task Predicate_ShouldSupportAtMost(int maximum, bool expectSuccess)
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 1).AtMost(maximum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage("""
				             Expected subject to
				             contain item matching x => x == 1 at most once,
				             but it contained it at least 2 times in [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, …]
				             """);
		}

		[Theory]
		[InlineData(1, 2, true)]
		[InlineData(2, 3, true)]
		[InlineData(3, 4, false)]
		public async Task Predicate_ShouldSupportBetween(int minimum, int maximum, bool expectSuccess)
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 1).Between(minimum).And(maximum.Times());

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected subject to
				              contain item matching x => x == 1 between {minimum} and {maximum} times,
				              but it contained it 2 times in [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, …]
				              """);
		}

		[Theory]
		[InlineData(1, false)]
		[InlineData(2, true)]
		[InlineData(3, false)]
		public async Task Predicate_ShouldSupportExactly(int times, bool expectSuccess)
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 1).Exactly(times);

			await That(Act).Should().Throw<XunitException>().OnlyIf(!expectSuccess)
				.WithMessage($"""
				              Expected subject to
				              contain item matching x => x == 1 exactly {(times == 1 ? "once" : $"{times} times")},
				              but it contained it {(times == 1 ? "at least " : "")}2 times in [1, 1, 2, 3, 5, 8, 13, 21, 34, 55, …]
				              """);
		}

		[Theory]
		[AutoData]
		public async Task Predicate_WhenEnumerableContainsExpectedValue_ShouldSucceed(
			List<int> subject, int expected)
		{
			subject.Add(expected);

			async Task Act()
				=> await That(subject).Should().Contain(x => x == expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task Predicate_WhenEnumerableDoesNotContainsExpectedValue_ShouldFail(
			int[] subject, int expected)
		{
			while (subject.Contains(expected))
			{
				expected++;
			}

			async Task Act()
				=> await That(subject).Should().Contain(x => x == expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              contain item matching x => x == expected at least once,
				              but it contained it 0 times in [{string.Join(", ", subject.Select(s => Formatter.Format(s)))}]
				              """);
		}
	}

	public sealed class NotContainTests
	{
		[Fact]
		public async Task Item_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().NotContain(0)
					.And.NotContain(0);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Item_DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().NotContain(5);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain 5,
				             but it did
				             """);
		}

		[Fact]
		public async Task Item_ShouldSupportEquivalent()
		{
			IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(20).Select(x => new MyClass { Value = x });
			MyClass unexpected = new() { Value = 5 };

			async Task Act()
				=> await That(subject).Should().NotContain(unexpected).Equivalent();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain MyClass {
				               Inner = <null>,
				               Value = 5
				             },
				             but it did
				             """);
		}

		[Theory]
		[AutoData]
		public async Task Item_WhenEnumerableContainsUnexpectedValue_ShouldFail(
			List<int> subject, int unexpected)
		{
			subject.Add(unexpected);

			async Task Act()
				=> await That(subject).Should().NotContain(unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              not contain {Formatter.Format(unexpected)},
				              but it did
				              """);
		}

		[Theory]
		[AutoData]
		public async Task Item_WhenEnumerableDoesNotContainsUnexpectedValue_ShouldSucceed(
			int[] subject, int unexpected)
		{
			while (subject.Contains(unexpected))
			{
				unexpected++;
			}

			async Task Act()
				=> await That(subject).Should().NotContain(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().NotContain(x => x == 0)
					.And.NotContain(x => x == 0);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().NotContain(x => x == 5);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain item matching x => x == 5,
				             but it did
				             """);
		}

		[Theory]
		[AutoData]
		public async Task Predicate_WhenEnumerableContainsUnexpectedValue_ShouldFail(
			List<int> subject, int unexpected)
		{
			subject.Add(unexpected);

			async Task Act()
				=> await That(subject).Should().NotContain(x => x == unexpected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain item matching x => x == unexpected,
				             but it did
				             """);
		}

		[Theory]
		[AutoData]
		public async Task Predicate_WhenEnumerableDoesNotContainsUnexpectedValue_ShouldSucceed(
			int[] subject, int unexpected)
		{
			while (subject.Contains(unexpected))
			{
				unexpected++;
			}

			async Task Act()
				=> await That(subject).Should().NotContain(x => x == unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
