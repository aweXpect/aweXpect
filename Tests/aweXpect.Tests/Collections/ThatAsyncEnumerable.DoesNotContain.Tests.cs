#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;
using aweXpect.Equivalency;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed class DoesNotContain
	{
		public sealed class ItemTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).DoesNotContain(0)
						.And.DoesNotContain(0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

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
				IAsyncEnumerable<MyClass> subject = Factory.GetAsyncFibonacciNumbers(x => new MyClass(x), 20);
				MyClass unexpected = new(5);

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected).Equivalent();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain MyClass {
					               StringValue = "",
					               Value = 5
					             } equivalent,
					             but it contained it at least 1 times in [
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

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsUnexpectedValue_ShouldFail(
				List<int> values, int unexpected)
			{
				values.Add(unexpected);
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              does not contain {Formatter.Format(unexpected)},
					              but it contained it 1 times in {Formatter.Format(values, FormattingOptions.MultipleLines)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableDoesNotContainsUnexpectedValue_ShouldSucceed(
				int[] values, int unexpected)
			{
				while (values.Contains(unexpected))
				{
					unexpected++;
				}

				IAsyncEnumerable<int> subject = ToAsyncEnumerable(values);

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				int expected = 42;
				IAsyncEnumerable<int>? subject = null;

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
			public async Task WhenStringDiffersInCase_ShouldSucceed()
			{
				string[] values = ["a", "b", "c",];
				string unexpected = "A";

				IAsyncEnumerable<string> subject = ToAsyncEnumerable(values);

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenStringDiffersInCase_WhileIgnoringCase_ShouldFail()
			{
				string[] values = ["a", "b", "c",];
				string unexpected = "A";

				IAsyncEnumerable<string> subject = ToAsyncEnumerable(values);

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected).IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             does not contain "A" ignoring case,
					             but it contained it 1 times in [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				string expected = "foo";
				IAsyncEnumerable<string>? subject = null;

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
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).DoesNotContain(x => x == 0)
						.And.DoesNotContain(x => x == 0);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

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
				List<int> values, int unexpected)
			{
				values.Add(unexpected);
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

				async Task Act()
					=> await That(subject).DoesNotContain(x => x == unexpected);

				await That(Act).Throws<XunitException>()
					.WithMessage($"""
					              Expected that subject
					              does not contain item matching x => x == unexpected,
					              but it contained it 1 times in {Formatter.Format(values, FormattingOptions.MultipleLines)}
					              """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableDoesNotContainsUnexpectedValue_ShouldSucceed(
				int[] values, int unexpected)
			{
				while (values.Contains(unexpected))
				{
					unexpected++;
				}

				IAsyncEnumerable<int> subject = ToAsyncEnumerable(values);

				async Task Act()
					=> await That(subject).DoesNotContain(x => x == unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<string>? subject = null;

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
#endif
