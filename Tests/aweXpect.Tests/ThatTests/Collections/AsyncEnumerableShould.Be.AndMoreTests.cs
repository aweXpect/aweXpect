#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public partial class Be
	{
		public sealed class AndMoreTests
		{
			[Fact]
			public async Task AnyOrder_CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order,
					             but it was very different (> 20 deviations)
					             """);
			}

			[Fact]
			public async Task AnyOrder_VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order,
					             but it lacked 10 of 10 expected items:
					               101,
					               102,
					               103,
					               104,
					               105,
					               106,
					               107,
					               108,
					               109,
					               110
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order,
					             but it lacked 3 of 6 expected items:
					               "x",
					               "y",
					               "z"
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order,
					             but it
					               did not contain any additional items and
					               lacked 3 of 3 expected items:
					                 "a",
					                 "b",
					                 "c"
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesAtBeginOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "c"
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "a"
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesInSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order,
					             but it
					               did not contain any additional items and
					               lacked 2 of 5 expected items:
					                 "d",
					                 "e"
					             """);
			}


			[Fact]
			public async Task AnyOrder_WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it was very different (> 20 deviations)
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it lacked 10 of 10 expected items:
					               101,
					               102,
					               103,
					               104,
					               105,
					               106,
					               107,
					               108,
					               109,
					               110
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it lacked 3 of 6 expected items:
					               "x",
					               "y",
					               "z"
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b", "c", "a"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 3 of 3 expected items:
					                 "a",
					                 "b",
					                 "c"
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_EmptyCollectionWithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 2 of 2 expected items:
					                 "a",
					                 "b"
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesInSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 2 of 5 expected items:
					                 "d",
					                 "e"
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task SameOrder_CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item,
					             but it was very different (> 20 deviations)
					             """);
			}

			[Fact]
			public async Task SameOrder_EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item,
					             but it
					               did not contain any additional items and
					               lacked 3 of 3 expected items:
					                 "a",
					                 "b",
					                 "c"
					             """);
			}

			[Fact]
			public async Task SameOrder_VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item,
					             but it lacked 10 of 10 expected items:
					               101,
					               102,
					               103,
					               104,
					               105,
					               106,
					               107,
					               108,
					               109,
					               110
					             """);
			}

			[Fact]
			public async Task SameOrder_WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item,
					             but it
					               did not contain any additional items and
					               lacked 3 of 6 expected items:
					                 "x",
					                 "y",
					                 "z"
					             """);
			}

			[Fact]
			public async Task SameOrder_WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c" and
					               did not contain any additional items
					             """);
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesAtBeginOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "c"
					             """);
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item,
					             but it
					               contained item "b" at index 1 instead of "a" and
					               contained item "c" at index 2 instead of "b" and
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "a"
					             """);
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesInSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task SameOrder_WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item,
					             but it
					               did not contain any additional items and
					               lacked 2 of 5 expected items:
					                 "d",
					                 "e"
					             """);
			}


			[Fact]
			public async Task SameOrder_WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
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
			public async Task SameOrderIgnoringDuplicates_CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it was very different (> 20 deviations)
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 3 of 3 expected items:
					                 "a",
					                 "b",
					                 "c"
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_EmptyCollectionWithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 2 of 2 expected items:
					                 "a",
					                 "b"
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it lacked 10 of 10 expected items:
					               101,
					               102,
					               103,
					               104,
					               105,
					               106,
					               107,
					               108,
					               109,
					               110
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it lacked 3 of 6 expected items:
					               "x",
					               "y",
					               "z"
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c" and
					               did not contain any additional items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesInSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 2 of 5 expected items:
					                 "d",
					                 "e"
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndMore().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one more item ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}
		}
	}
}
#endif
