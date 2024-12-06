using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class EnumerableShould
{
	public partial class Be
	{
		public sealed class AndLessTests
		{
			[Fact]
			public async Task AnyOrder_CompletelyDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 11);
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order,
					             but it was very different (> 20 deviations)
					             """);
			}

			[Fact]
			public async Task AnyOrder_VeryDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 10);
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order,
					             but it
					               contained item 1 at index 0 that was not expected and
					               contained item 2 at index 1 that was not expected and
					               contained item 3 at index 2 that was not expected and
					               contained item 4 at index 3 that was not expected and
					               contained item 5 at index 4 that was not expected and
					               contained item 6 at index 5 that was not expected and
					               contained item 7 at index 6 that was not expected and
					               contained item 8 at index 7 that was not expected and
					               contained item 9 at index 8 that was not expected and
					               contained item 10 at index 9 that was not expected
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithAdditionalAndMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithAdditionalItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithAdditionalItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrder_EmptyCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithCollectionInDifferentOrder_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order,
					             but it
					               contained item "c" at index 3 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order,
					             but it
					               contained item "c" at index 3 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesInExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithDuplicatesInSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order,
					             but it
					               contained item "a" at index 1 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrder_WithMissingItem_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrder_WithMissingItems_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().NotThrow();
			}


			[Fact]
			public async Task AnyOrder_WithSameCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_CompletelyDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 11);
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it was very different (> 20 deviations)
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_VeryDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 10);
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it
					               contained item 1 at index 0 that was not expected and
					               contained item 2 at index 1 that was not expected and
					               contained item 3 at index 2 that was not expected and
					               contained item 4 at index 3 that was not expected and
					               contained item 5 at index 4 that was not expected and
					               contained item 6 at index 5 that was not expected and
					               contained item 7 at index 6 that was not expected and
					               contained item 8 at index 7 that was not expected and
					               contained item 9 at index 8 that was not expected and
					               contained item 10 at index 9 that was not expected
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithAdditionalAndMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithAdditionalItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithAdditionalItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_EmptyCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b", "c", "a"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_EmptyCollectionWithDuplicatesInExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithCollectionInDifferentOrder_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesInExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithDuplicatesInSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithMissingItem_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithMissingItems_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task AnyOrderIgnoringDuplicates_WithSameCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().InAnyOrder().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less in any order ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrder_CompletelyDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 11);
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less,
					             but it was very different (> 20 deviations)
					             """);
			}

			[Fact]
			public async Task SameOrder_EmptyCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_VeryDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 10);
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less,
					             but it
					               contained item 1 at index 0 that was not expected and
					               contained item 2 at index 1 that was not expected and
					               contained item 3 at index 2 that was not expected and
					               contained item 4 at index 3 that was not expected and
					               contained item 5 at index 4 that was not expected and
					               contained item 6 at index 5 that was not expected and
					               contained item 7 at index 6 that was not expected and
					               contained item 8 at index 7 that was not expected and
					               contained item 9 at index 8 that was not expected and
					               contained item 10 at index 9 that was not expected
					             """);
			}

			[Fact]
			public async Task SameOrder_WithAdditionalAndMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less,
					             but it
					               contained item "d" at index 3 instead of "x" and
					               contained item "e" at index 4 instead of "y"
					             """);
			}

			[Fact]
			public async Task SameOrder_WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithAdditionalItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrder_WithAdditionalItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrder_WithCollectionInDifferentOrder_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c" and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less,
					             but it
					               contained item "c" at index 0 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less,
					             but it
					               contained item "c" at index 3 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesInExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithDuplicatesInSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less,
					             but it
					               contained item "a" at index 0 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrder_WithMissingItem_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrder_WithMissingItems_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().NotThrow();
			}


			[Fact]
			public async Task SameOrder_WithSameCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_CompletelyDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 11);
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it was very different (> 20 deviations)
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_EmptyCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_EmptyCollectionWithDuplicatesInExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_VeryDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 10);
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it
					               contained item 1 at index 0 that was not expected and
					               contained item 2 at index 1 that was not expected and
					               contained item 3 at index 2 that was not expected and
					               contained item 4 at index 3 that was not expected and
					               contained item 5 at index 4 that was not expected and
					               contained item 6 at index 5 that was not expected and
					               contained item 7 at index 6 that was not expected and
					               contained item 8 at index 7 that was not expected and
					               contained item 9 at index 8 that was not expected and
					               contained item 10 at index 9 that was not expected
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithAdditionalAndMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it
					               contained item "d" at index 3 instead of "x" and
					               contained item "e" at index 4 instead of "y"
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithAdditionalItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithAdditionalItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithCollectionInDifferentOrder_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c" and
					               contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesInExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithDuplicatesInSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it contained all expected items
					             """);
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithMissingItem_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithMissingItems_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().NotThrow();
			}

			[Fact]
			public async Task SameOrderIgnoringDuplicates_WithSameCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Should().Be(expected).AndLess().IgnoringDuplicates();

				await That(Act).Should().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             match collection expected and at least one item less ignoring duplicates,
					             but it contained all expected items
					             """);
			}
		}
	}
}
