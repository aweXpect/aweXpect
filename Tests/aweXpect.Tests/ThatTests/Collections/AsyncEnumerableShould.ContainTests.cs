#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;
using aweXpect.Tests.TestHelpers;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class ContainTests
	{
		[Fact]
		public async Task Item_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().Contain(1)
					.And.Contain(1);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Item_DoesNotMaterializeEnumerable()
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().Contain(5);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task Item_WhenEnumerableContainsExpectedValue_ShouldSucceed(
			List<int> values, int expected)
		{
			values.Add(expected);
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

			async Task Act()
				=> await That(subject).Should().Contain(expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task Item_WhenEnumerableDoesNotContainsExpectedValue_ShouldFail(
			int[] values, int expected)
		{
			while (values.Contains(expected))
			{
				expected++;
			}
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values);

			async Task Act()
				=> await That(subject).Should().Contain(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              contain {Formatter.Format(expected)},
				              but it was [{string.Join(", ", values.Select(s => Formatter.Format(s)))}]
				              """);
		}

		[Fact]
		public async Task Predicate_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().Contain(_ => true)
					.And.Contain(_ => true);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotMaterializeEnumerable()
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().Contain(x => x == 5);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task Predicate_WhenEnumerableContainsExpectedValue_ShouldSucceed(
			List<int> values, int expected)
		{
			values.Add(expected);
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

			async Task Act()
				=> await That(subject).Should().Contain(x => x == expected);

			await That(Act).Should().NotThrow();
		}

		[Theory]
		[AutoData]
		public async Task Predicate_WhenEnumerableDoesNotContainsExpectedValue_ShouldFail(
			int[] values, int expected)
		{
			while (values.Contains(expected))
			{
				expected++;
			}
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

			async Task Act()
				=> await That(subject).Should().Contain(x => x == expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage($"""
				              Expected subject to
				              contain item matching x => x == expected,
				              but it was [{string.Join(", ", values.Select(s => Formatter.Format(s)))}]
				              """);
		}
	}

	public sealed class NotContainTests
	{
		[Fact]
		public async Task Item_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().NotContain(0)
					.And.NotContain(0);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Item_DoesNotMaterializeEnumerable()
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

			async Task Act()
				=> await That(subject).Should().NotContain(5);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             not contain 5,
				             but it did
				             """);
		}

		[Theory]
		[AutoData]
		public async Task Item_WhenEnumerableContainsUnexpectedValue_ShouldFail(
			List<int> values, int unexpected)
		{
			values.Add(unexpected);
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

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
			int[] values, int unexpected)
		{
			while (values.Contains(unexpected))
			{
				unexpected++;
			}
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values);

			async Task Act()
				=> await That(subject).Should().NotContain(unexpected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotEnumerateTwice()
		{
			ThrowWhenIteratingTwiceAsyncEnumerable subject = new();

			async Task Act()
				=> await That(subject).Should().NotContain(x => x == 0)
					.And.NotContain(x => x == 0);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task Predicate_DoesNotMaterializeEnumerable()
		{
			IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

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
			List<int> values, int unexpected)
		{
			values.Add(unexpected);
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values.ToArray());

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
			int[] values, int unexpected)
		{
			while (values.Contains(unexpected))
			{
				unexpected++;
			}
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(values);

			async Task Act()
				=> await That(subject).Should().NotContain(x => x == unexpected);

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
