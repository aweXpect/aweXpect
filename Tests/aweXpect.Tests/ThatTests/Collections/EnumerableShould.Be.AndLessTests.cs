using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public partial class Be
	{
		public sealed class AndLessTests
		{
			[Fact]
			public async Task AnyOrder_WithCollectionInDifferentOrder_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithSameCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesInSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected in any order,
					             but it contained item "a" at index 1 that was not expected
					             """);
			}

			[Fact]
			public async Task AnyOrder_EmptyCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected in any order,
					             but it lacked 3 of 3 expected items: [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesInExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected in any order,
					             but it lacked 1 of 4 expected items: ["a"]
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected in any order,
					             but it contained item "c" at index 3 that was not expected
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected in any order,
					             but it lacked 1 of 4 expected items: ["c"]
					             """);
			}


			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithCollectionInDifferentOrder_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithSameCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesInSubject_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_EmptyCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b", "c", "a"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected in any order ignoring duplicates,
					             but it lacked 3 of 3 expected items: [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_EmptyCollectionWithDuplicatesInExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected in any order ignoring duplicates,
					             but it lacked 2 of 2 expected items: [
					               "a",
					               "b"
					             ]
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesInExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithCollectionInDifferentOrder_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected,
					             but it contained
					               item "c" at index 1 instead of "b" and
					               item "b" at index 2 instead of "c"
					             """);
			}

			[Fact]
			public async Task SameOrder_WithAdditionalItem_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithSameCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesInSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected,
					             but it contained
					               item "a" at index 1 instead of "b" and
					               item "b" at index 2 instead of "c" and
					               item "c" at index 3 that was not expected
					             """);
			}

			[Fact]
			public async Task SameOrder_EmptyCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected,
					             but it lacked 3 of 3 expected items: [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesInExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected);

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected,
					             but it contained
					               item "b" at index 1 instead of "a" and
					               item "c" at index 2 instead of "b"
					             """);
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected,
					             but it contained item "c" at index 3 that was not expected
					             """);
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected,
					             but it lacked 1 of 4 expected items: ["c"]
					             """);
			}


			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithCollectionInDifferentOrder_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected ignoring duplicates,
					             but it contained item "c" at index 1 instead of "b"
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithSameCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesInSubject_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_EmptyCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected ignoring duplicates,
					             but it lacked 3 of 3 expected items: [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_EmptyCollectionWithDuplicatesInExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected ignoring duplicates,
					             but it lacked 2 of 2 expected items: [
					               "a",
					               "b"
					             ]
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesInExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}
		}
	}
}
