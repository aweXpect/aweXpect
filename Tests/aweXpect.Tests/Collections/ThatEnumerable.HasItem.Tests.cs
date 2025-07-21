﻿using System.Collections.Generic;
using System.Linq;
using aweXpect.Equivalency;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class HasItem
	{
		public sealed class PredicateTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).HasItem(_ => true)
						.And.HasItem(_ => true).AtIndex(0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).HasItem(a => a == 5);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsDifferentItemAtGivenIndex_ShouldSucceed()
			{
				int[] subject = [0, 1, 2,];

				async Task Act()
					=> await That(subject).HasItem(_ => false).AtIndex(2);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              has item _ => false at index 2,
					              but it had item 2 at index 2

					              Collection:
					              {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenEnumerableContainsExpectedItemAtGivenIndex_ShouldSucceed()
			{
				int[] subject = [0, 1, 2,];

				async Task Act()
					=> await That(subject).HasItem(_ => true).AtIndex(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsNoItemAtGivenIndex_ShouldSucceed()
			{
				List<int> subject =
				[
					0,
					1,
					2,
				];

				async Task Act()
					=> await That(subject).HasItem(_ => true).AtIndex(3);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              has item _ => true at index 3,
					              but it did not contain any item at index 3

					              Collection:
					              {Formatter.Format(subject)}
					              """);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldFail()
			{
				List<int> subject = [];

				async Task Act()
					=> await That(subject).HasItem(_ => true);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item _ => true,
					             but it did not contain any item

					             Collection:
					             []
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithAnyIndex_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasItem(_ => true);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item _ => true,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithFixedIndex_ShouldFail()
			{
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasItem(_ => true).AtIndex(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item _ => true at index 0,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WithMultipleFailures_ShouldIncludeCollectionOnlyOnce()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);

				async Task Act()
					=> await That(subject).HasItem(_ => false).AtIndex(0).And.HasItem(_ => false).AtIndex(1).And
						.HasItem(_ => false)
						;

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item _ => false at index 0 and has item _ => false at index 1 and has item _ => false,
					             but it had item "a" at index 0 and it had item "b" at index 1 and it did not match at any index

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}
		}

		public sealed class ItemTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).HasItem(1)
						.And.HasItem(1).AtIndex(0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).HasItem(5);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsDifferentItemAtGivenIndex_ShouldSucceed(
				List<int> subject, int expected)
			{
				subject.Add(0);
				subject.Add(1);
				subject.Insert(2, expected);

				async Task Act()
					=> await That(subject).HasItem(expected - 1).AtIndex(2);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              has item {expected - 1} at index 2,
					              but it had item {expected} at index 2

					              Collection:
					              {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsExpectedItemAtGivenIndex_ShouldSucceed(
				List<int> subject, int expected)
			{
				subject.Add(0);
				subject.Add(1);
				subject.Insert(2, expected);

				async Task Act()
					=> await That(subject).HasItem(expected).AtIndex(2);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsNoItemAtGivenIndex_ShouldSucceed(int expected)
			{
				List<int> subject =
				[
					0,
					1,
					expected,
				];

				async Task Act()
					=> await That(subject).HasItem(expected).AtIndex(3);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              has item {expected} at index 3,
					              but it did not contain any item at index 3

					              Collection:
					              {Formatter.Format(subject)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableIsEmpty_ShouldFail(int expected)
			{
				List<int> subject = [];

				async Task Act()
					=> await That(subject).HasItem(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              has item {expected},
					              but it did not contain any item

					              Collection:
					              []
					              """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithAnyIndex_ShouldFail()
			{
				int expected = 42;
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasItem(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item 42,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithFixedIndex_ShouldFail()
			{
				int expected = 42;
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).HasItem(expected).AtIndex(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item 42 at index 0,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WithMultipleFailures_ShouldIncludeCollectionOnlyOnce()
			{
				IEnumerable<int> subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).HasItem(4).AtIndex(0).And.HasItem(5).AtIndex(1).And.HasItem(6)
						;

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item 4 at index 0 and has item 5 at index 1 and has item 6,
					             but it had item 1 at index 0 and it had item 2 at index 1 and it did not match at any index

					             Collection:
					             [1, 2, 3]
					             """);
			}
		}

		public sealed class StringItemTests
		{
			[Fact]
			public async Task AsPrefix_WhenItemDoesNotStartWithExpected_ShouldFail()
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("f").AsPrefix().AtIndex(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item starting with "f" at index 1,
					             but it had item "bar" at index 1

					             Collection:
					             [
					               "foo",
					               "bar",
					               "baz"
					             ]
					             """);
			}

			[Fact]
			public async Task AsPrefix_WhenItemStartsWithExpected_ShouldSucceed()
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("b").AsPrefix().AtIndex(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task AsRegex_WhenItemDoesNotMatch_ShouldFail()
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("f[aeiou]?o").AsRegex().AtIndex(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item matching regex "f[aeiou]?o" at index 1,
					             but it had item "bar" at index 1

					             Collection:
					             [
					               "foo",
					               "bar",
					               "baz"
					             ]
					             """);
			}

			[Fact]
			public async Task AsRegex_WhenItemMatches_ShouldSucceed()
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("b[aeiou]?r").AsRegex().AtIndex(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task AsSuffix_WhenItemDoesNotEndWithExpected_ShouldFail()
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("o").AsSuffix().AtIndex(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item ending with "o" at index 1,
					             but it had item "bar" at index 1

					             Collection:
					             [
					               "foo",
					               "bar",
					               "baz"
					             ]
					             """);
			}

			[Fact]
			public async Task AsSuffix_WhenItemEndsWithExpected_ShouldSucceed()
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("r").AsSuffix().AtIndex(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task AsWildcard_WhenItemDoesNotMatch_ShouldFail()
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("f*o").AsWildcard().AtIndex(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item matching "f*o" at index 1,
					             but it had item "bar" at index 1

					             Collection:
					             [
					               "foo",
					               "bar",
					               "baz"
					             ]
					             """);
			}

			[Fact]
			public async Task AsWildcard_WhenItemMatches_ShouldSucceed()
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("b?r").AsWildcard().AtIndex(1);

				await That(Act).DoesNotThrow();
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task ShouldSupportIgnoringCase(bool ignoreCase)
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("BAR").IgnoringCase(ignoreCase).AtIndex(1);

				await That(Act).Throws<XunitException>().OnlyIf(!ignoreCase)
					.WithMessage("""
					             Expected that subject
					             has item equal to "BAR" at index 1,
					             but it had item "bar" at index 1

					             Collection:
					             [
					               "foo",
					               "bar",
					               "baz"
					             ]
					             """);
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task ShouldSupportIgnoringLeadingWhiteSpace(bool ignoreLeadingWhiteSpace)
			{
				IEnumerable<string?> subject = [" foo", "\tbar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("bar").IgnoringLeadingWhiteSpace(ignoreLeadingWhiteSpace).AtIndex(1);

				await That(Act).Throws<XunitException>().OnlyIf(!ignoreLeadingWhiteSpace)
					.WithMessage("""
					             Expected that subject
					             has item equal to "bar" at index 1,
					             but it had item "\tbar" at index 1

					             Collection:
					             [
					               " foo",
					               "\tbar",
					               "baz"
					             ]
					             """);
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task ShouldSupportIgnoringNewlineStyle(bool ignoreNewlineStyle)
			{
				IEnumerable<string?> subject = ["a\nb", "c\nd", "e\nf",];

				async Task Act()
					=> await That(subject).HasItem("c\r\nd").IgnoringNewlineStyle(ignoreNewlineStyle).AtIndex(1);

				await That(Act).Throws<XunitException>().OnlyIf(!ignoreNewlineStyle)
					.WithMessage("""
					             Expected that subject
					             has item equal to "c\r\nd" at index 1,
					             but it had item "c\nd" at index 1

					             Collection:
					             [
					               "a\nb",
					               "c\nd",
					               "e\nf"
					             ]
					             """);
			}

			[Theory]
			[InlineData(true)]
			[InlineData(false)]
			public async Task ShouldSupportIgnoringTrailingWhiteSpace(bool ignoreTrailingWhiteSpace)
			{
				IEnumerable<string?> subject = ["foo ", "bar\t", "baz",];

				async Task Act()
					=> await That(subject).HasItem("bar").IgnoringTrailingWhiteSpace(ignoreTrailingWhiteSpace)
						.AtIndex(1);

				await That(Act).Throws<XunitException>().OnlyIf(!ignoreTrailingWhiteSpace)
					.WithMessage("""
					             Expected that subject
					             has item equal to "bar" at index 1,
					             but it had item "bar\t" at index 1

					             Collection:
					             [
					               "foo ",
					               "bar\t",
					               "baz"
					             ]
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsDifferentItemAtGivenIndex_ShouldSucceed(
				List<string> subject)
			{
				subject.Add("a");
				subject.Add("b");
				subject.Insert(2, "bar");

				async Task Act()
					=> await That(subject).HasItem("foo").AtIndex(2);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              has item equal to "foo" at index 2,
					              but it had item "bar" at index 2

					              Collection:
					              {Formatter.Format(subject, FormattingOptions.MultipleLines)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsExpectedItemAtGivenIndex_ShouldSucceed(
				List<string> subject)
			{
				subject.Add("a");
				subject.Add("b");
				subject.Insert(2, "bar");

				async Task Act()
					=> await That(subject).HasItem("bar").AtIndex(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEnumerableContainsNoItemAtGivenIndex_ShouldSucceed()
			{
				List<string> subject = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).HasItem("c").AtIndex(3);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item equal to "c" at index 3,
					             but it did not contain any item at index 3

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableIsEmpty_ShouldFail()
			{
				List<string?> subject = [];

				async Task Act()
					=> await That(subject).HasItem("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item equal to "foo",
					             but it did not contain any item

					             Collection:
					             []
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithAnyIndex_ShouldFail()
			{
				IEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject!).HasItem("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item equal to "foo",
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_WithFixedIndex_ShouldFail()
			{
				IEnumerable<string?>? subject = null;

				async Task Act()
					=> await That(subject!).HasItem("bar").AtIndex(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item equal to "bar" at index 0,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WithCustomStringComparer_WhenItemsDoNotMatch_ShouldFail()
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("BaR").Using(new IgnoreCaseForVocalsComparer()).AtIndex(1);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item equal to "BaR" using IgnoreCaseForVocalsComparer at index 1,
					             but it had item "bar" at index 1

					             Collection:
					             [
					               "foo",
					               "bar",
					               "baz"
					             ]
					             """);
			}

			[Fact]
			public async Task WithCustomStringComparer_WhenItemsMatch_ShouldSucceed()
			{
				IEnumerable<string?> subject = ["foo", "bar", "baz",];

				async Task Act()
					=> await That(subject).HasItem("bAr").Using(new IgnoreCaseForVocalsComparer()).AtIndex(1);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMultipleFailures_ShouldIncludeCollectionOnlyOnce()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);

				async Task Act()
					=> await That(subject).HasItem("d").AtIndex(0).And.HasItem("e").AtIndex(1).And.HasItem("f")
						;

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item equal to "d" at index 0 and has item equal to "e" at index 1 and has item equal to "f",
					             but it had item "a" at index 0 and it had item "b" at index 1 and it did not match at any index

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}
		}

		public sealed class EquivalentTests
		{
			[Fact]
			public async Task WhenEquivalentItemIsFound_ShouldSucceed()
			{
				IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(20).Select(x => new MyClass(x));
				MyClass expected = new(5);

				async Task Act()
					=> await That(subject).HasItem(expected).Equivalent();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenEquivalentItemIsNotFound_ShouldFail()
			{
				IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(20).Select(x => new MyClass(x));
				MyClass expected = new(4);

				async Task Act()
					=> await That(subject).HasItem(expected).Equivalent();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item MyClass {
					               StringValue = "",
					               Value = 4
					             } equivalent,
					             but it did not match at any index

					             Collection:
					             [
					               MyClass {
					                 StringValue = "",
					                 Value = 1
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 1
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 2
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 3
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 5
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 8
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 13
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 21
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 34
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 55
					               },
					               …
					             ]
					             """);
			}
		}

		public sealed class UsingTests
		{
			[Fact]
			public async Task WithAllDifferentComparer_ShouldFail()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).HasItem(1).Using(new AllDifferentComparer());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has item 1 using AllDifferentComparer,
					             but it did not match at any index

					             Collection:
					             [
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
			public async Task WithAllEqualComparer_ShouldSucceed()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers(20);

				async Task Act()
					=> await That(subject).HasItem(4).Using(new AllEqualComparer());

				await That(Act).DoesNotThrow();
			}
		}
	}
}
