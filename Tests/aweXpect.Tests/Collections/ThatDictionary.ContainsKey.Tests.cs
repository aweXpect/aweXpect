using System.Collections.Generic;

namespace aweXpect.Tests;

public sealed partial class ThatDictionary
{
	public sealed class ContainsKey
	{
		public sealed class Tests
		{
			[Fact]
			public async Task WhenKeyExists_ShouldSucceed()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).ContainsKey(2);

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenKeyIsMissing_ShouldFail()
			{
				IDictionary<int, int> subject = ToDictionary([1, 2, 3], [0, 0, 0]);

				async Task Act()
					=> await That(subject).ContainsKey(0);

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains key 0,
					             but it contained only [
					               1,
					               2,
					               3
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<string, int>? subject = null;

				async Task Act()
					=> await That(subject).ContainsKey("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains key "foo",
					             but it was <null>
					             """);
			}
		}

		public sealed class WhoseValueTests
		{
			[Fact]
			public async Task WhenKeyExists_ButValueDoesNotMatch_ShouldFail()
			{
				IDictionary<int, string> subject = ToDictionary([1, 2, 3], ["foo", "bar", "baz"]);

				async Task Act()
					=> await That(subject).ContainsKey(2).WhoseValue.IsEqualTo("foo");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains key 2 whose value is equal to "foo",
					             but value [2] was "bar" which differs at index 0:
					                ↓ (actual)
					               "bar"
					               "foo"
					                ↑ (expected)
					             """);
			}

			[Fact]
			public async Task WhenKeyExists_ShouldSucceed()
			{
				IDictionary<int, string> subject = ToDictionary([1, 2, 3], ["foo", "bar", "baz"]);

				async Task Act()
					=> await That(subject).ContainsKey(2).WhoseValue.IsEqualTo("bar");

				await That(Act).DoesNotThrow();
			}

			[Fact]
			public async Task WhenKeyIsMissing_ShouldFail()
			{
				IDictionary<int, string> subject = ToDictionary([1, 2, 3], ["foo", "bar", "baz"]);

				async Task Act()
					=> await That(subject).ContainsKey(0).WhoseValue.IsEqualTo("bar");

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains key 0 whose value is equal to "bar",
					             but it contained only [
					               1,
					               2,
					               3
					             ]
					             """);
			}

			[Fact]
			public async Task WhenSubjectIsNull_ShouldFail()
			{
				IDictionary<string, string>? subject = null;

				async Task Act()
					=> await That(subject).ContainsKey("foo").WhoseValue.IsEmpty();

				await That(Act).Throws<XunitException>()
					.WithMessage("""
					             Expected that subject
					             contains key "foo" whose value is empty,
					             but it was <null>
					             """);
			}
		}
	}
}
