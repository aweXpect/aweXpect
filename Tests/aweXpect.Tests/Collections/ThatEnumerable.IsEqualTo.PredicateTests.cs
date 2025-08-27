using System.Collections.Generic;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class IsEqualTo
	{
		public sealed class WithPredicatesInSameOrderTests
		{
			[Fact]
			public async Task CollectionWithMoreThan20Deviations_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 21);

				async Task Act()
					=> await That(subject).IsEqualTo(Array.Empty<Func<int, bool>>());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection Array.Empty<Func<int, bool>>() in order,
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
					               (… and maybe others)
					             ]
					             """);
			}


			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 11);
				IEnumerable<Func<int, bool>> expected =
					Enumerable.Range(100, 11).Select<int, Func<int, bool>>(x => a => a == x);

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
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
					             """);
			}

			[Fact]
			public async Task EmptyCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());

				async Task Act()
					=> await That(subject).IsEqualTo([
						x => x == "a",
						x => x == "b",
						x => x == "c",
					]);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection [
					             	x => x == "a",
					             	x => x == "b",
					             	x => x == "c",
					             ] in order,
					             but it lacked all 3 expected items

					             Collection:
					             []
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 10);
				IEnumerable<Func<int, bool>> expected = Enumerable.Range(101, 10)
					.Select<int, Func<int, bool>>(x => a => a == x);

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
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
					               lacked all 10 expected items

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
					             """);
			}

			[Fact]
			public async Task WhenExpectedIsNull_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 11);
				IEnumerable<Func<int, bool>>? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected!);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it cannot compare to <null>
					             """);
			}

			[Fact]
			public async Task WhenSubjectAndExpectedIsNull_ShouldSucceed()
			{
				IEnumerable<int>? subject = null;
				IEnumerable<Func<int, bool>>? expected = null;

				async Task Act()
					=> await That(subject).IsEqualTo(expected!);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IEnumerable<string>? subject = null;
				IEnumerable<Func<string, bool>> expected = [];

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it was <null>
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "x", "y", "z");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it
					               contained item "d" at index 3 instead of Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "x"
					                 }
					               } and
					               contained item "e" at index 4 instead of Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "y"
					                 }
					               } and
					               lacked 3 of 6 expected items:
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "x"
					                 }
					               },
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "y"
					                 }
					               },
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "z"
					                 }
					               }
					             
					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it contained item "d" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
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
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it
					               contained item "c" at index 1 instead of Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "b"
					                 }
					               } and
					               contained item "b" at index 2 instead of Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "c"
					                 }
					               }
					             
					             Collection:
					             [
					               "a",
					               "c",
					               "b"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it lacked 1 of 4 expected items: Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "c"
					               }
					             }
					             
					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it contained item "c" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it
					               contained item "b" at index 1 instead of Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "a"
					                 }
					               } and
					               contained item "c" at index 2 instead of Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "b"
					                 }
					               } and
					               lacked 1 of 4 expected items: Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "a"
					                 }
					               }
					             
					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it contained item "a" at index 0 that was not expected

					             Collection:
					             [
					               "a",
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "d");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it lacked 1 of 4 expected items: Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "d"
					               }
					             }

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "d", "e");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order,
					             but it lacked 2 of 5 expected items:
					               Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "d"
					               }
					             },
					               Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "e"
					               }
					             }

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}


			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected);

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class WithPredicatesInSameOrderIgnoringDuplicatesTests
		{
			[Fact]
			public async Task CollectionWithMoreThan20Deviations_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 21);

				async Task Act()
					=> await That(subject).IsEqualTo(Array.Empty<Func<int, bool>>()).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection Array.Empty<Func<int, bool>>() in order ignoring duplicates,
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
					               (… and maybe others)
					             ]
					             """);
			}

			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 11);
				IEnumerable<Func<int, bool>> expected = Enumerable.Range(100, 11)
					.Select<int, Func<int, bool>>(x => a => a == x);

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order ignoring duplicates,
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
					             """);
			}

			[Fact]
			public async Task EmptyCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order ignoring duplicates,
					             but it lacked all 3 unique expected items

					             Collection:
					             []
					             """);
			}

			[Fact]
			public async Task EmptyCollectionWithDuplicatesInExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "a", "b");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order ignoring duplicates,
					             but it lacked all 2 unique expected items

					             Collection:
					             []
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 10);
				IEnumerable<Func<int, bool>> expected = Enumerable.Range(101, 10)
					.Select<int, Func<int, bool>>(x => a => a == x);

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order ignoring duplicates,
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
					               lacked all 10 unique expected items

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
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "x", "y", "z");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order ignoring duplicates,
					             but it
					               contained item "d" at index 3 instead of Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "x"
					                 }
					               } and
					               contained item "e" at index 4 instead of Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "y"
					                 }
					               } and
					               lacked 3 of 6 expected items:
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "x"
					                 }
					               },
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "y"
					                 }
					               },
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "z"
					                 }
					               }
					             
					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order ignoring duplicates,
					             but it contained item "d" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order ignoring duplicates,
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
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order ignoring duplicates,
					             but it
					               contained item "c" at index 1 instead of Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "b"
					                 }
					               } and
					               contained item "b" at index 2 instead of Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "c"
					                 }
					               }
					             
					             Collection:
					             [
					               "a",
					               "c",
					               "b"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "d");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order ignoring duplicates,
					             but it lacked 1 of 4 expected items: Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "d"
					               }
					             }

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "d", "e");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in order ignoring duplicates,
					             but it lacked 2 of 5 expected items:
					               Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "d"
					               }
					             },
					               Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "e"
					               }
					             }

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class WithPredicatesInAnyOrderTests
		{
			[Fact]
			public async Task CollectionWithMoreThan20Deviations_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 21);

				async Task Act()
					=> await That(subject).IsEqualTo(Array.Empty<Func<int, bool>>()).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection Array.Empty<Func<int, bool>>() in any order,
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
					               (… and maybe others)
					             ]
					             """);
			}

			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 11);
				IEnumerable<Func<int, bool>> expected = Enumerable.Range(100, 11)
					.Select<int, Func<int, bool>>(x => a => a == x);

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
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
					             """);
			}

			[Fact]
			public async Task EmptyCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
					             but it lacked all 3 expected items

					             Collection:
					             []
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 10);
				IEnumerable<Func<int, bool>> expected = Enumerable.Range(101, 10)
					.Select<int, Func<int, bool>>(x => a => a == x);

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
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
					               lacked all 10 expected items

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
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "x", "y", "z");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected and
					               lacked 3 of 6 expected items:
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "x"
					                 }
					               },
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "y"
					                 }
					               },
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "z"
					                 }
					               }
					             
					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
					             but it contained item "d" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
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
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
					             but it lacked 1 of 4 expected items: Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "c"
					               }
					             }

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
					             but it contained item "c" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
					             but it lacked 1 of 4 expected items: Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "a"
					               }
					             }

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
					             but it contained item "a" at index 1 that was not expected

					             Collection:
					             [
					               "a",
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "d");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
					             but it lacked 1 of 4 expected items: Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "d"
					               }
					             }

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "d", "e");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order,
					             but it lacked 2 of 5 expected items:
					               Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "d"
					               }
					             },
					               Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "e"
					               }
					             }

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}


			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder();

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class WithPredicatesInAnyOrderIgnoringDuplicatesTests
		{
			[Fact]
			public async Task CollectionWithMoreThan20Deviations_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 21);

				async Task Act()
					=> await That(subject).IsEqualTo(Array.Empty<Func<int, bool>>()).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection Array.Empty<Func<int, bool>>() in any order ignoring duplicates,
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
					               (… and maybe others)
					             ]
					             """);
			}


			[Fact]
			public async Task CompletelyDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 11);
				IEnumerable<Func<int, bool>> expected = Enumerable.Range(100, 11)
					.Select<int, Func<int, bool>>(x => a => a == x);

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order ignoring duplicates,
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
					             """);
			}

			[Fact]
			public async Task EmptyCollection_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "a", "b", "c", "a");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order ignoring duplicates,
					             but it lacked all 3 unique expected items

					             Collection:
					             []
					             """);
			}

			[Fact]
			public async Task EmptyCollectionWithDuplicatesInExpected_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(Array.Empty<string>());
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "a", "b");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order ignoring duplicates,
					             but it lacked all 2 unique expected items

					             Collection:
					             []
					             """);
			}

			[Fact]
			public async Task VeryDifferentCollections_ShouldFail()
			{
				IEnumerable<int> subject = Enumerable.Range(1, 10);
				IEnumerable<Func<int, bool>> expected = Enumerable.Range(101, 10)
					.Select<int, Func<int, bool>>(x => a => a == x);

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order ignoring duplicates,
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
					               lacked all 10 unique expected items

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
					             """);
			}

			[Fact]
			public async Task WithAdditionalAndMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "x", "y", "z");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order ignoring duplicates,
					             but it
					               contained item "d" at index 3 that was not expected and
					               contained item "e" at index 4 that was not expected and
					               lacked 3 of 6 expected items:
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "x"
					                 }
					               },
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "y"
					                 }
					               },
					                 Func<string, bool> {
					                 Method = Boolean <ToFuncEnumerable>b__0(System.String),
					                 Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                   item = "z"
					                 }
					               }
					             
					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d",
					               "e"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order ignoring duplicates,
					             but it contained item "d" at index 3 that was not expected

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "d"
					             ]
					             """);
			}

			[Fact]
			public async Task WithAdditionalItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "d", "e",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order ignoring duplicates,
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
					             """);
			}

			[Fact]
			public async Task WithCollectionInDifferentOrder_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "c", "b",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesAtEndOfSubject_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInExpected_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithDuplicatesInSubject_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WithMissingItem_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "d");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order ignoring duplicates,
					             but it lacked 1 of 4 expected items: Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "d"
					               }
					             }
					             
					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithMissingItems_ShouldFail()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c", "d", "e");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             matches collection expected in any order ignoring duplicates,
					             but it lacked 2 of 5 expected items:
					               Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "d"
					               }
					             },
					               Func<string, bool> {
					               Method = Boolean <ToFuncEnumerable>b__0(System.String),
					               Target = ThatEnumerable.<>c__DisplayClass11_0<string> {
					                 item = "e"
					               }
					             }

					             Collection:
					             [
					               "a",
					               "b",
					               "c"
					             ]
					             """);
			}

			[Fact]
			public async Task WithSameCollection_ShouldSucceed()
			{
				IEnumerable<string> subject = ToEnumerable(["a", "b", "c",]);
				IEnumerable<Func<string, bool>> expected = ToFuncEnumerable("a", "b", "c");

				async Task Act()
					=> await That(subject).IsEqualTo(expected).InAnyOrder().IgnoringDuplicates();

				await That(Act).DoesNotThrow();
			}
		}
	}
}
