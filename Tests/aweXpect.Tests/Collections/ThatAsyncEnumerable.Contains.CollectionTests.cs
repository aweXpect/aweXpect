#if NET8_0_OR_GREATER
using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public partial class Contains
	{
		public sealed class InSameOrderTests
		{

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int>? expected = null;

				async Task Act()
					=> await That(subject).Contains(expected!);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
					             but it cannot compare to <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject!).Contains([]);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection [] in order,
					             but it was <null>
					             """);
			}
			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 11));
				IEnumerable<int> expected = Enumerable.Range(100, 11);

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
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
			public async Task EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
					             but it lacked 3 of 3 expected items:
					               "a",
					               "b",
					               "c"
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
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
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
					             but it lacked 3 of 6 expected items:
					               "x",
					               "y",
					               "z"
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
					             but it lacked 2 of 6 expected items:
					               "a",
					               "e"
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c"
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
					             but it lacked 1 of 4 expected items: "c"
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
					             but it
					               contained item "b" at index 1 instead of "a" and
					               contained item "c" at index 2 instead of "b" and
					               lacked 1 of 4 expected items: "a"
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
					             but it lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order,
					             but it lacked 2 of 5 expected items:
					               "d",
					               "e"
					             """);
			}


			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected);

				await That(Act).Does().NotThrow();
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
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order ignoring duplicates,
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
			public async Task EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order ignoring duplicates,
					             but it lacked 3 of 3 expected items:
					               "a",
					               "b",
					               "c"
					             """);
			}

			[Fact]
			public async Task EmptyCollectionWithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order ignoring duplicates,
					             but it lacked 2 of 2 expected items:
					               "a",
					               "b"
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order ignoring duplicates,
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
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order ignoring duplicates,
					             but it lacked 3 of 6 expected items:
					               "x",
					               "y",
					               "z"
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order ignoring duplicates,
					             but it lacked 2 of 5 expected items:
					               "a",
					               "e"
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order ignoring duplicates,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c"
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order ignoring duplicates,
					             but it lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in order ignoring duplicates,
					             but it lacked 2 of 5 expected items:
					               "d",
					               "e"
					             """);
			}

			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).IgnoringDuplicates();

				await That(Act).Does().NotThrow();
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
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order,
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
			public async Task EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order,
					             but it lacked 3 of 3 expected items:
					               "a",
					               "b",
					               "c"
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order,
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
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order,
					             but it lacked 3 of 6 expected items:
					               "x",
					               "y",
					               "z"
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order,
					             but it lacked 2 of 6 expected items:
					               "a",
					               "e"
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order,
					             but it lacked 1 of 4 expected items: "c"
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order,
					             but it lacked 1 of 4 expected items: "a"
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order,
					             but it lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order,
					             but it lacked 2 of 5 expected items:
					               "d",
					               "e"
					             """);
			}


			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder();

				await That(Act).Does().NotThrow();
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
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order ignoring duplicates,
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
			public async Task EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b", "c", "a"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order ignoring duplicates,
					             but it lacked 3 of 3 expected items:
					               "a",
					               "b",
					               "c"
					             """);
			}

			[Fact]
			public async Task EmptyCollectionWithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order ignoring duplicates,
					             but it lacked 2 of 2 expected items:
					               "a",
					               "b"
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order ignoring duplicates,
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
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order ignoring duplicates,
					             but it lacked 3 of 6 expected items:
					               "x",
					               "y",
					               "z"
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order ignoring duplicates,
					             but it lacked 2 of 5 expected items:
					               "a",
					               "e"
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order ignoring duplicates,
					             but it lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected in any order ignoring duplicates,
					             but it lacked 2 of 5 expected items:
					               "d",
					               "e"
					             """);
			}

			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
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
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
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
			public async Task EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
					             but it
					               did not contain any additional items and
					               lacked 3 of 3 expected items:
					                 "a",
					                 "b",
					                 "c"
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
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
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
					             but it
					               did not contain any additional items and
					               lacked 3 of 6 expected items:
					                 "x",
					                 "y",
					                 "z"
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
					             but it lacked 2 of 6 expected items:
					               "a",
					               "e"
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c" and
					               did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "c"
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
					             but it
					               contained item "b" at index 1 instead of "a" and
					               contained item "c" at index 2 instead of "b" and
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "a"
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
					             but it
					               did not contain any additional items and
					               lacked 2 of 5 expected items:
					                 "d",
					                 "e"
					             """);
			}


			[Fact]
			public async Task WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order,
					             but it did not contain any additional items
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
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
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
			public async Task EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 3 of 3 expected items:
					                 "a",
					                 "b",
					                 "c"
					             """);
			}

			[Fact]
			public async Task EmptyCollectionWithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 2 of 2 expected items:
					                 "a",
					                 "b"
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
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
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it lacked 3 of 6 expected items:
					               "x",
					               "y",
					               "z"
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it lacked 2 of 5 expected items:
					               "a",
					               "e"
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it
					               contained item "c" at index 1 instead of "b" and
					               contained item "b" at index 2 instead of "c" and
					               did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 2 of 5 expected items:
					                 "d",
					                 "e"
					             """);
			}

			[Fact]
			public async Task WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in order ignoring duplicates,
					             but it did not contain any additional items
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
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
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
			public async Task EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
					             but it
					               did not contain any additional items and
					               lacked 3 of 3 expected items:
					                 "a",
					                 "b",
					                 "c"
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
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
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
					             but it lacked 3 of 6 expected items:
					               "x",
					               "y",
					               "z"
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
					             but it
					               did not contain any additional items and
					               lacked 2 of 6 expected items:
					                 "a",
					                 "e"
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "c"
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "a"
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
					             but it
					               did not contain any additional items and
					               lacked 2 of 5 expected items:
					                 "d",
					                 "e"
					             """);
			}


			[Fact]
			public async Task WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order,
					             but it did not contain any additional items
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
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
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
			public async Task EmptyCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b", "c", "a"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 3 of 3 expected items:
					                 "a",
					                 "b",
					                 "c"
					             """);
			}

			[Fact]
			public async Task EmptyCollectionWithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(Array.Empty<string>());
				string[] expected = ["a", "a", "b"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 2 of 2 expected items:
					                 "a",
					                 "b"
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(Enumerable.Range(1, 10));
				IEnumerable<int> expected = Enumerable.Range(101, 10);

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
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
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c", "x", "y", "z"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it lacked 3 of 6 expected items:
					               "x",
					               "y",
					               "z"
					             """);
			}

			[Fact]
			public async Task WithAdditionalExpectedItemAtBeginningAndEnd_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["b", "b", "c", "d"]);
				string[] expected = ["a", "b", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 2 of 5 expected items:
					                 "a",
					                 "e"
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "d", "e"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "c", "b"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtBeginOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["c", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 1 of 4 expected items: "d"
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c", "d", "e"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it
					               did not contain any additional items and
					               lacked 2 of 5 expected items:
					                 "d",
					                 "e"
					             """);
			}

			[Fact]
			public async Task WithSameCollection_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c"]);
				string[] expected = ["a", "b", "c"];

				async Task Act()
					=> await That(subject).Contains(expected).Properly().InAnyOrder().IgnoringDuplicates();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             contain collection expected and at least one additional item in any order ignoring duplicates,
					             but it did not contain any additional items
					             """);
			}
		}
	}
}
#endif
