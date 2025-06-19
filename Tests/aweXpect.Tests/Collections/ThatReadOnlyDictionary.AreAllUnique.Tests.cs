using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace aweXpect.Tests;

public sealed partial class ThatReadOnlyDictionary
{
	public sealed class AreAllUnique
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IReadOnlyDictionary<int, int> subject = ToDictionary([1, 1, 1,]);

				async Task Act()
					=> await That(subject).AreAllUnique().Using(new AllDifferentComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllValuesAreUnique_ShouldSucceed()
			{
				IReadOnlyDictionary<int, int> subject = ToDictionary([1, 2, 3,]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IReadOnlyDictionary<int, int> subject = ToDictionary([1, 2, 3, 1,]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values,
					             but it contained 1 duplicate:
					               1

					             Dictionary:
					             {[0] = 1, [1] = 2, [2] = 3, [3] = 1}
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IReadOnlyDictionary<int, int> subject = ToDictionary([1, 2, 3, 1, 2, -1,]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values,
					             but it contained 2 duplicates:
					               1,
					               2

					             Dictionary:
					             {[0] = 1, [1] = 2, [2] = 3, [3] = 1, [4] = 2, [5] = -1}
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				ReadOnlyDictionary<int, int>? subject = null;

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values,
					             but it was <null>
					             """);
			}
		}

		public sealed class StringTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IReadOnlyDictionary<int, string?> subject = ToDictionary(["a", "a", "a",]);

				async Task Act()
					=> await That(subject).AreAllUnique().Using(new AllDifferentComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllValuesAreUnique_ShouldSucceed()
			{
				IReadOnlyDictionary<int, string?> subject = ToDictionary(["a", "b", "c",]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasing_ShouldSucceed()
			{
				IReadOnlyDictionary<int, string?> subject = ToDictionary(["a", "A",]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasingAndCasingIsIgnored_ShouldFail()
			{
				IReadOnlyDictionary<int, string?> subject = ToDictionary(["a", "A",]);

				async Task Act()
					=> await That(subject).AreAllUnique().IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values ignoring case,
					             but it contained 1 duplicate:
					               "A"

					             Dictionary:
					             {
					               [0] = "a",
					               [1] = "A"
					             }
					             """);
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IReadOnlyDictionary<int, string?> subject = ToDictionary(["a", "b", "c", "a",]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values,
					             but it contained 1 duplicate:
					               "a"

					             Dictionary:
					             {
					               [0] = "a",
					               [1] = "b",
					               [2] = "c",
					               [3] = "a"
					             }
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IReadOnlyDictionary<int, string?> subject = ToDictionary(["a", "b", "c", "a", "b", "x",]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values,
					             but it contained 2 duplicates:
					               "a",
					               "b"

					             Dictionary:
					             {
					               [0] = "a",
					               [1] = "b",
					               [2] = "c",
					               [3] = "a",
					               [4] = "b",
					               [5] = "x"
					             }
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				ReadOnlyDictionary<int, string?>? subject = null;

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values,
					             but it was <null>
					             """);
			}
		}

		public sealed class MemberTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IReadOnlyDictionary<int, MyClass> subject = ToDictionary([1, 1, 1,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value).Using(new AllDifferentComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllValuesAreUnique_ShouldSucceed()
			{
				IReadOnlyDictionary<int, MyClass> subject = ToDictionary([1, 2, 3,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IReadOnlyDictionary<int, MyClass> subject = ToDictionary([1, 2, 3, 1,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values for x => x.Value,
					             but it contained 1 duplicate:
					               1

					             Dictionary:
					             {
					               [0] = ThatDictionary.MyClass {
					                 Inner = <null>,
					                 Value = 1
					               },
					               [1] = ThatDictionary.MyClass {
					                 Inner = <null>,
					                 Value = 2
					               },
					               [2] = ThatDictionary.MyClass {
					                 Inner = <null>,
					                 Value = 3
					               },
					               [3] = ThatDictionary.MyClass {
					                 Inner = <null>,
					                 Value = 1
					               }
					             }
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IReadOnlyDictionary<int, MyClass> subject =
					ToDictionary([1, 2, 3, 1, 2, -1,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values for x => x.Value,
					             but it contained 2 duplicates:
					               1,
					               2

					             Dictionary:
					             {
					               [0] = ThatDictionary.MyClass {
					                 Inner = <null>,
					                 Value = 1
					               },
					               [1] = ThatDictionary.MyClass {
					                 Inner = <null>,
					                 Value = 2
					               },
					               [2] = ThatDictionary.MyClass {
					                 Inner = <null>,
					                 Value = 3
					               },
					               [3] = ThatDictionary.MyClass {
					                 Inner = <null>,
					                 Value = 1
					               },
					               [4] = ThatDictionary.MyClass {
					                 Inner = <null>,
					                 Value = 2
					               },
					               [5] = ThatDictionary.MyClass {
					                 Inner = <null>,
					                 Value = -1
					               }
					             }
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				ReadOnlyDictionary<int, MyClass>? subject = null;

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values for x => x.Value,
					             but it was <null>
					             """);
			}
		}

		public sealed class StringMemberTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IReadOnlyDictionary<int, MyStringClass> subject = ToDictionary(["a", "a", "a",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value).Using(new AllDifferentComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllValuesAreUnique_ShouldSucceed()
			{
				IReadOnlyDictionary<int, MyStringClass> subject = ToDictionary(["a", "b", "c",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasing_ShouldSucceed()
			{
				IReadOnlyDictionary<int, MyStringClass> subject = ToDictionary(["a", "A",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasingAndCasingIsIgnored_ShouldFail()
			{
				IReadOnlyDictionary<int, MyStringClass> subject = ToDictionary(["a", "A",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value).IgnoringCase();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values for x => x.Value ignoring case,
					             but it contained 1 duplicate:
					               "A"

					             Dictionary:
					             {
					               [0] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "a"
					               },
					               [1] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "A"
					               }
					             }
					             """);
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IReadOnlyDictionary<int, MyStringClass>
					subject = ToDictionary(["a", "b", "c", "a",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values for x => x.Value,
					             but it contained 1 duplicate:
					               "a"

					             Dictionary:
					             {
					               [0] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "a"
					               },
					               [1] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "b"
					               },
					               [2] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "c"
					               },
					               [3] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "a"
					               }
					             }
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IReadOnlyDictionary<int, MyStringClass> subject =
					ToDictionary(["a", "b", "c", "a", "b", "x",], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values for x => x.Value,
					             but it contained 2 duplicates:
					               "a",
					               "b"

					             Dictionary:
					             {
					               [0] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "a"
					               },
					               [1] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "b"
					               },
					               [2] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "c"
					               },
					               [3] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "a"
					               },
					               [4] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "b"
					               },
					               [5] = ThatDictionary.AreAllUnique.StringMemberTests.MyStringClass {
					                 Value = "x"
					               }
					             }
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				ReadOnlyDictionary<int, MyStringClass>? subject = null;

				async Task Act()
					=> await That(subject).AreAllUnique(x => x.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique values for x => x.Value,
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
