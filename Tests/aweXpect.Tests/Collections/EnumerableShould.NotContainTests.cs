using System.Collections.Generic;
using System.Linq;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.Collections;

public sealed partial class EnumerableShould
{
	public sealed class NotContain
	{
		public sealed class ItemTests
		{
			[Fact]
			public async Task DoesNotEnumerateTwice()
			{
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().NotContain(0)
						.And.NotContain(0);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

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
				IEnumerable<MyClass> subject = Factory.GetFibonacciNumbers(20).Select(x => new MyClass
				{
					Value = x
				});
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
					               Inner = <null>,
					               Value = 5
					             },
					             but it did
					             """);
			}

			[Theory]
			[AutoData]
			public async Task WhenEnumerableContainsUnexpectedValue_ShouldFail(
				List<int> subject, int unexpected)
			{
				subject.Add(unexpected);

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
				int[] subject, int unexpected)
			{
				while (subject.Contains(unexpected))
				{
					unexpected++;
				}

				async Task Act()
					=> await That(subject).Should().NotContain(unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				int expected = 42;
				IEnumerable<int>? subject = null;

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
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<string> subject = Factory.GetFibonacciNumbers(x => $"item-{x}");

				async Task Act()
					=> await That(subject).Should().NotContain("item-5");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not contain "item-5",
					             but it did
					             """);
			}

			[Fact]
			public async Task ShouldSupportCasing()
			{
				string[] subject = ["FOO"];

				async Task Act()
					=> await That(subject).Should().NotContain("foo").IgnoringCase();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not contain "foo" ignoring case,
					             but it did
					             """);
			}

			[Fact]
			public async Task WhenEnumerableContainsUnexpectedValue_ShouldFail()
			{
				string[] subject = ["FOO", "foo"];

				async Task Act()
					=> await That(subject).Should().NotContain("foo");

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             not contain "foo",
					             but it did
					             """);
			}

			[Fact]
			public async Task WhenEnumerableDoesNotContainUnexpectedValue_ShouldSucceed()
			{
				string[] subject = ["FOO"];

				async Task Act()
					=> await That(subject).Should().NotContain("foo");

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				string expected = "foo";
				IEnumerable<string>? subject = null;

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
				ThrowWhenIteratingTwiceEnumerable subject = new();

				async Task Act()
					=> await That(subject).Should().NotContain(x => x == 0)
						.And.NotContain(x => x == 0);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task DoesNotMaterializeEnumerable()
			{
				IEnumerable<int> subject = Factory.GetFibonacciNumbers();

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
				List<int> subject, int unexpected)
			{
				subject.Add(unexpected);

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
				int[] subject, int unexpected)
			{
				while (subject.Contains(unexpected))
				{
					unexpected++;
				}

				async Task Act()
					=> await That(subject).Should().NotContain(x => x == unexpected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<string>? subject = null;

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
