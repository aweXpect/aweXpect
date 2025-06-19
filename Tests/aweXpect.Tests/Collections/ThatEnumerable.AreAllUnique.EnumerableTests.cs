using System.Collections;
using System.Linq;

// ReSharper disable PossibleMultipleEnumeration

namespace aweXpect.Tests;

public sealed partial class ThatEnumerable
{
	public sealed partial class AreAllUnique
	{
		public sealed class EnumerableTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IEnumerable subject = ToEnumerable([1, 1, 1,]);

				async Task Act()
					=> await That(subject).AreAllUnique().Using(new AllDifferentComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldSucceed()
			{
				IEnumerable subject = ToEnumerable([1, 2, 3,]);

				async Task Act()
					=> await That(subject).AreAllUnique();

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IEnumerable subject = ToEnumerable([1, 2, 3, 1,]);

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
				IEnumerable subject = ToEnumerable([1, 2, 3, 1, 2, -1,]);

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
				IEnumerable? subject = null;

				async Task Act()
					=> await That(subject)!.AreAllUnique();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items,
					             but it was <null>
					             """);
			}
		}

		public sealed class EnumerableNegatedTests
		{
			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldFail()
			{
				IEnumerable subject = ToEnumerable([1, 2, 3,]);

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
				IEnumerable subject = ToEnumerable([1, 2, 3, 1,]);

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.AreAllUnique());

				await That(Act).DoesNotThrow();
			}
		}

		public sealed class EnumerableMemberTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IEnumerable subject = ToEnumerable([1, 1, 1,]).Select(x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => (x as MyClass)?.Value).Using(new AllDifferentComparer());

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldSucceed()
			{
				IEnumerable subject = ToEnumerable([1, 2, 3,]).Select(x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => (x as MyClass)?.Value);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IEnumerable subject = ToEnumerable([1, 2, 3, 1,]).Select(x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => (x as MyClass)?.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items for x => (x as MyClass)?.Value,
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
				IEnumerable subject =
					ToEnumerable([1, 2, 3, 1, 2, -1,]).Select(x => new MyClass(x));

				async Task Act()
					=> await That(subject).AreAllUnique(x => (x as MyClass)?.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items for x => (x as MyClass)?.Value,
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
				IEnumerable? subject = null;

				async Task Act()
					=> await That(subject)!.AreAllUnique(x => (x as MyClass)?.Value);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             only has unique items for x => (x as MyClass)?.Value,
					             but it was <null>
					             """);
			}
		}

		public sealed class EnumerableNegatedMemberTests
		{
			[Fact]
			public async Task WhenAllItemsAreUnique_ShouldFail()
			{
				IEnumerable subject = ToEnumerable([1, 2, 3,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.AreAllUnique(x => (x as MyClass)?.Value));

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             has duplicate items for x => (x as MyClass)?.Value,
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
				IEnumerable subject = ToEnumerable([1, 2, 3, 1,], x => new MyClass(x));

				async Task Act()
					=> await That(subject).DoesNotComplyWith(it => it.AreAllUnique(x => (x as MyClass)?.Value));

				await That(Act).DoesNotThrow();
			}
		}
	}
}
