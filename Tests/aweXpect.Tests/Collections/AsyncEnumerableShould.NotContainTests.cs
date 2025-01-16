#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class AsyncEnumerableShould
{
	public sealed class NotContain
	{
		public sealed class ItemTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().NotContain(0)
						.And.NotContain(0);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().NotContain(5);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
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
					=> await That(subject).Should().NotContain(unexpected).Equivalent();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
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
					=> await That(subject).Should().NotContain(unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage($"""
					              Expected subject to
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
					=> await That(subject).Should().NotContain(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				int expected = 42;
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotContain(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
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
					=> await That(subject).Should().NotContain(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenStringDiffersInCase_WhileIgnoringCase_ShouldFail()
			{
				string[] values = ["a", "b", "c"];
				string unexpected = "A";

				IAsyncEnumerable<string> subject = ToAsyncEnumerable(values);

				async Task Act()
					=> await That(subject).Should().NotContain(unexpected).IgnoringCase();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
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
					=> await That(subject!).Should().NotContain(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
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
					=> await That(subject).Should().NotContain(x => x == 0)
						.And.NotContain(x => x == 0);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

				async Task Act()
					=> await That(subject).Should().NotContain(x => x == 5);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
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
					=> await That(subject).Should().NotContain(x => x == unexpected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
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
					=> await That(subject).Should().NotContain(x => x == unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject!).Should().NotContain(_ => true);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not contain item matching _ => true,
					             but it was <null>
					             """);
			}
		}
	}
}
#endif
