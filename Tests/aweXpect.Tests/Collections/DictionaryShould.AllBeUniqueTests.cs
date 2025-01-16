using System.Collections.Generic;
using aweXpect.Tests.TestHelpers;

namespace aweXpect.Tests.Collections;

public sealed partial class DictionaryShould
{
	public sealed class AllBeUnique
	{
		public sealed class Tests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IDictionary<int, int> subject = ToDictionary([1, 1, 1]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique().Using(new AllDifferentComparer());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenAllValuesAreUnique_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3, 1]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values,
					             but it contained 1 duplicate:
					               1
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3, 1, 2, -1]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values,
					             but it contained 2 duplicates:
					               1,
					               2
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<int, int>? subject = null;

				async Task Act()
					=> await That(subject!).Should().AllBeUnique();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values,
					             but it was <null>
					             """);
			}
		}

		public sealed class StringTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IDictionary<int, string> subject = ToDictionary(["a", "a", "a"]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique().Using(new AllDifferentComparer());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenAllValuesAreUnique_ShouldSucceed()
			{
				IDictionary<int, string> subject = ToDictionary(["a", "b", "c"]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasing_ShouldSucceed()
			{
				IDictionary<int, string> subject = ToDictionary(["a", "A"]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique();

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasingAndCasingIsIgnored_ShouldFail()
			{
				IDictionary<int, string> subject = ToDictionary(["a", "A"]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique().IgnoringCase();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values ignoring case,
					             but it contained 1 duplicate:
					               "A"
					             """);
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IDictionary<int, string> subject = ToDictionary(["a", "b", "c", "a"]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values,
					             but it contained 1 duplicate:
					               "a"
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IDictionary<int, string> subject = ToDictionary(["a", "b", "c", "a", "b", "x"]);

				async Task Act()
					=> await That(subject).Should().AllBeUnique();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values,
					             but it contained 2 duplicates:
					               "a",
					               "b"
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<int, string>? subject = null;

				async Task Act()
					=> await That(subject!).Should().AllBeUnique();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values,
					             but it was <null>
					             """);
			}
		}

		public sealed class MemberTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IDictionary<int, MyClass> subject = ToDictionary([1, 1, 1], x => new MyClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value).Using(new AllDifferentComparer());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenAllValuesAreUnique_ShouldSucceed()
			{
				IDictionary<int, MyClass> subject = ToDictionary([1, 2, 3], x => new MyClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IDictionary<int, MyClass> subject = ToDictionary([1, 2, 3, 1], x => new MyClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values for x => x.Value,
					             but it contained 1 duplicate:
					               1
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IDictionary<int, MyClass> subject =
					ToDictionary([1, 2, 3, 1, 2, -1], x => new MyClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values for x => x.Value,
					             but it contained 2 duplicates:
					               1,
					               2
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<int, MyClass>? subject = null;

				async Task Act()
					=> await That(subject!).Should().AllBeUnique(x => x.Value);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values for x => x.Value,
					             but it was <null>
					             """);
			}
		}

		public sealed class StringMemberTests
		{
			[Fact]
			public async Task ShouldUseCustomComparer()
			{
				IDictionary<int, MyStringClass> subject = ToDictionary(["a", "a", "a"], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value).Using(new AllDifferentComparer());

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenAllValuesAreUnique_ShouldSucceed()
			{
				IDictionary<int, MyStringClass> subject = ToDictionary(["a", "b", "c"], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasing_ShouldSucceed()
			{
				IDictionary<int, MyStringClass> subject = ToDictionary(["a", "A"], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value);

				await That(Act).Does().NotThrow();
			}

			[Fact]
			public async Task WhenDiffersInCasingAndCasingIsIgnored_ShouldFail()
			{
				IDictionary<int, MyStringClass> subject = ToDictionary(["a", "A"], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value).IgnoringCase();

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values for x => x.Value ignoring case,
					             but it contained 1 duplicate:
					               "A"
					             """);
			}

			[Fact]
			public async Task WhenItContainsDuplicates_ShouldFail()
			{
				IDictionary<int, MyStringClass> subject = ToDictionary(["a", "b", "c", "a"], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values for x => x.Value,
					             but it contained 1 duplicate:
					               "a"
					             """);
			}

			[Fact]
			public async Task WhenItContainsMultipleDuplicates_ShouldFail()
			{
				IDictionary<int, MyStringClass> subject =
					ToDictionary(["a", "b", "c", "a", "b", "x"], x => new MyStringClass(x));

				async Task Act()
					=> await That(subject).Should().AllBeUnique(x => x.Value);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values for x => x.Value,
					             but it contained 2 duplicates:
					               "a",
					               "b"
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<int, MyStringClass>? subject = null;

				async Task Act()
					=> await That(subject!).Should().AllBeUnique(x => x.Value);

				await That(Act).Does().Throw<XunitException>()
					.WithMessage("""
					             Expected subject to
					             only have unique values for x => x.Value,
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
