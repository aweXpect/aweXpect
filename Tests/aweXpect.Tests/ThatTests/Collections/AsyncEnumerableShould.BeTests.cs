#if NET6_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests.ThatTests.Collections;

public sealed partial class AsyncEnumerableShould
{
	public sealed class BeTests
	{
		[Fact]
		public async Task AnyOrder_CollectionWithMoreThan20Deviations_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 21));

			async Task Act()
				=> await That(subject).Should().Be([]).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection [] in any order,
				             but it was completely different: [
				               1,
				               2,
				               3,
				               4,
				               5,
				               6,
				               7,
				               8,
				               9,
				               10,
				               …
				             ] had more than 20 deviations compared to []
				             """);
		}

		[Fact]
		public async Task AnyOrder_CompletelyDifferentCollections_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
			IEnumerable<int> expected = Enumerable.Range(100, 11);

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it was completely different: [
				               1,
				               2,
				               3,
				               4,
				               5,
				               6,
				               7,
				               8,
				               9,
				               10,
				               …
				             ] had more than 20 deviations compared to [
				               100,
				               101,
				               102,
				               103,
				               104,
				               105,
				               106,
				               107,
				               108,
				               109,
				               …
				             ]
				             """);
		}

		[Fact]
		public async Task AnyOrder_VeryDifferentCollections_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
			IEnumerable<int> expected = Enumerable.Range(101, 10);

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
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
				               contained item 10 at index 9 that was not expected and
				               lacked 10 of 10 expected items:
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
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it
				               contained item "d" at index 3 that was not expected and
				               contained item "e" at index 4 that was not expected and
				               lacked 3 of 6 expected items:
				                 "x",
				                 "y",
				                 "z"
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithAdditionalItem_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it contained item "d" at index 3 that was not expected
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithAdditionalItems_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it
				               contained item "d" at index 3 that was not expected and
				               contained item "e" at index 4 that was not expected
				             """);
		}

		[Fact]
		public async Task AnyOrder_EmptyCollection_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it lacked 3 of 3 expected items:
				               "a",
				               "b",
				               "c"
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithCollectionInDifferentOrder_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrder_WithDuplicatesAtBeginOfSubject_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

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
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it lacked 1 of 4 expected items: "c"
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithDuplicatesAtEndOfSubject_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it contained item "c" at index 3 that was not expected
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithDuplicatesInExpected_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it lacked 1 of 4 expected items: "a"
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithDuplicatesInSubject_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it contained item "a" at index 1 that was not expected
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithMissingItem_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "d"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it lacked 1 of 4 expected items: "d"
				             """);
		}

		[Fact]
		public async Task AnyOrder_WithMissingItems_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "d", "e"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order,
				             but it lacked 2 of 5 expected items:
				               "d",
				               "e"
				             """);
		}


		[Fact]
		public async Task AnyOrder_WithSameCollection_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_CollectionWithMoreThan20Deviations_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 21));

			async Task Act()
				=> await That(subject).Should().Be([]).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection [] in any order ignoring duplicates,
				             but it was completely different: [
				               1,
				               2,
				               3,
				               4,
				               5,
				               6,
				               7,
				               8,
				               9,
				               10,
				               …
				             ] had more than 20 deviations compared to []
				             """);
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_CompletelyDifferentCollections_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
			IEnumerable<int> expected = Enumerable.Range(100, 11);

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
				             but it was completely different: [
				               1,
				               2,
				               3,
				               4,
				               5,
				               6,
				               7,
				               8,
				               9,
				               10,
				               …
				             ] had more than 20 deviations compared to [
				               100,
				               101,
				               102,
				               103,
				               104,
				               105,
				               106,
				               107,
				               108,
				               109,
				               …
				             ]
				             """);
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_VeryDifferentCollections_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
			IEnumerable<int> expected = Enumerable.Range(101, 10);

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
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
				               contained item 10 at index 9 that was not expected and
				               lacked 10 of 10 expected items:
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
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
				             but it
				               contained item "d" at index 3 that was not expected and
				               contained item "e" at index 4 that was not expected and
				               lacked 3 of 6 expected items:
				                 "x",
				                 "y",
				                 "z"
				             """);
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithAdditionalItem_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
				             but it contained item "d" at index 3 that was not expected
				             """);
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithAdditionalItems_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
				             but it
				               contained item "d" at index 3 that was not expected and
				               contained item "e" at index 4 that was not expected
				             """);
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_EmptyCollection_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
			string[] expected = ["a", "a", "b", "c", "a"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
				             but it lacked 3 of 3 expected items:
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
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
				             but it lacked 2 of 2 expected items:
				               "a",
				               "b"
				             """);
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithCollectionInDifferentOrder_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtBeginOfSubject_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtEndOfExpected_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithDuplicatesAtEndOfSubject_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithDuplicatesInExpected_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithDuplicatesInSubject_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithMissingItem_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "d"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
				             but it lacked 1 of 4 expected items: "d"
				             """);
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithMissingItems_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "d", "e"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected in any order ignoring duplicates,
				             but it lacked 2 of 5 expected items:
				               "d",
				               "e"
				             """);
		}

		[Fact]
		public async Task AnyOrderIgnoringDuplicates_WithSameCollection_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).InAnyOrder().IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrder_CollectionWithMoreThan20Deviations_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 21));

			async Task Act()
				=> await That(subject).Should().Be([]);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection [],
				             but it was completely different: [
				               1,
				               2,
				               3,
				               4,
				               5,
				               6,
				               7,
				               8,
				               9,
				               10,
				               …
				             ] had more than 20 deviations compared to []
				             """);
		}

		[Fact]
		public async Task SameOrder_CompletelyDifferentCollections_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
			IEnumerable<int> expected = Enumerable.Range(100, 11);

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it was completely different: [
				               1,
				               2,
				               3,
				               4,
				               5,
				               6,
				               7,
				               8,
				               9,
				               10,
				               …
				             ] had more than 20 deviations compared to [
				               100,
				               101,
				               102,
				               103,
				               104,
				               105,
				               106,
				               107,
				               108,
				               109,
				               …
				             ]
				             """);
		}

		[Fact]
		public async Task SameOrder_EmptyCollection_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it lacked 3 of 3 expected items:
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
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
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
				               contained item 10 at index 9 that was not expected and
				               lacked 10 of 10 expected items:
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
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it
				               contained item "d" at index 3 instead of "x" and
				               contained item "e" at index 4 instead of "y" and
				               lacked 3 of 6 expected items:
				                 "x",
				                 "y",
				                 "z"
				             """);
		}

		[Fact]
		public async Task SameOrder_WithAdditionalItem_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it contained item "d" at index 3 that was not expected
				             """);
		}

		[Fact]
		public async Task SameOrder_WithAdditionalItems_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it
				               contained item "d" at index 3 that was not expected and
				               contained item "e" at index 4 that was not expected
				             """);
		}

		[Fact]
		public async Task SameOrder_WithCollectionInDifferentOrder_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it
				               contained item "c" at index 1 instead of "b" and
				               contained item "b" at index 2 instead of "c"
				             """);
		}

		[Fact]
		public async Task SameOrder_WithDuplicatesAtBeginOfSubject_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it contained item "c" at index 0 that was not expected
				             """);
		}

		[Fact]
		public async Task SameOrder_WithDuplicatesAtEndOfExpected_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it lacked 1 of 4 expected items: "c"
				             """);
		}

		[Fact]
		public async Task SameOrder_WithDuplicatesAtEndOfSubject_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it contained item "c" at index 3 that was not expected
				             """);
		}

		[Fact]
		public async Task SameOrder_WithDuplicatesInExpected_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it
				               contained item "b" at index 1 instead of "a" and
				               contained item "c" at index 2 instead of "b" and
				               lacked 1 of 4 expected items: "a"
				             """);
		}

		[Fact]
		public async Task SameOrder_WithMissingItem_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "d"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it lacked 1 of 4 expected items: "d"
				             """);
		}

		[Fact]
		public async Task SameOrder_WithMissingItems_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "d", "e"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected,
				             but it lacked 2 of 5 expected items:
				               "d",
				               "e"
				             """);
		}


		[Fact]
		public async Task SameOrder_WithSameCollection_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected);

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_CollectionWithMoreThan20Deviations_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 21));

			async Task Act()
				=> await That(subject).Should().Be([]).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection [] ignoring duplicates,
				             but it was completely different: [
				               1,
				               2,
				               3,
				               4,
				               5,
				               6,
				               7,
				               8,
				               9,
				               10,
				               …
				             ] had more than 20 deviations compared to []
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_CompletelyDifferentCollections_ShouldFail()
		{
			IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
			IEnumerable<int> expected = Enumerable.Range(100, 11);

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it was completely different: [
				               1,
				               2,
				               3,
				               4,
				               5,
				               6,
				               7,
				               8,
				               9,
				               10,
				               …
				             ] had more than 20 deviations compared to [
				               100,
				               101,
				               102,
				               103,
				               104,
				               105,
				               106,
				               107,
				               108,
				               109,
				               …
				             ]
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_EmptyCollection_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it lacked 3 of 3 expected items:
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
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it lacked 2 of 2 expected items:
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
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
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
				               contained item 10 at index 9 that was not expected and
				               lacked 10 of 10 expected items:
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
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it
				               contained item "d" at index 3 instead of "x" and
				               contained item "e" at index 4 instead of "y" and
				               lacked 3 of 6 expected items:
				                 "x",
				                 "y",
				                 "z"
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithAdditionalItem_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it contained item "d" at index 3 that was not expected
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithAdditionalItems_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it
				               contained item "d" at index 3 that was not expected and
				               contained item "e" at index 4 that was not expected
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithCollectionInDifferentOrder_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it
				               contained item "c" at index 1 instead of "b" and
				               contained item "b" at index 2 instead of "c"
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtBeginOfSubject_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtEndOfExpected_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithDuplicatesAtEndOfSubject_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithDuplicatesInExpected_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithDuplicatesInSubject_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithMissingItem_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "d"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it lacked 1 of 4 expected items: "d"
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithMissingItems_ShouldFail()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c", "d", "e"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().Throw<XunitException>()
				.WithMessage("""
				             Expected subject to
				             match collection expected ignoring duplicates,
				             but it lacked 2 of 5 expected items:
				               "d",
				               "e"
				             """);
		}

		[Fact]
		public async Task SameOrderIgnoringDuplicates_WithSameCollection_ShouldSucceed()
		{
			IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
			string[] expected = ["a", "b", "c"];

			async Task Act()
				=> await That(subject).Should().Be(expected).IgnoringDuplicates();

			await That(Act).Should().NotThrow();
		}
	}
}
#endif
