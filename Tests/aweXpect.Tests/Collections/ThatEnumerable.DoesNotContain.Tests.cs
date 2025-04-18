using System.Collections.Generic;
using System.Linq;
using aweXpect.Equivalency;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed class DoesNotContain
	{
		public sealed class ItemTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).DoesNotContain(0)
						.And.DoesNotContain(0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).DoesNotContain(5);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain 5,
					             but it contained it at least 1 times in [
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
			public async Task ShouldSupportEquivalent()
			{
				IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(20).Select(x => new MyClass(x));
				MyClass unexpected = new(5);

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected).Equivalent();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain ThatEnumerable.MyClass {
					               Inner = <null>,
					               Value = 5
					             },
					             but it contained it at least 1 times in [
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 1
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 1
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 2
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 3
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 5
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 8
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 13
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 21
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 34
					               },
					               ThatEnumerable.MyClass {
					                 Inner = <null>,
					                 Value = 55
					               },
					               …
					             ]
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsUnexpectedValue_ShouldFail(
				List<int> subject, int unexpected)
			{
				subject.Add(unexpected);

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              does not contain {Formatter.Format(unexpected)},
					              but it contained it at least 1 times in {Formatter.Format(subject, FormattingOptions.MultipleLines)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableDoesNotContainsUnexpectedValue_ShouldSucceed(
				int[] subject, int unexpected)
			{
				while (subject.Contains(unexpected))
				{
					unexpected++;
				}

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				int expected = 42;
				IEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).DoesNotContain(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain 42,
					             but it was <null>
					             """);
			}
		}

		public sealed class StringItemTests
		{
			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<string> subject = Factory.GetFibonacciNumbers(x => $"item-{x}");

				async Task Act()
					=> await That(subject).DoesNotContain("item-5");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain "item-5",
					             but it contained it at least 1 times in [
					               "item-1",
					               "item-1",
					               "item-2",
					               "item-3",
					               "item-5",
					               "item-8",
					               "item-13",
					               "item-21",
					               "item-34",
					               "item-55",
					               …
					             ]
					             """);
			}

			[Fact]
			public async Task ShouldSupportCasing()
			{
				string[] subject = ["FOO",];

				async Task Act()
					=> await That(subject).DoesNotContain("foo").IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain "foo" ignoring case,
					             but it contained it at least 1 times in [
					               "FOO"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsUnexpectedValue_ShouldFail()
			{
				string[] subject = ["FOO", "foo",];

				async Task Act()
					=> await That(subject).DoesNotContain("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain "foo",
					             but it contained it at least 1 times in [
					               "FOO",
					               "foo"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenEnumerableDoesNotContainUnexpectedValue_ShouldSucceed()
			{
				string[] subject = ["FOO",];

				async Task Act()
					=> await That(subject).DoesNotContain("foo");

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				string expected = "foo";
				IEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).DoesNotContain(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain "foo",
					             but it was <null>
					             """);
			}
		}

		public sealed class PredicateTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).DoesNotContain(x => x == 0)
						.And.DoesNotContain(x => x == 0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

				async Task Act()
					=> await That(subject).DoesNotContain(x => x == 5);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain item matching x => x == 5,
					             but it contained it at least 1 times in [
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

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsUnexpectedValue_ShouldFail(
				List<int> subject, int unexpected)
			{
				subject.Add(unexpected);

				async Task Act()
					=> await That(subject).DoesNotContain(x => x == unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              does not contain item matching x => x == unexpected,
					              but it contained it at least 1 times in {Formatter.Format(subject, FormattingOptions.MultipleLines)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableDoesNotContainsUnexpectedValue_ShouldSucceed(
				int[] subject, int unexpected)
			{
				while (subject.Contains(unexpected))
				{
					unexpected++;
				}

				async Task Act()
					=> await That(subject).DoesNotContain(x => x == unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).DoesNotContain(_ => true);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain item matching _ => true,
					             but it was <null>
					             """);
			}
		}
	}
}
