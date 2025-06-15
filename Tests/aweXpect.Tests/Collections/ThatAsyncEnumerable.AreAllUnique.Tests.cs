#if NET8_0_OR_GREATER
using System.Collections.Generic;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatAsyncEnumerable
{
	public sealed class AreAllUnique
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(1, 1, 1);

				async Task Act()
					=> await That(subject).AreAllUnique().Using(new AllDifferentComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(1, 2, 3);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(1, 2, 3, 1);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items,
					             but it contained 1 duplicate:
					               1

					             Collection:
					             [1, 2, 3, 1]
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(1, 2, 3, 1, 2, -1);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items,
					             but it contained 2 duplicates:
					               1,
					               2

					             Collection:
					             [1, 2, 3, 1, 2, -1]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<int>? subject = null;

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items,
					             but it was <null>
					             """);
			}
		}

		public sealed class NegatedTests
		{
			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldFail()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(1, 2, 3);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.AreAllUnique());

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has duplicate items,
					             but all were unique

					             Collection:
					             [1, 2, 3]
					             """);
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldSucceed()
			{
				IAsyncEnumerable<int> subject = ToAsyncEnumerable(1, 2, 3, 1);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.AreAllUnique());

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class StringTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "a", "a",]);

				async Task Act()
					=> await That(subject).AreAllUnique().Using(new AllDifferentComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c",]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasing_ShouldSucceed()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "A",]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasingAndCasingIsIgnored_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "A",]);

				async Task Act()
					=> await That(subject).AreAllUnique().IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items ignoring case,
					             but it contained 1 duplicate:
					               "A"

					             Collection:
					             [
					               "a",
					               "A"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "a",]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items,
					             but it contained 1 duplicate:
					               "a"

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "a"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IAsyncEnumerable<string> subject = ToAsyncEnumerable(["a", "b", "c", "a", "b", "x",]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items,
					             but it contained 2 duplicates:
					               "a",
					               "b"

					             Collection:
					             [
					               "a",
					               "b",
					               "c",
					               "a",
					               "b",
					               "x"
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<string>? subject = null;

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items,
					             but it was <null>
					             """);
			}
		}

		public sealed class MemberTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable([1, 1, 1,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value).Using(new AllDifferentComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldSucceed()
			{
				IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable([1, 2, 3,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable([1, 2, 3, 1,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items for x => x.Value,
					             but it contained 1 duplicate:
					               1

					             Collection:
					             [
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
					                 Value = 1
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IAsyncEnumerable<MyClass> subject =
					ToAsyncEnumerable([1, 2, 3, 1, 2, -1,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items for x => x.Value,
					             but it contained 2 duplicates:
					               1,
					               2

					             Collection:
					             [
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
					                 Value = 1
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = 2
					               },
					               MyClass {
					                 StringValue = "",
					                 Value = -1
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<MyClass>? subject = null;

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items for x => x.Value,
					             but it was <null>
					             """);
			}
		}

		public sealed class NegatedMemberTests
		{
			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldFail()
			{
				IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable([1, 2, 3,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.AreAllUnique(x => x.Value));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has duplicate items for x => x.Value,
					             but all were unique

					             Collection:
					             [
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
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldSucceed()
			{
				IAsyncEnumerable<MyClass> subject = ToAsyncEnumerable([1, 2, 3, 1,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.AreAllUnique(x => x.Value));

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class StringMemberTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IAsyncEnumerable<MyStringClass>
					subject = ToAsyncEnumerable(["a", "a", "a",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value).Using(new AllDifferentComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldSucceed()
			{
				IAsyncEnumerable<MyStringClass>
					subject = ToAsyncEnumerable(["a", "b", "c",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasing_ShouldSucceed()
			{
				IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "A",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasingAndCasingIsIgnored_ShouldFail()
			{
				IAsyncEnumerable<MyStringClass> subject = ToAsyncEnumerable(["a", "A",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value).IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items for x => x.Value ignoring case,
					             but it contained 1 duplicate:
					               "A"

					             Collection:
					             [
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "a"
					               },
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "A"
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IAsyncEnumerable<MyStringClass>
					subject = ToAsyncEnumerable(["a", "b", "c", "a",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items for x => x.Value,
					             but it contained 1 duplicate:
					               "a"

					             Collection:
					             [
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "a"
					               },
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "b"
					               },
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "c"
					               },
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "a"
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IAsyncEnumerable<MyStringClass> subject =
					ToAsyncEnumerable(["a", "b", "c", "a", "b", "x",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items for x => x.Value,
					             but it contained 2 duplicates:
					               "a",
					               "b"

					             Collection:
					             [
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "a"
					               },
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "b"
					               },
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "c"
					               },
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "a"
					               },
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "b"
					               },
					               ThatAsyncEnumerable.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "x"
					               }
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IAsyncEnumerable<MyStringClass>? subject = null;

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items for x => x.Value,
					             but it was <null>
					             """);
			}

			private sealed class MyStringClass(string value)
			{
				public string Value { get; } = value;
			}
		}
	}
}
#endif
