﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public class IsContainedIn
	{
		public sealed class InSameOrderTests
		{
			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order,
					             but it had more than 20 deviations

					             Collection:
					             [
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
					             ]

					             Expected:
					             [
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
			public async Task EmptyCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order,
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

					             Collection:
					             [
					               1,
					               2,
					               3,
					               4,
					               5,
					               6,
					               7,
					               8,
					               9,
					               10
					             ]

					             Expected:
					             [
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
					             ]
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int>? expected = null;

				async Task Act()
					=> await That(subject).IsContainedIn(expected!);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order,
					             but it cannot compare to <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).IsContainedIn([]);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection [] in order,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c", "x", "y", "z",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order,
					             but it
					               contained item "d" at index 3 instead of "x" and
					               contained item "e" at index 4 instead of "y"

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c",
					               "x",
					               "y",
					               "z"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d",]);
				string[] expected = ["a", "b", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order,
					             but it contained item "d" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c"

					             Collection:
					             [
					               "a",
					               "c",
					               "b"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order,
					             but it contained item "c" at index 0 that was not expected

					             Collection:
					             [
					               "c",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order,
					             but it contained item "c" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order,
					             but it contained item "a" at index 0 that was not expected

					             Collection:
					             [
					               "a",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class InSameOrderIgnoringDuplicatesTests
		{
			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order ignoring duplicates,
					             but it had more than 20 deviations

					             Collection:
					             [
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
					             ]

					             Expected:
					             [
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
			public async Task EmptyCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task EmptyCollectionWithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order ignoring duplicates,
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

					             Collection:
					             [
					               1,
					               2,
					               3,
					               4,
					               5,
					               6,
					               7,
					               8,
					               9,
					               10
					             ]

					             Expected:
					             [
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
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c", "x", "y", "z",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order ignoring duplicates,
					             but it
					               contained item "d" at index 3 instead of "x" and
					               contained item "e" at index 4 instead of "y"

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c",
					               "x",
					               "y",
					               "z"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d",]);
				string[] expected = ["a", "b", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order ignoring duplicates,
					             but it contained item "d" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in order ignoring duplicates,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c"

					             Collection:
					             [
					               "a",
					               "c",
					               "b"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class InAnyOrderTests
		{
			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order,
					             but it had more than 20 deviations

					             Collection:
					             [
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
					             ]

					             Expected:
					             [
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
			public async Task EmptyCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order,
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

					             Collection:
					             [
					               1,
					               2,
					               3,
					               4,
					               5,
					               6,
					               7,
					               8,
					               9,
					               10
					             ]

					             Expected:
					             [
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
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c", "x", "y", "z",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c",
					               "x",
					               "y",
					               "z"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d",]);
				string[] expected = ["a", "b", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order,
					             but it contained item "d" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order,
					             but it contained item "c" at index 3 that was not expected

					             Collection:
					             [
					               "c",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order,
					             but it contained item "c" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order,
					             but it contained item "a" at index 1 that was not expected

					             Collection:
					             [
					               "a",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder();

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class InAnyOrderIgnoringDuplicatesTests
		{
			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order ignoring duplicates,
					             but it had more than 20 deviations

					             Collection:
					             [
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
					             ]

					             Expected:
					             [
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
			public async Task EmptyCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b", "c", "a",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task EmptyCollectionWithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order ignoring duplicates,
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

					             Collection:
					             [
					               1,
					               2,
					               3,
					               4,
					               5,
					               6,
					               7,
					               8,
					               9,
					               10
					             ]

					             Expected:
					             [
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
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c", "x", "y", "z",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c",
					               "x",
					               "y",
					               "z"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d",]);
				string[] expected = ["a", "b", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order ignoring duplicates,
					             but it contained item "d" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected in any order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class ProperlyInSameOrderTests
		{
			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order,
					             but it had more than 20 deviations

					             Collection:
					             [
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
					             ]

					             Expected:
					             [
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
			public async Task EmptyCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order,
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

					             Collection:
					             [
					               1,
					               2,
					               3,
					               4,
					               5,
					               6,
					               7,
					               8,
					               9,
					               10
					             ]

					             Expected:
					             [
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
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c", "x", "y", "z",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order,
					             but it
					               contained item "d" at index 3 instead of "x" and
					               contained item "e" at index 4 instead of "y"

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c",
					               "x",
					               "y",
					               "z"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d",]);
				string[] expected = ["a", "b", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c" and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "c",
					               "b"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order,
					             but it
					               contained item "c" at index 0 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "c",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order,
					             but it
					               contained item "c" at index 3 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order,
					             but it
					               contained item "a" at index 0 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}
		}

		public sealed class ProperlyInSameOrderIgnoringDuplicatesTests
		{
			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it had more than 20 deviations

					             Collection:
					             [
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
					             ]

					             Expected:
					             [
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
			public async Task EmptyCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task EmptyCollectionWithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
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

					             Collection:
					             [
					               1,
					               2,
					               3,
					               4,
					               5,
					               6,
					               7,
					               8,
					               9,
					               10
					             ]

					             Expected:
					             [
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
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c", "x", "y", "z",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it
					               contained item "d" at index 3 instead of "x" and
					               contained item "e" at index 4 instead of "y"

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c",
					               "x",
					               "y",
					               "z"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d",]);
				string[] expected = ["a", "b", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c" and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "c",
					               "b"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "c",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}
		}

		public sealed class ProperlyInAnyOrderTests
		{
			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order,
					             but it had more than 20 deviations

					             Collection:
					             [
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
					             ]

					             Expected:
					             [
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
			public async Task EmptyCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order,
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

					             Collection:
					             [
					               1,
					               2,
					               3,
					               4,
					               5,
					               6,
					               7,
					               8,
					               9,
					               10
					             ]

					             Expected:
					             [
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
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c", "x", "y", "z",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c",
					               "x",
					               "y",
					               "z"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d",]);
				string[] expected = ["a", "b", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "c",
					               "b"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order,
					             but it
					               contained item "c" at index 3 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "c",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order,
					             but it
					               contained item "c" at index 3 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order,
					             but it
					               contained item "a" at index 1 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}
		}

		public sealed class ProperlyInAnyOrderIgnoringDuplicatesTests
		{
			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it had more than 20 deviations

					             Collection:
					             [
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
					             ]

					             Expected:
					             [
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
			public async Task EmptyCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b", "c", "a",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task EmptyCollectionWithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
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

					             Collection:
					             [
					               1,
					               2,
					               3,
					               4,
					               5,
					               6,
					               7,
					               8,
					               9,
					               10
					             ]

					             Expected:
					             [
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
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c", "x", "y", "z",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c",
					               "x",
					               "y",
					               "z"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d",]);
				string[] expected = ["a", "b", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected and
					               contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "c",
					               "b"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "c",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c", "d", "e",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);
				string[] expected = ["a", "b", "c",];

				async Task Act()
					=> await That(subject).IsContainedIn(expected).Properly().InAnyOrder()
						.IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             is contained in collection expected which has at least one additional item in any order ignoring duplicates,
					             but it contained all expected items

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]

					             Expected:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}
		}
	}
}
#endif
