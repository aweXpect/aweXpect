#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;

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
					             not contain 5,
					             but it did
					             """);
			}

			[Fact]
			public async Task ShouldSupportEquivalent()
			{
				IAsyncEnumerable<MyClass> subject = Factory.GetAsyncFibonacciNumbers(x => new MyClass(x), 20);
				MyClass unexpected = new()
				{
					Value = 5
				};

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected).Equivalent();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             not contain MyClass {
					               Value = 5
					             } equivalent,
					             but it did
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
					              not contain {Formatter.Format(unexpected)},
					              but it did
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
					             not contain 42,
					             but it was <null>
					             """);
			}
		}

		public sealed class StringItemTests
		{
			[Fact]
			public async Task WhenStringDiffersInCase_ShouldSucceed()
			{
				string[] values = ["a", "b", "c"];
				string unexpected = "A";

				IAsyncEnumerable<string> subject = ToAsyncEnumerable(values);

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenStringDiffersInCase_WhileIgnoringCase_ShouldFail()
			{
				string[] values = ["a", "b", "c"];
				string unexpected = "A";

				IAsyncEnumerable<string> subject = ToAsyncEnumerable(values);

				async Task Act()
					=> await That(subject).DoesNotContain(unexpected).IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             not contain "A" ignoring case,
					             but it did
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
					             not contain "foo",
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
					             not contain item matching x => x == 5,
					             but it did
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
					.WithMessage("""
					             Expected that subject
					             not contain item matching x => x == unexpected,
					             but it did
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
					             not contain item matching _ => true,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
