﻿using System.Collections.Generic;
using System.Linq;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class AllBeTests
	{
		[Fact]
		public async Task Item_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().AllBe(1)
					.And.AllBe(1);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Item_DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().AllBe(1);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have all items be equal to 1,
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
		public async Task Item_ShouldUseCustomComparer()
		{
			int[] subject = Factory.GetFibonacciNumbers(20).ToArray();

			async Task Act()
				=> await That(subject).Should().AllBe(5).Using(new AllEqualComparer());

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Item_WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
		{
			int[] subject = Factory.GetFibonacciNumbers(20).ToArray();

			async Task Act()
				=> await That(subject).Should().AllBe(5);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have all items be equal to 5,
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
		public async Task Item_WhenNoItemsDiffer_ShouldSucceed()
		{
			int constantValue = 42;
			int[] subject = Factory.GetConstantValueEnumerable(constantValue, 20).ToArray();

			async Task Act()
				=> await That(subject).Should().AllBe(constantValue);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().AllBe(x => x > 0)
					.And.AllBe(x => x < 2);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotMaterializeEnumerable()
		{
			IEnumerable<int> subject = Factory.GetFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().AllBe(x => x <= 1);

			await That(Act).Should().Throw<XunitException>()
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
		public async Task Predicate_WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
		{
			int[] subject = Factory.GetFibonacciNumbers(20).ToArray();

			async Task Act()
				=> await That(subject).Should().AllBe(x => x is > 4 and < 6);

			await That(Act).Should().Throw<XunitException>()
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
		public async Task Predicate_WhenNoItemsDiffer_ShouldSucceed()
		{
			int constantValue = 42;
			int[] subject = Factory.GetConstantValueEnumerable(constantValue, 20).ToArray();

			async Task Act()
				=> await That(subject).Should().AllBe(constantValue);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task String_DoesNotMaterializeEnumerable()
		{
			IEnumerable<string> subject = Factory.GetFibonacciNumbers(i => $"item-{i}");

			async Task Act()
				=> await That(subject).Should().AllBe("item-1");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have all items be equal to "item-1",
				             but it contained at least 10 other items: [
				               "item-2",
				               "item-3",
				               "item-5",
				               "item-8",
				               "item-13",
				               "item-21",
				               "item-34",
				               "item-55",
				               "item-89",
				               "item-144",
				               …
				             ]
				             """);
		}

		[Fact]
		public async Task String_ShouldUseCustomComparer()
		{
			string[] subject = Factory.GetFibonacciNumbers(i => $"item-{i}", 20).ToArray();

			async Task Act()
				=> await That(subject).Should().AllBe("item-5").Using(new AllEqualComparer());

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[InlineData(true)]
		[InlineData(false)]
		public async Task String_WhenItemsDifferInCase_ShouldSucceedWhenIgnoringCase(bool ignoreCase)
		{
			string[] subject = ["foo", "FOO"];

			async Task Act()
				=> await That(subject).Should().AllBe("foo").IgnoringCase(ignoreCase);

			await That(Act).Should().Throw<XunitException>().OnlyIf(!ignoreCase)
				.WithMessage("""
				             Expected subject to
				             have all items be equal to "foo",
				             but it contained 1 other item: [
				               "FOO"
				             ]
				             """);
		}

		[Fact]
		public async Task String_WhenItemsDiffer_ShouldFailAndDisplayNotMatchingItems()
		{
			string[] subject = Factory.GetFibonacciNumbers(i => $"item-{i}", 10).ToArray();

			async Task Act()
				=> await That(subject).Should().AllBe("item-5");

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             have all items be equal to "item-5",
				             but it contained 9 other items: [
				               "item-1",
				               "item-1",
				               "item-2",
				               "item-3",
				               "item-8",
				               "item-13",
				               "item-21",
				               "item-34",
				               "item-55"
				             ]
				             """);
		}

		[Fact]
		public async Task String_WhenNoItemsDiffer_ShouldSucceed()
		{
			string constantValue = "foo";
			string[] subject = Factory.GetConstantValueEnumerable(constantValue, 20).ToArray();

			async Task Act()
				=> await That(subject).Should().AllBe(constantValue);

			await That(Act).Should().NotThrow();
		}
	}
}
