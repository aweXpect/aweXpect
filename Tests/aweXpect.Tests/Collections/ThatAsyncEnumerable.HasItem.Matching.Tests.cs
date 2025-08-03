﻿#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed partial class HasItem
	{
		public sealed class Matching
		{
			public sealed class PredicateTests
			{
				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IAsyncEnumerable<int> subject = Factory.GetAsyncFibonacciNumbers();

					async Task Act()
						=> await That(subject).HasItem().Matching(a => a == 5);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsDifferentItemAtGivenIndex_ShouldSucceed()
				{
					int[] subject = [0, 1, 2,];

					async Task Act()
						=> await That(subject).HasItem().Matching(_ => false).AtIndex(2);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has item matching _ => false at index 2,
						              but it had item 2 at index 2

						              Collection:
						              {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenEnumerableContainsExpectedItemAtGivenIndex_ShouldSucceed()
				{
					int[] subject = [0, 1, 2,];

					async Task Act()
						=> await That(subject).HasItem().Matching(_ => true).AtIndex(2);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsNoItemAtGivenIndex_ShouldFail()
				{
					List<int> subject =
					[
						0,
						1,
						2,
					];

					async Task Act()
						=> await That(subject).HasItem().Matching(_ => true).AtIndex(3);

					await That(Act).Throws<XunitException>()
						.WithMessage($"""
						              Expected that subject
						              has item matching _ => true at index 3,
						              but it did not contain any item at index 3

						              Collection:
						              {Formatter.Format(subject)}
						              """);
				}

				[Fact]
				public async Task WhenEnumerableIsEmpty_ShouldFail()
				{
					List<int> subject = [];

					async Task Act()
						=> await That(subject).HasItem().Matching(_ => true);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item matching _ => true,
						             but it did not contain any item

						             Collection:
						             []
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_WithAnyIndex_ShouldFail()
				{
					IAsyncEnumerable<int>? subject = null;

					async Task Act()
						=> await That(subject).HasItem().Matching(_ => true);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item matching _ => true,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_WithFixedIndex_ShouldFail()
				{
					IAsyncEnumerable<int>? subject = null;

					async Task Act()
						=> await That(subject).HasItem().Matching(_ => true).AtIndex(0);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item matching _ => true at index 0,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WithInvalidMatch_ShouldNotMatch()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable(0, 1, 2, 3, 4);

					async Task Act()
						=> await That(subject).HasItem().Matching(_ => true).WithInvalidMatch();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item matching _ => true with invalid match,
						             but it did not contain any item with invalid match

						             Collection:
						             [0, 1, 2, 3, 4]
						             """);
				}

				[Fact]
				public async Task WithMultipleFailures_ShouldIncludeCollectionOnlyOnce()
				{
					IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);

					async Task Act()
						=> await That(subject)
							.HasItem().Matching(_ => false).AtIndex(0).And
							.HasItem().Matching(_ => false).AtIndex(1).And
							.HasItem().Matching(_ => false)
					;

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item matching _ => false at index 0 and has item matching _ => false at index 1 and has item matching _ => false,
						             but it had item "a" at index 0 and it had item "b" at index 1 and it did not match at any index

						             Collection:
						             [
						               "a",
						               "b",
						               "c"
						             ]
						             """);
				}
			}

			public sealed class GenericPredicateTests
			{
				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IAsyncEnumerable<MyClass> subject = Factory.GetAsyncFibonacciNumbers<MyClass>(x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>(a => a.Value == 5);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsDifferentItemAtGivenIndex_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable<MyClass>([0, 1, 2,], x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>(_ => false).AtIndex(2);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass matching _ => false at index 2,
						             but it had item MyClass {
						               StringValue = "",
						               Value = 2
						             } at index 2

						             Collection:
						             [
						               MyClass {
						                 StringValue = "",
						                 Value = 0
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 1
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 2
						               }
						             ]
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsExpectedItemAtGivenIndex_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable<MyClass>([0, 1, 2,], x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>(_ => true).AtIndex(2);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsNoItemAtGivenIndex_ShouldFail()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable<MyClass>([0, 1, 2,], x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>(_ => true).AtIndex(3);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass matching _ => true at index 3,
						             but it did not contain any item at index 3

						             Collection:
						             [
						               MyClass {
						                 StringValue = "",
						                 Value = 0
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 1
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 2
						               }
						             ]
						             """);
				}

				[Fact]
				public async Task WhenEnumerableIsEmpty_ShouldFail()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(Array.Empty<MyClass>());

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>(_ => true);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass matching _ => true,
						             but it did not contain any item

						             Collection:
						             []
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_WithAnyIndex_ShouldFail()
				{
					IAsyncEnumerable<MyClass>? subject = null;

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>(_ => true);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass matching _ => true,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_WithFixedIndex_ShouldFail()
				{
					IAsyncEnumerable<MyClass>? subject = null;

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>(_ => true).AtIndex(0);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass matching _ => true at index 0,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenTypeDoesNotMatch_ShouldFail()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable(1, 2, 3);

					async Task Act()
						=> await That(subject).HasItem().Matching<uint>(_ => true);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type uint matching _ => true,
						             but it did not match at any index

						             Collection:
						             [1, 2, 3]
						             """);
				}

				[Fact]
				public async Task WhenTypeIsSubtype_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject =
						ToAsyncEnumerable<MyClass>([0, 1, 2,], x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>(_ => true);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenTypeIsSupertype_ShouldFail()
				{
					IAsyncEnumerable<MyBaseClass> subject =
						ToAsyncEnumerable<MyBaseClass>([0, 1, 2,], x => new MyBaseClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyClass>(_ => true);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyClass matching _ => true,
						             but it did not match at any index

						             Collection:
						             [
						               MyBaseClass {
						                 Value = 0
						               },
						               MyBaseClass {
						                 Value = 1
						               },
						               MyBaseClass {
						                 Value = 2
						               }
						             ]
						             """);
				}

				[Fact]
				public async Task WithInvalidMatch_ShouldNotMatch()
				{
					IAsyncEnumerable<MyClass> subject =
						ToAsyncEnumerable<MyClass>([0, 1, 2, 3, 4,], x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>(_ => true).WithInvalidMatch();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass matching _ => true with invalid match,
						             but it did not contain any item with invalid match

						             Collection:
						             [
						               MyClass {
						                 StringValue = "",
						                 Value = 0
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 1
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 2
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 3
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 4
						               }
						             ]
						             """);
				}
			}

			public sealed class GenericTests
			{
				[Fact]
				public async Task DoesNotMaterializeEnumerable()
				{
					IAsyncEnumerable<MyClass> subject = Factory.GetAsyncFibonacciNumbers<MyClass>(x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsDifferentItemAtGivenIndex_ShouldSucceed()
				{
					IAsyncEnumerable<MyBaseClass> subject =
						ToAsyncEnumerable<MyBaseClass>([0, 1, 2,], x => new MyBaseClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyClass>().AtIndex(2);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyClass at index 2,
						             but it had item MyBaseClass {
						               Value = 2
						             } at index 2

						             Collection:
						             [
						               MyBaseClass {
						                 Value = 0
						               },
						               MyBaseClass {
						                 Value = 1
						               },
						               MyBaseClass {
						                 Value = 2
						               }
						             ]
						             """);
				}

				[Fact]
				public async Task WhenEnumerableContainsExpectedItemAtGivenIndex_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable<MyClass>([0, 1, 2,], x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>().AtIndex(2);

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenEnumerableContainsNoItemAtGivenIndex_ShouldFail()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable<MyClass>([0, 1, 2,], x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>().AtIndex(3);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass at index 3,
						             but it did not contain any item at index 3

						             Collection:
						             [
						               MyClass {
						                 StringValue = "",
						                 Value = 0
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 1
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 2
						               }
						             ]
						             """);
				}

				[Fact]
				public async Task WhenEnumerableIsEmpty_ShouldFail()
				{
					IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable(Array.Empty<MyClass>());

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass,
						             but it did not contain any item

						             Collection:
						             []
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_WithAnyIndex_ShouldFail()
				{
					IAsyncEnumerable<MyClass>? subject = null;

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenSubjectIsNull_WithFixedIndex_ShouldFail()
				{
					IAsyncEnumerable<MyClass>? subject = null;

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>().AtIndex(0);

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass at index 0,
						             but it was <null>
						             """);
				}

				[Fact]
				public async Task WhenTypeDoesNotMatch_ShouldFail()
				{
					IAsyncEnumerable<int> subject = ToAsyncEnumerable(1, 2, 3);

					async Task Act()
						=> await That(subject).HasItem().Matching<uint>();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type uint,
						             but it did not match at any index

						             Collection:
						             [1, 2, 3]
						             """);
				}

				[Fact]
				public async Task WhenTypeIsSubtype_ShouldSucceed()
				{
					IAsyncEnumerable<MyClass> subject =
						ToAsyncEnumerable<MyClass>([0, 1, 2,], x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>();

					await That(Act).DoesNotThrow();
				}

				[Fact]
				public async Task WhenTypeIsSupertype_ShouldFail()
				{
					IAsyncEnumerable<MyBaseClass> subject =
						ToAsyncEnumerable<MyBaseClass>([0, 1, 2,], x => new MyBaseClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyClass>();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyClass,
						             but it did not match at any index

						             Collection:
						             [
						               MyBaseClass {
						                 Value = 0
						               },
						               MyBaseClass {
						                 Value = 1
						               },
						               MyBaseClass {
						                 Value = 2
						               }
						             ]
						             """);
				}

				[Fact]
				public async Task WithInvalidMatch_ShouldNotMatch()
				{
					IAsyncEnumerable<MyClass> subject =
						ToAsyncEnumerable<MyClass>([0, 1, 2, 3, 4,], x => new MyClass(x));

					async Task Act()
						=> await That(subject).HasItem().Matching<MyBaseClass>().WithInvalidMatch();

					await That(Act).Throws<XunitException>()
						.WithMessage("""
						             Expected that subject
						             has item of type MyBaseClass with invalid match,
						             but it did not contain any item with invalid match

						             Collection:
						             [
						               MyClass {
						                 StringValue = "",
						                 Value = 0
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 1
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 2
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 3
						               },
						               MyClass {
						                 StringValue = "",
						                 Value = 4
						               }
						             ]
						             """);
				}
			}
		}
	}
}
#endif
